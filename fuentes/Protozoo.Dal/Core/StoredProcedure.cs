using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;

namespace Protozoo.DAL.Core
{
   public class StoredProcedure<T>:IQuery
    {
    
       /// <summary>
       /// Evento que se dispara antes de la ejecución contra la base de datos
       /// </summary>
       public event EventHandler BeforeExecute = delegate { };
       
       /// <summary>
       /// Evento disparado después de recibir los resultados de la base de datos
       /// </summary>
       public event EventHandler AfterExecute = delegate { };

       
        /// <summary>Nombre del procedimiento almacenado</summary>
        public string Name
        {
            get { return this.Cmd.CommandText; }
        }

        public IDataReader ResultSet 
        { 
            get; 
            protected set; 
        }

        public IDataReader Execute()
        {
           LoadParametersValues();
            //
            // Ejecución del comando de base de datos
            //
            BeforeExecute(this, EventArgs.Empty);
            //
            // 
            // Ejecucion del comando. Por el momento se utiliza un DataSet para poder desconectar el reader.
            // TODO: Quitar el dataset y pasarlo a un DTO propio.
            //
            DataSet dst = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter((SqlCommand)Cmd);
            adp.Fill(dst);
            this.ResultSet = dst.CreateDataReader();
            //this.ResultSet = Cmd.ExecuteReader();
            AfterExecute(this, EventArgs.Empty);
            return this.ResultSet;
        }

        /// <summary>
        /// DbCommand interno.
        /// </summary>
        protected IDbCommand Cmd { get; set; }
    
        /// <summary>Interface de acceso a los parametros de cabecera del procedimiento</summary>
        private T _parameterAccesor;
        //
        // TODO: a futuro hay que intentar quitar este diccionario y utilizar únicamente la coleccion 
        //       parameters del objeto command.
        //
        private Dictionary<String, DbParameter> _parameters = new Dictionary<String, DbParameter>();

        /// <summary>
        /// Devuelve una referencia a la interfaz capaz de acceder a los parámetros del objeto comando
        /// </summary>
        public T Parameters
        {
            get { return _parameterAccesor; }
        }

       /// <summary>
       /// Constructor
       /// </summary>
       /// <param name="command">Objeto de comando de base de datos</param>
        public StoredProcedure(IDbCommand command)
        {
            if (command == null)
                throw new ArgumentNullException();
            this.Cmd = command;
            //
            // TODO: Afinar los tipos de comando mediante herencias o factorias.
            //
            Cmd = (SqlCommand) command;            
            Cmd.CommandText = ModelInspector.GetCommandName(typeof(T));
            Cmd.CommandType = CommandType.StoredProcedure;            
            
            foreach (PropertyInfo pi in ModelInspector.MembersDefinedAsParameters<T>())
            {
                SqlParameter param = (SqlParameter) Cmd.CreateParameter();
                param.ParameterName = String.Format("@{0}", ModelInspector.GetParameterName(pi));
                param.SqlDbType = ModelInspector.ParameterDefinition(pi).Type;
                //
                // TODO: Comprobar los parametros OUTPUT. ahora se están clavando todos como input
                //
                param.Direction = ParameterDirection.Input;
                #region REFACTOR
                //
                // Refactorizar esto que da vergüenza verlo
                //
                Type propType = null;
                try
                {
                    propType = pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(pi.PropertyType) : pi.PropertyType;
                }
                catch
                {
                    propType = pi.PropertyType;
                }
                if (propType.UnderlyingSystemType == typeof(int))
                {
                    param.SqlValue = SqlInt32.Null;
                }
                else if (propType.UnderlyingSystemType == typeof(long))
                {
                    param.SqlValue = SqlInt64.Null;
                }
                else if (propType.UnderlyingSystemType == typeof(DateTime))
                {
                    param.SqlValue = SqlDateTime.Null;
                }
                else if (propType.UnderlyingSystemType == typeof(String))
                {
                    param.SqlValue = SqlString.Null;
                }
                else if (propType == typeof(bool))
                {
                    param.SqlValue = SqlBoolean.Null;
                }
                else
                    throw new Exception("StoredProcedure: cannot map the type (" + pi.PropertyType.Name + ") to sql type");
                #endregion
                _parameters.Add(ModelInspector.GetParameterName(pi),  param);
                Cmd.Parameters.Add(param);
            }
            _parameterAccesor = DalCommandFactory.Create<T>();
        }


        /// <summary>
        /// Establece los valores de los parametros del objeto comando desde las propiedades de la 
        /// clase de acceso.
        /// </summary>
        protected void LoadParametersValues()
        {
            //
            // Carga valores de las propiedades en los parametros del comando
            //
            foreach (PropertyInfo pi in ModelInspector.MembersDefinedAsParameters<T>())
            {
                ParameterAttribute[] att = (ParameterAttribute[])pi.GetCustomAttributes(typeof(ParameterAttribute), false);
                DbParameter p = _parameters[(String.IsNullOrEmpty(att[0].Name) ? pi.Name : att[0].Name)];
                object value = pi.GetValue(_parameterAccesor, new object[] { });
                // HACK: las cadenas no pueden ser nullables...
                if (value != null && value.GetType() == typeof(string) && String.IsNullOrEmpty(value.ToString()))
                    value = null;
                else if (value != null)
                    p.Value = value;
                
            }
        }
    }
}

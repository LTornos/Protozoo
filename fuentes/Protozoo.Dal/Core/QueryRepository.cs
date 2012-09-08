using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Protozoo.DAL.Core
{
    public class QueryRepository
    {
        private IDbConnection conexion = null;

        public QueryRepository(IDbConnection commandInstance)
        {
            conexion = commandInstance;
        }

        public StoredProcedure<T> Create<T>() where T:class
        {
            StoredProcedure<T> instance = new StoredProcedure<T>(conexion.CreateCommand());
            instance.BeforeExecute += new System.EventHandler(instance_BeforeExecute);
            instance.AfterExecute += new System.EventHandler(instance_AfterExecute);
            return instance;
        }

        void instance_AfterExecute(object sender, System.EventArgs e)
        {
            //
            // TODO: En futuras versiones esto lo manejará un gestor de 
            //       conexiones. No tiene porque cerrarse la conexión cada
            //       vez que se ejecuta un procedimiento. Unicamente se 
            //       requiere cerrar su dataReader asociado.
            //
            conexion.Close();
        }

        void instance_BeforeExecute(object sender, System.EventArgs e)
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
        }

        public StoredProcedure<T> Create<T>(DbConnection connectionPool) where T : class
        {
            return new StoredProcedure<T>((SqlCommand)conexion.CreateCommand());
        }

        public IQuery Create(string procedureName)
        {
            throw new System.NotImplementedException();
        }
    }
}

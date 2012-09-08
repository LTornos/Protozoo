using System;
using System.Collections.Generic;
using System.Reflection;

namespace Protozoo.DAL.Core
{
    public static class ModelInspector
    {
        /// <summary>
        /// Obtiene el nombre del comando definido en la base de datos
        /// </summary>
        /// <param name="type">Tipo de objeto de comando</param>
        /// <returns>Nombre correspondiente del comando en la base de datos</returns>
        /// <remarks>Utiliza el nombre de la clase como nombre si no se especifica uno en su atributo de clase.</remarks>
        public static string GetCommandName(Type type)
        {
            StoredProcedureAttribute[] atts = (StoredProcedureAttribute[]) type.GetCustomAttributes(typeof(StoredProcedureAttribute), false);
            if (atts.Length > 0)
            { 
                if (!String.IsNullOrEmpty(atts[0].Name))
                    return atts[0].Name;
            else
                return type.Name;
            }
            
            return string.Empty;
        }

        /// <summary>
        /// Indica si una propiedad del la clase esta marcada como parametro del comando.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad de la clase</param>
        /// <param name="type">Tipo de la clase</param>
        /// <returns>Verdadero si la propiedad está marcada como parámetro</returns>
        public static bool IsDefinedAsParameter(string propertyName, Type type)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception("Property not found in class " + type.FullName);
            return IsDefinedAsParameter(property);
        }

        /// <summary>
        /// Indica si una propiedad del la clase esta marcada como parametro del comando.
        /// </summary>
        /// <param name="property">Objeto property info</param>
        /// <returns>Verdadero si la propiedad está marcada como parámetro</returns>
        public static bool IsDefinedAsParameter(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("Invalid property info");
            return property.GetCustomAttributes(typeof(ParameterAttribute), false).Length > 0;
        }

        /// <summary>
        /// Obtiene el nombre del parametro en la base de datos
        /// </summary>
        /// <param name="property">propiedad a inspeccionar</param>
        /// <returns>Nombre de la propiedad de la clase si no se ha especificado un nombre en el atributo Parameter.</returns>
        public static string GetParameterName(PropertyInfo property)
        {
            if (!IsDefinedAsParameter(property))
                throw new Exception("The member is not defined as stored procedure parameter");

            ParameterAttribute att = ParameterDefinition(property);
            if (att != null)
                if (String.IsNullOrEmpty(att.Name))
                    return property.Name;
                else                
                    return att.Name;
            throw new Exception();
        }

        /// <summary>
        /// Lista las propiedades de la clase definidas como parametros.
        /// </summary>
        /// <typeparam name="T">Tipo a inspeccionar</typeparam>
        /// <returns>Información de las propiedades </returns>
        public static IEnumerable<PropertyInfo> MembersDefinedAsParameters<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                ParameterAttribute[] att = (ParameterAttribute[])pi.GetCustomAttributes(typeof(ParameterAttribute), false);
                if (ModelInspector.IsDefinedAsParameter(pi))
                {
                    yield return pi;
                }
            }
        }

        /// <summary>
        /// Devuelve el atributo con los valores del parámetro
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static ParameterAttribute ParameterDefinition(PropertyInfo property)
        {
            return  IsDefinedAsParameter(property)?
                (ParameterAttribute)(property.GetCustomAttributes(typeof(ParameterAttribute), false)[0])
            :
                null
            ;
        }
    }
}

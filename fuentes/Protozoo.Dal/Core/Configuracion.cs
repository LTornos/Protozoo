using System;
using System.Configuration;
using Protozoo.Dal.Properties;

namespace Protozoo.DAL.Core
{
    public class Configuracion
    {
        public static string CadenaConexion
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
                }
                catch (NullReferenceException ex)
                {
                   // Log.ManejadorICO.RegistrarExcepcion(ex);
                    throw new Exception(string.Format(Resources.Error_NullReferenceException, "ConnectionStrings['ConexionSQL']"));
                }
            }
        }
    }
}

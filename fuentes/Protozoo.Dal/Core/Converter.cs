using System;
using System.Data;

namespace Protozoo.DAL.Core
{
    public class Converter
    {
        #region - GET -

        public static short GetInt16(DataRow row, string columnName)
        {
            if (row.Table.Columns[columnName] == null) return 0;
            short valorConvertido;
            var resultadoConversion = short.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion ? valorConvertido : (short)0;
        }

        public static int GetInt32(DataRow row, string columnName)
        {
            if (row.Table.Columns[columnName] == null) return 0;
            int valorConvertido;
            var resultadoConversion = int.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion ? valorConvertido : 0;
        }

        public static int GetInt32(IDataReader row, string columnName)
        {
            int valorConvertido;
            var resultadoConversion = int.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion ? valorConvertido : 0;
        }

        public static long GetInt64(DataRow row, string columnName)
        {
            if (row.Table.Columns[columnName] == null) return 0;
            long valorConvertido;
            var resultadoConversion = long.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion ? valorConvertido : 0;
        }

        ////public static string GetString(DataRow row, string columnName)
        ////{
        ////    /*if (!row.Table.Columns.Contains(columnName) ||row[columnName] == null || string.IsNullOrEmpty(row[columnName].ToString())) return string.Empty;*/
        ////    return row.Table.Columns[columnName] == null ? string.Empty : row[columnName].ToString().Trim();
        ////} 

        public static string GetString(IDataReader row, string columnName)
        {
            /*if (!row.Table.Columns.Contains(columnName) ||row[columnName] == null || string.IsNullOrEmpty(row[columnName].ToString())) return string.Empty;*/
            return row[columnName] == null ? string.Empty : row[columnName].ToString().Trim();
        } 

        public static bool GetBoolean(DataRow row, string columnName)
        {
            if (row.Table.Columns[columnName] == null) return false;

            //Primero mira si el valor devuelto es numérico
            int enteroSalida;
            bool resultadoConversionEntero = int.TryParse(row[columnName].ToString(), out enteroSalida);
            if (resultadoConversionEntero)
            {
                return enteroSalida != 0;
            }
            //Si no lo es, mira si es cadena "true"-"false"
            bool valorConvertido;
            var resultadoConversion = Boolean.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion && valorConvertido;
        }

        public static DateTime GetDateTime(DataRow row, string columnName)
        {
            if (row.Table.Columns[columnName] == null) return DateTime.MinValue;
            DateTime valorConvertido;
            var resultadoConversion = DateTime.TryParse(row[columnName].ToString(), out valorConvertido);
            return resultadoConversion ? valorConvertido : DateTime.MinValue;
        }

        public static int GetInt(string cadena, int porDefecto)
        {
            if (string.IsNullOrEmpty(cadena)) return porDefecto;
            int valorConvertido;
            var resultadoConversion = int.TryParse(cadena, out valorConvertido);
            return resultadoConversion ? valorConvertido : porDefecto;
        }

        public static string GetString(string cadena, string porDefecto)
        {
            return cadena ?? porDefecto;
        }

        public static bool GetBoolean(string cadena, bool porDefecto)
        {
            if (string.IsNullOrEmpty(cadena)) return porDefecto;
            bool valorConvertido;
            var resultadoConversion = bool.TryParse(cadena, out valorConvertido);
            return resultadoConversion ? valorConvertido : porDefecto;
        }

        public static DateTime GetDateTime(string cadena, DateTime porDefecto)
        {
            if (string.IsNullOrEmpty(cadena)) return porDefecto;
            DateTime valorConvertido;
            var resultadoConversion = DateTime.TryParse(cadena, out valorConvertido);
            return resultadoConversion ? valorConvertido : porDefecto;
        }

        #endregion

        #region - SET -
        

        #endregion
    }
}

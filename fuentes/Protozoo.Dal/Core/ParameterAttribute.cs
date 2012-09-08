using System;
using System.Data;

namespace Protozoo.DAL.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : System.Attribute
    {
        public ParameterAttribute()
        {
            Direction = ParameterDirection.Input;
        }

        public string Name { get; set; }
        public SqlDbType Type { get; set; }
        public object DefaultValue { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}

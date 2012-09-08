namespace Protozoo.DAL.Core
{
    public class StoredProcedureAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


namespace Protozoo.Ui.Web
{
    partial class _Default
    {
        //
        // Contexto de datos de la vista
        //
        protected class ViewData
        {
            public object Context { get; set; }

            public string GetValue(string propName)
            {
                return Context.GetType().GetProperty(propName).GetValue(Context, null).ToString();
            }
        }
    }
}

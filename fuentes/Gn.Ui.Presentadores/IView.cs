
namespace Protozoo.Ui.Presentadores.Views
{
    public interface IView
    {        
        void SetData (object data); 
        int Filter { get; }
        void NotifyUser(string msg);
    }
}

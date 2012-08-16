
namespace Gn.Ui.Presentadores
{
    public interface IView
    {
        int Filter { get; }
        void SetData(object data);
        void NotifyUser(string msg);
    }
}

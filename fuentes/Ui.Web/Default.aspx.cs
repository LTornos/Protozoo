using System;
using Protozoo.Ui.Presentadores;
using Protozoo.Ui.Presentadores.Views;

namespace Protozoo.Ui.Web                                
{
    public partial class _Default : System.Web.UI.Page, IView
    {
        protected ViewData _data = new ViewData();
        protected Presenter _viewController = null;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            _viewController = PresenterLocator.GetPresenter(this);   
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ctlDo.Click += (sender, ev) => _viewController.ProcessViewRequest();
        }

        #region Miembros de IView

        void IView.SetData(object data)
        {
            _data.Context = data;
            ctlUserAccount.Text = _data.GetValue("Account");
            ctlUserName.Text = _data.GetValue("Name");
        }

        int IView.Filter
        {
            get { return int.Parse(ctlDoContext.Text); }
        }

        void IView.NotifyUser(string msg)
        {
            ctlRes.InnerHtml += "- " + msg + "<br/>";
        }

        #endregion
    }
}

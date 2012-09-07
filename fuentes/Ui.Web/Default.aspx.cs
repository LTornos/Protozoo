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

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetData(object data)
        {
            _data.Context = data;
            ctlUserAccount.Text = _data.GetValue("Account");
            ctlUserName.Text = _data.GetValue("Name");
        }
                
        public void NotifyUser(string msg)
        {
            ctlRes.InnerHtml += "- " + msg + "<br/>";
        }
        
        public int Filter
        {
            get { return int.Parse(ctlDoContext.Text); }
        }

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

using System;

namespace Gn.Ui.Web                                //   Implementacion de vista
{   //                                                +----------------------------+
    public partial class _Default : System.Web.UI.Page, Gn.Ui.Presentadores.IView
    {
        private Gn.Ui.Presentadores.Presenter _presenter = new Gn.Ui.Presentadores.Presenter();

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            _presenter.View = this;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);                        
            ctlDo.Click += new EventHandler(ctlDo_Click);
        }
        
        void ctlDo_Click(object sender, EventArgs e)
        {                  
            // Solicitamos una accion a realizar.
            _presenter.ProcessViewRequest();                  
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
                
        private object _data = null;
        public void SetData(object data)
        {
            _data = data;            
            ctlUserAccount.Text = _data.GetType().GetProperty("Account").GetValue(_data,null).ToString();
            ctlUserName.Text = _data.GetType().GetProperty("Name").GetValue(_data,null).ToString();
        }
                
        public void NotifyUser(string msg)
        {
            ctlRes.InnerHtml += "- " + msg + "<br/>";
        }
        
        public int Filter
        {
            get { return int.Parse(ctlDoContext.Text); }
        }        
    }
}

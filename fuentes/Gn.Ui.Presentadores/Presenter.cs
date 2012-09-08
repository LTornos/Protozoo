using System;
using Protozoo.Core;
using Protozoo.Core.Entities;
using Protozoo.Ui.Presentadores.Views;

namespace Protozoo.Ui.Presentadores
{
    public class Presenter
    {

        public Presenter (IBusiness model)
	    {
            Model = model;
            Model.SomethingIsHappening += msg => this.View.NotifyUser(String.Format("[{0:hh:mm:ss}] {1}", DateTime.Now, msg));
	    }


        public void ProcessViewRequest()
        {
            try
            {
                Entity data = Model.DoSomething(View.Filter);
                this.View.SetData(data);
            }
            catch (Exception ex)
            { 
                this.View.NotifyUser("[!] " + ex.Message);
            }
        }

        public IView View { get; set; }

        public IBusiness Model { get; set; }
    }
}

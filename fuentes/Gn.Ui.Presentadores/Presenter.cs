using System;
using Bll.Entities;
using Bll.Tier1;
using Protozoo.Core;

namespace Gn.Ui.Presentadores
{
    public class Presenter
    {
        /// <summary>
        /// Implementación de negocio para comunicación en arquitectura distribuida con exposición de Wcf
        /// </summary>
        private IBusiness _business = new BusinessFront();
        
        /// <summary>
        /// Implementación de negocio para comunicación en arquitectura distribuida con exposición de Wcf (Servicios Duplex)
        /// </summary>
        //private IBusiness _business = new BusinessFrontDuplex();

        /// <summary>
        /// Implementación de negocio. Sin comunicación con otros Tier's
        /// </summary>
        //private IBusiness _business = new BusinessLayer2();

        public Presenter ()
	    {
            _business.SomethingIsHappening += new EventHandler(_business_SomethingIsHappening);
	    }

        void _business_SomethingIsHappening(object sender, EventArgs e)
        {
            this.View.NotifyUser(String.Format("[{0:hh:mm:ss}] Evento de negocio",DateTime.Now));
        }

        public void ProcessViewRequest()
        {
            try
            {
                Entity data = _business.DoSomething(View.Filter);
                this.View.SetData(data);
            }
            catch (Exception ex)
            { 
                this.View.NotifyUser("[!] " + ex.Message);
            }
        }

        public IView View { get; set; }
    }
}

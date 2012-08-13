using System;
using System.Linq;
using System.ServiceModel;
using Bll.Tier1.Backend;

namespace Bll.Tier1
{
    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Single)]
    public class BusinessFrontDuplex : Bll.Core.IBusiness, Backend.IBackendDuplexCallback, IDisposable
    {
        public event EventHandler SomethingIsHappening;
        private event EventHandler Exception;

        private BackendDuplexClient _client=null;

        public BusinessFrontDuplex()
        {
            this.SomethingIsHappening += delegate { };
        }

        public Bll.Entities.Entity DoSomething(int context)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            _client = new BackendDuplexClient(instanceContext);
             
            (_client as ICommunicationObject).Faulted += new EventHandler(BusinessFrontDuplex_Faulted);
            try
            {
                return _client.Process(context);                
            }
            catch 
            { 
                throw; 
            }
        }

        // NO LLAMAR DIRECTAMENTE A ESTE MÉTODO JAMÁS.
        void BusinessFrontDuplex_Faulted(object sender, EventArgs e)
        {
            this.Exception(this, EventArgs.Empty);
        }
      
        // Metodo para el callback desde el servicio remoto.
        public void Notify(Bll.Tier1.Backend.BackendDTOOfUsuarioExceptionQNgybuyY result)
        {
            if (result.Exceptions.Count() <= 0)
            {
                foreach (Message m in result.Messages)
                {
                    switch (m.Type)
                    {
                        case "Event":
                            this.SomethingIsHappening(this, EventArgs.Empty);
                            break;
                    }
                }
            }            
        }

        public void Dispose()
        {            
            _client.Close();
        }
    }
}

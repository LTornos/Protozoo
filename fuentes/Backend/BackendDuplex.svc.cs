using System;
using System.ServiceModel;
using Protozoo.Core;
using Protozoo.Core.Entities;
using Protozoo.Core.Tier2;

namespace Protozoo.Backend
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
   // [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class BackendDuplex : IBackendDuplex
    {     
        public Entity Process(int cmd)
        {
            IBusiness domainObject = new BusinessLayer2();
            BackendDTO<Entity, Exception> serviceMessage = new BackendDTO<Entity, Exception>();
            // Captura el evento de negocio y lo incluye en el mensaje del servicio
            domainObject.SomethingIsHappening += msg => 
                {
                    BackendDTO<Entity, Exception> message = new BackendDTO<Entity, Exception>();
                    message.Messages.Add(new Message("Something happened <" + msg + "> " + DateTime.Now.ToString(), "Event"));
                    Callback.Notify(message);
                };
            try
            {   // Llamada a negocio            
                serviceMessage.Data.Add(domainObject.DoSomething(cmd));
            }
            catch (Exception ex)
            {   // Se produce excepción, se incluye en el mensaje del servicio
                serviceMessage.Exceptions.Add(ex);
                throw new FaultException(ex.Message);
            }
            Callback.Notify(serviceMessage);
            return serviceMessage.Data[0];
        }

        private IBackendDuplexCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IBackendDuplexCallback>();
            }
        }
    }
}

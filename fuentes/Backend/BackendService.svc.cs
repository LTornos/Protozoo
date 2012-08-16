using System;
using Bll.Entities;
using Protozoo.Core;
using System.ServiceModel;

namespace Backend
{
    /// <summary>
    /// Servicio de backend. Que se usará para conectar con los objetos de dominio.
    /// </summary>
    public class BackendService : IBackendService
    {
        private readonly IBusiness _domainObject;

        public BackendService(IBusiness domainObject)
        {
            _domainObject = domainObject;        
        }
        
        public BackendDTO<Entity,Exception> Process(int cmd)
        {            
            BackendDTO<Entity, Exception> serviceMessage = new BackendDTO<Entity, Exception>();           
            // Captura el evento de negocio y lo incluye en el mensaje del servicio
            _domainObject.SomethingIsHappening+= delegate 
                { serviceMessage.Messages.Add(new Message("Something happened", "Event")); };            
            try
            {   // Llamada a negocio                
                serviceMessage.Data.Add(_domainObject.DoSomething(cmd));
            }
            catch(Exception ex)
            {   // Se produce excepción, se incluye en el mensaje del servicio
                serviceMessage.Exceptions.Add(ex);
                //throw new FaultException(ex.Message);                
            }     
            return serviceMessage;            
        }
    }
}

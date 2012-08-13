using System;
using System.ServiceModel;
using Bll.Core;
using Bll.Entities;
using Bll.Layer2;

namespace Backend
{
    /// <summary>
    /// Servicio de backend. Que se usará para conectar con los objetos de dominio.
    /// </summary>
    public class BackendService : IBackendService
    {
        public BackendDTO<Entity,Exception> Process(int cmd)
        {
            IBusiness domainObject = new BusinessLayer2();
            BackendDTO<Entity, Exception> serviceMessage = new BackendDTO<Entity, Exception>();           
            // Captura el evento de negocio y lo incluye en el mensaje del servicio
            domainObject.SomethingIsHappening+= delegate 
                { serviceMessage.Messages.Add(new Message("Something happened", "Event")); };            
            try
            {   // Llamada a negocio                
                serviceMessage.Data.Add(domainObject.DoSomething(cmd));
            }
            catch(Exception ex)
            {   // Se produce excepción, se incluye en el mensaje del servicio
                serviceMessage.Exceptions.Add(ex);
                throw new FaultException(ex.Message);                
            }     
            return serviceMessage;            
        }
    }
}

using System;
using System.Linq;
using Bll.Entities;
using Bll.Tier1.BackendServices;
using Protozoo.Core;
using System.ServiceModel;

namespace Bll.Tier1
{
    /// <summary>
    /// Implementación de la interfaz de negocio para la capa de Servidor de aplicaciones Web.
    /// </summary>
    public class BusinessFront: IBusiness
    {
        public BusinessFront()
        {
            SomethingIsHappening = delegate {};
        }

        public Entity DoSomething(int context)
        { 
            BackendServiceClient backend = new BackendServiceClient();
            BackendDTOOfEntityExceptionQNgybuyY result = null;
            result = backend.Process(context);
            if (result.Exceptions.Count() > 0)
            { 
                throw result.Exceptions[0];
            }
            result.Messages.ToList().ForEach(m =>
                {
                    switch (m.Type)
                    {
                        case "Event":                            
                            SomethingIsHappening(this, EventArgs.Empty);
                            break;
                    }
                }
            );
            return result.Data[0];
        }

        public event EventHandler SomethingIsHappening;
    }
}

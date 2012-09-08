using System;
using System.Linq;
using Protozoo.Core;
using Protozoo.Core.Entities;
using Protozoo.Core.Tier1.Backend;

namespace Protozoo.Core.Tier1
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
            BackendDTOOfEntityExceptionwB_PKy7mS result = null;
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
                            SomethingIsHappening(m.Text);
                            break;
                    }
                }
            );
            return result.Data[0];
        }

        public event Action<String> SomethingIsHappening;
    }
}

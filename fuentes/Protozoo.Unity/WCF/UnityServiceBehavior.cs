using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Protozoo.Unity.WCF
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        private IInstanceProvider _instanceProvider;

        public UnityServiceBehavior(IInstanceProvider instanceProvider)
        {
            _instanceProvider = instanceProvider;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { 
            // METODO NO IMPLEMENTADO.
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //
            // TODO: Buscar la manera de no requerir iterar toda la colección para establecer
            //       IInstanceProvider.
            //            
            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
                foreach (EndpointDispatcher ed in cd.Endpoints)
                    ed.DispatchRuntime.InstanceProvider = _instanceProvider;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            // METODO NO IMPLEMENTADO.
        }
    }
}

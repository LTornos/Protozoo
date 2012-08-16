using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace Protozoo.Unity.WCF
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private IUnityContainer Container;

        private Type ServiceType { get; set; }

        public UnityInstanceProvider(IUnityContainer container, Type serviceType)
        {            
            Container = container;
            ServiceType = serviceType;
        }
      
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return Container.Resolve(ServiceType);
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}

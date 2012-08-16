using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Protozoo.Unity.WCF
{
    //
    // TODO: Comprobar los ciclos de vida del contenedor y las instancias de los servicios.
    //       http://www.ladislavmrnka.com/2011/03/unity-build-in-lifetime-managers/
    //
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            IUnityContainer container = new UnityContainer();
            //
            // Archivo Config de la aplicación.
            //  Aquí se encuentra la definición de los servicios a tratar por ServiceHost
            //
            ((UnityConfigurationSection)ConfigurationManager.GetSection("unity")).Configure(container);
            //((UnityConfigurationSection)ConfigurationManager.GetSection("unity")).Configure(container.CreateChildContainer());
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterType<IServiceBehavior, UnityServiceBehavior>();
            container.RegisterInstance<Type>("service-type",  serviceType);
            container.RegisterType<IInstanceProvider, UnityInstanceProvider>
                (
                    new InjectionConstructor
                        (
                            new ResolvedParameter<IUnityContainer>(),
                            new ResolvedParameter<Type>("service-type")
                        )
                );
            container.RegisterType<ServiceHost, UnityServiceHost>
                (
                    new InjectionConstructor
                        (
                            new ResolvedParameter<IServiceBehavior>(),
                            new ResolvedParameter<Type>("service-type"),
                            baseAddresses
                        )
                );
            ServiceHost instance = container.Resolve<ServiceHost>();
            //container.d
            return instance;
        }
    }
}

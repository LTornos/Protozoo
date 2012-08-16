using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Protozoo.Unity.WCF
{
    public class UnityServiceHost : ServiceHost
    {
       // private IServiceBehavior Behavior { get; set; }

        public UnityServiceHost(IServiceBehavior behavior, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
          //  Behavior = behavior;
            this.Description.Behaviors.Add(behavior);
        }

        //
        // TODO: Comprobar la diferencia entre poner el Behavior aquí o en el constructor.
        //       Originalmente estaba aquí.
        //

        //protected override void OnOpening()
        //{
        //    base.OnOpening();
        //    if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
        //        this.Description.Behaviors.Add(Behavior);
        //}
    }
}

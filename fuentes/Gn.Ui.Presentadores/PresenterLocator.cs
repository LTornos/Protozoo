using Bll.Layer2;
using Bll.Tier1;
using Microsoft.Practices.Unity;
using Protozoo.Core;
using Protozoo.Ui.Presentadores.Views;

namespace Protozoo.Ui.Presentadores
{
    public class PresenterLocator
    {
        private static IUnityContainer _container = new UnityContainer();

        private const string _RESOLVE_IMPLEMENTATION_ = "Remote";

        static PresenterLocator()
        {
            _container.RegisterType<IBusiness, BusinessFront>("Remote");
            _container.RegisterType<IBusiness, BusinessFrontDuplex>("Remote-duplex");
            _container.RegisterType<IBusiness, BusinessLayer2>("Local");
            _container.RegisterType<Presenter>();
        }

        public static Presenter GetPresenter(IView view)
        {
            Presenter instance = _container.Resolve<Presenter>(new ParameterOverride("model",new ResolvedParameter<IBusiness>(_RESOLVE_IMPLEMENTATION_)));
            instance.View = view;
            return instance;
        }
    }
}

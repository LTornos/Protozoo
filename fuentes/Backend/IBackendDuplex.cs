using System;
using System.ServiceModel;
using Protozoo.Core.Entities;

namespace Protozoo.Backend
{
    [ServiceContract(CallbackContract = typeof(IBackendDuplexCallback), SessionMode=SessionMode.Required)]
    public interface IBackendDuplex
    {        
        [OperationContract(IsOneWay=false)]
        Entity Process(int cmd);
    }

    public interface IBackendDuplexCallback 
    {
        [OperationContract(IsOneWay=true)]
        void Notify(BackendDTO<Entity, Exception> msg);
    }
}

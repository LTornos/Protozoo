using System;
using System.ServiceModel;
using Protozoo.Core.Entities;

namespace Protozoo.Backend
{
    [ServiceContract]    
    public interface IBackendService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BackendDTO<Entity, Exception> Process(int cmd);
    }   
}

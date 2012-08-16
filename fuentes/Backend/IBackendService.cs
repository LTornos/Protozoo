using System;
using System.ServiceModel;
using Bll.Entities;

namespace Backend
{
    [ServiceContract]    
    public interface IBackendService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BackendDTO<Entity, Exception> Process(int cmd);

    }   
}

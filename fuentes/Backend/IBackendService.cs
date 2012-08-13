using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bll.Entities;

namespace Backend
{
    [ServiceContract]
    public interface IBackendService
    {
        [OperationContract]
        BackendDTO<Entity, Exception> Process(int cmd);

    }   
}

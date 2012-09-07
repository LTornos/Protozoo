using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Protozoo.Backend
{
    [DataContract]
    public class BackendDTO<TDto, TEx> where TEx : Exception
    {
        public BackendDTO()
        {
            Messages = new List<Message>();
            Data = new List<TDto>();
            Exceptions = new List<TEx>();
        }

        [DataMember]
        public List<Message> Messages { get; set; }

        [DataMember]
        public List<TDto> Data { get; set; }

        [DataMember]
        public List<TEx> Exceptions { get; set; } 

    }
}

using System.Runtime.Serialization;

namespace Protozoo.Backend
{
    [DataContract]
    public class Message
    {
        public Message()
        {

        }

        public Message(string text, string type)
        {
            Text = text;
            Type = type;
        }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}

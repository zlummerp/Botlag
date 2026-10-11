using System.Runtime.Serialization;

namespace Botlag.Models
{
    [DataContract]
    public class DropCall
    {
        [DataMember(Name = "Caller")]
        public required string Caller;

        [DataMember(Name = "DropCalled")]
        public required string DropCalled;
    }
}

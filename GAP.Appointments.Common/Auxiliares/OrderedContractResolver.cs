using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace GAP.Appointments.Common.Auxiliares
{
    public class OrderedContractResolver : DefaultContractResolver
    {
        protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).OrderBy(p => p.Order).ToList();
        }
    }
}

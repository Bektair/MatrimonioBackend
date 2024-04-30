using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace MatrimonioBackend.Models
{
    public static class RSVPExtension
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }



    }
}

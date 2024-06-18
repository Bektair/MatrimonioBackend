using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace MatrimonioBackend.DTOs.RSVP
{
    public static class RSVPReadDTOExtension
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }



    }
}

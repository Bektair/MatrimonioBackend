using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace MatrimonioBackend.Service
{
    public class JsonPatchService<T> where T : class
    {
        public static IList<Operation<T>> MakePatch(object rsvp)
        {
            var patch = new List<Operation<T>>();
            var props = rsvp.GetType().GetProperties();
            foreach ( var x in props )
            {
                var value = x.GetValue(rsvp, null);
                if (value != null)
                {
                    patch.Add(new Operation<T>()
                    {
                        op = "replace",
                        path = x.Name,
                        value = value,
                        from = ""
                    });
                }

            }
            return patch;
        }


    }
}

using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace MatrimonioBackend.Profiles
{
    public class CustomWeddingResolverUpdate : ITypeConverter<JsonPatchDocument<WeddingUpdateDTO>, JsonPatchDocument<Wedding>>
    {
            public JsonPatchDocument<Wedding> Convert(JsonPatchDocument<WeddingUpdateDTO> source, JsonPatchDocument<Wedding> destination, ResolutionContext context)
            {
                Console.WriteLine("resolvethis");

                var operations = source.Operations;
                //tring op, string path, string from, object value
                IEnumerable<Operation<Wedding>> operation = source.Operations.Select((op) => new Operation<Wedding>(op.op, op.path, op.from, op.value));



                var rsvpPatch = new JsonPatchDocument<Wedding>(operation.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
    }
}

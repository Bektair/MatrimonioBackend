using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace MatrimonioBackend.Profiles
{
    public class CustomRSVPResolverUpdate : ITypeConverter<JsonPatchDocument<RSVPUpdateDTO>, JsonPatchDocument<RSVP>>
    {
        public JsonPatchDocument<RSVP> Convert(JsonPatchDocument<RSVPUpdateDTO> source, JsonPatchDocument<RSVP> destination, ResolutionContext context)
        {
            Console.WriteLine("resolvethis");

            var operations = source.Operations;
            //tring op, string path, string from, object value
            IEnumerable<Operation<RSVP>> operation = source.Operations.Select((op) => new Operation<RSVP>(op.op, op.path, op.from, op.value));
             


            var rsvpPatch = new JsonPatchDocument<RSVP>(operation.ToList(), source.ContractResolver);


            return rsvpPatch;
        }
    }
}

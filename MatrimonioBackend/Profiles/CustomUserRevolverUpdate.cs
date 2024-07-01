using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace MatrimonioBackend.Profiles
{
    public class CustomUserRevolverUpdate : ITypeConverter<JsonPatchDocument<UserUpdateDTO>, JsonPatchDocument<MarryMonioUser>>
    {
        public JsonPatchDocument<MarryMonioUser> Convert(JsonPatchDocument<UserUpdateDTO> source, JsonPatchDocument<MarryMonioUser> destination, ResolutionContext context)
        {
            Console.WriteLine("resolvethis");

            var operations = source.Operations;
            //tring op, string path, string from, object value
            IEnumerable<Operation<MarryMonioUser>> operation = source.Operations.Select((op) => new Operation<MarryMonioUser>(op.op, op.path, op.from, op.value));



            var rsvpPatch = new JsonPatchDocument<MarryMonioUser>(operation.ToList(), source.ContractResolver);


            return rsvpPatch;
        }
    }
}

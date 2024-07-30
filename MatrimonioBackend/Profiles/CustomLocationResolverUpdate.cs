using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Text.Json;
namespace MatrimonioBackend.Profiles
{
    public class CustomLocationResolverUpdate
    {
        public class CustomLocationResolverMainUpdate : ITypeConverter<JsonPatchDocument<LocationUpdateDTO>, JsonPatchDocument<Location>>
        {

            //Change only translation ;)
            public JsonPatchDocument<Location> Convert(JsonPatchDocument<LocationUpdateDTO> source, JsonPatchDocument<Location> destination, ResolutionContext context)
            {
                Console.WriteLine("resolvethis");

                var operations = source.Operations;
                var nonTranslationOperations = operations.Where((e) => e.path != "/Translation");

                //tring op, string path, string from, object value
                IEnumerable<Operation<Location>> operation = nonTranslationOperations.Select((op) => new Operation<Location>(op.op, op.path, op.from, op.value));

                var rsvpPatch = new JsonPatchDocument<Location>(operation.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
        }

        public class CustomLocationTranslateResolverUpdate : ITypeConverter<JsonPatchDocument<LocationUpdateDTO>, JsonPatchDocument<LocationTranslation>>
        {

            //Change only translation ;)
            public JsonPatchDocument<LocationTranslation> Convert(JsonPatchDocument<LocationUpdateDTO> source, JsonPatchDocument<LocationTranslation> destination, ResolutionContext context)
            {
                var operations = source.Operations;
                var translationOp = operations.Where((e) => e.path == "/Translation").FirstOrDefault();

                string stringTranslate = translationOp.value.ToString();
                var actualValue = stringTranslate.Substring(1, stringTranslate.Length - 1);
                var translation = JsonSerializer.Deserialize<LocationTranslation>(stringTranslate);

                var translationPatch = JsonPatchService<LocationTranslation>.MakePatch(translation).Where(op => op != null & op.path != "IsDefaultLanguage");
                

                var rsvpPatch = new JsonPatchDocument<LocationTranslation>(translationPatch.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
        }
    }
}

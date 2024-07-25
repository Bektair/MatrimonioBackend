using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Text.Json;

namespace MatrimonioBackend.Profiles
{
    public class CustomWeddingResolverUpdate : ITypeConverter<JsonPatchDocument<WeddingUpdateDTO>, JsonPatchDocument<Wedding>>
    {

           //Change only translation ;)
            public JsonPatchDocument<Wedding> Convert(JsonPatchDocument<WeddingUpdateDTO> source, JsonPatchDocument<Wedding> destination, ResolutionContext context)
            {
                Console.WriteLine("resolvethis");

                var operations = source.Operations;
                var nonTranslationOperations = operations.Where((e)=>e.path != "/Translation");

            //tring op, string path, string from, object value
                IEnumerable<Operation<Wedding>> operation = nonTranslationOperations.Select((op) => new Operation<Wedding>(op.op, op.path, op.from, op.value));

                var rsvpPatch = new JsonPatchDocument<Wedding>(operation.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
    }

    public class CustomWeddingTranslateResolverUpdate : ITypeConverter<JsonPatchDocument<WeddingUpdateDTO>, JsonPatchDocument<WeddingTranslation>>
    {

        //Change only translation ;)
        public JsonPatchDocument<WeddingTranslation> Convert(JsonPatchDocument<WeddingUpdateDTO> source, JsonPatchDocument<WeddingTranslation> destination, ResolutionContext context)
        {
            var operations = source.Operations;
            var translationOp = operations.Where((e) => e.path == "/Translation").FirstOrDefault();

            string stringTranslate = translationOp.value.ToString();
            var actualValue = stringTranslate.Substring(1, stringTranslate.Length - 1);
            var translation = JsonSerializer.Deserialize<WeddingTranslation>(stringTranslate);

            var translationPatch = JsonPatchService<WeddingTranslation>.MakePatch(translation);


            var rsvpPatch = new JsonPatchDocument<WeddingTranslation>(translationPatch.ToList(), source.ContractResolver);


            return rsvpPatch;
        }
    }


    public class CustomWeddingResolverCreate : ITypeConverter<WeddingCreateDTO, Wedding>
    {
        public Wedding Convert(WeddingCreateDTO soruce, Wedding destination, ResolutionContext context)
        {
            var translations = new List<WeddingTranslation>();
            translations.Add(new WeddingTranslation()
            {
                Description = soruce.description,
                Dresscode = soruce.dresscode,
                Language = soruce.language,
                Title = soruce.title,
                IsDefaultLanguage = soruce.isDefaultLanguage
            });
            Wedding wedding = new Wedding() { 
                backgroundImage = soruce.backgroundImage,
                primaryColor = soruce.primaryColor,
                primaryFontColor = soruce.primaryFontColor,
                bodyFont = soruce.bodyFont,
                headingFont = soruce.headingFont,
                secoundaryColor = soruce.secoundaryColor,
                secoundaryFontColor = soruce.secoundaryFontColor,
                picture = soruce.picture,
                Translations = translations
            };
            return wedding;
        }
        
    }

  

}

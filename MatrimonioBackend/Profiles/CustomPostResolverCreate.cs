using AutoMapper;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Text.Json;

namespace MatrimonioBackend.Profiles
{
    public class CustomPostResolverCreate
    {
        public class CustomPostResolverTranslateCreate
: ITypeConverter<PostTranslationCreateDTO, PostTranslation>
        {
            public PostTranslation Convert(PostTranslationCreateDTO source, PostTranslation destination, ResolutionContext context)
            {
                var rcTranslation = new PostTranslation()
                {
                    Body = source.Body,
                    Title = source.Title,
                    Language = source.Language,
                    IsDefaultLanguage = source.IsDefaultLanguage,
                };

                return rcTranslation;
            }
        }
        public class CustomPostResolverCreatePost: ITypeConverter<PostCreateDTO, Post>
        {
            public Post Convert(PostCreateDTO source, Post destination, ResolutionContext context)
            {
                var translation = new PostTranslation()
                {
                    Body = source.Body,
                    Title = source.Title,
                    Language = source.Language,
                    IsDefaultLanguage = true
                };
                var translations = new List<PostTranslation>();
                translations.Add(translation);

                var post = new Post()
                {
                    AuthorId = source.AuthorId,
                    WeddingId = source.WeddingId,
                    Images = source.Images.Select((img)=> new PostImage { Role = img.Role, URI = img.URI}).ToList(),
                    Translations = translations
                };

                return post;
            }
        }

        public class CustomPostResolverUpdatePost : ITypeConverter<JsonPatchDocument<PostUpdateDTO>, JsonPatchDocument<Post>>
        {
            public JsonPatchDocument<Post> Convert(JsonPatchDocument<PostUpdateDTO> source, JsonPatchDocument<Post> destination, ResolutionContext context)
            {

                var operations = source.Operations;
                var nonTranslationOperations = operations.Where((e) => e.path == "/Translation");

                //tring op, string path, string from, object value
                IEnumerable<Operation<Post>> operation = nonTranslationOperations.Select((op) => new Operation<Post>(op.op, op.path, op.from, op.value));

                var rsvpPatch = new JsonPatchDocument<Post>(operation.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
        }

        public class CustomPostResolverUpdateTranslatePost : ITypeConverter<JsonPatchDocument<PostUpdateDTO>, JsonPatchDocument<PostTranslation>>
        {
            public JsonPatchDocument<PostTranslation> Convert(JsonPatchDocument<PostUpdateDTO> source, JsonPatchDocument<PostTranslation> destination, ResolutionContext context)
            {

                var operations = source.Operations;
                var translationOp = operations.Where((e) => e.path == "/Translation").FirstOrDefault();


                string stringTranslate = translationOp.value.ToString();
                var actualValue = stringTranslate.Substring(1, stringTranslate.Length - 1);
                var translation = JsonSerializer.Deserialize<PostTranslationUpdateDTO>(stringTranslate);

                var translationPatch = JsonPatchService<PostTranslation>.MakePatch(translation);


                var rsvpPatch = new JsonPatchDocument<PostTranslation>(translationPatch.ToList(), source.ContractResolver);


                return rsvpPatch;
            }
        }

    }
}

using AutoMapper;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;

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
    }
}

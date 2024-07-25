using MatrimonioBackend.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MatrimonioBackend.Service
{
    public static class TranslationService
    {

        public static ITranslation? GetLangOrDefaultTranslation(IEnumerable<ITranslation> translations, string lang)
        {
            var translate = translations.FirstOrDefault((trans)=>trans.Language == lang.ToUpper());



            return (translate == null) ? translations.FirstOrDefault((translation) => translation.IsDefaultLanguage) : translate;
        }

        public static bool TranslationAllreadyExists(IEnumerable<ITranslation> translations, string lang)
        {
            return translations.FirstOrDefault((e) => 
            e.Language.ToUpper() == lang.ToUpper()) != null;
        }
}
}

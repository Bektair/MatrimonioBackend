namespace MatrimonioBackend.Models.Constants
{

    public class Language
    {
        public const string EN = nameof(EN);
        public const string ES = nameof(ES);
        public const string IT = nameof(IT);
        public const string NO = nameof(NO);
        public static HashSet<string> supportedLanguages = new HashSet<string>()
        {
            Language.ES, Language.EN, Language.IT, Language.NO
        };
        public static bool IsSupported(string lang)
        {
            return supportedLanguages.Contains(lang.ToUpper());
        }
    }
}

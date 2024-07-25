namespace MatrimonioBackend.Models
{
    public interface ITranslation
    {
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}

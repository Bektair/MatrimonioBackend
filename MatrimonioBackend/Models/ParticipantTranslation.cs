using MatrimonioBackend.Models;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(WeddingId), nameof(UserId), nameof(Language))]
public class ParticipantTranslation : ITranslation
{
    public int? WeddingId { get; set; }
    public Guid? UserId { get; set; }
    public string Language { get; set; }
    public bool IsDefaultLanguage { get; set; }
    public string Role { get; set; }
    public Participant? Participant { get; set; }

}
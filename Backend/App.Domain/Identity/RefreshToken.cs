using System.ComponentModel.DataAnnotations;

namespace App.Domain.Identity;

public class RefreshToken
{
    public Guid Id { get; set; }
    
    [StringLength(36, MinimumLength = 36)] 
    public string Token { get; set; } = Guid.NewGuid().ToString();

    //UTC
    public DateTime ExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);

    [StringLength(36, MinimumLength = 36)] 
    public string? PreviousToken { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    //UTC
    public DateTime? PreviousTokenExpirationDateTime { get; set; }
}
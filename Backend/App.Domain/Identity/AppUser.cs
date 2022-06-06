using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser: IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public int Money { get; set; } = 1000000;

    public ICollection<Housing>? Housings { get; set; }

    public ICollection<Contract>? Contracts { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
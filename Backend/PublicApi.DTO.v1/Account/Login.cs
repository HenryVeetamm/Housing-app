using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Account;

public class Login
{
    [StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Wrong length on email!")]
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
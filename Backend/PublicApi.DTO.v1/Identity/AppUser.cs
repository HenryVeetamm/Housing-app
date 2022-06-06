namespace PublicApi.DTO.v1.Identity;

public class AppUser
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
}
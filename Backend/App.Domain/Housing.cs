using App.Domain.Enum;
using App.Domain.Identity;

namespace App.Domain;

public class Housing
{
    public Guid Id { get; set; }

    public int SquareMeters { get; set; }

    public int RoomsCount { get; set; }

    public string Description { get; set; } = default!;

    public string Location { get; set; } = default!;

    public string Amenities { get; set; } = default!;

    public bool IsAvailable { get; set; } = true;

    public string PictureUrl { get; set; } = default!;
    
    public int Price { get; set; }
    public EActionType DealType { get; set; }
    public Guid OwnerId { get; set; }
    public AppUser? Owner { get; set; }

    public ICollection<Contract>? Contracts { get; set; }


}
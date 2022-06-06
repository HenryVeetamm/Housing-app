using App.Domain.Identity;

namespace App.Domain;

public class Contract
{
    public Guid Id { get; set; }

    public Guid HousingUnitId { get; set; }
    public Housing? HousingUnit { get; set; }

    public Guid LesseeId { get; set; }
    public AppUser? Lessee { get; set; }

    public int MonthlyRent { get; set; }

    public int StartMonth { get; set; }
    public int StartYear { get; set; }

    public int? EndMonth { get; set; }
    public int? EndYear { get; set; }
    
    public bool Accepted { get; set; } = false;

    public ICollection<ContractService>? ContractServices { get; set; }
    public ICollection<Billing>? Billings { get; set; }

}
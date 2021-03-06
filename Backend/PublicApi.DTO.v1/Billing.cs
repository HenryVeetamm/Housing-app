namespace PublicApi.DTO.v1;

public class Billing
{
    public Guid Id { get; set; }

    public int BillingMonth { get; set; }
    public int BillingYear { get; set; }

    public bool Payed { get; set; } = false;

    public int TotalSum { get; set; }
    
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
}
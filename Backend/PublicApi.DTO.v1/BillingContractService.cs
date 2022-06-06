namespace PublicApi.DTO.v1;

public class BillingContractService
{
    public Guid Id { get; set; }

    public Guid BillingId { get; set; }
    public Billing? Billing { get; set; }
    
    public Guid ContractServiceId { get; set; }
    public ContractService? ContractService { get; set; }

    public int Amount { get; set; }

    public int ServiceTotalSum { get; set; }
}
namespace PublicApi.DTO.v1;

public class ContractService
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }

 
}
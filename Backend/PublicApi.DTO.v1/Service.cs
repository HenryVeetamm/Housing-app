namespace PublicApi.DTO.v1;

public class Service
{
    public Guid Id { get; set; }

    public int CostPerUnit { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool FixedPricing { get; set; } = true;
    
}
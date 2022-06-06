using AutoMapper;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.MappingProfiles;

public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        CreateMap<Housing, App.Domain.Housing>().ReverseMap();
        
        CreateMap<Contract, App.Domain.Contract>().ReverseMap();
        
        CreateMap<Billing, App.Domain.Billing>().ReverseMap();
        
        CreateMap<Service, App.Domain.Service>().ReverseMap();
        
        CreateMap<ContractService, App.Domain.ContractService>().ReverseMap();
        
        CreateMap<BillingContractService, App.Domain.BillingContractService>().ReverseMap();

        CreateMap<AppUser, App.Domain.Identity.AppUser>().ReverseMap();
    }
}
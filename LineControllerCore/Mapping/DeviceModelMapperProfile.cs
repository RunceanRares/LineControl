using AutoMapper;
using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;

namespace LineControllerCore.Mapping
{
  public class DeviceModelMapperProfile : Profile
  {
    public DeviceModelMapperProfile()
    {
      CreateMap<DeviceModel, DeviceModelViewModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.LastChangeUser, opt => opt.MapFrom(src => src.LastChangedUser));

      CreateMap<DeviceModelViewModel, DeviceModel>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
         .ForMember(dest => dest.LastChangedUser, opt => opt.MapFrom(src => src.LastChangeUser));

    }
  }
}

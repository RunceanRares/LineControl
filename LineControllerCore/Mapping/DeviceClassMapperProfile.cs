using AutoMapper;
using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;

namespace LineControllerCore.Mapping
{
  public class DeviceClassMapperProfile : Profile
  {
    public DeviceClassMapperProfile()
    {
      CreateMap<DeviceClass, DeviceClassViewModel>()
         .ForMember(dest => dest.LastChangeUser, opt => opt.MapFrom(src => src.LastChangedUser))
         .ForMember(dest => dest.CanBeUniversal, opt => opt.MapFrom(src => src.Modes.Count >= 2));

      // DeviceViewModel to DeviceClass
      CreateMap<DeviceClassViewModel, DeviceClass>()
         .ForMember(dest => dest.Modes, opt => opt.Ignore())
         .ForMember(dest => dest.LastChangedUser, opt => opt.MapFrom(src => src.LastChangeUser))
         .ForMember(dest => dest.DeviceModel, opt => opt.Ignore());


      CreateMap<DeviceClassMode, DeviceClassViewModel>()
         .ForMember(dest => dest.LastChangeUser, opt => opt.MapFrom(src => src.LastChangedUser));

      CreateMap<DeviceClassViewModel, DeviceClassMode>()
       .ForMember(dest => dest.LastChangedUser, opt => opt.MapFrom(src => src.LastChangeUser));

    }
  }
}

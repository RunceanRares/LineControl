using AutoMapper;
using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;

namespace LineControllerCore.Mapping
{
  public class DeviceClassModeMapperProfile : Profile
  {
      public DeviceClassModeMapperProfile()
      {
      CreateMap<DeviceClassMode, DeviceClassModeViewModel>()
          .ForMember(dest => dest.MeasurementMin, opt => opt.MapFrom(src => src.MeasurementMin))
          .ForMember(dest => dest.MeasurementMax, opt => opt.MapFrom(src => src.MeasurementMax))
          .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit))
          .ForMember(dest => dest.OutputMin, opt => opt.MapFrom(src => src.OutputMin))
          .ForMember(dest => dest.OutputMax, opt => opt.MapFrom(src => src.OutputMax))
          .ForMember(dest => dest.OutputUnit, opt => opt.MapFrom(src => src.OutputUnit))
          .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
          .ForMember(dest => dest.ModeId, opt => opt.MapFrom(src => src.Id));
      // .ForMember(dest => dest.DeviceClassId, opt => opt.MapFrom(src => src.DeviceClassId));

      CreateMap<DeviceClassModeViewModel, DeviceClassMode>()
          .ForMember(dest => dest.MeasurementMin, opt => opt.MapFrom(src => src.MeasurementMin))
          .ForMember(dest => dest.MeasurementMax, opt => opt.MapFrom(src => src.MeasurementMax))
          .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit))
          .ForMember(dest => dest.OutputMin, opt => opt.MapFrom(src => src.OutputMin))
          .ForMember(dest => dest.OutputMax, opt => opt.MapFrom(src => src.OutputMax))
          .ForMember(dest => dest.OutputUnit, opt => opt.MapFrom(src => src.OutputUnit))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
          .ForMember(dest => dest.MaterialNumber, opt => opt.MapFrom(src => src.MaterialNumber))
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModeId));
//.ForMember(dest => dest.DeviceClassId, opt => opt.MapFrom(src => src.DeviceClassId));
      }
  }
}
using AutoMapper;
using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Mapping
{
  public class ActivityTypeMapperProfile : Profile
  {
    public ActivityTypeMapperProfile()
    {
      CreateMap<ActivityType, ActivityTypeViewModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
        .ForMember(dest => dest.CostCenter, opt => opt.MapFrom(src => src.CostCenter))
        .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
        .ForMember(dest => dest.PassiveCostFactor, opt => opt.MapFrom(src => src.PassiveCostFactor));

      CreateMap<ActivityType, ActivityTypeSelectViewModel>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
         .ForMember(dest => dest.CostCenter, opt => opt.MapFrom(src => src.CostCenter))
         .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
         .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
         .ForMember(dest => dest.PassiveCostFactor, opt => opt.MapFrom(src => src.PassiveCostFactor));
    }
  }
}

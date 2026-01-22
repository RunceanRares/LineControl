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
  public class InventoryLocationMapperProfile : Profile
  {
    public InventoryLocationMapperProfile()
    {
      CreateMap<InventoryLocation, InventoryLocationViewModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
        .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.StoragePlaces.FirstOrDefault().RoomNumber))
        .ForMember(dest => dest.ResponsibleId, opt => opt.MapFrom(src => src.ResponsibleId))
        .ForMember(dest => dest.ActivityTypeId, opt => opt.MapFrom(src => src.ActivityTypeId))
        .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.StoragePlaces.FirstOrDefault().Building))
        .ForMember(dest => dest.CompanyLocationId, opt => opt.MapFrom(src => src.StoragePlaces.FirstOrDefault().CompanyLocationId))
        .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.StoragePlaces.FirstOrDefault().CompanyLocation))
        .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.StoragePlaces.FirstOrDefault().Floor))
        .ForMember(dest => dest.StoragePlaceResponsibles, opt => opt.MapFrom(src => src.StoragePlaces.Select(s => s.Responsible)))
        .ForMember(dest => dest.CostFactor, opt => opt.MapFrom(src => src.CostFactor))
        .ForMember(dest => dest.StoragePlaceCompanyLocations, opt => opt.MapFrom(src => src.StoragePlaces.Select(s => s.CompanyLocation)))
        .ForMember(dest => dest.ActivityTypeRate, opt => opt.MapFrom(src => src.ActivityType.Rate));
    }
  }
}

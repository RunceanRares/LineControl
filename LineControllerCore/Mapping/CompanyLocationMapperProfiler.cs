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
  public class CompanyLocationMapperProfiler : Profile
  {
    public CompanyLocationMapperProfiler()
    {
      CreateMap<CompanyLocation, CompanyLocationViewModel>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
  }
}

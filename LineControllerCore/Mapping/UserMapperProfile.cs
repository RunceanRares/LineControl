using AutoMapper;
using LineControl.Models;
using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;

namespace LineControllerCore.Mapping
{
  public class UserMapperProfile : Profile
  {
    public UserMapperProfile()
    {
      CreateMap<User, UserViewModel>();

      CreateMap<UserViewModel, User>()
         .ForMember(dest => dest.Manager, opt => opt.Ignore());

      CreateMap<User, DisplayUserViewModel>();
    }
  }

}

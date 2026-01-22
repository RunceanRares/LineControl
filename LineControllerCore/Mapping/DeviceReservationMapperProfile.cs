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
  public class DeviceReservationMapperProfile : Profile
  {
    public DeviceReservationMapperProfile()
    {
      CreateMap<ReservationPeriod, ReservationPeriodViewModel>();

      CreateMap<Device, DeviceReservation>();

      CreateMap<DeviceReservation, Device>();

      CreateMap<DeviceReservationSelectViewModel, DeviceReservation>();
    }
  }
}

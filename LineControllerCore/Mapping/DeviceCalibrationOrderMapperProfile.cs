using AutoMapper;
using LineControllerCore.Model;
using LineControllerCore.Models;
using LineControllerInfrastructure.Entities;

using System.Text.RegularExpressions;

namespace LineControllerCore.Mapping
{
  public class DeviceCalibrationOrderMapperProfile : Profile
  {
    public DeviceCalibrationOrderMapperProfile()
    {
      CreateMap<DeviceCalibrationOrderViewModel, DeviceCalibrationOrder>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Device, opt => opt.Ignore())
        .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
        .ForMember(dest => dest.CalibrationDate, opt => opt.MapFrom(src => src.CalibrationDate))
        .ForMember(dest => dest.Inspector, opt => opt.MapFrom(src => src.Inspector))
        .ForMember(dest => dest.TestLocation, opt => opt.MapFrom(src => src.TestLocation))
        .ForMember(dest => dest.ProcessingTime, opt => opt.MapFrom(src => src.ProcessingTime))
        .ForMember(dest => dest.MeasurementSpan, opt => opt.MapFrom(src => src.MeasurementSpan))
        .ForMember(dest => dest.CalibrationResult, opt => opt.MapFrom(src => src.CalibrationResult))
        .ForMember(dest => dest.SendEmail, opt => opt.MapFrom(src => src.SendEmail))
        .ForMember(dest => dest.PreviousDeviceStatusId, opt => opt.MapFrom(src => src.PreviousDeviceStatusId))
        .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.Edited))
        .ForMember(dest => dest.IsRoot, opt => opt.MapFrom(src => src.IsRoot))
        .ForMember(dest => dest.Root, opt => opt.Ignore())
        .ForMember(dest => dest.RootId, opt => opt.Ignore())
        .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());

      CreateMap<DeviceCalibrationOrder, DeviceCalibrationOrderViewModel>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
       .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.Device.ItemNumber))
       .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.Device.SerialNumber))
       .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.Root.AccountingNumber))
       .ForMember(dest => dest.ReceiverFirstName, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.FirstName : null))
       .ForMember(dest => dest.ReceiverLastName, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.LastName : null))
       .ForMember(dest => dest.ReceiverDepartment, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.Department : null))
       .ForMember(dest => dest.CreatedByFirstName, opt => opt.MapFrom(src => src.Device.CreatedBy != null ? src.Device.CreatedBy.FirstName : null))
       .ForMember(dest => dest.CreatedByLastName, opt => opt.MapFrom(src => src.Device.CreatedBy != null ? src.Device.CreatedBy.LastName : null))
       .ForMember(dest => dest.CreatedByDepartment, opt => opt.MapFrom(src => src.Device.CreatedBy != null ? src.Device.CreatedBy.Department : null))
       .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Root.Action.Name))
       .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.Root.Action.Id))
       .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Device.Status.Name))
       .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Device.StatusId))
       // .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.StatusId).FirstOrDefault()))
       .ForMember(dest => dest.CalibrationDate, opt => opt.MapFrom(src => src.CalibrationDate))
       .ForMember(dest => dest.Inspector, opt => opt.MapFrom(src => src.Inspector))
       .ForMember(dest => dest.TestLocation, opt => opt.MapFrom(src => src.TestLocation))
       .ForMember(dest => dest.ProcessingTime, opt => opt.MapFrom(src => src.ProcessingTime))
       .ForMember(dest => dest.CalibrationResult, opt => opt.MapFrom(src => src.CalibrationResult))
       .ForMember(dest => dest.SendEmail, opt => opt.MapFrom(src => src.SendEmail))
       .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Root.Comment))
       .ForMember(dest => dest.IsRoot, opt => opt.MapFrom(src => src.IsRoot))
       .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.Device.CreatedById))
       .ForMember(dest => dest.CalibrationInterval, opt => opt.MapFrom(src => src.Device.CalibrationInterval));

      CreateMap<DeviceCalibrationOrderViewModel, DeviceCalibrationOrder>()
        .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
        .ForMember(dest => dest.Device, opt => opt.Ignore()) // Ignore the navigation property
        .ForMember(dest => dest.RootId, opt => opt.MapFrom(src => src.RootId))
        .ForPath(dest => dest.Root.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
        .ForPath(dest => dest.Root.Comment, opt => opt.MapFrom(src => src.Comment))
        .ForMember(dest => dest.CalibrationDate, opt => opt.MapFrom(src => src.CalibrationDate))
        .ForMember(dest => dest.Inspector, opt => opt.MapFrom(src => src.Inspector))
        .ForMember(dest => dest.TestLocation, opt => opt.MapFrom(src => src.TestLocation))
        .ForMember(dest => dest.ProcessingTime, opt => opt.MapFrom(src => src.ProcessingTime))
        .ForMember(dest => dest.CalibrationResult, opt => opt.MapFrom(src => src.CalibrationResult))
        .ForMember(dest => dest.SendEmail, opt => opt.MapFrom(src => src.SendEmail))
        .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.Edited));

      CreateMap<DeviceCalibrationOrderViewModel, DeviceCalibrationOrderRoot>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
          .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.ActionId))
          .ForMember(dest => dest.Action, opt => opt.MapFrom(src => new CalibrationAction
          {
            Id = src.ActionId,
            Name = src.Action
          }))
          .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

      CreateMap<Device, DeviceCalibrationOrderCreateViewModel>()
       .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.ItemNumber))
       .ForMember(dest => dest.AccountingType, opt => opt.Ignore())
       .ForMember(dest => dest.AccountingNumber, opt => opt.Ignore())
       .ForMember(dest => dest.ActionId, opt => opt.Ignore())
       .ForMember(dest => dest.ReceiverId, opt => opt.Ignore())
       .ForMember(dest => dest.NoChannels, opt => opt.Ignore())
       .ForMember(dest => dest.TargetDate, opt => opt.Ignore())
       .ForMember(dest => dest.Comment, opt => opt.Ignore())
       .ForMember(dest => dest.Actions, opt => opt.Ignore());

      CreateMap<DeviceCalibrationOrderCreateViewModel, DeviceCalibrationOrderRoot>()
       .ForMember(dest => dest.Id, opt => opt.Ignore())
       .ForMember(dest => dest.AccountingType, opt => opt.MapFrom(src => src.AccountingType))
       .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
       .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.ActionId))
       .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
       .ForMember(dest => dest.NoChannels, opt => opt.MapFrom(src => src.NoChannels))
       .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId));

      CreateMap<DeviceCalibrationOrderStatusHistory, CalibrationViewModel>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CalibrationOrderId))
       .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.CalibrationOrder.DeviceId))
       .ForMember(dest => dest.LastStatus, opt => opt.MapFrom(src => src.StatusId));

      CreateMap<DeviceCalibrationOrderCreateViewModel, DeviceCalibrationOrderRoot>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.AccountingType, opt => opt.MapFrom(src => src.AccountingType))
        .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
        .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.ActionId))
        .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
        .ForMember(dest => dest.NoChannels, opt => opt.MapFrom(src => src.NoChannels))
        .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverId));
     }
  }
}

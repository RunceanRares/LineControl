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
        .ForMember(dest => dest.DeviceId, opt => opt.Ignore())
        .ForMember(dest => dest.CalibrationDate, opt => opt.MapFrom(src => src.CalibrationDate))
        .ForMember(dest => dest.Inspector, opt => opt.MapFrom(src => src.Inspector))
        .ForMember(dest => dest.TestLocation, opt => opt.MapFrom(src => src.TestLocation))
        .ForMember(dest => dest.ProcessingTime, opt => opt.MapFrom(src => src.ProcessingTime))
        .ForMember(dest => dest.MeasurementSpan, opt => opt.MapFrom(src => src.MeasurementSpan))
        .ForMember(dest => dest.CalibrationResult, opt => opt.MapFrom(src => src.CalibrationResult))
        .ForMember(dest => dest.SendEmail, opt => opt.MapFrom(src => src.SendEmail))
        .ForMember(dest => dest.PreviousDeviceStatusId, opt => opt.Ignore())
        .ForMember(dest => dest.Root, opt => opt.Ignore())
        .ForMember(dest => dest.RootId, opt => opt.Ignore())
        .ForMember(dest => dest.Edited, opt => opt.MapFrom(_ => true))
        .ForMember(dest => dest.IsRoot, opt => opt.Ignore())
        .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());

      CreateMap<DeviceCalibrationOrder, DeviceCalibrationOrderViewModel>()
       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
       .ForMember(dest => dest.DeviceClassId, opt => opt.MapFrom(src => src.Device.DeviceClassId))
       .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.Device.ItemNumber))
       .ForMember(dest => dest.DeviceModel, opt => opt.MapFrom(src => src.Device.DeviceClass.DeviceModel.Name))
       .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.Device.SerialNumber))
       .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.Root.AccountingNumber))
       .ForMember(dest => dest.ReceiverFirstName, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.FirstName : null))
       .ForMember(dest => dest.ReceiverLastName, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.LastName : null))
       .ForMember(dest => dest.ReceiverDepartment, opt => opt.MapFrom(src => src.Root.ReceiverId != null ? src.Root.Receiver.Department : null))
       .ForMember(dest => dest.CreatedByFirstName, opt => opt.MapFrom(src => src.StatusHistory.Single(sh => sh.StatusId == DeviceCalibrationOrderStatus.Received).LastChangedUser.FirstName))
       .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Device.CreatedBy.FirstName + src.Device.CreatedBy.LastName))
       .ForMember(dest => dest.CreatedByLastName, opt => opt.MapFrom(src => src.StatusHistory.Single(sh => sh.StatusId == DeviceCalibrationOrderStatus.Received).LastChangedUser.LastName))
       .ForMember(dest => dest.CreatedByDepartment, opt => opt.MapFrom(src => src.StatusHistory.Single(sh => sh.StatusId == DeviceCalibrationOrderStatus.Received).LastChangedUser.Department))
       .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Root.Action))
       .ForMember(dest => dest.ActionId, opt => opt.MapFrom(src => src.Root.ActionId))
       .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.StatusId).FirstOrDefault()))
       .ForMember(dest => dest.LastStatusModifierFirstName, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.LastChangedUser.FirstName).First()))
       .ForMember(dest => dest.LastStatusModifierLastName, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.LastChangedUser.LastName).First()))
       .ForMember(dest => dest.LastStatusModifierDepartment, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.LastChangedUser.Department).First()))
       .ForMember(dest => dest.LastStatusModificationDate, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.LastChangedDate).First()))
       .ForMember(dest => dest.CalibrationDate, opt => opt.MapFrom(src => src.CalibrationDate))
       .ForMember(dest => dest.Inspector, opt => opt.MapFrom(src => src.Inspector))
       .ForMember(dest => dest.TestLocation, opt => opt.MapFrom(src => src.TestLocation))
       .ForMember(dest => dest.ProcessingTime, opt => opt.MapFrom(src => src.ProcessingTime))
       .ForMember(dest => dest.HasMeasurementRange, opt => opt.MapFrom(src => src.Device.MeasurementMin != null && src.Device.MeasurementMax != null))
       .ForMember(dest => dest.MeasurementSpan, opt => opt.MapFrom(src => src.StatusHistory.OrderByDescending(sh => sh.LastChangedDate).Select(sh => sh.StatusId).First() == DeviceCalibrationOrderStatus.Received && src.Device.MeasurementMin != null && src.Device.MeasurementMax != null && src.MeasurementSpan == null ? (decimal)(src.Device.MeasurementMax - src.Device.MeasurementMin) : src.MeasurementSpan))
       .ForMember(dest => dest.CalibrationResult, opt => opt.MapFrom(src => src.CalibrationResult))
       .ForMember(dest => dest.SendEmail, opt => opt.MapFrom(src => src.SendEmail))
       .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Root.Comment))
       .ForMember(dest => dest.IsRoot, opt => opt.MapFrom(src => src.IsRoot))
       .ForMember(dest => dest.RootId, opt => opt.MapFrom(src => src.RootId))
       .ForMember(dest => dest.CalibrationInterval, opt => opt.MapFrom(src => src.Device.CalibrationInterval));

      CreateMap<Device, DeviceCalibrationOrderCreateViewModel>()
       .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.Id))
       .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.ItemNumber))
       .ForMember(dest => dest.DeviceModel, opt => opt.MapFrom(src => src.DeviceClass.DeviceModel.Name))
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
       .ForMember(dest => dest.DeviceClassId, opt => opt.MapFrom(src => src.CalibrationOrder.Device.DeviceClassId))
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

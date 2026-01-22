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
  public class DeviceIssuesMapperProfile : Profile
  {

    public DeviceIssuesMapperProfile()
    {
      DateTime? now = null;
      CreateMap<DeviceIssue, DeviceIssueEditViewModel>()
           .ForMember(dest => dest.IssueId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
           .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.Device.ItemNumber))
           .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientId))
           .ForMember(dest => dest.IsAccountingMandatory, opt => opt.MapFrom(_ => false))
           .ForMember(dest => dest.HasCalibrationDue, opt => opt.MapFrom(src => src.Device.CalibrationDate != null && src.Device.CalibrationInterval != null && src.Device.CalibrationDate.Value.AddMonths(src.Device.CalibrationInterval.Value) <= now))
           .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Device.Parent.ItemNumber))
           .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Device.ParentId))
           .ForMember(dest => dest.AccountingType, opt => opt.MapFrom(src => src.AccountingType))
           .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
           .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDatePlanned))
           .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Status, opt => opt.Ignore())
           .ForMember(dest => dest.CanIssue, opt => opt.MapFrom(_ => false))
           .ForMember(dest => dest.CanRetrieve, opt => opt.MapFrom(_ => true))
           .ForMember(dest => dest.IsRetrieve, opt => opt.MapFrom(_ => true))
           .ForMember(dest => dest.IsSaveRecipient, opt => opt.Ignore())
           .ForMember(dest => dest.HasReservations, opt => opt.Ignore())
           .ForMember(dest => dest.SavedRecipientId, opt => opt.Ignore())
           .ForMember(dest => dest.SavedCollectorId, opt => opt.Ignore())
           .ForMember(dest => dest.SavedAccountingNumber, opt => opt.Ignore())
           .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
           .ForMember(dest => dest.HasDeviceViewRight, opt => opt.Ignore())
           .ForMember(dest => dest.MinimReturnDate, opt => opt.Ignore())
           .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Recipient != null ? $"{src.Recipient.LastName}, {src.Recipient.FirstName}" : string.Empty))
           .ForMember(dest => dest.Reservations, opt => opt.Ignore());


      CreateMap<DeviceIssueEditViewModel, DeviceIssue>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IssueId))
          .ForMember(dest => dest.AccountingNumber, opt => opt.MapFrom(src => src.AccountingNumber))
          .ForMember(dest => dest.AccountingType, opt => opt.MapFrom(src => src.AccountingType))
          .ForMember(dest => dest.ReturnDatePlanned, opt => opt.MapFrom(src => src.ReturnDate))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Comment))

          // Ignorăm proprietățile de navigare sau calculate care nu trebuie scrise în DB
          .ForMember(dest => dest.Device, opt => opt.Ignore())
          .ForMember(dest => dest.Recipient, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
          .ForMember(dest => dest.LastChangedUser, opt => opt.Ignore());
    }
  }
}

using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Model
{
  public class DeviceClassViewModel
  {
    private int? deviceModelId;

    private int? oldDeviceModelId;

    public int Id { get; set; }
    [Display(Name = "Model")]
    [Required(ErrorMessage = "The 'Model' field is required.")]
    public int DeviceModelId
    {
      get => deviceModelId ?? oldDeviceModelId ?? 0;
      set { deviceModelId = value; }
    }

    public int OldDeviceModelId
    {
      get => oldDeviceModelId ?? deviceModelId ?? 0;
      set { oldDeviceModelId = value; }
    }

    public bool IsValidDeviceModelId => IsNew || deviceModelId != null;
    public bool IsNew
    {
      get
      {
        return Id < 1;
      }
    }
    public bool IsDisplay { get; set; }

    public double MaxFileSize { get; set; }

    public bool CanBeUniversal { get; set; }

    public string? LastChangeUser { get; set; }

  }
}

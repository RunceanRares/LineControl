namespace LineControl.Common
{
  public interface ILinkService
  {
    public string GetDeviceDetailsLink(int deviceId);
    public string GetCalibrationViewPageLink(int calibrationId);
  }
}

using System.Diagnostics.CodeAnalysis;

namespace LineControl.Common
{
  [ExcludeFromCodeCoverage]
  public class LinkService : ILinkService
  {
    private readonly IHttpContextAccessor contextAccessor;
    private readonly LinkGenerator linkGenerator;

    public LinkService(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
    {
       this.contextAccessor = contextAccessor;
       this.linkGenerator = linkGenerator;
    }

    public string GetCalibrationViewPageLink(int calibrationId)
    {
      return linkGenerator.GetUriByAction(contextAccessor.HttpContext!, "Edit", "Calibration", new { id = calibrationId });
    }

    public string GetDeviceDetailsLink(int deviceId)
    {
      return linkGenerator.GetUriByAction(contextAccessor.HttpContext!, "Details", "Device", new { id = deviceId });
    }
  }
}

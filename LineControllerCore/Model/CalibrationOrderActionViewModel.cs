
namespace LineControllerCore.Model
{
  public class CalibrationOrderActionViewModel
  {
    public const int Calibration = 1;
    public const int Checking = 2;
    public const int DmsApplication = 3;

    public int Id { get; set; }

    public string Name { get; set; }
  }
}

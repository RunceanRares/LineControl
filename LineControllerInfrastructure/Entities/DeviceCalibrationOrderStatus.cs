using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceCalibrationOrderStatus
  {
    public const int Received = 10;
    public const int InProgress = 20;
    public const int SentExternally = 30;

    ////-----virtual statuses-------
    // greater than all the active statuses
    public const int Active = 50;

    // less than all the closed statuses
    public const int Closed = 60;

    ////----------------------------

    public const int FinishedOK = 70;
    public const int FinishedNOK = 80;
    public const int FinishedAdjusted = 90;
    public const int FinishedOKAdjusted = 100;
    public const int FinishedScrap = 110;
    public const int Returned = 120;

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Name { get; set; }

    public static bool IsValid(int statusId)
    {
      return statusId switch
      {
        Received or InProgress or SentExternally or FinishedOK or FinishedNOK or FinishedAdjusted or FinishedOKAdjusted or FinishedScrap or Returned => true,
        _ => false,
      };
    }
  }
}

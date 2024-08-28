using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceStatus
  {
    public const int UsableId = 100;
    public const int DefectId = 200;
    public const int DisposedId = 300;
    public const int LostId = 400;
    public const int LockedId = 500;
    public const int InServiceId = 600;

    // virtual statuses
    public const int IssuedId = 130;
    public const int ReservedId = 150;
    public const int NotUsableId = 170;
    public const int NotUsableIssuedId = 190;

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Name { get; set; }  
  }
}
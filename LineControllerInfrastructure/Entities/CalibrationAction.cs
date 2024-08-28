using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class CalibrationAction
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string Name { get; set; }
  }
}

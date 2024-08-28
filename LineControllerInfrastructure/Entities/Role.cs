using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LineControllerInfrastructure.Entities
{
  public class Role : BaseModel
  {
    [Column("RoleId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [MaxLength(450)]
    public string Name { get; set; }

    public bool Deactivated { get; set; }

  }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LineControllerInfrastructure.Entities
{
  public abstract class BaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    public DateTime? LastChangedDate { get; set; }

    [ForeignKey(nameof(LastChangedUser))]
    public int? LastChangedUserId { get; set; }

    public virtual User? LastChangedUser { get; set; }
  }
}

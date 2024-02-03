using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class CompanyLocation : BaseModel
  {
    [Column("CompanyLocationId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [MinLength(3)]
    [MaxLength(3)]
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }
    [ExcludeFromCodeCoverage]
    public virtual ICollection<User> Users { get; } = new List<User>();

  }
}
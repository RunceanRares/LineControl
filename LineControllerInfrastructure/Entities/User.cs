using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities
{
  public class User : BaseModel
  {
    [Column("UserId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [Required]
    public string? PersonalNumber { get; set; }

    public string? Title { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? UserName { get; set; }

    public string? Department { get; set; }

    public string? CostCenter { get; set; }

    public int? ManagerId { get; set; }

    public User Manager { get; set; }

    public string? Email { get; set; }
    
    public int Status { get; set; }

    [MaxLength(30)]
    public string Phone { get; set; }

    [ForeignKey(nameof(CompanyLocation))]
    public int CompanyLocationId { get; set; }

    public CompanyLocation CompanyLocation { get; set; }
  }
}

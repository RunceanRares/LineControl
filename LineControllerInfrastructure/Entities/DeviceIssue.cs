using LineControllerInfrastructure.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceIssue : BaseModel
  {
    public const string AvoidDuplicateIndexName = "IX_DeviceIssue_AvoidDuplicate";

    [Column("DeviceIssueId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public Device Device { get; set; }

    [ForeignKey(nameof(Device))]
    public int DeviceId { get; set; }

    public User Recipient { get; set; }

    [ForeignKey(nameof(Recipient))]
    public int RecipientId { get; set; }

    public User Collector { get; set; }

    [ForeignKey(nameof(Collector))]
    public int? CollectorId { get; set; }

    public AccountingType? AccountingType { get; set; }

    public string AccountingNumber { get; set; }

    public DateTime? ReturnDatePlanned { get; set; }

    public DateTime? ReturnDateActual { get; set; }

    public DateTime IssueDate { get; set; }

    public string Description { get; set; }

    public DeviceReservation Reservation { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public int? CreatedById { get; set; }

    public User CreatedBy { get; set; }
  }
}
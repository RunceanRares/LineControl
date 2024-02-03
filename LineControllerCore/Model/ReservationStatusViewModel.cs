using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class ReservationStatusViewModel
  {
    public const int OpenId = 1;
    public const int CancelBeforeDeadlineId = 3;
    public const int CancelAfterDeadlineId = 2;
    public const int NotCollectedId = 4;
    public const int CollectedId = 5;
    public const int AlternativeRejectedOrNotPresentId = 6;

    public int Id { get; set; }

    public string Name { get; set; }
  }
}

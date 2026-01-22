using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities.Enums
{
  public enum ReservationStatusEnum
  {
    Open = 1,
    CanceledDeadline = 2,
    CanceledNoDeadline = 3,
    NotCollected = 4,
    Collected = 5,
    AlternativeRejected = 6
  }
}

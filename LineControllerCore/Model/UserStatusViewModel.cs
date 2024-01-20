using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class UserStatusViewModel
  {
    public const int StatusActive = 1;
    public const int StatusInactive = 0;

    public string? Text { get; init; }

    public int Value { get; init; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class UserSelectViewModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Department { get; set; } = null!;
    public string Display
    {
      get
      {
        return this.LastName + ", " + this.FirstName + " - " + this.Department;
      }
    }
    public string DisplayName
    {
      get
      {
        return this.LastName + ", " + this.FirstName;
      }
    }
  }
}

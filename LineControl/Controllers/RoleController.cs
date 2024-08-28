using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class RoleController : Controller
  {
    private readonly RoleManager<Role> _roleManager;

    public RoleController(RoleManager<Role> roleManager)
    {
      _roleManager = roleManager;
    }

    public async Task<IActionResult> CreateRole(string roleName)
    {
      if (!await _roleManager.RoleExistsAsync(roleName))
      {
        var role = new Role { Name = roleName };
        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
          return Ok($"Role {roleName} created successfully.");
        }
        return BadRequest(result.Errors);
      }
      return BadRequest("Role already exists.");
    }
  }
}

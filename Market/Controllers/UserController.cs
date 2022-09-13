using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Market.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();

            if(response.Status == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
    }
}

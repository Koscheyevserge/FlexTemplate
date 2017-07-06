using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Login
{
    public class LoginController : FlexController
    {
        public LoginController(ControllerServices services) : base(services)
        {

        }

        public async Task<IActionResult> LoginAsGuest()
        {
            await BllServices.Login("savchuk89", "password", true, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LoginAsAdmin()
        {
            await BllServices.Login("Supervisor", "Supervisor", true, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await BllServices.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
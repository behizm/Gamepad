using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gamepad.Service.Models.ViewModels;

namespace Gamepad.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            //var addResult = await Services.Role.AddUserToRoleAsync(new RoleUserModel
            //{
            //    Rolename = "FirstRole",
            //    Username = "behizm"
            //});
            //if (!addResult.Succeeded)
            //{
            //    ViewBag.Error1 = addResult.Errors.FirstOrDefault();
            //}

            //var removeResult = await Services.Role.RemoveUserFromRoleAsync(new RoleUserModel
            //{
            //    Rolename = "FirstRole",
            //    Username = "behizm"
            //});
            //if (!removeResult.Succeeded)
            //{
            //    ViewBag.Error2 = removeResult.Errors.FirstOrDefault();
            //}

            return View();
        }
    }
}
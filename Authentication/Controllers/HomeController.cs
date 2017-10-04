using Core;
using Core.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Authentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDBManager _dbManager;
        public HomeController(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            string username = User.Identity.Name;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public void LogOut()
        {
            FormsAuthentication.SignOut();
            RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public string AddUserInfo(UserModel model)
        {
            model.UserName = User.Identity.Name;
            var done = _dbManager.EditUser(model);
            return "Thanks,Your info is completed";
        }


        [Authorize(Roles = "user")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
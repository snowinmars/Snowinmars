using System.Web.Mvc;
using Snowinmars.Ui.App_Start;

namespace Snowinmars.Ui.Controllers
{
    [Route("Home")]
    [Authorize]
    [Internationalization]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("banned")]
        [AllowAnonymous]
        public ActionResult Banned()
        {
            return this.View();
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Route("emailAdmin")]
        [AllowAnonymous]
        public JsonResult EmailAdmin(string message)
        {
            //bool wasSendToAdmin = EmailService.SendToAdmin(message);

            //if (wasSendToAdmin)
            //{
            //    return ControllerHelper.GetSuccessJsonResult(true);
            //}

            return ControllerHelper.GetFailJsonResult();
        }
    }
}
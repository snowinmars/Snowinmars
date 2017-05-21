using Snowinmars.Bll.Interfaces;
using System;
using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    public class ApiController : Controller
    {
        private readonly IAuthorLogic authorLogic;
        private readonly IBookLogic bookLogic;
        private readonly IUserLogic userLogic;

        public ApiController(IAuthorLogic authorLogic, IBookLogic bookLogic, IUserLogic userLogic)
        {
            this.authorLogic = authorLogic;
            this.bookLogic = bookLogic;
            this.userLogic = userLogic;
        }

        [HttpGet]
        [Route("author")]
        public JsonResult Author(Guid id)
        {
            try
            {
                var author = this.authorLogic.Get(id);

                return ControllerHelper.GetSuccessJsonResult(author);
            }
            catch
            {
                return ControllerHelper.GetFailJsonResult();
            }
        }

        [HttpGet]
        [Route("book")]
        public JsonResult Book(Guid id)
        {
            try
            {
                var book = this.bookLogic.Get(id);

                return ControllerHelper.GetSuccessJsonResult(book);
            }
            catch
            {
                return ControllerHelper.GetFailJsonResult();
            }
        }

        [HttpGet]
        [Route("userById")]
        public JsonResult UserById(Guid id)
        {
            try
            {
                var user = this.userLogic.Get(id);

                user.PasswordHash = ""; // TODO this is hole. redo
                user.Salt = "";

                return ControllerHelper.GetSuccessJsonResult(user);
            }
            catch
            {
                return ControllerHelper.GetFailJsonResult();
            }
        }

        [HttpGet]
        [Route("userByUsername")]
        public JsonResult UserByUsername(string username)
        {
            try
            {
                var user = this.userLogic.Get(username);

                user.PasswordHash = ""; // TODO this is hole. redo
                user.Salt = "";

                return ControllerHelper.GetSuccessJsonResult(user);
            }
            catch
            {
                return ControllerHelper.GetFailJsonResult();
            }
        }

       
    }
}
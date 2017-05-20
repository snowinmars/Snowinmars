using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snowinmars.Bll.Interfaces;

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

                return GetSuccessJsonResult(author);
            }
            catch
            {
                return GetFailJsonResult();
            }
        }

        private JsonResult GetSuccessJsonResult(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("book")]
        public JsonResult Book(Guid id)
        {
            try
            {
                var book = this.bookLogic.Get(id);


                return GetSuccessJsonResult(book);
               
            }
            catch
            {
                return GetFailJsonResult();
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


                return GetSuccessJsonResult(user);
            
            }
            catch
            {
                return GetFailJsonResult();
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

                return GetSuccessJsonResult(user);
             
            }
            catch
            {
                return GetFailJsonResult();
            }
        }

        private static JsonResult GetFailJsonResult()
        {
            return new JsonResult { Data = new { success = false } };
        }
    }
}
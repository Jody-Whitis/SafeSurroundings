using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using SafeSurroundings.Services;

namespace SafeSurroundings.Controllers
{
    /// <summary>
    /// Base Controller for common functionality.
    /// </summary>
    public abstract class BaseController : Controller
    {
        public BaseController()
        {

        }

        // GET: Base
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [UserAuthentication]
        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [UserAuthentication]
        public virtual ActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        [UserAuthentication]
        public virtual ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// Return a standard status code, or override for custom codes.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected virtual int SetStatus(Exception ex)
        {
            if ((ex != null) && (!string.IsNullOrEmpty(ex.Message.ToString()))){
                return Convert.ToInt16(HttpStatusCode.BadRequest);
            }
            else
            {
                return Convert.ToInt16(HttpStatusCode.OK);
            }
        }
    }
}
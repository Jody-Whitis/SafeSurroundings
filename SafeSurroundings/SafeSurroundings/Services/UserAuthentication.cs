using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace SafeSurroundings.Services
{  
        /// <summary>
        /// Validates Session GUID for User Authentication
        /// </summary>
        public class UserAuthentication : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
                if ((filterContext.HttpContext.Session.Keys.Count == 0) && (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["sessionGUID"]))))
                {
                filterContext.Result = new HttpUnauthorizedResult();
                }
            }

            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
                {        
                filterContext.Result = new RedirectResult("~/Home/Index");             
                }
            }
        }    
}
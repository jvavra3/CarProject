using CarProject.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarProject.Filters
{
    public class Logging:ActionFilterAttribute
    {
        FilterContext db = new FilterContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            LogModel Log = new LogModel()
            {

                //Check if user is Authenticated then take his name else take string Anonymous
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",

                //Check for proxy server then aquare IP address (if "HTTP_x_FORWARDED_FOR" is Null) then get UserHostAddress
                IpAddress = request.ServerVariables["HTTP_x_FORWARDED_FOR"] ?? request.UserHostAddress,

                //Controller and action
                Area = request.RawUrl,
                //Time
                TimeAccess = DateTime.Now.ToString()

            };

            db.Logs.Add(Log);
            db.SaveChanges();


            base.OnActionExecuting(filterContext);
        }

    }
}
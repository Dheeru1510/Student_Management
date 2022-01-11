using System;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Student_Management.Common
{
    public class CustomActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var res = ((System.Net.Http.ObjectContent)(filterContext.Response.Content)).Value;
            var method = Convert.ToString(filterContext.Request.Method);

            FileLogger.Log(JsonSerializer.Serialize(res), "Req_Res_LogFile_");
        }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                var method = Convert.ToString(filterContext.Request.Method);
                var ipAddress = ((HttpContextWrapper)filterContext.Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                var url = filterContext.Request.RequestUri.AbsolutePath;
                var actArr = filterContext.ActionArguments;
                string body = "";
                if (actArr.Count > 0)
                {
                    var obj = actArr.FirstOrDefault().Value;
                    body = JsonSerializer.Serialize(obj);
                }
                string msg = string.Empty;
                if (method == "GET")
                {
                    msg = $"Method: {method}, IP_Address: {ipAddress}, URL: {url}";
                }
                else
                {
                    msg = $"Method: {method}, IP_Address: {ipAddress}, URL: {url}, RequestBody: {body}";
                }

                FileLogger.Log(msg, "Req_Res_LogFile_");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
            }
        }

    }
}
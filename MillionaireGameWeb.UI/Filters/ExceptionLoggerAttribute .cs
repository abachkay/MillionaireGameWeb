using log4net;
using MillionaireGameWeb.Entities;
using System;
using System.Web.Mvc;

namespace MillionaireGameWeb.UI.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {                  
            LogManager.GetLogger("LOGGER").Error(filterContext.Exception.Message);
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception is System.Net.Mail.SmtpException || filterContext.Exception is FormatException)
            {
                filterContext.Result = new RedirectResult("~/Content/EmailErrorPage.html");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Content/ServerErrorPage.html");
            }           
        }
    }
}
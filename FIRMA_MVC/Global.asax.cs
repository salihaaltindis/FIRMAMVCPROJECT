using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FIRMA_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["sayi"] = 0;
        }

        protected void Application_End()
        {
            Application["sayi"] = 0;
        }


        protected void Session_Start()
        {
            Application["sayi"] = (int)(Application["sayi"]) + 1;
            Session["saat"] = DateTime.Now.ToShortTimeString();
        }
        protected void Session_End()
        {
            Application["sayi"] = (int)(Application["sayi"]) - 1;

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("/Admin/Hata/Index?mesaj=" + exc.Message.Replace("\r\n", ""));//hatacontrollerdeki viewe hatay� y�nlendirmek i�in response.redirect yapt�k.
        }
    }


}
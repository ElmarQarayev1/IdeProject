using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace IDE_CASHCOUNT
{
    public class Global : HttpApplication
    {


        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Code that runs on application startup
            Application["SiteVisitedCounter"] = 0;
            //to check how many users have currently opened our site write the following line
            Application["OnlineUserCounter"] = 0;
        }




        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Application.Lock();
            Application["SiteVisitedCounter"] = Convert.ToInt32(Application["SiteVisitedCounter"]) + 1;
            //to check how many users have currently opened our site write the following line
            Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer
            // or SQLServer, the event is not raised.
            Application.Lock();
            Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) - 1;
            Application.UnLock();

        }


    }
}
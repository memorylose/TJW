using System;
using System.Security.Principal;
using System.Web.Routing;
using System.Web.Security;
using TJW.Utils;

namespace TJW.UI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);  
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("ForDetail", "Detail/{ClothGuid}", "~/Detail.aspx");
            routes.MapPageRoute("ForNewsDetail", "NewsDetail/{NewsId}", "~/NewsDetail.aspx");
            routes.MapPageRoute("ForList", "List/{ListId}", "~/List.aspx");
            routes.MapPageRoute("ForListPager", "List/{ListId}/{Pager}", "~/List.aspx");
            routes.MapPageRoute("ForNewsList", "NewsList", "~/NewsList.aspx");
            routes.MapPageRoute("ForNewsPager", "NewsList/{Pager}", "~/NewsList.aspx");
            routes.MapPageRoute("ForTea", "Tea", "~/CY/Index.aspx");
            routes.MapPageRoute("ForTeaDetail", "Tea/D/{TeaId}", "~/CY/CYDetail.aspx");
            routes.MapPageRoute("ForTeaPager", "Tea/{Pager}", "~/CY/Index.aspx");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (this.Context.User != null)
            {
                if (this.Context.User.Identity.IsAuthenticated)
                {
                    if (this.Context.User.Identity is FormsIdentity)
                    {
                        string strUser = ((FormsIdentity)this.Context.User.Identity).Ticket.UserData;
                        string[] roles = SeriaFunc.DnSerializeFun(strUser).Roles.Split(',');
                        this.Context.User = new GenericPrincipal(this.Context.User.Identity, roles);
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
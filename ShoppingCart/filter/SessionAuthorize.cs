using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace ShoppingCart.filter
{
    public class SessionAuthorize: ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext c)
        {
            if(string.IsNullOrEmpty((string) c.HttpContext.Session["user"]))
                c.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
        }
      
    }
}
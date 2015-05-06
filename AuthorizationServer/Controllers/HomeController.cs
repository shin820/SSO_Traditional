using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AuthorizationServer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Authorize(string redirect_uri, string client_id)
        {
            return GrantCode(client_id, redirect_uri);
        }

        public ActionResult GrantCode(string client_id, string redirect_uri)
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + Request.ApplicationPath + "/authorize?" +
                         string.Format("client_id={0}&response_type=code&redirect_uri={1}", client_id,
                             redirect_uri);

            //Server.Transfer(url);
            return Redirect(url);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Infrastructure;

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

        public ActionResult SimpleAuthorize(string redirect_uri, string client_id)
        {
            string url = redirect_uri +
                         "?Id=14906&Name=cahsms&PropertyId=63018&Phone=312-606-3876&Email=test@sms-assist.com&FirstName=FirstName&LastName=LastName";
            return Redirect(url);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeAPI.Controllers
{
    public class AuthorizeController : Controller
    {
        // GET: Authorize
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
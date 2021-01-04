using CRS.WEB.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.ActiveRecord;

namespace CRS.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string Account, string Password)
        {
            ViewBag.PrivilegeList = AppHelper.Privileges;
            return View();
        }

    }
}
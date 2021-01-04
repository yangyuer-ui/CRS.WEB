using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using CRS.Domain;
using CRS.Service;
using CRS.Core;
using NHibernate.Criterion;
using CRS.WEB.Apps;


namespace CRS.WEB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        // GET: Login
        [HttpGet]
        public ActionResult LoginIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginIndex(string Account, string Password)
        {
        
            IList<ICriterion> listQuery = new List<ICriterion>()
            { Expression.Eq("Account",Account),
            Expression.Eq("PassWord",Password),
            Expression.Eq("Status",1)};
            SysUser user = Container.Instance.Resolve<ISysUserService>().Query(listQuery).FirstOrDefault();

            if (user != null)
            {
                AppHelper.LoginedUser = user;
                return Redirect("/Home/index?account");
            }
            else
            {
                Response.Write("<script>alert('用户名或密码错误');</script>");
                return View();
            }
        }
    }
}
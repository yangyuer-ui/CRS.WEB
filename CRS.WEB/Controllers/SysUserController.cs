using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Domain;
using CRS.Service;
using CRS.Core;
using NHibernate.Criterion;

namespace CRS.WEB.Controllers
{
    public class SysUserController : Controller
    {
        // GET: SysUser
        // GET: SysUser
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UserData()
        {
            IList<SysUser> userData = Container.Instance.Resolve<ISysUserService>().GetAll();
            var data = userData.Select(r =>
              new
              {
                  ID = r.ID,
                  Name = r.Name,
                  Account = r.Account,
                  Status = r.Status,
              });
            var users = new
            {
                code = 0,
                msg = "",
                count = userData.Count,
                data = data,
            };
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult CreateUser()
        {
            //绑定角色
            IList<ICriterion> queryrole = new List<ICriterion>();
            queryrole.Add(Expression.IsNotNull("Status"));
            ViewBag.Role = Container.Instance.Resolve<ISysRoleService>().Find(queryrole).OrderBy(m => m.ID).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(string Account, string UserName, string PassWord, string Status, string roleids)
        {
            //绑定角色
            IList<ICriterion> queryrole = new List<ICriterion>();
            queryrole.Add(Expression.IsNotNull("Status"));
            ViewBag.Role = Container.Instance.Resolve<ISysRoleService>().Find(queryrole).OrderBy(m => m.ID).ToList();


            SysUser user = new SysUser();
            user.Name = UserName;
            user.Account = Account;
            user.PassWord = PassWord;
            if (Status == null)
            {
                user.Status = 0;
            }
            else
            {
                user.Status = int.Parse(Status);
            }
            user.SysRoleList = new List<SysRole>();
            SysRole role = new SysRole();


            roleids = roleids.Substring(0, roleids.LastIndexOf(","));
            string[] strArray = roleids.Split(',');
            foreach (string roleid in strArray)
            {
                int newroleid = int.Parse(roleid.ToString());
                role = Container.Instance.Resolve<ISysRoleService>().GetEntity(newroleid);
                user.SysRoleList.Add(role);
            }

            Container.Instance.Resolve<ISysUserService>().Add(user);
            return View();
        }


        //删除操作
        public void Delete(int id)
        {
            Container.Instance.Resolve<ISysUserService>().Delete(id);
        }

        #region 编辑
        public void DellStr(SysUser user)
        {
            string roleids = "";
            if (user.SysRoleList == null)
            {
                user.SysRoleList = new List<SysRole>();
            }
            else if (user.SysRoleList != null)
            {

                foreach (SysRole role in user.SysRoleList)
                {
                    roleids = roleids + role.ID.ToString() + ",";
                }
            }
            ViewBag.roleids = roleids;
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SysUser user = Container.Instance.Resolve<ISysUserService>().GetEntity(id);
            //绑定角色
            IList<ICriterion> queryrole = new List<ICriterion>();
            queryrole.Add(Expression.IsNotNull("Status"));
            ViewBag.Role = Container.Instance.Resolve<ISysRoleService>().Find(queryrole).OrderBy(m => m.ID).ToList();
            //初始化 处理CheckBox中字符串
            DellStr(user);

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(string Account, string UserName, string Status, int dept, string roleids, int id)
        {
            SysUser user = Container.Instance.Resolve<ISysUserService>().GetEntity(id);
            //绑定角色
            IList<ICriterion> queryrole = new List<ICriterion>();
            queryrole.Add(Expression.IsNotNull("Status"));
            ViewBag.Role = Container.Instance.Resolve<ISysRoleService>().Find(queryrole).OrderBy(m => m.ID).ToList();
            user.Name = UserName;
            user.Account = Account;

            if (Status == null)
            {
                user.Status = 0;
            }
            else
            {
                user.Status = int.Parse(Status);
            }

        
            user.SysRoleList = new List<SysRole>();
            SysRole role = new SysRole();
            roleids = roleids.Substring(0, roleids.LastIndexOf(","));
            string[] strArray = roleids.Split(',');
            foreach (string roleid in strArray)
            {
                int newroleid = int.Parse(roleid.ToString());
                role = Container.Instance.Resolve<ISysRoleService>().GetEntity(newroleid);
                user.SysRoleList.Add(role);
            }
            Container.Instance.Resolve<ISysUserService>().Update(user);

            DellStr(user);

            return View(user);
        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Domain;
using CRS.Service;
using CRS.Core;
using NHibernate.Criterion;
using CRS.WEB.Apps;
using System.Text;

namespace CRS.WEB.Controllers
{
    public class StationController : Controller
    {
        // GET: Station
        public ActionResult Index()
        {
            IList<PathInfo> pathData = Container.Instance.Resolve<IPathInfoService>().GetAll().Where(m => m.Status is 1).ToList();
            ViewBag.pathData = pathData;
            IList<StationInfo> stationData = Container.Instance.Resolve<IStationInfoService>().GetAll();
            ViewBag.stationData = stationData;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            StationInfo staion = Container.Instance.Resolve<IStationInfoService>().GetEntity(id);
            ViewBag.id = id;
            return View(staion);
        }
        [HttpPost]
        public ActionResult Edit(int stationId, string stationName,int stationCode,string status)
        {
            StationInfo staion = Container.Instance.Resolve<IStationInfoService>().GetEntity(stationId);
            staion.StationName = stationName;
            staion.StationCode = stationCode;
            if (status == null)
            {
                staion.Status = 0;
            }
            else
            {
                staion.Status = int.Parse(status);
            }
            Core.Container.Instance.Resolve<IStationInfoService>().Update(staion);
            return View(staion);
        }

        [HttpPost]
        public ActionResult CreateUser(string stationName, int stationCode, string status)
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


    }
}
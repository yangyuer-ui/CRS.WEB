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

namespace CRS.WEB.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task

        #region  发布任务      
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TaskData()
        {
            IList<TaskInfo> taskData = Container.Instance.Resolve<ITaskInfoService>().GetAll();
            var data = taskData.Select(r =>
              new
              {
                  ID = r.ID,
                  Car = r.Car.CarNumber,
                  Driver = r.Driver.Name,
                  Path = r.Path.PathName,
                  ResidueSeating = r.ResidueSeating,
                  StartTime = r.StartTime.ToString(),
                  Status = IsTask() == 0 ? "已发车" : "未发车",
              });
            var tasks = new
            {
                code = 0,
                msg = "",
                count = taskData.Count,
                data = data,
            };
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        //验证是否发车
        public int IsTask()
        {
            if (Container.Instance.Resolve<ITaskInfoService>().GetAll().ToList().Exists(r => r.StartTime.AddDays(1) == DateTime.Now))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            //获取司机
            IList<SysUser> driver = new List<SysUser>();
            IList<SysUser> lstuser = Container.Instance.Resolve<ISysUserService>().GetAll().Where(u => u.Status == 1).ToList();
            foreach (var s in lstuser)
            {
                bool IsTeacher = false;
                foreach (SysRole r in s.SysRoleList)
                {
                    if (r.Name == "司机")
                    {
                        IsTeacher = true;
                        driver.Add(s);
                        break;
                    }
                }
                if (IsTeacher)
                {
                    ViewBag.driver = driver;
                }
            }
            //获取所有正常班车
            IList<ICriterion> querycar = new List<ICriterion>();
            querycar.Add(Expression.IsNotNull("Status"));
            ViewBag.Car = Container.Instance.Resolve<ICarInfoService>().Find(querycar).OrderBy(m => m.ID).ToList();

            //获取所有正常路线
            IList<ICriterion> querypath = new List<ICriterion>();
            querypath.Add(Expression.IsNotNull("Status"));
            ViewBag.Path = Container.Instance.Resolve<IPathInfoService>().Find(querypath).OrderBy(m => m.ID).ToList();
        
            return View();
        }

        [HttpPost]
        public ActionResult Create(int carid, int pathid, int userid, string status, DateTime startdate)
        {

            //获取司机
        
            TaskInfo task = new TaskInfo();
            if (status == null)
            {
                task.Status = 0;
            }
            else
            {
                task.Status = int.Parse(status);
            }
            //班车
            CarInfo car = new CarInfo();
            car = Container.Instance.Resolve<ICarInfoService>().GetEntity(carid);
            task.Car = car;

            //教师
            PathInfo path = new PathInfo();
            path = Container.Instance.Resolve<IPathInfoService>().GetEntity(pathid);
            task.Path = path;

            SysUser driver = new SysUser();
            driver = Container.Instance.Resolve<ISysUserService>().GetEntity(userid);
            task.Driver = driver;
            task.StartTime = startdate;
            task.ResidueSeating = task.Car.TotalSeating;

            Container.Instance.Resolve<ITaskInfoService>().Add(task);
            return View();
        }
        #endregion

        #region 查询任务
        public ActionResult FindTaskIndex()
        {
            return View();
        }

        public JsonResult FindTaskData()
        {
            SysUser user = AppHelper.LoginedUser;
            //记录当前用户
            string  userName = user.Name;
            IList<TaskInfo> taskData = Container.Instance.Resolve<ITaskInfoService>().GetAll().Where(u => u.StartTime>=DateTime.Now&&u.Driver.Name== userName).ToList();
            var data = taskData.Select(r =>
              new
              {
                  ID = r.ID,
                  Car = r.Car.CarNumber,
                  Driver = r.Driver.Name,
                  Path = r.Path.PathName,
                  ResidueSeating = r.ResidueSeating,
                  StartTime = r.StartTime.ToString()
              });
            var tasks = new
            {
                code = 0,
                msg = "",
                count = taskData.Count,
                data = data,
            };
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 历史发车
        public ActionResult HistroyTask()
        {
            return View();
        }
        public JsonResult HistroyTaskData()
        {
            SysUser user = AppHelper.LoginedUser;
            //记录当前用户
            string userName = user.Name;
            IList<TaskInfo> taskData = Container.Instance.Resolve<ITaskInfoService>().GetAll().Where(u => u.StartTime < DateTime.Now && u.Driver.Name == userName).ToList();
            var data = taskData.Select(r =>
              new
              {
                  ID = r.ID,
                  Car = r.Car.CarNumber,
                  Driver = r.Driver.Name,
                  Path = r.Path.PathName,
                  ResidueSeating = r.ResidueSeating,
                  StartTime = r.StartTime.ToString()
              });
            var tasks = new
            {
                code = 0,
                msg = "",
                count = taskData.Count,
                data = data,
            };
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
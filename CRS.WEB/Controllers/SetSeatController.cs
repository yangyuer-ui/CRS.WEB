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
    public class SetSeatController : Controller
    {
        // GET: SetSeat
        public ActionResult Index()
        {
            SysUser user = AppHelper.LoginedUser;
            //记录当前用户
            ViewBag.UserName = user.Name;

            return View();
        }
        public JsonResult TaskData()
        {
            IList<TaskInfo> taskData = Container.Instance.Resolve<ITaskInfoService>().GetAll().Where(r=>r.StartTime>=DateTime.Now.ToLocalTime()).ToList();
            var data = taskData.Select(r =>
              new
              {
                  ID = r.ID,
                  Car = r.Car.CarNumber,
                  Driver = r.Driver.Name,
                  Path = r.Path.PathName,
                  ResidueSeating = r.ResidueSeating,
                  StartTime = r.StartTime.ToString(),
                  Status = r.Status
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

        [HttpGet]
        public ActionResult SetSeat(int id)
        {
            //获取当前任务的车辆信息的座次
            IList<ICriterion> querycar = new List<ICriterion>();
            querycar.Add(Expression.IsNotNull("Status"));
            int seatCount= Container.Instance.Resolve<ITaskInfoService>().GetEntity(id).Car.TotalSeating;
            ViewBag.Seat = seatCount;
            List<int> all = new List<int>();

            IList<ICriterion> qry= new List<ICriterion>();
            qry.Add(Expression.Eq("Task.ID", id))
;           IList<SetSeatInfo> allOrder= Container.Instance.Resolve<ISetSeatInfoService>().Find(qry);
            List<int> carNumberList = (from m in allOrder
                                select m.SeatNumber).ToList();
            for (int i = 1; i <= seatCount; i++)
            {
                if (!carNumberList.Contains(i))
                {
                    all.Add(i);
                }
            }
            ViewBag.all = all;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
         public ActionResult SetSeat(int carNumber, int taskId)
        {
            SysUser passenger = AppHelper.LoginedUser;
            //记录乘客
            SysUser pas = new SysUser();
            pas= Container.Instance.Resolve<ISysUserService>().GetEntity(passenger.ID);
            SetSeatInfo setSeat = new SetSeatInfo();
            setSeat.Passenger = pas;

            //任务ID
            TaskInfo task = new TaskInfo();
            task = Container.Instance.Resolve<ITaskInfoService>().GetEntity(taskId);
            setSeat.Task = task;
            //座位号
            setSeat.SeatNumber = carNumber;
            //订座时间
            setSeat.SeatTime = DateTime.Now;
            //上车时间等于发车时间
            setSeat.RidingTime =task.StartTime;
            Container.Instance.Resolve<ISetSeatInfoService>().Add(setSeat);

            TaskInfo taskInfo = Container.Instance.Resolve<ITaskInfoService>().GetEntity(taskId);
            int ResidueSeating = taskInfo.ResidueSeating-1;
            taskInfo.ResidueSeating = ResidueSeating;
            Container.Instance.Resolve<ITaskInfoService>().Update(taskInfo);

            return View();
        }

        #region 查看定座
        public JsonResult SetSeatData()
        {
            IList<SetSeatInfo> setseatData = Container.Instance.Resolve<ISetSeatInfoService>().GetAll();
            var data = setseatData.Select(r =>
              new
              {
                  ID = r.ID,
                  Passenger = r.Passenger.Name,
                  RidingStatus = r.RidingStatus,
                  RidingTime = r.RidingTime,
                  SeatNumber = r.SeatNumber,
                  SeatTime = r.SeatTime.ToString(),
                  PathName = r.Task.Path.PathName,
              });
            var setseat = new
            {
                code = 0,
                msg = "",
                count = setseatData.Count,
                data = data,
            };
            return Json(setseat, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetSeatIndex()
        {
            return View();
        }

        public ActionResult SetSeatDetail(int id)
        {
            SetSeatInfo setseat = Container.Instance.Resolve<ISetSeatInfoService>().GetEntity(id);
            return View(setseat);
        }
        #endregion

    }
}
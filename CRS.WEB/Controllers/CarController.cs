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
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult CarData()
        {
            IList<CarInfo> carData = Container.Instance.Resolve<ICarInfoService>().GetAll();
            var data = carData.Select(r =>
              new
              {
                  ID = r.ID,
                  CarName = r.CarName,
                  CarNumber = r.CarNumber,
                  CarType = r.CarType,
                  TotalSeating = r.TotalSeating,
                  Status = r.Status
              });
            var users = new
            {
                code = 0,
                msg = "",
                count = carData.Count,
                data = data,
            };
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string carName, string carNumber, string carType, int TotalSeating, string  status)
        {
            CarInfo car = new CarInfo();
            car.TotalSeating = TotalSeating;
            car.CarName = carName;
            car.CarNumber = carNumber;
            car.CarType = carType;
            if (status == null)
            {
                car.Status = 0;
            }
            else
            {
                car.Status = int.Parse(status);
            }
            Container.Instance.Resolve<ICarInfoService>().Add(car);
            return View();
        }


        //删除操作
        public void Delete(int id)
        {
            Container.Instance.Resolve<ISysUserService>().Delete(id);
        }

        #region 编辑

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CarInfo car = Container.Instance.Resolve<ICarInfoService>().GetEntity(id);
            //初始化 处理CheckBox中字符串
            return View(car);
        }
        [HttpPost]
        public ActionResult Edit(string carName, string carNumber, string carType, int TotalSeating, string status, int id)
        {
            CarInfo car = Container.Instance.Resolve<ICarInfoService>().GetEntity(id);
            car.TotalSeating = TotalSeating;
            car.CarName = carName;
            car.CarNumber = carNumber;
            car.CarType = carType;
            if (status==null)
            { 
                car.Status = 0;
            }
            else
            {
                car.Status = int.Parse(status);
            }
            Container.Instance.Resolve<ICarInfoService>().Update(car);
            return View(car);
        }
        #endregion



    }
}
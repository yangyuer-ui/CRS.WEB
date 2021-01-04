using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Domain;
using CRS.Service;
using CRS.Core;

namespace CRS.WEB.Controllers
{
    public class PathController : Controller
    {
        // GET: Path
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult PathData()
        {
            IList<PathInfo> pathData = Container.Instance.Resolve<IPathInfoService>().GetAll();
            var data = pathData.Select(r =>
              new
              {
                  ID = r.ID,
                  PathName = r.PathName,
                  EndPlace = r.EndPlace,
                  StartPlace = r.StartPlace,
                  Status = r.Status
              });
            var users = new
            {
                code = 0,
                msg = "",
                count = pathData.Count,
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
        public ActionResult Create(string pathName, string endPlace, string startPlace, string status)
        {
            PathInfo path = new PathInfo();
            path.PathName = pathName;
            path.EndPlace = endPlace;
            path.StartPlace = startPlace;
            if (status == null)
            {
                path.Status = 0;
            }
            else
            {
                path.Status = int.Parse(status);
            }
            Container.Instance.Resolve<IPathInfoService>().Add(path);
            return View();
        }


        //删除操作
        public void Delete(int id)
        {
            Container.Instance.Resolve<IPathInfoService>().Delete(id);
        }

        #region 编辑

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PathInfo car = Container.Instance.Resolve<IPathInfoService>().GetEntity(id);
            //初始化 处理CheckBox中字符串
            return View(car);
        }
        [HttpPost]
        public ActionResult Edit(string pathName, string endPlace, string startPlace, string status, int id)
        {
            PathInfo path = Container.Instance.Resolve<IPathInfoService>().GetEntity(id);
            path.PathName = pathName;
            path.EndPlace = endPlace;
            path.StartPlace = startPlace;
            if (status == null)
            {
                path.Status = 0;
            }
            else
            {
                path.Status = int.Parse(status);
            }
            Container.Instance.Resolve<IPathInfoService>().Update(path);
            return View(path);
        }
        #endregion

    }
}
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
        public ActionResult Create(int pathId,string stationName, int stationCode, string status)
        {
            StationInfo station = new StationInfo();
            station.StationName = stationName;
            station.StationCode = stationCode;
            if (status == null)
            {
                station.Status = 0;
            }
            else
            {
                station.Status = int.Parse(status);
            }
            PathInfo path = new PathInfo();
            path = Container.Instance.Resolve<IPathInfoService>().GetEntity(pathId);
            station.Path = path;
            Container.Instance.Resolve<IStationInfoService>().Add(station);
            return View();
        }

        public void Delete(int stationId)
        {
            Container.Instance.Resolve<IStationInfoService>().Delete(stationId);
        }

    }
}
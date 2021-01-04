using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRS.Domain;
using CRS.Manager;
using CRS.Service;
using NHibernate.Criterion;

namespace CRS.Component
{
    public class StationComponent : BaseComponent<StationInfo, StationInfoManager>, IStationInfoService
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CRS.Domain
{
    [ActiveRecord("StationInfo")]
    public  class StationInfo : BaseEntity<StationInfo>
    {
        [Display(Name = "站点名")]
        [Property]
        public string StationName { get; set; }

        [BelongsTo(Column = "PathID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        [Display(Name = "路线ID")]
        public PathInfo  Path{ get; set; }

        [Display(Name = "站点码")]
        [Property]
        public int StationCode { get; set; }

        [Display(Name = "状态")]
        [Property]
        public int Status
        { get; set; }
    }
}

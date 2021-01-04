using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CRS.Domain
{
    [ActiveRecord("TaskInfo")]
    public  class TaskInfo : BaseEntity<TaskInfo>
    {
        [Display(Name = "出发时间")]
        [Property]
        public DateTime StartTime { get; set; }


        [BelongsTo(Column = "CarID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        [Display(Name = "班车ID")]
        public CarInfo Car { get; set; }

        [Display(Name = "发车司机")]
        [BelongsTo(Column = "DriverID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        public SysUser Driver { get; set; }

        [BelongsTo(Column = "PathID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        [Display(Name = "路线ID")]
        public PathInfo Path { get; set; }

        [Display(Name = "剩余座次")]
        [Property]
        public int ResidueSeating { get; set; }

        [Display(Name = "状态")]
        [Property]
        public int Status { get; set; }
    }
}

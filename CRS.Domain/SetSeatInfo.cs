using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;
using System.Threading.Tasks;

namespace CRS.Domain
{
    [ActiveRecord("SetSeatInfo")]
    public class SetSeatInfo : BaseEntity<SetSeatInfo>
    {
        [BelongsTo(Column = "PassengerID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        [Display(Name = "乘客ID")]
        public SysUser Passenger { get; set; }


        [BelongsTo(Column = "TaskID", Cascade = CascadeEnum.None, Lazy = FetchWhen.Immediate)]
        [Display(Name = "任务ID")]
        public TaskInfo Task { get; set; }


        [Display(Name = "座号")]
        [Property]
        public int SeatNumber { get; set; }

        [Display(Name = "订座状态")]
        [Property]
        public bool RidingStatus
        { get; set; }

        [Display(Name = "到达时间")]
        [Property]
        public DateTime RidingTime { get; set; }

        [Display(Name = "订座时间")]
        [Property]
        public DateTime SeatTime { get; set; }
    }
}

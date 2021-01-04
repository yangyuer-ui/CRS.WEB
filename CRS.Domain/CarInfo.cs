using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CRS.Domain
{
    [ActiveRecord("CarInfo")]
   public class CarInfo : BaseEntity<CarInfo>
    {
        [Display(Name = "班车名")]
        [Property(Length = 50, NotNull = true)]
        public string CarName { get; set; }

        [Display(Name = "班车号")]
        [Property(Length = 50, NotNull = true)]
        public string CarNumber { get; set; }

        [Display(Name = "车型")]
        [Property(Length = 50, NotNull = true)]
        public string CarType { get; set; }

        [Display(Name = "总座次")]
        [Property]
        public int TotalSeating{ get; set; }
        [Display(Name = "状态")]
        [Property]
        public int Status
        { get; set; }
    }
}

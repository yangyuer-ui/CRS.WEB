using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CRS.Domain
{
    [ActiveRecord("PathInfo")]
    public class PathInfo : BaseEntity<PathInfo>
    {
        [Display(Name = "路线名")]
        [Property(Length = 50, NotNull = true)]
        public string PathName { get; set; }

        [Display(Name = "出发地")]
        [Property(Length = 50, NotNull = true)]
        public string StartPlace { get; set; }

        [Display(Name = "目的地")]
        [Property(Length = 50, NotNull = true)]
        public string EndPlace { get; set; }

        [Display(Name = "状态")]
        [Property]
        public int Status
        { get; set; }
    }
}

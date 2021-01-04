using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Castle.ActiveRecord;

namespace CRS.Domain
{
    /// <summary>
    /// 实体类：系统模块
    /// </summary>
    [ActiveRecord("SysMenu")]
    public class SysMenu : BaseEntity<SysMenu>
    {
        /// <summary>
        /// 模块名
        /// </summary>
        [Display(Name = "模块名")]
        [Property(Length = 50, NotNull = true)]
        public string Name { get; set; }

        /// <summary>
        /// 控制器名
        /// </summary>
        [Display(Name = "控制器名")]
        [Property(Length = 30)]
        public string ControllerName { get; set; }

        /// <summary>
        /// Action名
        /// </summary>
        [Display(Name = "Action名")]
        [Property(Length = 30)]
        public string ActionName { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        [Display(Name = "排序码")]
        [Property(NotNull = true, Default = "10")]
        [Required(ErrorMessage = "不能为空")]
        public int SortCode { get; set; }

        /// <summary>
        /// 是否在菜单中显示
        /// </summary>
        [Display(Name = "是否显示")]
        [Property(NotNull = true)]
        public bool Status
        { get; set; }

        /// <summary>
        /// 上级模块
        /// </summary>
        [BelongsTo(Type = typeof(SysMenu), Column = "ParentId", Lazy = FetchWhen.OnInvoke)]
        [Display(Name = "上级模块")]
        public SysMenu ParentID { get; set; }

        /// <summary>
        /// 有权限的角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysRole_SysMenu", ColumnKey = "MenuID", ColumnRef = "RoleID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "有权限的角色")]
        public IList<SysRole> SysRoleList { get; set; }
    }
}

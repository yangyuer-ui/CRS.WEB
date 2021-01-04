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
    /// 实体类：用户
    /// </summary>
    [ActiveRecord("SysRole")]
    public class SysRole : BaseEntity<SysRole>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [Property(Length = 30, NotNull = true)]
        [Display(Name = "角色名")]
        public string Name { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "角色状态")]
        public int Status { get; set; }

        /// <summary>
        /// 所拥有用户
        /// </summary>
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUser_SysRole", ColumnRef = "UserID", ColumnKey = "RoleID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "所拥有用户")]
        public IList<SysUser> SysUserList { get; set; }

        /// <summary>
        /// 拥有的权限
        /// </summary>
        [HasAndBelongsToMany(typeof(SysMenu), Table = "SysRole_SysMenu", ColumnKey = "RoleID", ColumnRef = "MenuID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "拥有的权限")]
        public IList<SysMenu> SysMenuList { get; set; }
    }
}

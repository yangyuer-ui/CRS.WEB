using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CRS.Domain
{
    [ActiveRecord("SysUser")]
    public class SysUser : BaseEntity<SysUser>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Property(Length = 30, NotNull = true)]
        public string Name { get; set; }

        /// <summary>
        /// 登录账户
        /// </summary>
        [Display(Name = "登录账户")]
        [Property(Length = 50, NotNull = true)]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [Property(Length = 50, NotNull = true)]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户状态
        ///     1：正常
        ///     0：禁用
        /// </summary>
        [Display(Name = "用户状态")]
        [Property(NotNull = true)]
        public int Status { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysUser_SysRole", ColumnKey = "UserID", ColumnRef = "RoleID", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "所属角色")]
        public IList<SysRole> SysRoleList { get; set; }

    }
}

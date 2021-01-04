using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRS.Domain;

namespace CRS.Service
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">操作员输入的用户名</param>
        /// <param name="password">操作员输入的密码</param>
        /// <returns>当用户名和密码成功匹配时返回匹配的用户信息，否则返回null</returns>
        SysUser Login(string account, string password);

    }
}

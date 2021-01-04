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
    public class SysUserComponent : BaseComponent<SysUser, SysUserManager>, ISysUserService
    {
        public SysUser Login(string account, string password)
        {
            IList<ICriterion> criterionList = new List<ICriterion>();
            criterionList.Add(Expression.Eq("Account", account));
            criterionList.Add(Expression.Eq("PassWord", password));

            SysUser user = manager.Query(criterionList).FirstOrDefault();
            return user;
        }
    }
}

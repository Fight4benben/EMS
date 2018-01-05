using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IUserContext
    {
        /// <summary>
        /// 匹配用户名与密码
        /// </summary>
        /// <param name="userId">传入用户名</param>
        /// <param name="password">传入密码</param>
        /// <returns>匹配成功返回true，失败返回false</returns>
        bool MatchUser(string userName, string password);
    }
}

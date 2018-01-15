using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;

namespace EMS.DAL.RepositoryImp
{
    public class UserContext : IUserContext
    {
        private readonly EnergyDB _db = new EnergyDB();
        public bool MatchUser(string userName, string password)
        {
            //当密码为空的情况下，默认转化为空字符串
            if (string.IsNullOrEmpty(password))
                password = "";

            bool result = false;
            byte[] raw = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] computedPass = md5.ComputeHash(raw);

            string mdHash = BitConverter.ToString(computedPass).Replace("-","").ToLower();

            int count = _db.User.Where(user => user.UserName == userName && user.Password == mdHash).Count();

            if (count == 1)
                result = true;

            return result;
        }
    }
}

using EMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IUserBuildingDbContext
    {
        List<User> GetUsers();
        List<UserBuilding> GetUserBuildings(string userName);
        int AddBuild(string userName,string buildId);
        int DeleteBuild(string userName, string buildId);
    }
}

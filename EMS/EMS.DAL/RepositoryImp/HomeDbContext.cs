using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.StaticResources;
using EMS.DAL.ViewModels;

namespace EMS.DAL.RepositoryImpl
{
    public class HomeDbContext:IHomeDbContext
    {
        private readonly EnergyDB _db = new EnergyDB();

        /// <summary>
        /// 根据建筑ID获取当前建筑信息
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <returns>BuildInfo实体类</returns>
        public BuildInfo GetBuildById(string buildId)
        {
            return _db.BuildInfo.Find(buildId);
        }

        /// <summary>
        /// 获取所有建筑物
        /// </summary>
        /// <returns></returns>
        public IQueryable<BuildInfo> GetBuilds()
        {
            return _db.BuildInfo;
        }

        /// <summary>
        /// 根据当前登录用户获取建筑列表
        /// </summary>
        /// <param name="userName">登陆用户名</param>
        /// <returns>建筑列表</returns>
        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(HomeResources.BuildListSQL,new SqlParameter("@UserName",userName)).ToList();
        }

        /// <summary>
        /// 根据建筑ID和日期，返回分类的当月与当年能耗数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">日期</param>
        /// <returns>List<EnergyClassify></returns>
        public List<EnergyClassify> GetEnergyClassifyValues(string buildId, string date)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EndDate",date)
            };
            return _db.Database.SqlQuery<EnergyClassify>(HomeResources.EnergyClassifySQL, sqlParameters).ToList();
        }

        public EnergyItemDict GetEnergyItemByCode(string energyItemCode)
        {
            return _db.EnergyItemDict.Find(energyItemCode); 
        }

        /// <summary>
        /// 返回当月分项数据列表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">日期</param>
        /// <returns>List<EnergyItem></returns>
        public List<EnergyItem> GetEnergyItemValues(string buildId, string date)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EndDate",date)
            };

            return _db.Database.SqlQuery<EnergyItem>(HomeResources.EnergyItemSQL, sqlParameters).ToList() ;
        }

        /// <summary>
        /// 返回小时用能数据列表
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="date">日期时间精确到小时</param>
        /// <returns>List<HourValue></returns>
        public List<HourValue> GetHourValues(string buildId, string date)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@BuildId",buildId),
                new SqlParameter("@EndDate",date)
            };

            return _db.Database.SqlQuery<HourValue>(HomeResources.HourValueSQL,sqlParameters).ToList();
        }
    }
}

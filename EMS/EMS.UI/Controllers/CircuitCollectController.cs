using EMS.DAL.RepositoryImp;
using EMS.DAL.Services;
using EMS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
namespace EMS.UI.Controllers
{
    public class CircuitCollectController : ApiController
    {
        CircuitCollectService service = new CircuitCollectService();

        /// <summary>
        /// 数据集抄
        /// 初始加载：获取用户名查询建筑列表，第一栋建筑对应的分类，
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回完整的数据：包含建筑列表，能源按钮列表，回路列表</returns>
        public object Get()
        {
            try
            {
                string userName = User.Identity.Name;
                return service.GetViewModelByUserName(userName);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 数据集抄
        /// 根据用户传入的建筑ID，查找该建筑的能源按钮列表，第一个分类能耗回路列表，
        /// </summary>
        /// <param name="buildId"></param>
        /// <returns>该建筑的能源按钮列表，第一个分类能耗回路列表</returns>
        public object Get(string buildId)
        {
            try
            {
                return service.GetViewModel(buildId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户传入的建筑ID和能耗分类，查找该建筑第一个分类对应的全部回路当日0点0分至查询当时的集抄数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗</param>
        /// <returns>返回数据：回路列表</returns>
        public object Get(string buildId, string energyCode)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 根据用户传入的建筑ID、分类能耗，支路IDs，起始时间，结束时间；查找对应的集抄数据
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">能耗分类</param>
        /// <param name="circuitIDs">支路ID</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>返回数据：指定的集抄数据</returns>
        public object Get(string buildId, string energyCode, string circuitIDs, string startDate, string endDate)
        {
            try
            {
                string[] ids = circuitIDs.Split(',');
                return service.GetViewModel(buildId, energyCode, ids, startDate, endDate);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
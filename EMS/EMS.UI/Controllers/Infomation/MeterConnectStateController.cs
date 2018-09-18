using EMS.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class MeterConnectStateController : ApiController
    {
        MeterConnectStateService service = new MeterConnectStateService();

        /// <summary>
        /// 默认获取 建筑ID，分类能耗，所有仪表状态
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>建筑ID，分类能耗，第一栋建筑的所有仪表状态</returns>
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

        public object Get(string buildId,string energyCode)
        {
            try
            {
                return service.GetViewModel(buildId,energyCode);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 获取仪表通讯状态
        /// </summary>
        /// <param name="buildId">建筑ID</param>
        /// <param name="energyCode">分类能耗</param>
        /// <param name="type">"type"=0 在线 ；"type"=1 离线 </param>
        /// <returns>累计中断时间 "DiffDate"格式为 "0:00:04" 表示 为0天0小时4分钟</returns>
        public object Get(string buildId, string energyCode, string type)
        {
            try
            {
                return service.GetViewModel(buildId, energyCode, type);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
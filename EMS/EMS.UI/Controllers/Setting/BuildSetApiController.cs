using EMS.DAL.Entities;
using EMS.DAL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers
{
    [Authorize]
    public class BuildSetApiController : ApiController
    {
        BuildSetService service = new BuildSetService();
        /// <summary>
        /// 获取用能管理的建筑列表
        /// 初始加载：获取用户名查询建筑列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>包含建筑列表</returns>
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
        /// 获取建筑详细信息
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        public object Get(string buildID)
        {
            try
            {
                return service.GetViewModel(buildID);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 修改建筑信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Update([FromBody] JObject obj)
        {
            try
            {
                string buildID = obj["buildID"].ToString();
                string buildName = obj["buildName"].ToString();
                string buildAddr = obj["buildAddr"].ToString();
                decimal buildLong = Convert.ToDecimal(obj["buildLong"].ToString());
                decimal buildLat = Convert.ToDecimal(obj["buildLat"].ToString());

                decimal totalArea = Convert.ToDecimal(obj["totalArea"].ToString());
                int numberOfPeople = Convert.ToInt32(obj["numberOfPeople"].ToString());
                int transCount = Convert.ToInt32(obj["transCount"].ToString());
                //安装变压器容量，运行容量，监控仪表数量，联系电话
                int installCapacity = Convert.ToInt32(obj["installCapacity"].ToString());
                int operateCapacity = Convert.ToInt32(obj["operateCapacity"].ToString());
                int designMeters = Convert.ToInt32(obj["designMeters"].ToString());

                return service.UpdatePartBuildInfo(buildID, buildName, buildAddr,
                    buildLong, buildLat, totalArea, numberOfPeople,
                    transCount, installCapacity, operateCapacity, designMeters);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 修改建筑信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut]
        public object Updata([FromBody] JObject obj)
        {
            try
            {
                BuildInfoSet inputBuildInfoSet = new BuildInfoSet();

                inputBuildInfoSet.BuildID = obj["BuildID"].ToString();
                inputBuildInfoSet.BuildName = obj["BuildName"].ToString();
                inputBuildInfoSet.AliasName = obj["AliasName"].ToString();
                inputBuildInfoSet.BuildOwner = obj["BuildOwner"].ToString();
                inputBuildInfoSet.BuildAddr = obj["BuildAddr"].ToString();
                inputBuildInfoSet.BuildLong = Convert.ToDecimal(obj["BuildLong"].ToString());

                inputBuildInfoSet.BuildLat = Convert.ToDecimal(obj["BuildLat"].ToString());
                inputBuildInfoSet.UpFloor = Convert.ToInt32(obj["UpFloor"].ToString());
                inputBuildInfoSet.DownFloor = Convert.ToInt32(obj["DownFloor"].ToString());
                inputBuildInfoSet.TotalArea = Convert.ToDecimal(obj["TotalArea"].ToString());
                inputBuildInfoSet.AirArea = Convert.ToDecimal(obj["AirArea"].ToString());

                inputBuildInfoSet.DesignDept = obj["DesignDept"].ToString();
                inputBuildInfoSet.WorkDept = obj["WorkDept"].ToString();
                inputBuildInfoSet.CreateUser = User.Identity.Name;
                inputBuildInfoSet.NumberOfPeople = Convert.ToInt32(obj["NumberOfPeople"].ToString());
                inputBuildInfoSet.TransCount = Convert.ToInt32(obj["TransCount"].ToString());
                //安装变压器容量，运行容量，监控仪表数量，联系电话
                inputBuildInfoSet.InstallCapacity = Convert.ToInt32(obj["InstallCapacity"].ToString());
                inputBuildInfoSet.OperateCapacity = Convert.ToInt32(obj["OperateCapacity"].ToString());
                inputBuildInfoSet.DesignMeters = Convert.ToInt32(obj["DesignMeters"].ToString());
                inputBuildInfoSet.Mobiles = obj["Mobiles"].ToString();

                return service.UpdateBuild(inputBuildInfoSet);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }

        /// <summary>
        /// 删除建筑
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpDelete]
        public object Delete([FromBody] JObject obj)
        {
            try
            {
                string buildID = obj["buildID"].ToString();
                return service.DeleteBuild(buildID);
            }
            catch (Exception e)
            {
                return 9999;
            }
        }
    }
}
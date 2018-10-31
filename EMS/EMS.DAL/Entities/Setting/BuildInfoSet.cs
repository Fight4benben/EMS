using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class BuildInfoSet
    {
        public string BuildID { get; set; }
        public string DataCenterID { get; set; }
        public string BuildName { get; set; }
        public string AliasName { get; set; }
        public string BuildOwner { get; set; }
        public int State { get; set; }
        public string DistrictCode { get; set; }
        public string BuildAddr { get; set; }
        public decimal? BuildLong { get; set; }
        public decimal? BuildLat { get; set; }
        public int? BuildYear { get; set; }
        public int? UpFloor { get; set; }
        public int? DownFloor { get; set; }
        public string BuildFunc { get; set; }
        public decimal TotalArea { get; set; }
        public decimal AirArea { get; set; }
        public decimal HeatArea { get; set; }
        public string AirType { get; set; }
        public string HeatType { get; set; }
        public decimal BodyCoef { get; set; }
        public string StruType { get; set; }
        public string WallMatType { get; set; }
        public string WallWarmType { get; set; }
        public string WallWinType { get; set; }
        public string GlassType { get; set; }
        public string WinFrameType { get; set; }
        public int IsStandard { get; set; }
        public string DesignDept { get; set; }
        public string WorkDept { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? MonitorDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public int? NumberOfPeople { get; set; }
        public decimal? SPArea { get; set; }
        public string Image { get; set; }
        public int? TransCount { get; set; }
        public decimal? InstallCapacity { get; set; }
        public decimal? OperateCapacity { get; set; }
        public int? DesignMeters { get; set; }
        public int? Mobiles { get; set; }
        public int? ModelID { get; set; }
	
    }
}

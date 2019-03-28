using EMS.DAL.Entities;
using EMS.DAL.IRepository.Home;
using EMS.DAL.StaticResources;
using EMS.DAL.StaticResources.Home;
using EMS.DAL.ViewModels;
using EMS.DAL.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp.Home
{
    public class SvgDbContext : ISvgDbContext
    {
        private readonly EnergyDB _db = new EnergyDB();
        private readonly HistoryDB _hisdb = new HistoryDB();

        public List<BuildViewModel> GetBuildsByUserName(string userName)
        {
            return _db.Database.SqlQuery<BuildViewModel>(SharedResources.BuildListSQL, new SqlParameter("@UserName", userName)).ToList();
        }

        public SvgDataViewModel GetDataViewModel(string buildId, string svgId)
        {
            SvgBinding svgBinding = GetSvgBinding(svgId);

            if (svgBinding == null)
                return null;

            string[] meterArray = svgBinding.Meters.Split('|');
            string[] paramArray = svgBinding.ParamStrings.Split('|');


            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT F_MeterID MeterID
                             ,SUBSTRING(F_TagName,CHARINDEX('_',F_TagName)+1,LEN(F_TagName) -CHARINDEX('_',F_TagName)) ParamName
                             ,F_RecentData Value
                             FROM HistoryData
                             WHERE F_BuildID = @BuildId
                             AND F_MeterID in (");
            builder.Append("'" + string.Join("','", meterArray) + "')");
            builder.Append(" AND F_Year = YEAR(GETDATE())");
            if(paramArray.Length>0)
            {
                builder.Append("AND (");
                for (int i = 0; i < paramArray.Length; i++)
                {
                    if (i == 0)
                        builder.Append("(F_TagName like ('%" + paramArray[i] + "'))");
                    else
                        builder.Append("or (F_TagName like ('%" + paramArray[i] + "'))");
                }
                builder.Append(")");
            }

            string sql = builder.ToString();

            List<SvgTempValue> tempValues = _hisdb.Database.SqlQuery<SvgTempValue>(sql, new SqlParameter("@BuildId", buildId)).ToList();

            SvgDataViewModel viewModel = new SvgDataViewModel();
            viewModel.MeterValueList = new List<SvgMeterParam>();

            if (tempValues.Count > 0)
            {
                foreach (string item in meterArray)
                {
                    List<SvgTempValue> tempParams =  tempValues.FindAll(o => o.MeterId == item);

                    SvgMeterParam meterParam = new SvgMeterParam();
                    meterParam.MeterID = item;
                    meterParam.ParamList = new List<SvgParamValue>();
                    if (tempParams.Count > 0)
                    {
                        foreach (var paramValue in tempParams)
                        {
                            meterParam.ParamList.Add(new SvgParamValue()
                            {
                                Code = paramValue.ParamName,
                                Value = paramValue.Value
                            });
                        }  
                    }

                    viewModel.MeterValueList.Add(meterParam);
                }
            }

            return viewModel;
        }

        public List<SvgInfo> GetSvgListByBuildId(string buildId)
        {
            return _db.Database.SqlQuery<SvgInfo>(SvgResources.SvgListSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        public string GetSvgViewById(string svgId)
        {
            List<string> fileNames = _db.Database.SqlQuery<string>(SvgResources.SvgPathSQL, new SqlParameter("@SvgID", svgId)).ToList();

            if (fileNames.Count > 0)
                return fileNames[0];
            else
                return "not exist";
        }

        private SvgBinding GetSvgBinding(string svgId)
        {
            List<SvgBinding> list =  _db.Database.SqlQuery<SvgBinding>(SvgResources.SvgBindingSQL, new SqlParameter("@SvgID", svgId)).ToList();

            if (list.Count > 0)
                return list.First();

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.StaticResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class RegionDbContextTest
    {
        [TestMethod]
        public void TestRegionMainCompare()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionMainCompareValueList("000001G001","2018-07-13","01000","Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}",item.ID,item.Name,item.Time,item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainRank()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<RankValue> list =  context.GetRegionMainRankValueList("000001G001", "2018/07/13", "01000", "Demo");

            foreach (RankValue item in list)
            {
                Console.WriteLine("ClassifyID:{0};ClassifyName:{1};Name:{2};Value:{3}", item.ClassifyID, item.ClassifyName, item.Name, item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainPie()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionPieValueList("000001G001", "2018-07-13", "01000", "Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}", item.ID, item.Name, item.Time, item.Value);
            }
        }

        [TestMethod]
        public void TestRegionMainBarTrend()
        {
            IRegionMainDbContext context = new RegionMainDbContext();
            List<EMSValue> list = context.GetRegionStackValueList("000001G001", "2018-07-13", "01000", "Demo");

            foreach (EMSValue item in list)
            {
                Console.WriteLine("ID:{0};Name:{1};Time:{2};Value:{3}", item.ID, item.Name, item.Time, item.Value);
            }
        }

        [TestMethod]
        public void TestRegionReportBySqlReader()
        {
            EnergyDB _db = new EnergyDB();

            string connString = "data source=192.168.105.227,1433;initial catalog=EMS;persist security info=True;user id=sa;password=Acrel001";

            List<ReportValue> list = new List<ReportValue>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string sql = string.Format(RegionReportResources.YearReportSQL, @"'000001G0010002',
                                                                                    '000001G0010005',
                                                                                    '000001G0010006',
                                                                                    '000001G0010007',
                                                                                    '000001G0010008',
                                                                                    '000001G0010009',
                                                                                    '000001G0010010',
                                                                                    '000001G0010011',
                                                                                    '000001G0010012',
                                                                                    '000001G0010013',
                                                                                    '000001G0010014'");
                
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@BuildID","000001G001"),
                    new SqlParameter("@EnergyItemCode","01000"),
                    new SqlParameter("@EndTime","2018-01-01")
                };

                cmd.Parameters.AddRange(sqlParameters);

                SqlDataReader reader =  cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReportValue report = new ReportValue();
                    report.Id = reader["ID"].ToString();
                    report.Name = reader["Name"].ToString();
                    report.Time = Convert.ToDateTime(reader["Time"]);
                    report.Value = Convert.ToDecimal(reader["Value"]);

                    list.Add(report);
                }
            }

            Console.WriteLine(list.Count);
        }
    }
}

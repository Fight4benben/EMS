using System;
using System.Data.SqlClient;
using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.StaticResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class DepartmentDbTest
    {
        [TestMethod]
        public void TestGetReport()
        {
            EnergyDB _db = new EnergyDB();

            DepartmentReportDbContext context = new DepartmentReportDbContext();
            string[] depts = {
                 "D000001G001001",
                 "D000001G001002"
            };

            string  sql = string.Format(DepartmentReportResources.YearReportSQL, "'" + string.Join("','", depts) + "'");
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@EndTime";
            param.SqlDbType = SqlDbType.Date;
            param.Value = "2018-07-01";
            SqlParameter[] sqlParameters = {
                param,
                new SqlParameter("@EnergyItemCode","02000")
            };
            String connStr = _db.Database.Connection.ConnectionString;
            List<ReportValue> list = new List<ReportValue>();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.Add(new SqlParameter("@EndTime", "2018-07-01"));
                cmd.Parameters.Add(new SqlParameter("@EnergyItemCode", "02000"));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


                foreach (DataRow row in table.Rows)
                {
                    ReportValue report = new ReportValue();
                    report.Id = row["ID"].ToString();
                    report.Name = row["Name"].ToString();
                    report.Time = Convert.ToDateTime(row["Time"]);
                    report.Value = Convert.ToDecimal(row["Value"]);
                    list.Add(report);
                }
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }


            Console.WriteLine(list.Count);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

        }
    }
}

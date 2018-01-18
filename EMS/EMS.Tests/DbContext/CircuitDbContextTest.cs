using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.DAL.IRepository;
using System.Collections.Generic;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class CircuitDbContextTest
    {
        [TestMethod]
        public void TestGetCircuits()
        {
            ICircuitReportDbContext context = new CircuitReportDbContext();

            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode("000001G001","01000");

            foreach (var circuit in circuits)
            {
                Console.WriteLine("支路编号：{0}；支路名称：{1}；仪表编号：{2}；上级支路代码：{3}；",circuit.CircuitId,circuit.CircuitName,circuit.MeterId,circuit.ParentId);
            }

        }

        [TestMethod]
        public void TestGetEnergyItemDict()
        {
            ICircuitReportDbContext context = new CircuitReportDbContext();

            List<EnergyItemDict> items = context.GetEnergyItemDictByBuild("000001G001");

            foreach (var item in items)
            {
                Console.WriteLine("分类编号：{0}；分类名称：{1}；", item.EnergyItemCode,item.EnergyItemName);
            }

        }

        [TestMethod]
        public void TestGetCircuitHourValue()
        {
            ICircuitReportDbContext context = new CircuitReportDbContext();

            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode("000001G001", "01000");

            List<string> circuitList = new List<string>();

            foreach (var circuit in circuits)
            {
                circuitList.Add(circuit.CircuitId);
            }

            List<ReportValue> list = context.GetReportValueList(circuitList.ToArray(),"2018-01-16","DD");

            Console.WriteLine(list.Count);
        }

        [TestMethod]
        public void TestGetMonthReport()
        {
            ICircuitReportDbContext context = new CircuitReportDbContext();
            List<string> circuits = new List<string>();
            circuits.Add("000001G0010001");
            circuits.Add("000001G0010002");

            List<ReportValue> list = context.GetReportValueList(circuits.ToArray(),"2018-01-16" ,"MM");

            Console.WriteLine(list.Count);
        }

        [TestMethod]
        public void TestGetYearReport()
        {
            ICircuitReportDbContext context = new CircuitReportDbContext();
            List<string> circuits = new List<string>();
            circuits.Add("000001G0010001");
            circuits.Add("000001G0010002");

            List<ReportValue> list = context.GetReportValueList(circuits.ToArray(), "2018-01-16", "YY");

            Console.WriteLine(list.Count);
        }
    }
}

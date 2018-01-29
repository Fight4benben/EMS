using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class CircuitOverviewDbContextTest
    {
        /// <summary>
        /// 当日负荷曲线数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuitLoadValue()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuitLoadValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }

        /// <summary>
        /// 当日环比数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuitMomDayValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuitMomDayValueList("000001G001", "000001G0010001", today.ToString("yyyy-MM-dd HH:mm:ss"));
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }

        /// <summary>
        /// 当月环比数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuitMomMonthValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuitMomMonthValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }

        /// <summary>
        /// 最近48小时用能数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuit48HoursValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuit48HoursValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }

        /// <summary>
        /// 最近31天用能数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuit31DaysValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuit31DaysValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }

        /// <summary>
        /// 最近12个月用能数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuit12MonthValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuit12MonthValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }


        /// <summary>
        /// 最近3年用能数据
        /// </summary>
        [TestMethod]
        public void TestGetCircuit3YearValueList()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();
            DateTime today = DateTime.Now;

            List<CircuitValue> circuitValues = context.GetCircuit3YearValueList("000001G001", "000001G0010001", today.ToString());
            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.Name, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }
        }
    }
}

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
        [TestMethod]
        public void TestGetCircuitLoadValue()
        {
            ICircuitOverviewDbContext context = new CircuitOverviewDbContext();

            DateTime today = DateTime.Now;
            List<CircuitValue> circuitValues = context.GetCircuitLoadValueList("000001G001", "000001G0010001", today.ToString());

            foreach (var circuit in circuitValues)
            {
                Console.WriteLine("支路编码：{0}, 能耗编码：{1}, 时间：{2}, 数值：{3}；", circuit.CircuitId, circuit.EnergyItemCode, circuit.Time.ToString("yyyy-MM-dd HH:mm:ss"), circuit.Value);
            }

        }
    }
}

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
            ICircuitDbContext context = new CircuitDbContext();

            List<Circuit> circuits = context.GetCircuitListByBIdAndEItemCode("000001G001","01000");

            foreach (var circuit in circuits)
            {
                Console.WriteLine("支路编号：{0}；支路名称：{1}；仪表编号：{2}；上级支路代码：{3}；",circuit.CircuitId,circuit.CircuitName,circuit.MeterId,circuit.ParentId);
            }

        }

        [TestMethod]
        public void TestGetEnergyItemDict()
        {
            ICircuitDbContext context = new CircuitDbContext();

            List<EnergyItemDict> items = context.GetEnergyItemDictByBuild("000001G001");

            foreach (var item in items)
            {
                Console.WriteLine("分类编号：{0}；分类名称：{1}；", item.EnergyItemCode,item.EnergyItemName);
            }

        }
    }
}

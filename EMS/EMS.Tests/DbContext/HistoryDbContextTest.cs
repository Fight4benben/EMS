using System;
using System.Collections.Generic;
using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class HistoryTest
    {
        [TestMethod]
        public void TestGetHistoryValue()
        {
            HistoryDbContext context = new HistoryDbContext();
            List<HistoryValue> list =  context.GetHistoryValues(new string[] { "000001G0010001", "000001G0010002" }, new string[] { "31000000000711" }, new DateTime(2018, 4, 27, 10, 30, 0));

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("MeterID:{0};MeterParamID：{1};Value:{2}",list[i].MeterID,list[i].MeterParamID,list[i].Value);
            }
        }
    }
}

using EMS.DAL.Entities;
using EMS.DAL.RepositoryImp;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class HistoryParamDbContextTest
    {
        [TestMethod]
        public void TestGetHistoryPramaValue()
        {
            HistoryParamDbContext context = new HistoryParamDbContext();

            DateTime today = DateTime.Now;
            string circuitIDs = "000001G0010001,000001G0010002,000001G0010003,000001G0010004,000001G0010005";
            string circuitID = "000001G0010001";
            string[] ids = circuitIDs.Split(',');

            string circuitPrame = "31000000000711,31000000000700,31000000000701,31000000000702";
            string[] prame = circuitPrame.Split(',');

            List<HistoryParameterValue> historyParameterValueList = context.GetHistoryParamValue(circuitID, prame,today,60);

            Console.WriteLine(UtilTest.GetJson(historyParameterValueList));

        }

        [TestMethod]
        public void TestHistoryParamBinary()
        {
            
            HistoryParamDbContext context = new HistoryParamDbContext();
            Acrel.HisDB.GetData getData = new Acrel.HisDB.GetData();

            string circuitID = "000001G0010001";
            string circuitPrame = "31000000000711,31000000000700,31000000000701,31000000000702";
            string[] prame = circuitPrame.Split(',');

            List<HistoryBinarys> list = context.GetHistoryBinaryString(circuitID,prame,DateTime.Now);

            foreach (HistoryBinarys item in list)
            {
                Dictionary<DateTime,double> dic = getData.GetContinueBytesOfFive(item.Value,new DateTime(2018,10,25,0,0,0),new DateTime(2018,10,25,13,0,0),5);

                Console.WriteLine(dic.Count);
            }

            Console.WriteLine(list.Count);
        }

    }
}

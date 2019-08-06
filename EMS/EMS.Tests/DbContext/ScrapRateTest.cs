using EMS.DAL.Entities;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class ScrapRateTest
    {


        EnergyDB _db = new EnergyDB();

        private static List<DataValue> resultDataList = new List<DataValue>();
        [TestMethod]
        public void TestMethod1()
        {
            object viewModel = 1;

            string sql = @"SELECT Max( CASE WHEN  ParentCircuit.F_CircuitID IS NULL THEN '-1' ELSE ParentCircuit.F_CircuitID END) ParentID,
	                Circuit.F_CircuitID AS ID ,sum( F_Value) Value
                    FROM T_ST_CircuitMeterInfo Circuit
                    INNER JOIN T_DT_EnergyItemDict ItemDict ON Circuit.F_EnergyItemCode = ItemDict.F_EnergyItemCode
                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
	                INNER JOIN T_MC_MeterDayResult DayResult ON Meter.F_MeterID = DayResult.F_MeterID
                    INNER JOIN T_ST_MeterParamInfo ParamInfo ON DayResult.F_MeterParamID = ParamInfo.F_MeterParamID
	                LEFT JOIN T_ST_CircuitMeterInfo ParentCircuit ON Circuit.F_ParentID=ParentCircuit.F_CircuitID 
                    WHERE ParamInfo.F_IsEnergyValue=1
                    AND Circuit.F_BuildID = '000001G001'
                    AND Circuit.F_EnergyItemCode = '01000'
	                AND F_StartDay BETWEEN CONVERT(VARCHAR(7),@EndDate,120)+'-01 00:00' AND DATEADD(DAY,-DAY(@EndDate),DATEADD(MM,1,@EndDate))
	                GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName ";

            string sqlTest = @"SELECT Max( CASE WHEN  ParentCircuit.F_CircuitID IS NULL THEN '-1' ELSE ParentCircuit.F_CircuitID END) ParentID,
	                Circuit.F_CircuitID AS ID ,Max(DayResult.F_StartDay) 'Time', SUM( F_Value) Value
                    FROM T_ST_CircuitMeterInfo Circuit
                    INNER JOIN T_DT_EnergyItemDict ItemDict ON Circuit.F_EnergyItemCode = ItemDict.F_EnergyItemCode
                    INNER JOIN T_ST_MeterUseInfo Meter ON Circuit.F_MeterID = Meter.F_MeterID
	                INNER JOIN T_OV_ScrapRate DayResult ON Meter.F_MeterID = DayResult.F_MeterID
	                LEFT JOIN T_ST_CircuitMeterInfo ParentCircuit ON Circuit.F_ParentID=ParentCircuit.F_CircuitID 
                    WHERE Circuit.F_BuildID = '000001G001'
                    AND Circuit.F_EnergyItemCode = '01000'
	                AND F_StartDay BETWEEN CONVERT(VARCHAR(7),@EndDate,120)+'-01 00:00' AND DATEADD(DAY,-DAY(@EndDate),DATEADD(MM,1,@EndDate))
	                GROUP BY Circuit.F_CircuitID,Circuit.F_CircuitName 
	                order by ID ";

            SqlParameter[] parameters ={
                new SqlParameter("@EndDate","2019-08-01")

            };

            List<DataValue> DataList = _db.Database.SqlQuery<DataValue>(sqlTest, parameters).ToList();

            var realDataList = ChirldNode(DataList);

            Console.WriteLine(UtilTest.GetJson(realDataList));


            //var parantNode = DataList.Where(x => x.ParentID == "-1");
            //List<DataValue> RealDataList = new List<DataValue>();

            //foreach (var node in parantNode)
            //{
            //    var realDataList = ChirldNode(DataList, node);

            //    Console.WriteLine(UtilTest.GetJson(realDataList));
            //}

            //Console.WriteLine(UtilTest.GetJson(parantNode));
        }




        public List<DataValue> ChirldNode(List<DataValue> orignDatas)
        {

            //如果父级id为-1，则查询该父级下子节点 求和   求差    分摊值   损耗率   真实值  
            foreach (var node in orignDatas)//原始数据
            {

                //根据判断条件查找到根（root）节点
                if (node.ParentID == "-1")
                {
                    Console.WriteLine(UtilTest.GetJson(GetScrapData(orignDatas, node)));

                }
            }


            return null;


        }

        /// <summary>
        /// 获取 损耗率、真实值 （相邻两级损耗率）
        /// </summary>
        /// <param name="orignDatas"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<DataValue> GetScrapData(List<DataValue> orignDatas, DataValue node)
        {

            //List<DataValue> resultDataList = new List<DataValue>();
            decimal sum = 0;
            //求下级节点 获得一个集合  

            foreach (var item in orignDatas)//原始数据
            {
                //根据root节点id=item的父级，则查找到  回路A(root)下面的回路B1 回路b2

                if (node.ID == item.ParentID)
                {
                    sum = sum + item.Value;
                }
            }

            //与 子级节点 差(损耗值)
            decimal nodeScarpValue = ((node.RealValue == 0 ? node.Value : node.RealValue) - sum);

            foreach (var item in orignDatas)//原始数据
            {
                //根据root节点id=item的父级，则查找到  回路A(root)下面的回路B1 回路b2
                if (node.ID == item.ParentID)
                {
                    //子级节点 总和
                    Console.WriteLine("节点ID：" + node.ID + "  的子级节点 总和：" + sum);

                    //损耗值
                    Console.WriteLine("节点ID：" + node.ID + "  与 子级节点 差(损耗值)：" + nodeScarpValue);

                    //分摊损耗值
                    decimal ftScrapValue = (nodeScarpValue) * (item.Value / (sum));
                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  分摊损耗值：" + ftScrapValue);

                    //分摊损耗率
                    decimal ftScrapRate = (nodeScarpValue) * (item.Value / (sum)) / node.Value;

                    //真实值
                    decimal chirldRealValue = item.Value + (nodeScarpValue) * (item.Value / (sum));

                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  分摊损耗率：" + ftScrapRate);
                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  真实值：" + chirldRealValue);

                    item.ScrapValue = ftScrapValue;
                    item.ScrapRate = ftScrapRate;
                    item.RealValue = chirldRealValue;

                    resultDataList.Add(item);
                    Console.WriteLine("===============================================================================");

                    GetScrapData(orignDatas, item);
                }

            }
            return resultDataList;
        }

        /// <summary>
        /// 获取 损耗率、真实值（与根节点损耗率）
        /// </summary>
        /// <param name="orignDatas">原始数据</param>
        /// <param name="node">节点</param>
        /// <param name="rootNodeValue">根节点表示值</param>
        /// <returns></returns>
        private static List<DataValue> GetScrapData(List<DataValue> orignDatas, DataValue node, decimal rootNodeValue)
        {

            decimal sum = 0;
            //求下级节点 获得一个集合  

            foreach (var item in orignDatas)//原始数据
            {
                //根据root节点id=item的父级，则查找到  回路A(root)下面的回路B1 回路b2

                if (node.ID == item.ParentID)
                {
                    sum = sum + item.Value;
                }
            }

            //与 子级节点 差(损耗值)
            decimal nodeScarpValue = ((node.RealValue == 0 ? node.Value : node.RealValue) - sum);

            foreach (var item in orignDatas)//原始数据
            {
                //根据root节点id=item的父级，则查找到  回路A(root)下面的回路B1 回路b2
                if (node.ID == item.ParentID)
                {
                    //子级节点 总和
                    Console.WriteLine("节点ID：" + node.ID + "  的子级节点 总和：" + sum);

                    //损耗值
                    Console.WriteLine("节点ID：" + node.ID + "  与 子级节点 差(损耗值)：" + nodeScarpValue);

                    //分摊损耗值
                    decimal ftScrapValue = (nodeScarpValue) * (item.Value / (sum));
                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  分摊损耗值：" + ftScrapValue);

                    //分摊损耗率
                    decimal ftScrapRate = (nodeScarpValue) * (item.Value / (sum)) / rootNodeValue;

                    //真实值
                    decimal chirldRealValue = item.Value + (nodeScarpValue) * (item.Value / (sum));

                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  分摊损耗率：" + ftScrapRate);
                    Console.WriteLine("节点ID：" + node.ID + "  的子节点ID：" + item.ID + "  真实值：" + chirldRealValue);

                    item.ScrapValue = ftScrapValue;
                    item.ScrapRate = ftScrapRate;
                    item.RealValue = chirldRealValue;

                    resultDataList.Add(item);
                    Console.WriteLine("===============================================================================");

                    GetScrapData(orignDatas, item, rootNodeValue);
                }

            }
            return resultDataList;
        }



    }

    public class DataValue
    {
        public string ParentID { get; set; }
        public string ID { get; set; }
        public DateTime Time { get; set; }
        //表示值
        public decimal Value { get; set; }
        //真实值
        public decimal RealValue { get; set; }
        public decimal ScrapValue { get; set; }
        public decimal ScrapRate { get; set; }

    }




    /*


                //存储计算出真实值
                List<DataValue> RealDataList = new List<DataValue>();

                //1、获取子节点的值
                var children = orignDatas.Where(x => x.ParentID == parentNode.ID);

                //遍历子节点
                foreach (var item in children)
                {
                    DataValue node = new DataValue();
                    node.ParentID = item.ParentID;
                    node.ID = item.ID;
                    node.Value = item.Value;

                    //子级节点 表示值总和
                    decimal chirldTotalValue = 0;

                    foreach (var currentItem in children)
                    {
                        chirldTotalValue = chirldTotalValue + currentItem.Value;
                    }

                    //父级 与子级 差值
                    decimal nodeScrapValue = parentNode.RealValue - chirldTotalValue;

                    //当前节点 分摊损耗值
                    decimal shareScrapValue = nodeScrapValue * (item.Value / (chirldTotalValue));

                    //分摊损耗率
                    decimal shareScrapRate = shareScrapValue / parentNode.RealValue;

                    node.ScrapRate = shareScrapRate;
                    node.RealValue = item.Value + shareScrapValue;
                    RealDataList.Add(node);

                    children.Where(x => x.ParentID == item.ID);
                    ChirldNode(orignDatas, item);


                }
                */
}

using EMS.DAL.Entities;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.DbContext
{
    [TestClass]
    public class GetHDTest
    {
        public class HistoryBinarys
        {
            public string CircuitID { get; set; }
            public string CircuitName { get; set; }
            public string ParamName { get; set; }
            public byte[] Value { get; set; }
        }

        private static readonly byte[] nullBytes = new byte[]
        {
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128
        };

        private static readonly byte[] nullBytesOfFive = new byte[]
        {
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128,
            0,
            0,
            240,
            112,
            2,
            0,
            128
        };

        public static double SelectBinarysToDoubleOriOfFive(SqlBinary sourceBinary, int FIndex)
        {
            byte[] array = new byte[8];
            byte[] array2 = new byte[8];
            Array.Copy(SelectBinarysOriOfFive(sourceBinary, FIndex), array, 7);
            byte[] array3 = new byte[4];
            array3[0] = array[0];
            array3[1] = array[1];
            array3[2] = Convert.ToByte((array[2] & 15));
            uint num = BitConverter.ToUInt32(array3, 0);
            array2[0] = (byte)(array[2] >> 4 | (int)array[3] << 4);
            array2[1] = (byte)(array[3] >> 4 | (int)array[4] << 4);
            array2[2] = (byte)(array[4] >> 4 | (int)array[5] << 4);
            array2[3] = (byte)(array[5] >> 4 | (int)array[6] << 4);
            array2[4] = (byte)((array[6] & 127) >> 4);
            long num2 = ((array[6] >> 7 == 1) ? -1L : 1L) * BitConverter.ToInt64(array2, 0);
            if (num2 >= 0L)
            {
                return (double)num2 + num / 1000000.0;
            }
            return (double)num2 - num / 1000000.0;
        }


        public static double SelectBinarysToDoubleByDateOfFive(SqlBinary sourceBinary, int day, int hour, int minute)
        {
            return SelectBinarysToDoubleOriOfFive(sourceBinary, (day - 1) * 24 * 12 + hour * 12 + minute / 5 + 1);
        }


        public static byte[] SelectBinarysOriOfFive(SqlBinary sourceBinary, int FIndex)
        {
            byte[] array = new byte[]
            {
                0,
                0,
                240,
                112,
                2,
                0,
                128
            };
            if (!sourceBinary.IsNull && sourceBinary.Length >= FIndex * 7)
            {
                Array.Copy(sourceBinary.Value, (FIndex - 1) * 7, array, 0, 7);
            }
            return array;
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        private HistoryDB _db = new HistoryDB();

        public List<HistoryBinarys> GetHistoryString(string[] meterIds, string[] meterParamIds, DateTime time)
        {
            string month = time.Month.ToString("00");
            int day = time.Day;
            int hour = time.Hour;
            int minute = time.Minute;

            string meters = "('" + string.Join("','", meterIds) + "')";
            string meterparams = "('" + string.Join("','", meterParamIds) + "')";

            string sql = @"SELECT Circuit.F_CircuitID AS CircuitID, F_CircuitName AS CircuitName
                                , ParamInfo.F_MeterParamName AS ParamName
                                , F_Month" + month +
                                @" AS Value FROM HistoryData WITH(NOLOCK)
                                INNER JOIN EMS.dbo.T_ST_CircuitMeterInfo Circuit ON Circuit.F_MeterID=HistoryData.F_MeterID
	                            INNER JOIN EMS.dbo.T_ST_MeterParamInfo ParamInfo ON ParamInfo.F_MeterParamID= HistoryData.F_MeterParamID
                                WHERE F_Year = " + time.Year +
                                " AND Circuit.F_CircuitID in" + meters + "" +
                                " AND HistoryData.F_MeterParamID in" + meterparams + "";

            return _db.Database.SqlQuery<HistoryBinarys>(sql).ToList();

        }


        public double GetHistoryValue(byte[] oriString, int day, int hour, int minute)
        {
            SqlBinary oriBinary = (SqlBinary)oriString;

            double value = SelectBinarysToDoubleByDateOfFive(oriBinary, day, hour, minute);
            return value;
        }

        [TestMethod]
        public void GetHistoryParamValueTest()
        {
            DateTime today = DateTime.Now;

            string circuitIDs = "000001G0010001";
            string[] ids = circuitIDs.Split(',');

            string circuitPrame = "31000000000711,31000000000700,31000000000701,31000000000702";
            string[] prame = circuitPrame.Split(',');

            DateTime startTime = DateTime.Now;
            int step = 65;

            List<HistoryBinarys> historyBinarys = GetHistoryString(ids, prame, today);

            //int day = 0;
            //int hour = 0;
            //int minute = 0;
            //Console.WriteLine(UtilTest.GetJson(historyBinarys));

            List<HistoryParameterValue> historyValueList = new List<HistoryParameterValue>();

            foreach (var item in historyBinarys)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    for (int minute = 0; minute < 56; minute = minute + step)
                    {
                        HistoryParameterValue historyValue = new HistoryParameterValue();
                        historyValue.ID = item.CircuitID;
                        historyValue.Name = item.CircuitName;
                        historyValue.ParamName = item.ParamName;

                        historyValue.Time = new DateTime(startTime.Year, startTime.Month, startTime.Day, hour, minute, 0);
                        double value = GetHistoryValue(item.Value, startTime.Day, hour, minute);
                        if (-9999 != value)
                        {
                            historyValue.Value = value;
                            historyValueList.Add(historyValue);
                        }

                    }
                }
            }
            Console.WriteLine(UtilTest.GetJson(historyValueList));
        }
    }
}

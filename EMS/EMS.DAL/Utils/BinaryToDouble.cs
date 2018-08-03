﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Utils
{
    public class BinaryToDouble
    {
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

        public static double SelectBinarysToDoubleByDateOfFive(SqlBinary sourceBinary, int day, int hour, int minute)
        {
            return SelectBinarysToDoubleOriOfFive(sourceBinary, (day - 1) * 24 * 12 + hour * 12 + minute / 5 + 1);
        }

        public static double GetHistoryParamValue(byte[] binaryByte, int day, int hour, int minute)
        {
            SqlBinary sqlBinarys = (SqlBinary)binaryByte;
            double value = SelectBinarysToDoubleByDateOfFive(sqlBinarys, day, hour, minute);
            return value;
        }
    }
}

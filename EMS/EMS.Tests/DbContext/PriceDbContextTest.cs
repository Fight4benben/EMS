using EMS.DAL.Entities;
using EMS.DAL.IRepository;
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
    public class PriceDbContextTest
    {
        [TestMethod]
        public void TestGetPrice()
        {
            IPriceDbContext context = new PriceDbContext();
            //Console.WriteLine("123");
            //Price price = new Price();
            //price.ElectriPrice = 1.5;

            Price price = context.GetPrice("000001G001");

            //List<EnergyPrice> energyPrice = new List<EnergyPrice>();
            foreach (System.Reflection.PropertyInfo item in price.GetType().GetProperties())
            {
                Console.WriteLine("{0},{1}", item.Name,item.GetValue(price,null));
            }


        }
    }
}

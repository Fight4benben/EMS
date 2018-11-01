using EMS.DAL.Entities;
using EMS.DAL.Services;
using EMS.DAL.ViewModels;
using EMS.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{
    [TestClass]
    public class BuildSetServiceTest
    {
        [TestMethod]
        public void TestGetBuildListByuser()
        {
            BuildSetService service = new BuildSetService();
            BuildSetViewModel ViewModel = service.GetViewModelByUserName("admin");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestGetBuildInfo()
        {
            BuildSetService service = new BuildSetService();
            BuildSetViewModel ViewModel = service.GetViewModel("000001G001");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestAddBuild_OK()
        {
            BuildSetService service = new BuildSetService();

            BuildSetViewModel ViewModel = service.GetAllBuilds();
            BuildViewModel lasstbuild = ViewModel.Builds.Last();
            string lastBID = lasstbuild.BuildID;
            string newBuildID;
            int bID = Convert.ToInt16(lastBID.Substring(lastBID.Length - 3));

            if (bID + 1 < 10)
            {
                newBuildID = "000001G00" + (bID + 1).ToString();
            }
            else if (bID + 1 >= 10 && bID + 1 < 100)
            {
                newBuildID = "000001G0" + (bID + 1).ToString();
            }
            else
            {
                newBuildID = "000001G" + (bID + 1).ToString();
            }

            BuildInfoSet buildInfoSet = new BuildInfoSet();

            buildInfoSet.BuildID = newBuildID;
            buildInfoSet.DataCenterID = "000001";
            buildInfoSet.BuildName = "TestName" + (bID + 1).ToString();
            buildInfoSet.AliasName = "TestName" + (bID + 1).ToString();
            buildInfoSet.BuildOwner = "TestOwner" + (bID + 1).ToString();

            buildInfoSet.DistrictCode = "310000";
            buildInfoSet.BuildAddr = "Addr Test";
            buildInfoSet.BuildLong = 123;
            buildInfoSet.BuildLat = 45;
            buildInfoSet.BuildYear = 2018;

            buildInfoSet.UpFloor = (bID + 1);
            buildInfoSet.DownFloor = bID;
            buildInfoSet.BuildFunc = "G";
            buildInfoSet.TotalArea = 5000;
            buildInfoSet.AirArea = 3000;

            buildInfoSet.DesignDept = "设计单位"+(bID + 1).ToString();
            buildInfoSet.WorkDept = "使用单位" + (bID + 1).ToString();
            buildInfoSet.CreateTime = DateTime.Now;
            buildInfoSet.CreateUser = "Admin";
            buildInfoSet.MonitorDate = DateTime.Now;

            buildInfoSet.AcceptDate = DateTime.Now;
            buildInfoSet.NumberOfPeople = 500;
            buildInfoSet.SPArea = 500;
            buildInfoSet.Image = null;
            buildInfoSet.TransCount = 5;

            buildInfoSet.InstallCapacity = 500;
            buildInfoSet.OperateCapacity = 400;
            buildInfoSet.DesignMeters =20+ bID + 1;
            buildInfoSet.Mobiles = "12345678901";


            //BuildID,DataCenterID,BuildName,AliasName,BuildOwner
            //,DistrictCode,BuildAddr,BuildLong,BuildLat,BuildYear
            //,UpFloor,DownFloor,BuildFunc,TotalArea,AirArea
            //,DesignDept,WorkDept,CreateTime,CreateUser,MonitorDate
            //,AcceptDate,NumberOfPeople,SPArea,Image,TransCount
            //,InstallCapacity,OperateCapacity,DesignMeters,Mobiles

            ViewModel = service.AddBuild(buildInfoSet);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestUpdataBuild_OK()
        {
            BuildSetService service = new BuildSetService();

            BuildSetViewModel ViewModel = service.GetAllBuilds();
            BuildViewModel lasstbuild = ViewModel.Builds.Last();
         
            string newBuildID="000001G004";
         

            BuildInfoSet buildInfoSet = new BuildInfoSet();

            buildInfoSet.BuildID = newBuildID;
            buildInfoSet.DataCenterID = "000001";
            buildInfoSet.BuildName = "TestName" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buildInfoSet.AliasName = "TestName" + DateTime.Now.ToString("HH:mm:ss");
            buildInfoSet.BuildOwner = "TestOwner" ;

            buildInfoSet.DistrictCode = "310000";
            buildInfoSet.BuildAddr = "Addr Test";
            buildInfoSet.BuildLong = 123;
            buildInfoSet.BuildLat = 45;
            buildInfoSet.BuildYear = 2018;

            buildInfoSet.UpFloor = (DateTime.Now.Hour + 1);
            buildInfoSet.DownFloor = DateTime.Now.Hour;
            buildInfoSet.BuildFunc = "G";
            buildInfoSet.TotalArea = 5000;
            buildInfoSet.AirArea = 3000;

            buildInfoSet.DesignDept = "设计单位" ;
            buildInfoSet.WorkDept = "使用单位" ;
            buildInfoSet.CreateTime = DateTime.Now;
            buildInfoSet.CreateUser = "Admin";
            buildInfoSet.MonitorDate = DateTime.Now;

            buildInfoSet.AcceptDate = DateTime.Now;
            buildInfoSet.NumberOfPeople = 500;
            buildInfoSet.SPArea = 500;
            buildInfoSet.Image = null;
            buildInfoSet.TransCount = 5;

            buildInfoSet.InstallCapacity = 500;
            buildInfoSet.OperateCapacity = 400;
            buildInfoSet.DesignMeters = 20 ;
            buildInfoSet.Mobiles = "12345678901";


            //BuildID,DataCenterID,BuildName,AliasName,BuildOwner
            //,DistrictCode,BuildAddr,BuildLong,BuildLat,BuildYear
            //,UpFloor,DownFloor,BuildFunc,TotalArea,AirArea
            //,DesignDept,WorkDept,CreateTime,CreateUser,MonitorDate
            //,AcceptDate,NumberOfPeople,SPArea,Image,TransCount
            //,InstallCapacity,OperateCapacity,DesignMeters,Mobiles

            ViewModel = service.UpdateBuild(buildInfoSet);

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }

        [TestMethod]
        public void TestDeleteBuild()
        {
            BuildSetService service = new BuildSetService();
            BuildSetViewModel ViewModel = service.DeleteBuild("000001G005");

            Console.WriteLine(UtilTest.GetJson(ViewModel));
        }
    }
}

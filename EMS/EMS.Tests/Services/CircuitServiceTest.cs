﻿using System;
using System.Linq;
using System.Collections.Generic;
using EMS.DAL.Entities;
using EMS.DAL.IRepository;
using EMS.DAL.RepositoryImp;
using EMS.DAL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMS.DAL.ViewModels;
using System.Data.SqlClient;

namespace EMS.Tests.Services
{
    [TestClass]
    public class CircuitServiceTest
    {
        [TestMethod]
        public void TestGetChildrenCircuit()
        {
            CircuitReportService service = new CircuitReportService();
            List<TreeViewModel> treeView = service.GetTreeListViewModel("000001G001", "01000");

            Console.WriteLine(treeView);
        }

        [TestMethod]
        public void TestGetEnergyItemDictByBuild()
        {
            string[] list = new string[] { "0001","0002"};
            string join  = string.Join("','",list);
            Console.WriteLine("'"+join+"'");
            //CircuitService service = new CircuitService();
            //service.
        }

        [TestMethod]
        public void TestGetCircuitView()
        {
            CircuitReportService service = new CircuitReportService();
            CircuitReportViewModel view = service.GetViewModel("admin");
            Console.WriteLine(view);
        }
    }
}

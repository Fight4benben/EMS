using EMS.DAL.Entities;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IEnergyItemTreeView
    {
        List<TreeViewModel> GetEnergyItemTreeViewList(string buildId);
        List<TreeViewModel> GetEnergyItemTreeViewList(string buildId, string energyItemCode);
    }
}

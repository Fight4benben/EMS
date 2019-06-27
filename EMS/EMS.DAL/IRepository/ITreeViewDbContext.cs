using EMS.DAL.Entities;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface ITreeViewDbContext
    {
        //List<TreeViewModel> GetDepartmentTreeViewList(string buildId);
        List<TreeViewModel> GetDepartmentTreeViewList(string buildId, string energyItemCode);
        string[] GetDepartmentIDs(string buildId, string energyItemCode);
        List<TreeViewModel> GetRegionTreeViewList(string buildId);
        List<TreeViewModel> GetRegionTreeViewList(string buildId, string energyItemCode);
        string[] GetRegionIDs(string buildId);

        //获取建筑列表
        List<BuildViewModel> GetBuildsByUserName(string userName);
        //获取分类能耗
        List<EnergyItemDict> GetEnergyItemDictByBuild(string buildId);
        //获取支路树状结构
        List<TreeViewModel> GetCircuitTreeListViewModel(string buildId, string energyCode);
    }
}

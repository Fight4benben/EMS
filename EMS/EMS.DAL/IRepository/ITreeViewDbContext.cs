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
        List<TreeViewModel> GetDepartmentTreeViewList(string buildId);
        List<TreeViewModel> GetDepartmentTreeViewList(string buildId, string energyItemCode);
        string[] GetDepartmentIDs(string buildId);
        List<TreeViewModel> GetRegionTreeViewList(string buildId);
        List<TreeViewModel> GetRegionTreeViewList(string buildId, string energyItemCode);
        string[] GetRegionIDs(string buildId);
    }
}

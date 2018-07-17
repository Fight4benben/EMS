using EMS.DAL.Entities;
using EMS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Utils
{
    public class Util
    {
        public static DateTime ConvertString2DateTime(string date, string formatPattern)
        {
            DateTime dt;
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = formatPattern;
            return dt = Convert.ToDateTime(date, dtFormat);
        }

        /// <summary>
        /// 根据list生成树状结构
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetTreeViewModel(List<TreeViewInfo> treeViewInfos)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();

            var parentItemCodes = treeViewInfos.Where(c => (c.ParentID == "-1"));
            foreach (var parentItem in parentItemCodes)
            {
                TreeViewModel parentNode = new TreeViewModel();
                List<TreeViewModel> children = GetChildrenNodes(treeViewInfos, parentItem);
                parentNode.Id = parentItem.ID;
                parentNode.Text = parentItem.Name;
                if (children.Count != 0)
                    parentNode.Nodes = children;
                treeViewModel.Add(parentNode);
            }

            return treeViewModel;
        }

        /// <summary>
        /// 递归调用方式填充树状结构的子节点
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <param name="parentItem"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetChildrenNodes(List<TreeViewInfo> treeViewInfos, TreeViewInfo parentItem)
        {
            string parentID = parentItem.ID;
            List<TreeViewModel> treeViewList = new List<TreeViewModel>();
            var children = treeViewInfos.Where(c => c.ParentID == parentID);

            foreach (var item in children)
            {
                TreeViewModel node = new TreeViewModel();
                node.Id = item.ID;
                node.Text = item.Name;
                if (GetChildrenNodes(treeViewInfos, item).Count > 0)
                    node.Nodes = GetChildrenNodes(treeViewInfos, item);

                treeViewList.Add(node);
            }

            return treeViewList;
        }

        /// <summary>
        /// 获取所有部门/区域ID
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <returns></returns>
        public static string[] GetAllIDs(List<TreeViewInfo> treeViewInfos)
        {
            List<string> list = new List<string>();
            foreach (var Item in treeViewInfos)
            {
                list.Add(Item.ID);
            }
            return list.ToArray();
        }
    }
}

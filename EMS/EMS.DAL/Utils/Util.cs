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

        public static DateTime GetMonthEndDate(string date)
        {
            int year = Convert.ToInt32(date.Split('-')[0]);
            int month = Convert.ToInt32(date.Split('-')[1]);

            DateTime first = new DateTime(year, month, 1);
            DateTime end = first.AddMonths(1).AddDays(-1);

            return end;

        }

        /// <summary>
        /// 根据list生成树状结构
        /// </summary>
        /// <param name="treeViewInfos"></param>
        /// <returns></returns>
        public static List<TreeViewModel> GetTreeViewModel(List<TreeViewInfo> treeViewInfos)
        {
            List<TreeViewModel> treeViewModel = new List<TreeViewModel>();

            foreach (var item in treeViewInfos)
            {
                TreeViewInfo info = treeViewInfos.Find(e => e.ID == item.ParentID);
                if (info == null)
                {
                    TreeViewModel parent = new TreeViewModel();
                    List<TreeViewModel> children = GetChildrenNodes(treeViewInfos, item);
                    parent.Id = item.ID;
                    parent.Text = item.Name;

                    if (children.Count != 0)
                        parent.Nodes = children;

                    treeViewModel.Add(parent);
                }
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

        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);
            
            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        public static string FilterSql(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.Trim().ToLower();

            if(s.IndexOf("=") >0)
                s = s.Remove(s.IndexOf("="));
            //s = s.Replace("=", "");
            if (s.IndexOf("'") > 0)
                s = s.Remove(s.IndexOf("'"));
            if (s.IndexOf(";") > 0)
                s = s.Remove(s.IndexOf(";"));
            if (s.IndexOf(" and ") > 0)
                s = s.Remove(s.IndexOf(" and "));
            if (s.IndexOf(" or ") > 0)
                s = s.Remove(s.IndexOf(" or "));
            if (s.IndexOf("select") > 0)
                s = s.Remove(s.IndexOf("select"));
            if (s.IndexOf("update") > 0)
                s = s.Remove(s.IndexOf("update"));
            if (s.IndexOf("insert") > 0)
                s = s.Remove(s.IndexOf("insert"));
            if (s.IndexOf("delete") > 0)
                s = s.Remove(s.IndexOf("delete"));
            if (s.IndexOf("declare") > 0)
                s = s.Remove(s.IndexOf("declare"));
            if (s.IndexOf("exec") > 0)
                s = s.Remove(s.IndexOf("exec"));
            if (s.IndexOf("drop") > 0)
                s = s.Remove(s.IndexOf("drop"));
            if (s.IndexOf("create") > 0)
                s = s.Remove(s.IndexOf("create"));
            if (s.IndexOf("alter") > 0)
                s = s.Remove(s.IndexOf("alter"));
            if (s.IndexOf("%") > 0)
                s = s.Remove(s.IndexOf("%"));
            if (s.IndexOf("--") > 0)
                s = s.Remove(s.IndexOf("--"));
 
            return s;
        }
    }
}

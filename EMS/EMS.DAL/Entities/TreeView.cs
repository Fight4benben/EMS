using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class TreeView
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<TreeView> Nodes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.ViewModels
{
    public class TreeViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<TreeViewModel> Nodes { get; set; }
    }
}

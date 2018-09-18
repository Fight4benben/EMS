using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class ConnectState
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CollectionName { get; set; }
        public string States { get; set; }
        public string DisConnectTime { get; set; }
        public string DiffDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class HistoryDB : DbContext
    {
        public HistoryDB():base("name=History")
        { }
    }
}

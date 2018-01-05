using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class EnergyDB :DbContext
    {
        public EnergyDB():base("name=Energy")
        {
        }

        public virtual DbSet<BuildInfo> BuildInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildInfo>().Property(e => e.BuildId).IsUnicode(false);
        }
    }
}

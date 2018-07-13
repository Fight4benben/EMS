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
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<EnergyItemDict> EnergyItemDict { get; set; }
        public virtual DbSet<BuildExtendInfo> BuildExtendInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuildInfo>().Property(e => e.BuildId).IsUnicode(false);

            modelBuilder.Entity<User>().Property(e => e.UserId);

            modelBuilder.Entity<EnergyItemDict>().Property(e => e.EnergyItemCode).IsUnicode(false);

            modelBuilder.Entity<BuildExtendInfo>().Property(e => e.BuildID).IsUnicode(false);
        }
    }
}

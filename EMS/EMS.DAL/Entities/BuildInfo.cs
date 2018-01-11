using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    [Table("T_BD_BuildBaseInfo")]
    public class BuildInfo
    {
        [Key]
        [Column("F_BuildID")]
        public string BuildId { get; set; }

        [Column("F_BuildName")]
        public string BuildName { get; set; }

        [Column("F_BuildAddr")]
        public string BuildAddr { get; set; }

        [Column("F_BuildLong",TypeName ="numeric")]
        public decimal? BuildLong { get; set; }

        [Column("F_BuildLat", TypeName = "numeric")]
        public decimal? BuildLat { get; set; }

        [Column("F_TotalArea", TypeName = "numeric")]
        public decimal? TotalArea { get; set; }

        [Column("F_TransCount")]
        public int? TransCount { get; set; }

        [Column("F_InstallCapacity")]
        public int? InstallCapacity { get; set; }

        [Column("F_OperateCapacity")]
        public int? OperateCapacity { get; set; }

        [Column("F_DesignMeters")]
        public int? DesignMeters { get; set; }

    }
}

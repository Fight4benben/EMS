using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities.Setting
{
    [Table("T_ST_Svg")]
    public class Svg
    {
        [Key]
        [Column("F_BuildID",Order = 1)]
        [StringLength(16)]
        public string BuildId { get; set; }

        [Key]
        [Column("F_SvgID",Order =2)]
        [StringLength(20)]
        public string SvgId { get; set; }

        [Column("F_SvgName")]
        [StringLength(50)]
        public string SvgName { get; set; }

        [Column("F_Path")]
        public string Path { get; set; }
    }
}

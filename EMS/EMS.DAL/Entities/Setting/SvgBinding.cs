using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities.Setting
{
    [Table("T_ST_SvgBinding")]
    public class SvgBinding
    {
        [Key]
        [Column("F_ID")]
        public int ID { get; set; }

        [Column("F_SvgID")]
        public string SvgId { get; set; }

        [Column("F_Meters")]
        public string Meters { get; set; }

        [Column("F_Params")]
        public string Params { get; set; }

    }
}

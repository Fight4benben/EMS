using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    [Table("T_BD_BuildExInfo")]
    public class BuildExtendInfo
    {
        [Key]
        [Column("F_BuildID")]
        public string BuildID { get; set; }

        [Column("F_ExtendFunc")]
        public string ShowMode { get; set; }
    }
}

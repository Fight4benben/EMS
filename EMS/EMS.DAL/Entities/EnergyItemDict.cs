using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    [Table("T_DT_EnergyItemDict")]
    public class EnergyItemDict
    {
        [Key]
        [Column("F_EnergyItemCode")]
        public string EnergyItemCode { get; set; }

        [Column("F_EnergyItemName")]
        public string  EnergyItemName { get; set; }
    }
}

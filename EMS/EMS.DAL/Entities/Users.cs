using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Entities
{
    public class User
    {
        [Key]
        [Required]
        [Column("F_UserID")]
        public int UserId { get; set; }
        
        [Column("F_UserName")]
        public string UserName { get; set; }

        [Column("F_Password")]
        public string Password { get; set; }
    }
}

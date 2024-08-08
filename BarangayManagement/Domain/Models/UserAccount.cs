using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("UserAccount", Schema = "security")]
    public partial class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }
        [StringLength(150)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        public short? Role { get; set; }
        public bool? IsActive { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

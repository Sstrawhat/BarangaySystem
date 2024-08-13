using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Status", Schema = "maintenance")]
    public partial class Status
    {
        [Key]
        public int StatusId { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Value { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

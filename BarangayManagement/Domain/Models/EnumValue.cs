using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("EnumValues", Schema = "maintenance")]
    public partial class EnumValue
    {
        [Key]
        public int EnumValueId { get; set; }
        [StringLength(150)]
        public string Source { get; set; }
        [StringLength(150)]
        public string DisplayName { get; set; }
        public short? Value { get; set; }
        public short? SortOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

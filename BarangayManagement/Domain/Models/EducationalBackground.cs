using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    [Table("EducationalBackground", Schema = "residence")]
    public partial class EducationalBackground
    {
        [Key]
        public int EducationalBackgroundId { get; set; }
        public int? ResidentProfileId { get; set; }
        [StringLength(150)]
        public string SchoolName { get; set; }
        public short? EducationalAttainment { get; set; }
        [StringLength(50)]
        public string YearCompleted { get; set; }
        [StringLength(150)]
        public string Course { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

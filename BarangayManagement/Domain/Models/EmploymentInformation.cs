using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("EmploymentInformation", Schema = "residence")]
    public partial class EmploymentInformation
    {
        [Key]
        public int EmploymentInformationId { get; set; }
        public int? ResidentProfileId { get; set; }
        [StringLength(150)]
        public string Occupation { get; set; }
        [StringLength(150)]
        public string CompanyName { get; set; }
        [StringLength(150)]
        public string CompanyAddress { get; set; }
        public short? Status { get; set; }
        [StringLength(200)]
        public string Duration { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

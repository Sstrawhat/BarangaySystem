using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("ResidentProfile", Schema = "residence")]
    public partial class ResidentProfile
    {
        [Key]
        public int ResidentProfileId { get; set; }
        [StringLength(50)]
        public string ResidentNumber { get; set; }
        [StringLength(150)]
        public string Firstname { get; set; }
        [StringLength(150)]
        public string Lastname { get; set; }
        [StringLength(150)]
        public string Middlename { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateofBirth { get; set; }
        public short? Gender { get; set; }
        [StringLength(200)]
        public string BirthPlace { get; set; }
        public short? CivilStatus { get; set; }
        [StringLength(20)]
        public string Height { get; set; }
        [StringLength(20)]
        public string Weight { get; set; }
        [StringLength(150)]
        public string EmailAddress { get; set; }
        [StringLength(50)]
        public string PrimaryContactNo { get; set; }
        [StringLength(50)]
        public string SecondaryContactNo { get; set; }
        [StringLength(200)]
        public string PrimaryAddress { get; set; }
        [StringLength(200)]
        public string SecondaryAddress { get; set; }
        public int? UserAccountId { get; set; }
        [StringLength(200)]
        public string ProfileImageName { get; set; }
        [StringLength(50)]
        public string ProfileImageTag { get; set; }
        public int? CreateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public int? LastUpdateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

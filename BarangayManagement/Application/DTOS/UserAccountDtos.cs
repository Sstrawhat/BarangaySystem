using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Application.DTOS
{
    public class UserAccountDtos
    {
        public int UserAccountId { get; set; }

        [StringLength(150)]
        [CustomAttributeDuplicate("security.UserAccount", "Username", "UserAccountId")]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Firstname { get; set; }

        [StringLength(150)]
        [Required]
        public string Lastname { get; set; }

        [StringLength(150)]
        [Required]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        [Required]
        public string MobileNumber { get; set; }

        [StringLength(150)]
        [Required]
        public string Password { get; set; }
        public byte? UserType { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsAgreeToTerms { get; set; }

        public string UserTypeText { get; set; }
    }
}

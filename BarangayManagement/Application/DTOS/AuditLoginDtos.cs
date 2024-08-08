using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class AuditLoginDtos
    {
        public long AuditLoginId { get; set; }
        public string Username { get; set; }
        public bool? IsLoginSuccess { get; set; }
        public DateTime? AccessDate { get; set; }
        public int? CreateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdateId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}

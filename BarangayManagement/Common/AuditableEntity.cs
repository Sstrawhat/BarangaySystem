using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AuditableEntity
    {
        public int Id { get; set; }
        public int CreateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdateId { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}

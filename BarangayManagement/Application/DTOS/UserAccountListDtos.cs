using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UserAccountListDtos
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string IsActive { get; set; }

        public string UserType { get; set; }

        public int TotalCount { get; set; }
    }
}

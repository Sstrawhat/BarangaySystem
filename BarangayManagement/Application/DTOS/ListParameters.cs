using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class ListParameters
    {
        public string SortField { get; set; } = "";
        public string SortType { get; set; } = "asc";
        public short PageNumber { get; set; }

        public short RowCount { get; set; }
    }
}

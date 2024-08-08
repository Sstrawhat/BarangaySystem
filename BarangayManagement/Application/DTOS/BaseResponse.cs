using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class BaseResponse<T>
        where T : class, new()
    {

        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; } = new List<string>();

        public int TotalCount { get; set; } = 0;

        public BaseResponse()
        {
            this.Data = new T();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomAttributeDuplicate : Attribute
    {
        public string Table { get; }
        public string Field { get; }

        public string Key { get; set; }

        public CustomAttributeDuplicate(string Table, string Field, string key)
        {
            this.Table = Table;
            this.Field = Field;
            this.Key = key;

        }
    }
}

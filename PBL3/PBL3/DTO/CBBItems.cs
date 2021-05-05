using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    class CBBItems
    {
        public int value { get; set; }
        public string text { get; set; }
        public override string ToString()
        {
            return text.ToString();
        }
    }
}

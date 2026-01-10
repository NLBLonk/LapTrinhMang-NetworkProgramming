using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class Json
    {
        public String type { get; set; }
        public String content { get; set; }
        public Json(String type, String content)
        {
            this.type = type;
            this.content = content;
        }
    }
}

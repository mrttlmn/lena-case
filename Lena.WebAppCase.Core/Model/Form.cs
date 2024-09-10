using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LENA.WebAppCase.Core.Model
{

    public class Form
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public List<Field> fields { get; set; }
    }
}

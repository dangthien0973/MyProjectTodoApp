using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TodoUpdateRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}

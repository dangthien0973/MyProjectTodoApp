using Model.Enums;
using Model.SeekWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class TaskListSearch :PagingParameters
    {
        public string Name { get; set; } = string.Empty;

        public Guid? AssigneeId { get; set; } 

        public Priority? Priority { get; set; }
    }
    public class MyGenericClass<T>
    {
        public T GetObject()
        {
            T myObject = default(T);
            // Rest of the code
            return myObject;
        }
    }
}

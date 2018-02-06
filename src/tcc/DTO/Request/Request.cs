using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Request<T>
    {
        public long RequestID { get; set; }
        public T Data { get; set; }
    }
}

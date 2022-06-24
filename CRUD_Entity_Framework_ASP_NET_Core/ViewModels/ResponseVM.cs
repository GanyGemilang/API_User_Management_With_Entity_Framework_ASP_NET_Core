using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.ViewModels
{
    public class ResponseVM
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

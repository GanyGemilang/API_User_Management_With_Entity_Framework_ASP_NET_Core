using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.ViewModels
{
    public class insertLoginVM
    {
        public string username { get; set; }
        public bool online { get; set; }
        public string token { get; set; }
        public DateTime expiredToken { get; set; }
    }
}

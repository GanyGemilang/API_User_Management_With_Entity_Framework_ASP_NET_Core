using CRUD_Entity_Framework_ASP_NET_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Entity_Framework_ASP_NET_Core.Utilities
{
    public static class Response
    {
        public static ResponseVM ResponseMessage(string Code, string Status, string Message, object Data)
        {
            ResponseVM Hasil = new ResponseVM();
            Hasil.Code = Code;
            Hasil.Status = Status;
            Hasil.Message = Message;
            Hasil.Data = Data;
            return Hasil;
        }
    }
}

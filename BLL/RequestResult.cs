using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RequestResult
    {
        public RequestResult()
        {
        }

        public RequestResult(bool status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }


    }
}

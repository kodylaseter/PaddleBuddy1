using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaddleBuddy.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Detail { get; set; }

        public Response()
        {
            Success = false;
            Detail = "Not set yet!";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Model
{
    public class Response
    {
        public object response { get; set; }
        public int statusCode { get; set; }
        public string status { get; set; }
    }
}

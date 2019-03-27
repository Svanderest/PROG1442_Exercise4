using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Client.Utils
{
    //Added as per:
    //http://www.codeproject.com/Articles/825274/ASP-NET-Web-Api-Unwrapping-HTTP-Error-Results-and#Unwrapping-and-Handling-Errors-and-Exceptions-in-Web-Api
    public class ApiException : Exception
    {
        public HttpResponseMessage Response { get; set; }
        public ApiException(HttpResponseMessage response)
        {
            this.Response = response;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.Response.StatusCode;
            }
        }


        public IEnumerable<string> Errors
        {
            get
            {
                return this.Data.Values.Cast<string>().ToList();
            }
        }
    }
}

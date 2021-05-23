using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL.Objects.Poco_s
{
    public class Log : IPoco
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public LogCategories Categories { get; set; }
        public string MethodName { get; set; }
        public string Url { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public bool IsSucceed { get; set; }
        public double MethodDuration { get; set; }

        public Log()
        {

        }

        public Log(LogCategories categories, string methodName, string url, string request)
        {
            Categories = categories;
            MethodName = methodName;
            Url = url;
            Request = request;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}

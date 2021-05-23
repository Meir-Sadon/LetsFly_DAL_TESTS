using LetsFly_DAL.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL.Objects
{
    public class ReponseDetails
    {
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseNote { get; set; }

        public ReponseDetails()
        {
            ResponseCode = ResponseCode.Success;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

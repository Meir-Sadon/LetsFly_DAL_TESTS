using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsFly_DAL.Objects.Poco_s
{
    public class Message : IPoco
    {
        public long MsgId { get; set; }
        public long MessageNumByUser { get; set; }

        public long SenderId { get; set; }
        public string SenderName { get; set; }

        public long ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime ValidUntil { get; set; }

        public string IsReaded { get; set; }
        public string IsMarked { get; set; }
        public string IsDeleted { get; set; }
        public string IsSpammed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this,Formatting.Indented);
        }
    }
}

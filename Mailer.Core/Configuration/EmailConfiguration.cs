using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Configuration
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string FromName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
    }
}

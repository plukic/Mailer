using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Base
{
    public class BaseDto<TKey>
    {
        public TKey Id { get; set; }
    }
}

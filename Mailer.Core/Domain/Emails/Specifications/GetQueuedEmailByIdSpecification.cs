using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Specifications
{
    public class GetQueuedEmailByIdSpecification : Specification<QueuedEmail>, ISingleResultSpecification
    {

        public GetQueuedEmailByIdSpecification(int emailId, string currentUserEmail)
        {
            Query.Where(x => x.Id == emailId)
                .Where(x=>x.From==currentUserEmail);

        }
    }
}

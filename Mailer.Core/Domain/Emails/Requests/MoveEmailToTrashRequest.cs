using Ardalis.Result;
using Mailer.Core.Domain.Emails.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Requests
{
    public class MoveEmailToTrashRequest : IRequest<Result<EmailDto>>
    {
        public int EmailId { get; set; }

        public MoveEmailToTrashRequest(int emailId)
        {
            EmailId = emailId;
        }
    }
}

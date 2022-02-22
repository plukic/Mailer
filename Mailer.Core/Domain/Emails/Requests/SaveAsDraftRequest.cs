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
    public class SaveAsDraftRequest : IRequest<Result<EmailDto>>
    {
        public SendEmailDto Data { get; set; }

        public SaveAsDraftRequest(SendEmailDto data)
        {
            Data = data;
        }
    }

}

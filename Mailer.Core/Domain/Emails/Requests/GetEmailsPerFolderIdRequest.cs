using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Folders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Requests
{
    public class GetEmailsPerFolderIdRequest : IRequest<List<EmailDto>>
    {
        public FolderType FolderType { get; set; }

        public GetEmailsPerFolderIdRequest(FolderType folderType)
        {
            FolderType = folderType;
        }
    }
}

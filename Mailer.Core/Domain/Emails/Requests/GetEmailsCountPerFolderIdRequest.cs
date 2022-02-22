using Mailer.Core.Domain.Folders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Requests
{
    public class GetEmailsCountPerFolderIdRequest : IRequest<int>
    {
        public FolderType FolderType { get; set; }
        public GetEmailsCountPerFolderIdRequest(FolderType folderType)
        {
            FolderType = folderType;
        }
    }
}

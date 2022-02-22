using Mailer.Core.Domain.Emails.Dtos;
using Mailer.Core.Domain.Folders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Mailer.Core.Domain.Emails.Requests
{
    public class GetEmailsPerFolderIdRequest : IRequest<StaticPagedList<EmailDto>>
    {
        public FolderType FolderType { get; set; }
        public EmailPriority? EmailPriority { get; set; }
        public string SearchTerm { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetEmailsPerFolderIdRequest(FolderType folderType, EmailPriority? emailPriority, string searchTerm, int page=1, int pageSize=10)
        {
            FolderType = folderType;
            EmailPriority = emailPriority;
            SearchTerm = searchTerm;
            Page = page;
            PageSize = pageSize;
        }
    }
}

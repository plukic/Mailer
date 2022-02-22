using AutoMapper;
using Mailer.Core.Domain.Emails.Dtos;

namespace Mailer.Core.Domain.Emails
{
    public class EmailMappingProfile : Profile
    {

        public EmailMappingProfile()
        {
            CreateMap<QueuedEmail, EmailDto>();
        }
    }
}

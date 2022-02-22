using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Core.Domain.Emails.Notifications
{
    public class CloseDraftMailNotification : INotification
    {
        public int Id { get; set; }

        public CloseDraftMailNotification(int id)
        {
            Id = id;
        }
    }
}

using Mailer.Core.Domain.Emails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Infrastructure.DataAccess
{
    public class MailerDbContext : DbContext
    {

        public DbSet<QueuedEmail> QueuedEmails { get; set; }

        public MailerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MailerDbContext).Assembly);

            // Apply global filters in one place
            //builder.SetQueryFilterOnAllEntities<ISoftDeletableEntity>(b => !b.IsDeleted);
        }
    }
}

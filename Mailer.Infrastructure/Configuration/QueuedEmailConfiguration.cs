using Mailer.Core.Domain.Emails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Infrastructure.Configuration
{
    public class QueuedEmailConfiguration : IEntityTypeConfiguration<QueuedEmail>
    {
        public void Configure(EntityTypeBuilder<QueuedEmail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.From).IsRequired(true).HasMaxLength(500);
            builder.Property(x => x.FromName).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.To).IsRequired(true).HasMaxLength(500);
            builder.Property(x => x.ToName).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ReplyTo).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ReplyToName).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Cc).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Bcc).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Subject).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.EmailPriority).IsRequired(true);
            builder.Property(x => x.Body).IsRequired(false);
            builder.Property(x => x.BodyStriped).IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).IsRequired(true);
            builder.Property(x => x.SentOnUtc).IsRequired(false);
        }
    }
}

using Ardalis.Specification.EntityFrameworkCore;
using Mailer.Core.Data;
using Mailer.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Infrastructure.Data
{
    public class MyRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        private readonly MailerDbContext dbContext;

        public MyRepository(MailerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        // Not required to implement anything. Add additional functionalities if required.
    }
}

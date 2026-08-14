using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Application.interfaces
{
    public interface IOSDbContext
    {

        public DatabaseFacade Database { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

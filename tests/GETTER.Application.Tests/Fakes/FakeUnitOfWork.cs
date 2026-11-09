using GETTER.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GETTER.Application.Tests.Fakes
{
    public sealed class FakeUnitOfWork : IUnitOfWork
    {
        public int SaveChangesCallCount { get; private set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SaveChangesCallCount++;
            return Task.FromResult(SaveChangesCallCount);
        }
    }
}

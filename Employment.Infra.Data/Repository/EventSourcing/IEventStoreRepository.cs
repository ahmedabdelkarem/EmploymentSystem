using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employment.Domain.Core.Events;

namespace Employment.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
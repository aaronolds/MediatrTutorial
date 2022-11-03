namespace MediatrTutorial.Data.EventStore;

using System.Threading.Tasks;
using global::EventStore.ClientAPI;

public interface IEventStoreDbContext {
    Task<IEventStoreConnection> GetConnection();

    Task AppendToStreamAsync(params EventData[] events);
}
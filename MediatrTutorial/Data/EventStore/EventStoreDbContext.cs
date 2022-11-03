namespace MediatrTutorial.Data.EventStore;

using System.Net;
using System.Threading.Tasks;
using global::EventStore.ClientAPI;

public class EventStoreDbContext : IEventStoreDbContext {
    public async Task<IEventStoreConnection> GetConnection() {
        var connection = EventStoreConnection.Create(
            new IPEndPoint(IPAddress.Loopback, 1113),
            nameof(MediatrTutorial));

        await connection.ConnectAsync();

        return connection;
    }

    public async Task AppendToStreamAsync(params EventData[] events) {
        const string appName = nameof(MediatrTutorial);
        var connection = await GetConnection();

        await connection.AppendToStreamAsync(appName, ExpectedVersion.Any, events);
    }
}
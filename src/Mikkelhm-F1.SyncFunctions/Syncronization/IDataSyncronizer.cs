using System.Threading.Tasks;

namespace Mikkelhm_F1.SyncFunctions.Syncronization
{
    public interface IDataSyncronizer
    {
        Task SyncSeasons();
        Task SyncDrivers();
        Task SyncCircuits();
        Task SyncRaces();
    }
}

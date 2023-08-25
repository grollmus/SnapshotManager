using SnapshotManager.Core.TiaPortal;

namespace Grollmus.TiaPortal.Model.Specify;

internal class DbContainer : IGlobalDB
{
    private readonly GlobalDB _db;

    public DbContainer(GlobalDB globalDb)
    {
        _db = globalDb;
    }

    public ISnapshot GetSnapshot()
    {
        var interfaceSnapshot = _db.GetService<InterfaceSnapshot>();
        if (interfaceSnapshot == null)
            return null;

        return new Snapshot(interfaceSnapshot);
    }
}
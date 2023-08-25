using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model.Specific;

internal class DbContainer : IGlobalDB
{
    private readonly GlobalDB _db;

    public DbContainer(GlobalDB globalDb)
    {
        _db = globalDb;
    }

    public string Name => _db.Name;

    public ISnapshot GetSnapshot()
    {
        var interfaceSnapshot = _db.GetService<InterfaceSnapshot>();
        if (interfaceSnapshot == null)
            return null;

        return new Snapshot(interfaceSnapshot);
    }
}
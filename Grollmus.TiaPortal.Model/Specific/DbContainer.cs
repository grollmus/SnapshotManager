using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model.Specific;

internal class DbContainer : IGlobalDb
{
    private readonly GlobalDB _globalDb;

    public DbContainer(GlobalDB globalDb)
    {
        _globalDb = globalDb;
    }

    public string Name => _globalDb.Name;

    public ISnapshot GetSnapshot()
    {
        var interfaceSnapshot = _globalDb.GetService<InterfaceSnapshot>();
        return interfaceSnapshot == null ? null : new Snapshot(interfaceSnapshot);
    }
}
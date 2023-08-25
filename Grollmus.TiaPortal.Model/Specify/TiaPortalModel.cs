using System.Collections.Generic;
using System.Linq;
using SnapshotManager.Core.TiaPortal;

namespace Grollmus.TiaPortal.Model.Specify;

internal class TiaPortalModel : ITiaPortalModel
{
    public IEnumerable<GlobalDB> _globalDBs;

    public TiaPortalModel(IEnumerable<GlobalDB> globalDBs)
    {
        _globalDBs = globalDBs;
    }

    public IEnumerable<IGlobalDB> GetGlobalDBs()
    {
        return _globalDBs.Select(db => new DbContainer(db));
    }
}
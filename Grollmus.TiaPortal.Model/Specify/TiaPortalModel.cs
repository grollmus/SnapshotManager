using System.Collections.Generic;
using System.Linq;
using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model.Specify;

internal class TiaPortalModel : ITiaPortalModel
{
    private readonly IEnumerable<GlobalDB> _globalDBs;

    public TiaPortalModel(IEnumerable<GlobalDB> globalDBs)
    {
        _globalDBs = globalDBs;
    }

    public IEnumerable<IGlobalDB> GetGlobalDBs()
    {
        return _globalDBs.Select(db => new DbContainer(db));
    }
}
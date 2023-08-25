using System.Collections.Generic;
using Grollmus.TiaPortal.Model.Specify;
using SnapshotManager.Core.TiaPortal;

namespace Grollmus.TiaPortal.Model;

public class TiaPortalModelFactory
{
    public ITiaPortalModel GetModel(IEnumerable<GlobalDB> globalDBs)
    {
        return new TiaPortalModel(globalDBs);
    }
}
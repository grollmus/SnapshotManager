using SnapshotManager.Core.Snapshot;
using System.Collections.Generic;

namespace SnapshotManager.Core.TiaPortal
{
    public interface ITiaPortalModel
    {
        IEnumerable<IGlobalDB> GetGlobalDBs();
    }
}

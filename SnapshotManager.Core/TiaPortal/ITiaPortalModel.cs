using System.Collections.Generic;

namespace SnapshotManager.Core.TiaPortal;

public interface ITiaPortalModel
{
    IEnumerable<IGlobalDB> GetGlobalDBs();
}
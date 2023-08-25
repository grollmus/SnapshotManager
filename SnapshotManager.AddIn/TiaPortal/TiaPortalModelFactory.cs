using Siemens.Engineering.SW.Blocks;
using SnapshotManager.Core.TiaPortal;
using SnapshotManager.Core.TiaPortal.Specify;
using System.Collections.Generic;

namespace SnapshotManager.AddIn.TiaPortal
{
    public class TiaPortalModelFactory
    {
        public ITiaPortalModel GetModel(IEnumerable<GlobalDB> globalDBs)
        {
            return new TiaPortalModel(globalDBs);
        }
    }
}

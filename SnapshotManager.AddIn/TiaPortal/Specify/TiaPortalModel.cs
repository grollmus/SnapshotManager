using Siemens.Engineering.SW.Blocks;
using SnapshotManager.Core.Snapshot;
using System.Collections.Generic;
using System.Linq;

namespace SnapshotManager.Core.TiaPortal.Specify
{
    internal class TiaPortalModel : ITiaPortalModel
    {
        public TiaPortalModel(IEnumerable<GlobalDB> globalDBs)
        {
            _globalDBs = globalDBs;
        }

        public IEnumerable<GlobalDB> _globalDBs;

        public IEnumerable<IGlobalDB> GetGlobalDBs()
        {
            return _globalDBs.Select(db => new DbContainer(db));
        }
    }
}

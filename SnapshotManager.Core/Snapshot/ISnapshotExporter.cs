using SnapshotManager.Core.TiaPortal;
using System.Collections.Generic;

namespace SnapshotManager.Core.Snapshot
{
    public interface ISnapshotExporter
    {
        void Export(IEnumerable<ISnapshot> snapshots);
    }    
}
using System.Collections.Generic;
using SnapshotManager.Core.TiaPortal;

namespace SnapshotManager.Core.Snapshot;

public interface ISnapshotExporter
{
    void Export(IEnumerable<ISnapshot> snapshots);
}
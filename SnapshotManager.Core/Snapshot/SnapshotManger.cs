using SnapshotManager.Core.TiaPortal;
using System.Linq;

namespace SnapshotManager.Core.Snapshot;

public class SnapshotManager
{
    private readonly ITiaPortalModel _model;

    public SnapshotManager(ITiaPortalModel model)
    {
        _model = model;
    }

    public void SaveSnapshot(ISnapshotExporter exporter)
    {
        var snapshots = _model.GetGlobalDBs().Select(db => db.GetSnapshot());

        exporter.Export(snapshots);
    }
}
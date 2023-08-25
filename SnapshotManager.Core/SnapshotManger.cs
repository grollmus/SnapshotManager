using Grollmus.TiaPortal.Model;
using SnapshotManager.Core.Specific;

namespace SnapshotManager.Core;

public class SnapshotManager
{
    private readonly ITiaPortalModel _model;

    public SnapshotManager(ITiaPortalModel model)
    {
        _model = model;
    }

    public ISnapshotExporter GetExporter()
    {
        return new SnapshotExporter(_model);
    }
}
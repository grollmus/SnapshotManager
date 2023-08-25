using System.IO;
using System.Linq;
using Grollmus.TiaPortal.Model;

namespace SnapshotManager.Core.Specific;

public class SnapshotExporter : ISnapshotExporter
{
    private readonly FileInfo _targetFile;
    private readonly ITiaPortalModel _tiaPortalModel;

    public SnapshotExporter(ITiaPortalModel tiaPortalModel, string targetRootPath = @"c:\temp\tia-snapshot")
    {
        _tiaPortalModel = tiaPortalModel;
        _targetFile = new FileInfo(Path.Combine(targetRootPath, "MyInterfaceSnapshot.xml"));
    }

    public void ExportSnapshots()
    {
        var snapshots = _tiaPortalModel.GetGlobalDBs().Select(db => db.GetSnapshot());
        foreach (var snapshot in snapshots)
            snapshot.Export(_targetFile);
    }
}
using System.IO;
using System.Linq;
using Grollmus.TiaPortal.Model;

namespace SnapshotManager.Core.Specific;

internal class SnapshotToFileExporter : ISnapshotExporter
{
    private readonly FileInfo _targetFile;
    private readonly ITiaPortalModel _tiaPortalModel;

    public SnapshotToFileExporter(ITiaPortalModel tiaPortalModel, string targetRootPath = @"c:\temp\tia-snapshot")
    {
        _tiaPortalModel = tiaPortalModel;
        _targetFile = new FileInfo(Path.Combine(targetRootPath, "MyInterfaceSnapshot.xml"));
    }

    public int ExportSnapshots()
    {
        var successfulExportsCount = 0;

        var snapshots = _tiaPortalModel.GetGlobalDBs().Select(db => db.GetSnapshot()).Where(snap => snap != null);

        foreach (var snapshot in snapshots)
            if (snapshot.Export(_targetFile))
                successfulExportsCount++;

        return successfulExportsCount;
    }
}
using SnapshotManager.Core.TiaPortal;
using System.Collections.Generic;
using System.IO;

namespace SnapshotManager.Core.Snapshot;

public class SnapshotExporter : ISnapshotExporter
{
    public readonly FileInfo _targetFile;

    public SnapshotExporter(string targetRootPath = @"c:\temp\tia-snapshot")
    {
        _targetFile = new FileInfo(Path.Combine(targetRootPath, "MyInterfaceSnapshot.xml"));
    }  

    public void Export(IEnumerable<ISnapshot> snapshots)
    {
        foreach (var snapshot in snapshots)
        {
            snapshot.Export(_targetFile);
        }
    }
}

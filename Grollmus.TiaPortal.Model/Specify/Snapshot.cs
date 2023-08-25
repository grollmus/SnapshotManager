using System.IO;
using System.Runtime.Serialization;
using SnapshotManager.Core.TiaPortal;

namespace Grollmus.TiaPortal.Model.Specify;

internal class Snapshot : ISnapshot
{
    private InterfaceSnapshot _interfaceSnapshot;

    public Snapshot(InterfaceSnapshot interfaceSnapshot)
    {
        _interfaceSnapshot = interfaceSnapshot;
    }

    public void Export(FileInfo targetFile)
    {
        _interfaceSnapshot.Export(targetFile, ExportOptions.WithReadOnly);
    }
}
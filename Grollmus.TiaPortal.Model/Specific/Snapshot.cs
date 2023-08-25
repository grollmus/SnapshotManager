using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model.Specific;

internal class Snapshot : ISnapshot
{
    private readonly InterfaceSnapshot _interfaceSnapshot;

    public Snapshot(InterfaceSnapshot interfaceSnapshot)
    {
        _interfaceSnapshot = interfaceSnapshot;
    }

    public void Export(FileInfo targetFile)
    {
        _interfaceSnapshot.Export(targetFile, ExportOptions.WithReadOnly);
    }
}
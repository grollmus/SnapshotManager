using System;
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

    public bool Export(FileInfo targetFile)
    {
        try
        {
            _interfaceSnapshot.Export(targetFile, ExportOptions.WithReadOnly);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
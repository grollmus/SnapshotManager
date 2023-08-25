using Siemens.Engineering;
using Siemens.Engineering.SW.Blocks;
using SnapshotManager.Core.Snapshot;
using System.IO;

namespace SnapshotManager.Core.TiaPortal.Specify
{
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
}

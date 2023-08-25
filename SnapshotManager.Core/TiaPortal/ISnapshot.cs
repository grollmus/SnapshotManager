using System.IO;

namespace SnapshotManager.Core.TiaPortal;

public interface ISnapshot
{
    void Export(FileInfo targetFile);
}
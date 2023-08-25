using System.IO;

namespace Grollmus.TiaPortal.Model;

public interface ISnapshot
{
    bool Export(FileInfo targetFile);
}
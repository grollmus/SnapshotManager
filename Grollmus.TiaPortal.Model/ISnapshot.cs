using System.IO;

namespace Grollmus.TiaPortal.Model;

public interface ISnapshot
{
    void Export(FileInfo targetFile);
}
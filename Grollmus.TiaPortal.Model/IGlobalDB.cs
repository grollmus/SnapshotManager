namespace Grollmus.TiaPortal.Model;

public interface IGlobalDB
{
    string Name { get; }

    ISnapshot GetSnapshot();
}
namespace Grollmus.TiaPortal.Model;

public interface IGlobalDb
{
    string Name { get; }

    ISnapshot GetSnapshot();
}
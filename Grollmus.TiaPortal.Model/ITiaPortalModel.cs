using System.Collections.Generic;

namespace Grollmus.TiaPortal.Model;

public interface ITiaPortalModel
{
    IEnumerable<IGlobalDB> GetGlobalDBs();
}
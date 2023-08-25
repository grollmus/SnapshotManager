using System.Collections.Generic;
using Grollmus.TiaPortal.Model.Specify;
using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model;

public class TiaPortalModelFactory
{
    public ITiaPortalModel GetModel(IEnumerable<GlobalDB> globalDBs)
    {
        return new TiaPortalModel(globalDBs);
    }
}
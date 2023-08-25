using System.Collections.Generic;
using Grollmus.TiaPortal.Model.Specific;
using Siemens.Engineering.SW.Blocks;

namespace Grollmus.TiaPortal.Model;

public class TiaPortalModelFactory
{
    public ITiaPortalModel GetModel(IEnumerable<GlobalDB> globalDBs) => new TiaPortalModel(globalDBs);
}
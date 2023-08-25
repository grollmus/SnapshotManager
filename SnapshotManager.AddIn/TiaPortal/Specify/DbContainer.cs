using Siemens.Engineering.SW.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapshotManager.Core.TiaPortal.Specify
{
    internal class DbContainer: IGlobalDB
    {
        private readonly GlobalDB _db;

        public DbContainer(GlobalDB globalDb)
        {
            _db = globalDb;
        }

        public ISnapshot GetSnapshot()
        {
            var interfaceSnapshot = _db.GetService<InterfaceSnapshot>();
            if (interfaceSnapshot == null)
                return null;

            return new Snapshot(interfaceSnapshot);
        }
    }
}

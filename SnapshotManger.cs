using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.Blocks.Interface;
using System.IO;

namespace SnapshotManager
{
    public class SnapshotManager
    {
        public void saveSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            foreach(GlobalDB curSelection in menuSelectionProvider.GetSelection<GlobalDB>()) {
                InterfaceSnapshot interfaceSnapshot =  curSelection.GetService<InterfaceSnapshot>();
                string path = @"C:\temp\MyInterfaceSnapshot.xml";
                if(File.Exists(path))
                {
                    File.Delete(path);
                }
                interfaceSnapshot.Export(new FileInfo("C:\\temp\\MyInterfaceSnapshot.xml"), ExportOptions.WithReadOnly);                
            }
        }

        public void restoreSnapshot()
        {

        }
    }
}

using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;
using SnapshotManager.Core.Snapshot;
using SnapshotManager.UI;
using System.Windows;
using Grollmus.TiaPortal.Model;

namespace SnapshotManager
{
    public class SnapshotManagerContextMenuAddIn : ContextMenuAddIn
    {
        /// <summary>
        /// The instance of TIA Portal in which the Add-In works.
        /// <para>Enables Add-In to interact with TIA Portal.</para>
        /// </summary>
        readonly TiaPortal m_TiaPortal;

        /// <summary>
        /// The display name of the Add-In.
        /// TODO: Enter your display name here
        /// </summary>
        private const string s_DisplayNameOfAddIn = "Snapshot Manager";

        /// <summary>
        /// The constructor of the SnapshotManagerContextMenuAddIn.
        /// Creates an instance of the class SnapshotManagerContextMenuAddIn.
        /// <para>- Called from SnapshotManagerAddInProvider when the first right-click is performed in TIA Portal.</para>
        /// <para>- The base class' constructor of ContextMenuAddIn will also be executed.</para>
        /// </summary>
        /// <param name="tiaPortal">
        /// Represents the instance of TIA Portal in which the Add-In will work.
        /// </param>
        public SnapshotManagerContextMenuAddIn(TiaPortal tiaPortal) : base(s_DisplayNameOfAddIn)
        {
            m_TiaPortal = tiaPortal;
        }

        /// <summary>
        /// The method is provided to create a submenu of the Add-In's context menu item.
        /// Called when a mouse-over is performed on the Add-In's context menu item.
        /// </summary>
        /// <param name="addInRootSubmenu">
        /// Submenu of the Add-In's context menu item.
        /// </param>
        /// <example>
        /// ActionItems can be created with or without a checkbox or a radiobutton.
        /// In this example, only simple ActionItems will be created, which will start the Add-In program code.
        /// </example>
        protected override void BuildContextMenuItems(ContextMenuAddInRoot addInRootSubmenu)
        {
            /* Method addInRootSubmenu.Items.AddActionItem
            *  will create a new context menu item with specified text
            *  - its 1st input parameter is the label text of the context menu item;
            *  - its 2nd input parameter is the delegate, which will be executed when the context menu item is clicked;
            *  - its 3rd input parameter is the delegate, which will be executed when the mouse is over the context menu item;
            *  - its generic type parameter (inside the  "<>"-brackets) is the type of AddActionItem,
            *    e.g. AddActionItem<DeviceItem> will create a context menu item that will be displayed on a right-click on a DeviceItem,
            *    whereas AddActionItem<Project> will create a context menu item that will be displayed on a right-click on the project name.
            */
            addInRootSubmenu.Items.AddActionItem<GlobalDB>("Save Snapshot", OnDoSaveSnapshot, OnCanSaveSnapshot);
            addInRootSubmenu.Items.AddActionItem<GlobalDB>("Restore Snapshot", OnDoRestoreSnapshot, OnCanRestoreSnapshot);
            addInRootSubmenu.Items.AddActionItem<GlobalDB>("Settings", OnDoShowSettings, OnCanShowSettings);
        }

        /// <summary>
        /// The method contains the program code of the TIA Add-In.
        /// Called when the context menu item 'Save Snapshot' (added in the body of the method BuildContextMenuItems) is chosen.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB, because there are only snapshots from global datablock possible)
        /// </param>
        private void OnDoSaveSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            var factory = new TiaPortalModelFactory();
            var model = factory.GetModel(menuSelectionProvider.GetSelection<GlobalDB>());

            var exporter = new SnapshotExporter();
            var snapshotManager = new Core.Snapshot.SnapshotManager(model);

            snapshotManager.SaveSnapshot(exporter);
        }

        /// <summary>
        /// Called when mouse is over the context menu item 'Save Snapshot'.
        /// The returned value will be used to enable or disable it.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB)
        /// </param>
        private MenuStatus OnCanSaveSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            // TODO: Change the code here
            // MenuStatus
            //  Enabled  = Visible
            //  Disabled = Visible but not executable
            //  Hidden   = Item will not be shown

            return MenuStatus.Enabled;
        }

        /// <summary>
        /// The method contains the program code of the Add-In.
        /// Called when the context menu item 'Restore Snapshot' (added in the body of the method BuildContextMenuItems) is chosen.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB)
        /// </param>
        private void OnDoRestoreSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            SnapshotManagerOld snapshotManager = new SnapshotManagerOld();
            snapshotManager.restoreSnapshot(menuSelectionProvider);
        }

        /// <summary>
        /// Called when mouse is over the context menu item 'Restore Snapshot'.
        /// The returned value will be used to enable or disable it.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB)
        /// </param>
        private MenuStatus OnCanRestoreSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            // TODO: Change the code here
            // MenuStatus
            //  Enabled  = Visible
            //  Disabled = Visible but not executable
            //  Hidden   = Item will not be shown
            return MenuStatus.Enabled;
        }

        /// <summary>
        /// The method contains the program code of the Add-In.
        /// Called when the context menu item 'Settings' (added in the body of the method BuildContextMenuItems) is chosen.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB)
        /// </param>
        private void OnDoShowSettings(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            var factory = new SnapshotManagerUiFactory();
            var content = factory.GetSettingsUi();

            var window = new Window { Content = content };

            window.ShowDialog();
        }

        /// <summary>
        /// Called when mouse is over the context menu item 'Restore Snapshot'.
        /// The returned value will be used to enable or disable it.
        /// </summary>
        /// <param name="menuSelectionProvider">
        /// Here, the same generic type as was used in addInRootSubmenu.Items.AddActionItem must be used
        /// (here it has to be GlobalDB)
        /// </param>
        private MenuStatus OnCanShowSettings(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
            // TODO: Change the code here
            // MenuStatus
            //  Enabled  = Visible
            //  Disabled = Visible but not executable
            //  Hidden   = Item will not be shown
            return MenuStatus.Enabled;
        }
    }
}

using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.Blocks.Interface;
using Siemens.Engineering.SW;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using SnapshotManager;

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
            SnapshotManager snapshotManager = new SnapshotManager();
            snapshotManager.saveSnapshot(menuSelectionProvider);
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
            // TODO: Change the code here
            // Program of AddIn
            foreach(GlobalDB curSelection in menuSelectionProvider.GetSelection<GlobalDB>()) {
                XmlDocument snapShotXML = new XmlDocument();
                snapShotXML.Load("C:\\temp\\MyInterfaceSnapshot.xml");
                XmlNode root = snapShotXML.DocumentElement;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(snapShotXML.NameTable);
                nsmgr.AddNamespace("sv", "http://www.siemens.com/automation/Openness/SW/Interface/Snapshot/v1");

                XmlNodeList savedXmlValues = root.SelectNodes("descendant::sv:Value[@Path]", nsmgr);
                foreach(XmlNode savedXmlvalue in savedXmlValues)
                {
                    Debug.WriteLine(savedXmlvalue.InnerText);
                    XmlAttributeCollection attributes = savedXmlvalue.Attributes;
                    foreach(XmlAttribute attribute in attributes)
                    {
                        if(attribute.Name == "Path") 
                        {
                            PlcBlockInterface plcBlockInterface = curSelection.Interface;
                            MemberComposition members = plcBlockInterface.Members;
                            Member member = members.Find(attribute.InnerText);   
                            member.SetAttribute("StartValue", savedXmlvalue.InnerText);
                        }
                        
                    }
                }                
            }
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
    }
}

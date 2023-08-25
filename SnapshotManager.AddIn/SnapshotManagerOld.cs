using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.Blocks.Interface;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace SnapshotManager
{
    public class SnapshotManagerOld
    {
        public void restoreSnapshot(MenuSelectionProvider<GlobalDB> menuSelectionProvider)
        {
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
    }
}
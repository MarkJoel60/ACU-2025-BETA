// Decompiled with JetBrains decompiler
// Type: PX.Data.Gi_v6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model.ImportExport;
using PX.DbServices.Model.ImportExport.Upgrade;
using PX.DbServices.Model.Schema;
using System;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class Gi_v6 : XmlEntityUpgrader
{
  private const string _newRelations = "  <relations format-version=\"3\" relations-version=\"20160530\" main-table=\"GIDesign\">\r\n    <link from = \"GIFilter (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIGroupBy (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIMassAction (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIMassUpdateField (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GINavigationScreen (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GINavigationParameter (DesignID, ScreenID)\" to=\"GINavigationScreen (DesignID, ScreenID)\" />\r\n    <link from = \"GIOn (DesignID, RelationNbr)\" to=\"GIRelation (DesignID, LineNbr)\" />\r\n    <link from = \"GIRecordDefault (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIRelation (DesignID, ParentTable)\" to=\"GITable (DesignID, Alias)\" />\r\n    <link from = \"GIRelation (DesignID, ChildTable)\" to=\"GITable (DesignID, Alias)\" />\r\n    <link from = \"GIResult (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIResult (ObjectName, DesignID)\" to=\"GITable (Alias, DesignID)\" />\r\n    <link from = \"GISort (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GITable (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIWhere (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"SiteMap (Url)\" to=\"GIDesign (DesignID)\" type=\"WeakByUrl\" linkname=\"toDesignById\" baseurl=\"~/GenericInquiry/GenericInquiry.aspx\" paramnames=\"id\" />\r\n    <link from = \"SiteMap (Url)\" to=\"GIDesign (Name)\" type=\"WeakByUrl\" linkname=\"toDesignByName\" baseurl=\"~/GenericInquiry/GenericInquiry.aspx\" />\r\n\t<link from = \"ListEntryPoint(ListScreenID)\" to=\"SiteMap(ScreenID)\" type=\"Weak\" />\r\n    <link from = \"SiteMap (ScreenID)\" to=\"GIDesign (PrimaryScreenIDNew)\" linkname=\"to1Screen\" />\r\n    <link from = \"SiteMap (NodeID)\" to=\"SiteMap (ParentID)\" type=\"WeakToParent\" recursiveNesting=\"yes\" />\r\n    <link from = \"GIDesign (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIFilter (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIGroupBy (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIOn (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIRelation (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIResult (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GISort (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GITable (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIWhere (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n  </relations>";

  public string EntityNameToUpgrade => "GIDesign";

  public int MaxVersionToUpgradeFrom => 20160530;

  public int OrderNumber => 100;

  public bool Upgrade(XmlEntityBeingUpgraded entity)
  {
    if (!this.UpgradeData(entity.Data))
      return false;
    entity.Template = ExportTemplate.XmlSerializer.ReadFrom(XElement.Parse("  <relations format-version=\"3\" relations-version=\"20160530\" main-table=\"GIDesign\">\r\n    <link from = \"GIFilter (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIGroupBy (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIMassAction (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIMassUpdateField (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GINavigationScreen (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GINavigationParameter (DesignID, ScreenID)\" to=\"GINavigationScreen (DesignID, ScreenID)\" />\r\n    <link from = \"GIOn (DesignID, RelationNbr)\" to=\"GIRelation (DesignID, LineNbr)\" />\r\n    <link from = \"GIRecordDefault (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIRelation (DesignID, ParentTable)\" to=\"GITable (DesignID, Alias)\" />\r\n    <link from = \"GIRelation (DesignID, ChildTable)\" to=\"GITable (DesignID, Alias)\" />\r\n    <link from = \"GIResult (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIResult (ObjectName, DesignID)\" to=\"GITable (Alias, DesignID)\" />\r\n    <link from = \"GISort (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GITable (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"GIWhere (DesignID)\" to=\"GIDesign (DesignID)\" />\r\n    <link from = \"SiteMap (Url)\" to=\"GIDesign (DesignID)\" type=\"WeakByUrl\" linkname=\"toDesignById\" baseurl=\"~/GenericInquiry/GenericInquiry.aspx\" paramnames=\"id\" />\r\n    <link from = \"SiteMap (Url)\" to=\"GIDesign (Name)\" type=\"WeakByUrl\" linkname=\"toDesignByName\" baseurl=\"~/GenericInquiry/GenericInquiry.aspx\" />\r\n\t<link from = \"ListEntryPoint(ListScreenID)\" to=\"SiteMap(ScreenID)\" type=\"Weak\" />\r\n    <link from = \"SiteMap (ScreenID)\" to=\"GIDesign (PrimaryScreenIDNew)\" linkname=\"to1Screen\" />\r\n    <link from = \"SiteMap (NodeID)\" to=\"SiteMap (ParentID)\" type=\"WeakToParent\" recursiveNesting=\"yes\" />\r\n    <link from = \"GIDesign (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIFilter (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIGroupBy (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIOn (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIRelation (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIResult (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GISort (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GITable (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n    <link from = \"GIWhere (NoteID)\" to=\"Note (NoteID)\" type=\"Note\" />\r\n  </relations>"));
    entity.Layout = SchemaXmlLayout.fromExportTemplate(entity.Template);
    return true;
  }

  private bool UpgradeData(XElement data)
  {
    foreach (XElement element in data.Elements((XName) "GIDesign"))
    {
      XElement xelement1 = element.Elements((XName) "row").FirstOrDefault<XElement>() ?? element;
      XElement xelement2 = xelement1.Elements((XName) "SiteMap").FirstOrDefault<XElement>((Func<XElement, bool>) (e =>
      {
        XAttribute xattribute = e.Attribute((XName) "linkname");
        return xattribute == null || xattribute.Value == "toDesign";
      }));
      xelement2?.SetAttributeValue((XName) "linkname", (object) "toDesignById");
      XElement xelement3 = xelement2?.Element((XName) "row");
      xelement3?.SetAttributeValue((XName) "NodeID", (object) xelement1.Attribute((XName) "DesignID").Value);
      if (xelement2 == null)
      {
        XElement content1 = xelement1.Elements((XName) "SiteMap").FirstOrDefault<XElement>();
        if (content1 != null)
        {
          content1.SetAttributeValue((XName) "NodeID", (object) xelement1.Attribute((XName) "DesignID").Value);
          XElement content2 = new XElement((XName) "SiteMap");
          content2.SetAttributeValue((XName) "linkname", (object) "toDesignById");
          content1.Remove();
          content1.Name = (XName) "row";
          content2.Add((object) content1);
          xelement1.Add((object) content2);
          xelement3 = content1;
        }
      }
      XAttribute xattribute1 = xelement1.Attribute((XName) "PrimaryScreenID");
      if (xattribute1 != null)
      {
        string key = xattribute1.Value;
        XElement xelement4 = xelement1.Elements((XName) "SiteMap").FirstOrDefault<XElement>((Func<XElement, bool>) (e => e.Attribute((XName) "linkname")?.Value == "to1Screen"))?.Element((XName) "row");
        string screenId = xelement4?.Attribute((XName) "ScreenID")?.Value;
        if (!string.IsNullOrEmpty(key) && string.IsNullOrEmpty(screenId))
          screenId = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(key)?.ScreenID;
        if (!string.IsNullOrEmpty(screenId))
        {
          xelement4?.SetAttributeValue((XName) "NodeID", (object) key);
          xelement4?.SetAttributeValue((XName) "ScreenID", (object) null);
          xelement1.SetAttributeValue((XName) "PrimaryScreenIDNew", (object) screenId);
          xelement1.SetAttributeValue((XName) "PrimaryScreenID", (object) null);
          XElement content3 = new XElement((XName) "GINavigationScreen");
          content3.SetAttributeValue((XName) "ScreenID", (object) screenId);
          content3.SetAttributeValue((XName) "WindowMode", (object) "I");
          foreach (XElement content4 in xelement1.Elements((XName) "GINavigationParameter").ToArray<XElement>())
          {
            content3.Add((object) content4);
            content4.Remove();
          }
          xelement1.Add((object) content3);
          XElement content5 = xelement1.Elements((XName) "ListEntryPoint").FirstOrDefault<XElement>();
          if (content5 != null)
          {
            content5.SetAttributeValue((XName) "EntryScreenID", (object) screenId);
            content5.SetAttributeValue((XName) "EntryScreenNodeID", (object) null);
            xelement3?.Add((object) content5);
            content5.Remove();
          }
          foreach (XElement descendant in xelement1.Descendants((XName) "GIResult"))
          {
            descendant.SetAttributeValue((XName) "SuppressNav", (object) null);
            descendant.SetAttributeValue((XName) "DefaultNav", (object) "1");
          }
        }
      }
    }
    return true;
  }
}

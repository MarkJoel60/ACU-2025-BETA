// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.RMColumnUpgrader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model.ImportExport.Upgrade;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.Reports;

public class RMColumnUpgrader : XmlEntityUpgrader
{
  public string EntityNameToUpgrade => "RMReport";

  public int MaxVersionToUpgradeFrom => 20200317;

  public int OrderNumber => 100;

  public bool Upgrade(XmlEntityBeingUpgraded entity)
  {
    bool flag = false;
    foreach (XElement xpathSelectElement in entity.Data.XPathSelectElements("RMColumnSet//*[@ColumnType and not(@CellEvalOrder)]"))
    {
      xpathSelectElement.Add((object) new XAttribute((XName) "CellEvalOrder", xpathSelectElement.Attribute((XName) "ColumnType")?.Value == "1" ? (object) "1" : (object) "0"));
      flag = true;
    }
    return flag;
  }
}

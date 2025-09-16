// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ChildTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Data.BQL;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.Maintenance.GI;

public class ChildTable : LinkedTable
{
  [PXString]
  [PXUIField(DisplayName = "Description", IsReadOnly = true)]
  public override 
  #nullable disable
  string Description
  {
    get
    {
      if (string.IsNullOrEmpty(this.FullName))
        return (string) null;
      System.Type type = PXBuildManager.GetType(this.FullName, false);
      return (object) type == null ? (string) null : XmlDocsExtensions.GetXmlDocsSummary(type, true);
    }
  }

  public new class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ChildTable.fullName>
  {
  }
}

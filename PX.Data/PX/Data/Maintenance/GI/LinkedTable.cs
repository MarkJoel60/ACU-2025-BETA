// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.LinkedTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Data.BQL;
using System;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.Maintenance.GI;

[Serializable]
public class LinkedTable : PXTablesSelectorAttribute.SingleTable
{
  [PXString]
  [PXUIField(DisplayName = "Description", IsReadOnly = true, Visible = false)]
  public virtual 
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

  [PXString(256 /*0x0100*/, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Linked To Fields", IsReadOnly = true)]
  public string LinkedToFields { get; set; }

  [PXString(256 /*0x0100*/, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Linked From", IsReadOnly = true)]
  public string LinkedFrom { get; set; }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkedTable.description>
  {
  }

  public abstract class linkedToFields : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkedTable.linkedToFields>
  {
  }

  public abstract class linkedFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkedTable.linkedFrom>
  {
  }
}

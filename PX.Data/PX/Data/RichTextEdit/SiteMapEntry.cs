// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.SiteMapEntry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.RichTextEdit;

/// <exclude />
[Serializable]
public class SiteMapEntry : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "ScreenID", Visible = true)]
  public 
  #nullable disable
  string ScreenID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Title", Visible = true)]
  public string Title { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Module", Visible = true)]
  public string Module { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public string Url { get; set; }

  public SiteMapEntry(PXSiteMapNode node)
  {
    this.Title = node.Title;
    this.ScreenID = node.ScreenID;
    PXSiteMapNode parentNode = node.ParentNode;
    int num = 0;
    while (parentNode != null && parentNode.ParentNode != null && parentNode.ParentNode.ParentNode != null && num++ < 1000)
      parentNode = parentNode.ParentNode;
    this.Module = parentNode == null ? string.Empty : parentNode.Title;
    this.Url = node.Url;
  }

  public SiteMapEntry()
  {
  }

  /// <exclude />
  public abstract class screenid : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapEntry.screenid>
  {
  }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapEntry.title>
  {
  }

  /// <exclude />
  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapEntry.module>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapEntry.url>
  {
  }
}

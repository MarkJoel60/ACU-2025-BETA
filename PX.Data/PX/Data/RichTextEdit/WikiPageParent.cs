// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.WikiPageParent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.RichTextEdit;

[Serializable]
public class WikiPageParent : WikiPage2
{
  [PXDefault]
  [PXDBString(BqlField = typeof (WikiPageLanguage.title))]
  [PXUIField(DisplayName = "Module", Visibility = PXUIVisibility.Visible)]
  public override 
  #nullable disable
  string Title
  {
    get => base.Title;
    set => base.Title = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageParent.pageID>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageParent.title>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageParent.parentUID>
  {
  }
}

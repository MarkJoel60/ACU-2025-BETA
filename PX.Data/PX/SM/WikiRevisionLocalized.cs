// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiRevisionLocalized
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiRevisionLocalized : WikiRevision
{
  [PXDBGuid(false, IsKey = true)]
  public override Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(IsKey = true)]
  [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
  public override 
  #nullable disable
  string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevisionLocalized.pageID>
  {
  }

  public new abstract class language : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiRevisionLocalized.language>
  {
  }
}

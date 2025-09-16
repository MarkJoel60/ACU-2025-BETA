// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageLink
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
public class WikiPageLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Language;
  protected int? _PageRevisionID;
  protected Guid? _LinkID;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(IsKey = true)]
  [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Version ID")]
  public virtual int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? LinkID
  {
    get => this._LinkID;
    set => this._LinkID = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageLink.pageID>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageLink.language>
  {
  }

  public abstract class pageRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageLink.pageRevisionID>
  {
  }

  public abstract class linkID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageLink.linkID>
  {
  }
}

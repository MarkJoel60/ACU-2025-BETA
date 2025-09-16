// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiRevisionTag
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
public class WikiRevisionTag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _WikiID;
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Language;
  protected int? _PageRevisionID;
  protected int? _TagID;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiPage.wikiID))]
  public virtual Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiPage.pageID))]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(IsKey = true)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<WikiTag.tagID, Where<WikiTag.wikiID, Equal<Current<WikiRevisionTag.wikiID>>>>), SubstituteKey = typeof (WikiTag.description))]
  [PXUIField(DisplayName = "Tag")]
  public virtual int? TagID
  {
    get => this._TagID;
    set => this._TagID = value;
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevisionTag.wikiID>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevisionTag.pageID>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiRevisionTag.language>
  {
  }

  public abstract class pageRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiRevisionTag.pageRevisionID>
  {
  }

  public abstract class tagID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiRevisionTag.tagID>
  {
  }
}

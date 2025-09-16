// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageFilter
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
public class WikiPageFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _WikiID;
  protected 
  #nullable disable
  string _Wiki;
  protected string _Parent;
  protected Guid? _PageID;
  protected string _Art;
  protected string _Language;
  protected int? _PageRevisionID;
  protected bool? _ShowRevisions;
  protected bool? _PrefetchProvider;
  protected bool? _PrefetchSiteMap;
  protected bool? _RefreshTree;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Wiki Name")]
  [PXSelector(typeof (Search<WikiPage.pageID, Where<WikiPage.wikiID, Equal<emptyGuid>>>))]
  public virtual Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Wiki Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Wiki
  {
    get => this._Wiki;
    set => this._Wiki = value;
  }

  [PXDBString(IsUnicode = true)]
  public virtual string Parent
  {
    get => this._Parent;
    set => this._Parent = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Wiki Page Key")]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Page Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Art
  {
    get => this._Art;
    set => this._Art = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBInt]
  public virtual int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Show Versions")]
  public virtual bool? ShowRevisions
  {
    get => this._ShowRevisions;
    set => this._ShowRevisions = value;
  }

  [PXDBBool]
  public virtual bool? PrefetchProvider
  {
    get => this._PrefetchProvider;
    set => this._PrefetchProvider = value;
  }

  [PXDBBool]
  public virtual bool? PrefetchSiteMap
  {
    get => this._PrefetchSiteMap;
    set => this._PrefetchSiteMap = value;
  }

  [PXDBBool]
  public virtual bool? RefreshTree
  {
    get => this._RefreshTree;
    set => this._RefreshTree = value;
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageFilter.wikiID>
  {
  }

  public abstract class wiki : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageFilter.wiki>
  {
  }

  public abstract class parent : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageFilter.parent>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageFilter.pageID>
  {
  }

  public abstract class art : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageFilter.art>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageFilter.language>
  {
  }

  public abstract class pageRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageFilter.pageRevisionID>
  {
  }

  public abstract class showRevisions : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageFilter.showRevisions>
  {
  }

  public abstract class prefetchProvider : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WikiPageFilter.prefetchProvider>
  {
  }

  public abstract class prefetchSiteMap : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WikiPageFilter.prefetchSiteMap>
  {
  }

  public abstract class refreshTree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageFilter.refreshTree>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageWithCurrentLanguage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select2<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<WikiPage.pageID>, And<WikiPageLanguage.language, Equal<defaultCulture>>>, LeftJoin<WikiPageCurrentLanguage, On<WikiPageCurrentLanguage.pageID, Equal<WikiPage.pageID>, And<WikiPageCurrentLanguage.language, Equal<currentCulture>>>>>, Where<WikiPage.pageID, Equal<WikiPage.pageID>>>), Persistent = false)]
[Serializable]
public class WikiPageWithCurrentLanguage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected Guid? _WikiID;
  protected int? _ArticleType;
  protected Guid? _ParentUID;
  protected double? _Number;
  protected bool? _Folder;
  protected 
  #nullable disable
  string _Name;
  protected string _Summary;
  protected string _SummaryLoc;

  [PXDBGuid(false, IsKey = true, BqlTable = typeof (WikiPage))]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PageID { get; set; }

  [PXDBGuid(false, BqlTable = typeof (WikiPage))]
  [PXUIField(DisplayName = "Wiki ID")]
  public virtual Guid? WikiID { get; set; }

  [PXDBInt(BqlTable = typeof (WikiPage))]
  [PXDefault]
  public virtual int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXDBGuid(false, BqlTable = typeof (WikiPage))]
  [PXUIField(DisplayName = "Parent Folder")]
  public virtual Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBDouble(BqlTable = typeof (WikiPage))]
  [PXDefault(0.0)]
  public virtual double? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  [PXDBBool(BqlTable = typeof (WikiPage))]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Folder")]
  public virtual bool? Folder
  {
    get => this._Folder;
    set => this._Folder = value;
  }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = true, BqlTable = typeof (WikiPage))]
  [PXUIField(DisplayName = "ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(BqlField = typeof (WikiPageLanguage.title))]
  public virtual string Title { get; set; }

  [PXDBString(BqlField = typeof (WikiPageLanguage.summary), IsUnicode = true)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string Summary
  {
    get => this._Summary;
    set => this._Summary = value;
  }

  [PXDBString(BqlField = typeof (WikiPageCurrentLanguage.title))]
  public virtual string TitleLoc { get; set; }

  [PXDBString(BqlField = typeof (WikiPageCurrentLanguage.summary), IsUnicode = true)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string SummaryLoc
  {
    get => this._SummaryLoc;
    set => this._SummaryLoc = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageWithCurrentLanguage.pageID>
  {
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageWithCurrentLanguage.wikiID>
  {
  }

  public abstract class articleType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.articleType>
  {
  }

  public abstract class parentUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.parentUID>
  {
  }

  public abstract class number : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.number>
  {
  }

  public abstract class folder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageWithCurrentLanguage.folder>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageWithCurrentLanguage.name>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageWithCurrentLanguage.title>
  {
  }

  public abstract class summary : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.summary>
  {
  }

  public abstract class titleLoc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.titleLoc>
  {
  }

  public abstract class summaryLoc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageWithCurrentLanguage.summaryLoc>
  {
  }
}

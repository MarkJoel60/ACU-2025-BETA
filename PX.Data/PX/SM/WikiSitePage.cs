// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSitePage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXTable(new System.Type[] {typeof (WikiPage.pageID)})]
[Serializable]
public class WikiSitePage : WikiPage
{
  protected bool? _Hidden;
  protected bool? _Indexed;
  protected bool? _Expanded;
  protected 
  #nullable disable
  string _FormUrl;
  protected bool? _HideMenu;
  protected string _Description;
  protected bool? _Secure;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Folder")]
  [PXSelector(typeof (Search<WikiPageSimple.pageID, Where<WikiPageSimple.wikiID, Equal<Current<WikiPage.wikiID>>, Or<WikiPageSimple.pageID, Equal<Current<WikiPage.wikiID>>>>>), SubstituteKey = typeof (WikiPageSimple.name))]
  public override Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBInt]
  [PXDefault(13)]
  public override int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hidden From Menu")]
  public virtual bool? Hidden
  {
    get => this._Hidden;
    set => this._Hidden = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Indexed")]
  public virtual bool? Indexed
  {
    get => this._Indexed;
    set => this._Indexed = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Expanded")]
  public virtual bool? Expanded
  {
    get => this._Expanded;
    set => this._Expanded = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Form URL")]
  public virtual string FormUrl
  {
    get => this._FormUrl;
    set => this._FormUrl = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hide Menu")]
  public virtual bool? HideMenu
  {
    get => this._HideMenu;
    set => this._HideMenu = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Title")]
  public override string Summary
  {
    get => this._Summary;
    set => this._Summary = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Secured")]
  public virtual bool? Secure
  {
    get => this._Secure;
    set => this._Secure = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiSitePage.pageID>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiSitePage.parentUID>
  {
  }

  public new abstract class articleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiSitePage.articleType>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSitePage.hidden>
  {
  }

  public abstract class indexed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSitePage.indexed>
  {
  }

  public abstract class expanded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSitePage.expanded>
  {
  }

  public abstract class formUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSitePage.formUrl>
  {
  }

  public abstract class hideMenu : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSitePage.hideMenu>
  {
  }

  public new abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSitePage.summary>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSitePage.description>
  {
  }

  public abstract class secure : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSitePage.secure>
  {
  }
}

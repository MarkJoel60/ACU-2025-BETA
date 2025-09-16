// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiDescriptor
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
public class WikiDescriptor : WikiPage
{
  protected int? _WikiArticleType;
  protected int? _SPWikiArticleType;
  protected 
  #nullable disable
  string _UrlEdit;
  protected Guid? _DeletedID;
  private bool? _HoldEntry;
  protected int? _SiteMapTagID;
  protected bool? _RequestApproval;
  protected Guid? _CssID;
  protected Guid? _CssPrintID;
  protected string _WikiTitle;
  protected bool? _IsActive;
  protected double? _Position;
  protected string _WikiDescription;
  protected string _DefaultIcon;

  [PXDBGuid(false)]
  [PXWikiSelector]
  [PXUIField(DisplayName = "Page ID")]
  public override Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Wiki ID")]
  public override Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBString(IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXWikiSelector(typeof (WikiDescriptor.name))]
  public override string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBInt]
  [PXDefault(10)]
  [PX.SM.WikiArticleType.List]
  [PXUIField(DisplayName = "Article Type")]
  public virtual int? WikiArticleType
  {
    get => this._WikiArticleType;
    set => this._WikiArticleType = value;
  }

  [PXDBInt]
  [PXDefault(10)]
  [PX.SM.WikiArticleType.List]
  [PXUIField(DisplayName = "Article Type")]
  public virtual int? SPWikiArticleType
  {
    get => this._SPWikiArticleType;
    set => this._SPWikiArticleType = value;
  }

  [PXDBString(InputMask = "")]
  public virtual string UrlEdit
  {
    get => this._UrlEdit;
    set => this._UrlEdit = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Public Virtual Path")]
  public virtual string PubVirtualPath { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? DeletedID
  {
    get => this._DeletedID;
    set => this._DeletedID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public override int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXDBDouble]
  [PXDefault(0.0)]
  public override double? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Null)]
  public override bool? Versioned
  {
    get => this._Versioned;
    set => this._Versioned = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Hold on Edit")]
  public bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<WikiTag.tagID, Where<WikiTag.wikiID, Equal<Current<WikiDescriptor.pageID>>>>), SubstituteKey = typeof (WikiTag.description))]
  [PXUIField(DisplayName = "Default Site Map Tag")]
  public virtual int? SiteMapTagID
  {
    get => this._SiteMapTagID;
    set => this._SiteMapTagID = value;
  }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Null)]
  public override bool? Folder
  {
    get => this._Folder;
    set => this._Folder = value;
  }

  [PXString]
  public override string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXString]
  public override string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? RequestApproval
  {
    get => this._RequestApproval;
    set => this._RequestApproval = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Style")]
  [PXSelector(typeof (WikiCss.cssID), SubstituteKey = typeof (WikiCss.name))]
  public virtual Guid? CssID
  {
    get => this._CssID;
    set => this._CssID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Print Style")]
  [PXSelector(typeof (WikiCss.cssID), SubstituteKey = typeof (WikiCss.name))]
  public virtual Guid? CssPrintID
  {
    get => this._CssPrintID;
    set => this._CssPrintID = value;
  }

  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXGuid]
  [PXUIField(DisplayName = "Site Map Location")]
  [PXWikiSiteMapLocation]
  public Guid? SitemapParent { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Site Map Title", Visibility = PXUIVisibility.Invisible)]
  public string SitemapTitle { get; set; }

  [PXDefault]
  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Title")]
  public string WikiTitle
  {
    get
    {
      if (this._WikiTitle == null && !string.IsNullOrEmpty(this.Name))
      {
        PXWikiMapNode siteMapNode = PXSiteMap.WikiProvider.FindSiteMapNode("", this.Name);
        if (siteMapNode != null)
          this._WikiTitle = siteMapNode.Title;
      }
      return this._WikiTitle;
    }
    set
    {
      this._WikiTitle = value;
      this.Title = value;
    }
  }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Show on Help Dashboard")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBDouble]
  [PXUIField(DisplayName = "Sequence")]
  public virtual double? Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Wiki Description")]
  public string WikiDescription
  {
    get
    {
      if (this._WikiDescription == null)
      {
        PXWikiMapNode siteMapNode = PXSiteMap.WikiProvider.FindSiteMapNode("", this.Name);
        if (siteMapNode != null)
          this._WikiDescription = siteMapNode.Summary;
      }
      return this._WikiDescription;
    }
    set
    {
      this._WikiDescription = value;
      this._Summary = value;
    }
  }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Section")]
  [WikiCategory.List]
  public virtual string Category { get; set; }

  [PXDBString(1000, InputMask = "")]
  [PXUIField(DisplayName = "Default Article")]
  [PXWikiDefaultUrl]
  public string DefaultUrl { get; set; }

  [PXDBString(IsUnicode = false)]
  [PXUIField(DisplayName = "Default Icon")]
  public string DefaultIcon
  {
    get => this._DefaultIcon;
    set => this._DefaultIcon = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.pageID>
  {
  }

  public new abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.wikiID>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.name>
  {
  }

  public abstract class wikiArticleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiDescriptor.wikiArticleType>
  {
  }

  public abstract class spwikiArticleType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiDescriptor.spwikiArticleType>
  {
  }

  public abstract class urlEdit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.urlEdit>
  {
  }

  public abstract class pubVirtualPath : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiDescriptor.pubVirtualPath>
  {
  }

  public abstract class deletedID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.deletedID>
  {
  }

  public new abstract class articleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiDescriptor.articleType>
  {
  }

  public new abstract class number : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  WikiDescriptor.number>
  {
  }

  public new abstract class versioned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiDescriptor.versioned>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiDescriptor.holdEntry>
  {
  }

  public abstract class siteMapTagID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiDescriptor.siteMapTagID>
  {
  }

  public new abstract class folder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiDescriptor.folder>
  {
  }

  public abstract class requestApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WikiDescriptor.requestApproval>
  {
  }

  public abstract class cssID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.cssID>
  {
  }

  public abstract class cssPrintID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.cssPrintID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.noteID>
  {
  }

  public abstract class sitemapParent : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.sitemapParent>
  {
  }

  public abstract class sitemapTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.sitemapTitle>
  {
  }

  public abstract class wikiTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.wikiTitle>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiDescriptor.isActive>
  {
  }

  public abstract class position : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  WikiDescriptor.position>
  {
  }

  public abstract class wikiDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiDescriptor.wikiDescription>
  {
  }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.category>
  {
  }

  public abstract class defaultUrl : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptor.defaultUrl>
  {
  }

  public abstract class defaultIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiDescriptor.defaultIcon>
  {
  }
}

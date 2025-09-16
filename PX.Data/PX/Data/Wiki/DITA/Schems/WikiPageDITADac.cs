// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.Schems.WikiPageDITADac
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.Wiki.DITA.Schems;

[Serializable]
public class WikiPageDITADac
{
  [Serializable]
  public class WikiPage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _PageID;
    protected Guid? _WikiID;
    protected int? _ArticleType;
    protected Guid? _ParentUID;
    protected double? _Number;
    protected 
    #nullable disable
    string _Name;
    protected bool? _Versioned;
    protected bool? _Folder;
    protected int? _CategoryID;
    protected Guid? _CreatedByID;
    protected System.DateTime? _CreatedDateTime;
    protected Guid? _LastModifiedByID;
    protected System.DateTime? _LastModifiedDateTime;
    protected byte[] _tstamp;
    protected int? _StatusID;
    protected Guid? _NoteID;

    [PXDBGuid(false, IsKey = true)]
    [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Guid? PageID
    {
      get => this._PageID;
      set => this._PageID = value;
    }

    [PXDBGuid(false)]
    public virtual Guid? WikiID
    {
      get => this._WikiID;
      set => this._WikiID = value;
    }

    [PXDBInt]
    [PXDefault]
    public virtual int? ArticleType
    {
      get => this._ArticleType;
      set => this._ArticleType = value;
    }

    [PXDBGuid(false)]
    [PXUIField(DisplayName = "Parent Folder")]
    public virtual Guid? ParentUID
    {
      get => this._ParentUID;
      set => this._ParentUID = value;
    }

    [PXDBDouble]
    [PXDefault(0.0)]
    public virtual double? Number
    {
      get => this._Number;
      set => this._Number = value;
    }

    [PXDefault]
    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "ID", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXDBBool]
    [PXDefault(true, PersistingCheck = PXPersistingCheck.Null)]
    [PXUIField(DisplayName = "Versioned")]
    public virtual bool? Versioned
    {
      get => this._Versioned;
      set => this._Versioned = value;
    }

    [PXDBBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Null)]
    [PXUIField(DisplayName = "Folder")]
    public virtual bool? Folder
    {
      get => this._Folder;
      set => this._Folder = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Category")]
    public virtual int? CategoryID
    {
      get => this._CategoryID;
      set => this._CategoryID = value;
    }

    [PXDBCreatedByID]
    [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
    public virtual Guid? CreatedByID
    {
      get => this._CreatedByID;
      set => this._CreatedByID = value;
    }

    [PXDBCreatedDateTime]
    [PXUIField(DisplayName = "Created at", Visible = true, Enabled = false)]
    public virtual System.DateTime? CreatedDateTime
    {
      get => this._CreatedDateTime;
      set => this._CreatedDateTime = value;
    }

    [PXDBLastModifiedByID]
    [PXUIField(DisplayName = "Last Modified by", Visible = true, Enabled = false)]
    public virtual Guid? LastModifiedByID
    {
      get => this._LastModifiedByID;
      set => this._LastModifiedByID = value;
    }

    [PXDBLastModifiedDateTime(PreserveTime = true)]
    [PXUIField(DisplayName = "Last Modified at", Visible = true, Enabled = false)]
    public virtual System.DateTime? LastModifiedDateTime
    {
      get => this._LastModifiedDateTime;
      set => this._LastModifiedDateTime = value;
    }

    [PXDBTimestamp]
    public virtual byte[] tstamp
    {
      get => this._tstamp;
      set => this._tstamp = value;
    }

    [PXDBInt]
    [PXDefault(0)]
    [WikiPageStatus.List]
    public virtual int? StatusID
    {
      get => this._StatusID;
      set => this._StatusID = value;
    }

    [PXNote]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    public abstract class pageID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.pageID>
    {
    }

    public abstract class wikiID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.wikiID>
    {
    }

    public abstract class articleType : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.articleType>
    {
    }

    public abstract class parentUID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.parentUID>
    {
    }

    public abstract class number : BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.number>
    {
    }

    public abstract class name : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.name>
    {
    }

    public abstract class versioned : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.versioned>
    {
    }

    public abstract class folder : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.folder>
    {
    }

    public abstract class categoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.categoryID>
    {
    }

    public abstract class createdByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.createdByID>
    {
    }

    public abstract class createdDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.lastModifiedByID>
    {
    }

    public abstract class lastModifiedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.lastModifiedDateTime>
    {
    }

    public abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      WikiPageDITADac.WikiPage.Tstamp>
    {
    }

    public abstract class statusID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.statusID>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageDITADac.WikiPage.noteID>
    {
    }
  }

  [Serializable]
  public class WikiPageLanguage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _PageID;
    protected string _Title;
    protected string _Language;
    protected int? _LastRevisionID;
    protected int? _LastPublishedID;
    protected System.DateTime? _LastPublishedDateTime;

    [PXDBGuid(false, IsKey = true)]
    public virtual Guid? PageID
    {
      get => this._PageID;
      set => this._PageID = value;
    }

    [PXDefault]
    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    [PXDBString(IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Language
    {
      get => this._Language;
      set => this._Language = value;
    }

    [PXDBInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Version ID")]
    public virtual int? LastRevisionID
    {
      get => this._LastRevisionID;
      set => this._LastRevisionID = value;
    }

    [PXDBInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Published ID")]
    public virtual int? LastPublishedID
    {
      get => this._LastPublishedID;
      set => this._LastPublishedID = value;
    }

    [PXDBDate(PreserveTime = true)]
    [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
    public virtual System.DateTime? LastPublishedDateTime
    {
      get => this._LastPublishedDateTime;
      set => this._LastPublishedDateTime = value;
    }

    public abstract class pageID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.pageID>
    {
    }

    public abstract class title : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.title>
    {
    }

    public abstract class language : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.language>
    {
    }

    public abstract class lastRevisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.lastRevisionID>
    {
    }

    public abstract class lastPublishedID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.lastPublishedID>
    {
    }

    public abstract class lastPublishedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      WikiPageDITADac.WikiPageLanguage.lastPublishedDateTime>
    {
    }
  }

  [Serializable]
  public class WikiRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _PageID;
    protected string _Language;
    protected int? _PageRevisionID;
    protected string _Content;
    protected Guid? _CreatedByID;
    protected System.DateTime? _CreatedDateTime;
    protected Guid? _UID;

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

    [PXDBString(IsUnicode = true)]
    [PXUIField(DisplayName = "Content")]
    public virtual string Content
    {
      get => this._Content;
      set => this._Content = value;
    }

    [PXDBCreatedByID]
    [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
    public virtual Guid? CreatedByID
    {
      get => this._CreatedByID;
      set => this._CreatedByID = value;
    }

    [PXDBCreatedDateTime(PreserveTime = true)]
    [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
    public virtual System.DateTime? CreatedDateTime
    {
      get => this._CreatedDateTime;
      set => this._CreatedDateTime = value;
    }

    [PXDBGuid(false)]
    public virtual Guid? UID
    {
      get => this._UID;
      set => this._UID = value;
    }

    public abstract class pageID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageDITADac.WikiRevision.pageID>
    {
    }

    public abstract class language : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      WikiPageDITADac.WikiRevision.language>
    {
    }

    public abstract class pageRevisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiRevision.pageRevisionID>
    {
    }

    public abstract class content : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      WikiPageDITADac.WikiRevision.content>
    {
    }

    public abstract class createdByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiRevision.createdByID>
    {
    }

    public abstract class createdDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      WikiPageDITADac.WikiRevision.createdDateTime>
    {
    }

    public abstract class uID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageDITADac.WikiRevision.uID>
    {
    }
  }

  [PXTable(new System.Type[] {typeof (WikiPageDITADac.WikiPage.pageID)})]
  [Serializable]
  public class WikiArticle : WikiPageDITADac.WikiPage
  {
    [PXDBGuid(false)]
    [PXUIField(DisplayName = "Parent Folder")]
    [PXSelector(typeof (Search<WikiPageSimple.pageID, Where<WikiPageSimple.wikiID, Equal<Current<WikiPageDITADac.WikiPage.wikiID>>, Or<WikiPageSimple.pageID, Equal<Current<WikiPageDITADac.WikiPage.wikiID>>>>>), SubstituteKey = typeof (WikiPageSimple.name))]
    public override Guid? ParentUID
    {
      get => this._ParentUID;
      set => this._ParentUID = value;
    }

    [PXDBInt]
    [PXDefault(10)]
    public override int? ArticleType
    {
      get => this._ArticleType;
      set => this._ArticleType = value;
    }

    [PXNote]
    public override Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    public new abstract class wikiID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiArticle.wikiID>
    {
    }

    public new abstract class pageID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiArticle.pageID>
    {
    }

    public new abstract class parentUID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiArticle.parentUID>
    {
    }

    public new abstract class articleType : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiPageDITADac.WikiArticle.articleType>
    {
    }

    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiPageDITADac.WikiArticle.noteID>
    {
    }
  }
}

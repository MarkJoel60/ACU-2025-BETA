// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private bool? _Selected;
  protected Guid? _PageID;
  protected Guid? _WikiID;
  protected int? _ArticleType;
  protected Guid? _ParentUID;
  protected double? _Number;
  protected 
  #nullable disable
  string _Name;
  protected string _Title;
  protected string _Summary;
  protected string _Keywords;
  protected bool? _Versioned;
  protected bool? _Folder;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected string _Language;
  protected int? _PageRevisionID;
  protected System.DateTime? _PageRevisionDateTime;
  protected Guid? _PageRevisionCreatedByID;
  protected System.DateTime? _PublishedDateTime;
  protected string _Content;
  protected int? _OldStatusID;
  protected int? _StatusID;
  protected int? _ApprovalGroupID;
  protected Guid? _ApprovalUserID;
  protected int? _Width;
  protected int? _Height;
  protected bool? _allowApprove;
  protected bool? _Approved;
  private short? _AccessRights;
  private short? _ParentAccessRights;
  protected bool? _isHtml;
  protected string _VisibleisHtml;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Wiki ID")]
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
  [PXUIField(DisplayName = "Article ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string Summary
  {
    get => this._Summary;
    set => this._Summary = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Keywords")]
  public virtual string Keywords
  {
    get => this._Keywords;
    set => this._Keywords = value;
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

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created", Visible = true, Enabled = false)]
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

  [PXDBLastModifiedDateTime(PreserveTime = true, UseSmallDateTime = false)]
  [PXUIField(DisplayName = "Last Modified", Visible = true, Enabled = false)]
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

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Language")]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXInt]
  public virtual int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  [PXDate]
  public virtual System.DateTime? PageRevisionDateTime
  {
    get => this._PageRevisionDateTime;
    set => this._PageRevisionDateTime = value;
  }

  [PXGuid]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  public virtual Guid? PageRevisionCreatedByID
  {
    get => this._PageRevisionCreatedByID;
    set => this._PageRevisionCreatedByID = value;
  }

  [PXDateAndTime(UseTimeZone = true, DisplayMask = "g")]
  [PXUIField(DisplayName = "Published Date", Visible = true, Enabled = false)]
  public virtual System.DateTime? PublishedDateTime
  {
    get => this._PublishedDateTime;
    set => this._PublishedDateTime = value;
  }

  [PXString(InputMask = "")]
  [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Content")]
  public virtual string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [WikiPageStatus.List]
  public virtual int? OldStatusID
  {
    get => this._OldStatusID;
    set => this._OldStatusID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [WikiPageStatus.List]
  public virtual int? StatusID
  {
    get => this._StatusID;
    set => this._StatusID = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Approval Group")]
  public virtual int? ApprovalGroupID
  {
    get => this._ApprovalGroupID;
    set => this._ApprovalGroupID = value;
  }

  [PXDBGuid(false)]
  [PXCompanyMemberSelector(typeof (WikiPage.approvalGroupID))]
  [PXUIField(DisplayName = "Approver ID")]
  public virtual Guid? ApprovalUserID
  {
    get => this._ApprovalUserID;
    set => this._ApprovalUserID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Width")]
  public virtual int? Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Height")]
  public virtual int? Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold
  {
    [PXDependsOnFields(new System.Type[] {typeof (WikiPage.statusID)})] get
    {
      int? statusId = this.StatusID;
      int num = 0;
      return new bool?(statusId.GetValueOrDefault() == num & statusId.HasValue || !this.StatusID.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        this.StatusID = new int?(0);
      }
      else
      {
        bool? hold = this.Hold;
        bool flag2 = true;
        if (!(hold.GetValueOrDefault() == flag2 & hold.HasValue))
          return;
        this.StatusID = new int?(1);
      }
    }
  }

  [PXBool]
  public virtual bool? AllowApprove
  {
    get => this._allowApprove;
    set => this._allowApprove = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Approved")]
  public virtual bool? Approved
  {
    [PXDependsOnFields(new System.Type[] {typeof (WikiPage.statusID)})] get
    {
      int? statusId = this.StatusID;
      int num = 3;
      return new bool?(statusId.GetValueOrDefault() == num & statusId.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        this.StatusID = new int?(3);
      }
      else
      {
        bool? approved = this.Approved;
        bool flag2 = true;
        if (!(approved.GetValueOrDefault() == flag2 & approved.HasValue))
          return;
        this.StatusID = new int?(0);
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Rejected")]
  public virtual bool? Rejected
  {
    [PXDependsOnFields(new System.Type[] {typeof (WikiPage.statusID)})] get
    {
      int? statusId = this.StatusID;
      int num = 2;
      return new bool?(statusId.GetValueOrDefault() == num & statusId.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        this.StatusID = new int?(2);
      }
      else
      {
        bool? rejected = this.Rejected;
        bool flag2 = true;
        if (!(rejected.GetValueOrDefault() == flag2 & rejected.HasValue))
          return;
        this.StatusID = new int?(0);
      }
    }
  }

  [PXShort]
  [PXUIField(DisplayName = "Access Rights")]
  [PXIntList(new int[] {-1, 0, 1, 2, 3, 4, 5}, new string[] {"Inherited", "Revoked", "View Only", "Edit", "Insert", "Publish", "Delete"})]
  public short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
  }

  [PXShort]
  [PXUIField(DisplayName = "Parent Access Rights", Enabled = false)]
  [PXIntList(new int[] {-1, 0, 1, 2, 3, 4, 5}, new string[] {"Not Set", "Revoked", "View Only", "Edit", "Insert", "Publish", "Delete"})]
  public short? ParentAccessRights
  {
    get => this._ParentAccessRights;
    set => this._ParentAccessRights = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Article Type")]
  public virtual bool? IsHtml
  {
    get => this.VisibleisHtml == "H" ? new bool?(true) : new bool?(false);
    set
    {
      bool? nullable = value;
      bool flag = true;
      this._VisibleisHtml = nullable.GetValueOrDefault() == flag & nullable.HasValue ? "H" : "W";
    }
  }

  [PXString]
  [PXStringList(new string[] {"W", "H"}, new string[] {"Wiki", "Html"})]
  [PXUIField(DisplayName = "Article Type")]
  [PXDefault("W")]
  public virtual string VisibleisHtml
  {
    get => this._VisibleisHtml;
    set => this._VisibleisHtml = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.selected>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.pageID>
  {
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.wikiID>
  {
  }

  public abstract class articleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.articleType>
  {
  }

  public abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.parentUID>
  {
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  WikiPage.number>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.name>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.title>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.summary>
  {
  }

  public abstract class keywords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.keywords>
  {
  }

  public abstract class versioned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.versioned>
  {
  }

  public abstract class folder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.folder>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPage.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.lastModifiedByID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPage.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WikiPage.Tstamp>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.language>
  {
  }

  public abstract class pageRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.pageRevisionID>
  {
  }

  public abstract class pageRevisionDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPage.pageRevisionDateTime>
  {
  }

  public abstract class pageRevisionCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPage.pageRevisionCreatedByID>
  {
  }

  public abstract class publishedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPage.publishedDateTime>
  {
  }

  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.content>
  {
  }

  public abstract class oldStatusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.oldStatusID>
  {
  }

  public abstract class statusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.statusID>
  {
  }

  public abstract class approvalGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.approvalGroupID>
  {
  }

  public abstract class approvalUserID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.approvalUserID>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPage.height>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.hold>
  {
  }

  public abstract class allowApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.allowApprove>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.rejected>
  {
  }

  public abstract class accessRights : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  WikiPage.accessRights>
  {
  }

  public abstract class parentaccessRights : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    WikiPage.parentaccessRights>
  {
  }

  public abstract class isHtml : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPage.isHtml>
  {
  }

  public abstract class visibleisHtml : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage.visibleisHtml>
  {
  }
}

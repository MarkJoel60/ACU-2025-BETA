// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileInfo
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
public class UploadFileInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _FileID;
  protected 
  #nullable disable
  string _Icon;
  protected string _Name;
  protected string _Comment;
  protected Guid? _RevisionCreatedByID;
  protected System.DateTime? _FileCreatedDateTime;
  protected System.DateTime? _RevisionCreatedDateTime;
  protected string _CheckedOutBy;
  protected int? _Size;
  protected int? _LastRevisionID;
  protected string _ExternalLink;
  protected string _WikiLink;

  public UploadFileInfo()
  {
  }

  public UploadFileInfo(UploadFile f, UploadFileRevision r)
  {
    this.FileID = f.FileID;
    this.Name = f.Name;
    this.Comment = r.Comment;
    this.FileCreatedDateTime = f.CreatedDateTime;
    this.LastRevisionID = f.LastRevisionID;
    this.RevisionCreatedByID = r.CreatedByID;
    this.RevisionCreatedDateTime = r.CreatedDateTime;
    this.Size = r.Size;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXImage]
  [PXUIField(DisplayName = "Type")]
  public virtual string Icon
  {
    get => this._Icon;
    set => this._Icon = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Description")]
  public virtual string Comment
  {
    get => this._Comment;
    set => this._Comment = value;
  }

  [PXDBGuid(false)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  [PXUIField(DisplayName = "Created By", Visible = true, Enabled = false)]
  public virtual Guid? RevisionCreatedByID
  {
    get => this._RevisionCreatedByID;
    set => this._RevisionCreatedByID = value;
  }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Creation Date", Visible = true, Enabled = false)]
  public virtual System.DateTime? FileCreatedDateTime
  {
    get => this._FileCreatedDateTime;
    set => this._FileCreatedDateTime = value;
  }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Last Modified", Visible = true, Enabled = false)]
  public virtual System.DateTime? RevisionCreatedDateTime
  {
    get => this._RevisionCreatedDateTime;
    set => this._RevisionCreatedDateTime = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Checked Out By")]
  public virtual string CheckedOutBy
  {
    get => this._CheckedOutBy;
    set => this._CheckedOutBy = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "File Size (kB)")]
  public virtual int? Size
  {
    get => this._Size;
    set => this._Size = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LastRevisionID
  {
    get => this._LastRevisionID;
    set => this._LastRevisionID = value;
  }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "External Link")]
  public virtual string ExternalLink
  {
    get => this._ExternalLink;
    set => this._ExternalLink = value;
  }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Wiki Link")]
  public virtual string WikiLink
  {
    get => this._WikiLink;
    set => this._WikiLink = value;
  }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileInfo.fileID>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.icon>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.name>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.comment>
  {
  }

  public abstract class revisionCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UploadFileInfo.revisionCreatedByID>
  {
  }

  public abstract class fileCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFileInfo.fileCreatedDateTime>
  {
  }

  public abstract class revisionCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFileInfo.revisionCreatedDateTime>
  {
  }

  public abstract class checkedOutBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.checkedOutBy>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFileInfo.size>
  {
  }

  public abstract class lastRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFileInfo.lastRevisionID>
  {
  }

  public abstract class externalLink : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.externalLink>
  {
  }

  public abstract class wikiLink : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileInfo.wikiLink>
  {
  }
}

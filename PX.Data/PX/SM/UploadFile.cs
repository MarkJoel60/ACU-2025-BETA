// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Wiki.Tags;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class UploadFile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _FileID;
  protected 
  #nullable disable
  string _Name;
  protected string _Comment;
  protected string _CheckedOutComment;
  protected bool? _Versioned;
  protected Guid? _CreatedByID;
  protected System.DateTime? _CreatedDateTime;
  protected int? _LastRevisionID;
  protected Guid? _CheckedOutBy;
  protected Guid? _PrimaryPageID;
  protected string _PrimaryScreenID;
  protected byte[] _tstamp;
  protected int? _FileRevisionID;
  protected byte[] _Data;
  private string _ext;
  protected System.DateTime? _SourceLastImportDate;
  protected System.DateTime? _SourceLastExportDate;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "File")]
  public virtual Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (UploadFileWithIDSelector.fileID))]
  public virtual string Name
  {
    get => this._Name;
    set
    {
      if (!(this._Name != value))
        return;
      this._Name = value;
      this.ShortName = Str.GetShortName(value);
      this._ext = (string) null;
    }
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "File Name")]
  public virtual string ShortName { get; set; }

  [PXUIField(DisplayName = "Comment", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Comment
  {
    get => this._Comment;
    set => this._Comment = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Check Out Comment", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string CheckedOutComment
  {
    get => this._CheckedOutComment;
    set => this._CheckedOutComment = value;
  }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Versioned")]
  public virtual bool? Versioned
  {
    get => this._Versioned;
    set => this._Versioned = value;
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedDateTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Added on", Visible = true, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LastRevisionID
  {
    get => this._LastRevisionID;
    set => this._LastRevisionID = value;
  }

  [PXDBGuid(false)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  [PXUIField(DisplayName = "Checked Out By", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? CheckedOutBy
  {
    get => this._CheckedOutBy;
    set => this._CheckedOutBy = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? PrimaryPageID
  {
    get => this._PrimaryPageID;
    set => this._PrimaryPageID = value;
  }

  [PXDBString]
  public virtual string PrimaryScreenID
  {
    get => this._PrimaryScreenID;
    set => this._PrimaryScreenID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXInt]
  public virtual int? FileRevisionID
  {
    get => this._FileRevisionID;
    set => this._FileRevisionID = value;
  }

  public virtual byte[] Data
  {
    get => this._Data;
    set => this._Data = value;
  }

  [PXString(IsUnicode = true)]
  public virtual string OriginalName { get; set; }

  [PXInt]
  public virtual int? Size { get; set; }

  [PXDateAndTime]
  public virtual System.DateTime? RevisionDate { get; set; }

  [PXUIField(DisplayName = "Tags", Visible = false)]
  [TagsSelector(typeof (UploadFile.fileID), typeof (UploadFile.tagIDs))]
  public virtual string Tags { get; set; }

  public virtual Guid[] TagIDs { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Hidden")]
  public virtual bool? IsHidden { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Extension")]
  public string Extansion
  {
    [PXDependsOnFields(new System.Type[] {typeof (UploadFile.name)})] get
    {
      if (this._ext == null)
      {
        this._ext = string.Empty;
        int num;
        if (!string.IsNullOrEmpty(this.Name) && (num = this.Name.LastIndexOf('.')) > -1 && num < this.Name.Length - 1)
          this._ext = this.Name.Substring(num + 1);
      }
      return this._ext;
    }
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize")]
  public virtual bool? Synchronizable { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Synchronization Type")]
  [FIleSyncTypes.List]
  public virtual string SourceType { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "File Location")]
  public virtual string SourceUri { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Username")]
  public virtual string SourceLogin { get; set; }

  [PXUIField(DisplayName = "Password")]
  [PXRSACryptString(1024 /*0x0400*/, IsViewDecrypted = false, IsUnicode = true)]
  public virtual string SourcePassword { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Folder Content")]
  public virtual bool? SourceIsFolder { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Import File Reg. Exp.")]
  public virtual string SourceMask { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Export File Naming Format")]
  [FileSyncNamingFormat.List]
  public virtual string SourceNamingFormat { get; set; }

  [PXDBDateAndTime]
  [PXUIField(DisplayName = "Last Import Date", Enabled = false)]
  public virtual System.DateTime? SourceLastImportDate
  {
    get => this._SourceLastImportDate;
    set => this._SourceLastImportDate = value;
  }

  [PXDBDateAndTime(UseTimeZone = false)]
  public virtual System.DateTime? SourceLastExportDate
  {
    get => this._SourceLastExportDate;
    set => this._SourceLastExportDate = value;
  }

  [PXDate(UseTimeZone = true, DisplayMask = "g")]
  [PXUIField(DisplayName = "Last Export Date", Enabled = false)]
  public virtual System.DateTime? LastExportDate
  {
    get
    {
      return !this._SourceLastExportDate.HasValue ? new System.DateTime?() : new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(this._SourceLastExportDate.Value, LocaleInfo.GetTimeZone()));
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Public")]
  public virtual bool? IsPublic { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(50)]
  [PXUIField(DisplayName = "SSH Private Key")]
  [PXSelector(typeof (Certificate.name))]
  public virtual string SshCertificateName { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is System File", IsReadOnly = true, Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual bool? IsSystem { get; set; }

  /// <summary>Inherit Access Rights from Entities</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Inherit Access Rights from Entities")]
  public virtual bool? IsAccessRightsFromEntities { get; set; }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.fileID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.name>
  {
  }

  public abstract class shortName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.shortName>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.comment>
  {
  }

  public abstract class checkedOutComment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFile.checkedOutComment>
  {
  }

  public abstract class versioned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.versioned>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFile.createdDateTime>
  {
  }

  public abstract class lastRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFile.lastRevisionID>
  {
  }

  public abstract class checkedOutBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.checkedOutBy>
  {
  }

  public abstract class primaryPageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.primaryPageID>
  {
  }

  public abstract class primaryScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFile.primaryScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UploadFile.Tstamp>
  {
  }

  public abstract class fileRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFile.fileRevisionID>
  {
  }

  public abstract class data : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UploadFile.data>
  {
  }

  public abstract class originalName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.originalName>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFile.size>
  {
  }

  public abstract class revisionDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UploadFile.revisionDate>
  {
  }

  public abstract class tags : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.tags>
  {
  }

  public abstract class tagIDs : IBqlField, IBqlOperand
  {
  }

  public abstract class isHidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.isHidden>
  {
  }

  public abstract class extansion : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.extansion>
  {
  }

  public abstract class synchronizable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.synchronizable>
  {
  }

  public abstract class sourceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.sourceType>
  {
  }

  public abstract class sourceUri : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.sourceUri>
  {
  }

  public abstract class sourceLogin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.sourceLogin>
  {
  }

  public abstract class sourcePassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.sourcePassword>
  {
  }

  public abstract class sourceIsFolder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.sourceIsFolder>
  {
  }

  public abstract class sourceMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFile.sourceMask>
  {
  }

  public abstract class sourceNamingFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFile.sourceNamingFormat>
  {
  }

  public abstract class sourceLastImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFile.sourceLastImportDate>
  {
  }

  public abstract class sourceLastExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFile.sourceLastExportDate>
  {
  }

  public abstract class lastExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFile.lastExportDate>
  {
  }

  public abstract class isPublic : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.isPublic>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.noteID>
  {
  }

  public abstract class sshCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFile.sshCertificateName>
  {
  }

  public abstract class isSystem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFile.isSystem>
  {
  }

  public abstract class isAccessRightsFromEntities : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UploadFile.isAccessRightsFromEntities>
  {
  }
}

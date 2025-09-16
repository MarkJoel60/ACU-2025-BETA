// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileRevision
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.IO;
using System.Net;

#nullable enable
namespace PX.SM;

[Serializable]
public class UploadFileRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _FileID;
  protected int? _FileRevisionID;
  private 
  #nullable disable
  byte[] _Data;
  private bool isDataLoaded;
  private Guid? _blobHandler;
  protected string _Comment;
  protected int? _Size;
  protected Guid? _CreatedByID;
  protected System.DateTime? _CreatedDateTime;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Version ID")]
  public virtual int? FileRevisionID
  {
    get => this._FileRevisionID;
    set => this._FileRevisionID = value;
  }

  [BlobStorage]
  public virtual byte[] Data
  {
    [PXDependsOnFields(new System.Type[] {typeof (UploadFileRevision.blobHandler), typeof (UploadFileRevision.blobData)})] get
    {
      if (!this.BlobHandler.HasValue)
        return this.BlobData;
      if (!this.isDataLoaded)
      {
        try
        {
          this._Data = PXBlobStorage.Load(this.BlobHandler.Value);
          this.isDataLoaded = true;
        }
        catch (Exception ex)
        {
          Exception exception = ex;
          webException = (WebException) null;
          while (true)
          {
            switch (exception)
            {
              case null:
              case WebException webException:
                goto label_7;
              default:
                exception = exception.InnerException;
                continue;
            }
          }
label_7:
          if (webException != null && webException.Message.ToLower().Contains("not found"))
            throw new FileNotFoundException(PXLocalizer.LocalizeFormat("The file '{0}' has not been found.", (object) (this.OriginalName ?? this.FileID.ToString())));
          throw;
        }
      }
      return this._Data;
    }
    set
    {
      this.BlobHandler = new Guid?();
      this.BlobData = value;
      this.isDataLoaded = false;
      this._Data = (byte[]) null;
    }
  }

  internal void MoveToStorage()
  {
    if (this.BlobHandler.HasValue)
      return;
    this._Data = this.BlobData;
    this.isDataLoaded = true;
    this.BlobHandler = new Guid?(PXBlobStorage.Save(this.BlobData));
    this.BlobData = (byte[]) null;
  }

  [PXDBBinary(DatabaseFieldName = "Data")]
  public virtual byte[] BlobData { get; set; }

  [PXDBGuid(false, IsKey = false)]
  [PXUIField(DisplayName = "BlobHandler")]
  [PXBlobStorageRemove]
  public Guid? BlobHandler
  {
    get => this._blobHandler;
    set => this._blobHandler = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Comment")]
  public virtual string Comment
  {
    get => this._Comment;
    set => this._Comment = value;
  }

  /// <summary>Size in Kilobytes (!).</summary>
  [PXDBInt]
  public virtual int? Size
  {
    get => this._Size;
    set => this._Size = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Original Name")]
  public virtual string OriginalName { get; set; }

  [PXDBDateAndTime(UseTimeZone = false)]
  public virtual System.DateTime? OriginalTimestamp { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedDateTime(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileRevision.fileID>
  {
  }

  public abstract class fileRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UploadFileRevision.fileRevisionID>
  {
  }

  public abstract class blobData : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UploadFileRevision.blobData>
  {
  }

  public abstract class blobHandler : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileRevision.blobHandler>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileRevision.comment>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFileRevision.size>
  {
  }

  public abstract class originalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileRevision.originalName>
  {
  }

  public abstract class originalTimestamp : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFileRevision.originalTimestamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileRevision.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFileRevision.createdDateTime>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileRevisionNoData
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
public class UploadFileRevisionNoData : UploadFileRevision
{
  protected long? _DataSize;
  protected 
  #nullable disable
  string _ReadableSize;
  protected new System.DateTime? _CreatedDateTime;

  [PXDBGuid(false, IsKey = true)]
  public override Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Version ID")]
  public override int? FileRevisionID
  {
    get => this._FileRevisionID;
    set => this._FileRevisionID = value;
  }

  public override byte[] BlobData
  {
    get => (byte[]) null;
    set
    {
    }
  }

  public override byte[] Data
  {
    get => (byte[]) null;
    set
    {
    }
  }

  [PXDBDataLength("Data")]
  public long? DataSize
  {
    get => this._DataSize;
    set => this._DataSize = value;
  }

  [PXString]
  [PXUIField(DisplayName = "File Size")]
  public string ReadableSize
  {
    get => this._ReadableSize;
    set => this._ReadableSize = value;
  }

  [PXDBCreatedDateTime(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
  public override System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  public new abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileRevisionNoData.fileID>
  {
  }

  public new abstract class fileRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UploadFileRevisionNoData.fileRevisionID>
  {
  }

  public new abstract class blobHandler : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UploadFileRevisionNoData.blobHandler>
  {
  }

  public abstract class dataSize : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  UploadFileRevisionNoData.dataSize>
  {
  }

  public abstract class readableSize : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileRevisionNoData.readableSize>
  {
  }

  public new abstract class comment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileRevisionNoData.comment>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UploadFileRevisionNoData.createdDateTime>
  {
  }
}

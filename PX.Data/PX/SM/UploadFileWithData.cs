// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileWithData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select2<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>>>))]
[Serializable]
public class UploadFileWithData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _Data;
  protected bool isDataLoaded;
  protected int? _Size;

  [PXDBGuid(false, IsKey = true, BqlField = typeof (UploadFile.fileID))]
  public virtual Guid? FileID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (UploadFileRevision.fileRevisionID))]
  public virtual int? FileRevisionID { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true, BqlField = typeof (UploadFile.name))]
  public virtual string Name { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true, BqlField = typeof (UploadFile.comment))]
  public virtual string Comment { get; set; }

  [PXString(InputMask = "", IsUnicode = true)]
  public virtual string ContentID { get; set; }

  public virtual byte[] Data
  {
    [PXDependsOnFields(new System.Type[] {typeof (UploadFileWithData.blobHandler), typeof (UploadFileWithData.blobData)})] get
    {
      if (!this.BlobHandler.HasValue)
        return this.BlobData;
      if (!this.isDataLoaded)
      {
        this.isDataLoaded = true;
        this._Data = PXBlobStorage.Load(this.BlobHandler.Value);
      }
      return this._Data;
    }
  }

  [PXDBBinary(BqlField = typeof (UploadFileRevision.blobData), DatabaseFieldName = "Data")]
  public virtual byte[] BlobData { get; set; }

  [PXDBGuid(false, IsKey = false, BqlField = typeof (UploadFileRevision.blobHandler))]
  public Guid? BlobHandler { get; set; }

  [PXDBInt(BqlField = typeof (UploadFileRevision.size))]
  public virtual int? Size { get; set; }

  [PXDBGuid(false, BqlField = typeof (NoteDoc.noteID))]
  public Guid? NoteID { get; set; }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileWithData.fileID>
  {
  }

  public abstract class fileRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UploadFileWithData.fileRevisionID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileWithData.name>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileWithData.comment>
  {
  }

  public abstract class contentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileWithData.contentID>
  {
  }

  public abstract class blobData : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UploadFileWithData.blobData>
  {
  }

  public abstract class blobHandler : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileWithData.blobHandler>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFileWithData.size>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileWithData.noteID>
  {
  }
}

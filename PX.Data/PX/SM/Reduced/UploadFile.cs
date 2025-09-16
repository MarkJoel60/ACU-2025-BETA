// Decompiled with JetBrains decompiler
// Type: PX.SM.Reduced.UploadFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM.Reduced;

[Serializable]
public class UploadFile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _FileID;
  protected Guid? _PrimaryPageID;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "File")]
  public virtual Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? PrimaryPageID
  {
    get => this._PrimaryPageID;
    set => this._PrimaryPageID = value;
  }

  public abstract class fileID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.fileID>
  {
  }

  public abstract class primaryPageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFile.primaryPageID>
  {
  }
}

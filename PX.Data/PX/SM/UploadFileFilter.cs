// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileFilter
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
public class UploadFileFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _FileID;
  protected 
  #nullable disable
  string _Name;
  protected int? _FileRevisionID;
  protected string _HashAccess;

  [PXDBGuid(false)]
  public virtual Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXInt]
  public virtual int? FileRevisionID
  {
    get => this._FileRevisionID;
    set => this._FileRevisionID = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  public virtual string HashAccess
  {
    get => this._HashAccess;
    set => this._HashAccess = value;
  }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileFilter.fileID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileFilter.name>
  {
  }

  public abstract class fileRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UploadFileFilter.fileRevisionID>
  {
  }

  public abstract class hashAccess : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UploadFileFilter.hashAccess>
  {
  }
}

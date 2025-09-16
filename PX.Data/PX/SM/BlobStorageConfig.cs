// Decompiled with JetBrains decompiler
// Type: PX.SM.BlobStorageConfig
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
public class BlobStorageConfig : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Provider")]
  [ProviderListAttriubute]
  public 
  #nullable disable
  string Provider { get; set; }

  [PXDBBool(IsKey = false)]
  [PXUIField(DisplayName = "Allow Saving Files")]
  public bool? AllowWrite { get; set; }

  [PXDBBool(IsKey = false)]
  [PXUIField(DisplayName = "IsActive")]
  public bool? IsActive { get; set; }

  public abstract class provider : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlobStorageConfig.provider>
  {
  }

  public abstract class allowWrite : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlobStorageConfig.allowWrite>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlobStorageConfig.isActive>
  {
  }
}

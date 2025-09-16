// Decompiled with JetBrains decompiler
// Type: PX.SM.UPStorageParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (InstallationSetup))]
[PXHidden]
[Serializable]
public class UPStorageParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _StorageProvider;

  [PXDBString(32 /*0x20*/, IsKey = true)]
  [PXUIField(DisplayName = "Storage Provider", Visible = false, Enabled = false)]
  public virtual string StorageProvider
  {
    get => this._StorageProvider;
    set => this._StorageProvider = value;
  }

  [PXDBString(50, IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public string Name { get; set; }

  [PXDBString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Value")]
  public string Value { get; set; }

  public abstract class storageProvider : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPStorageParameters.storageProvider>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPStorageParameters.name>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPStorageParameters.value>
  {
  }
}

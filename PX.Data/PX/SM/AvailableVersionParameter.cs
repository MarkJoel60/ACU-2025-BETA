// Decompiled with JetBrains decompiler
// Type: PX.SM.AvailableVersionParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Available Version")]
[Serializable]
public class AvailableVersionParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Version;
  protected string _Name;
  protected string _Value;

  [PXString(10, IsKey = true)]
  [PXDefault("")]
  public virtual string Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [PXString(1024 /*0x0400*/, IsKey = true)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXString]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public abstract class version : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AvailableVersionParameter.version>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersionParameter.name>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableVersionParameter.value>
  {
  }
}

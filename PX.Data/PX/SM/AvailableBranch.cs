// Decompiled with JetBrains decompiler
// Type: PX.SM.AvailableBranch
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
public class AvailableBranch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Name;
  protected string _Description;

  [PXString(255 /*0xFF*/, IsKey = true, InputMask = "")]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXString(1024 /*0x0400*/)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableBranch.name>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AvailableBranch.description>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.CacheEntityItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class CacheEntityItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Key;
  protected string _SubKey;
  protected string _Name;
  protected string _Path;
  protected string _Icon;
  protected int? _Number;

  [PXString(IsKey = true)]
  public virtual string Key
  {
    get => this._Key;
    set => this._Key = value;
  }

  [PXString]
  public virtual string SubKey
  {
    get => this._SubKey;
    set => this._SubKey = value;
  }

  [PXString(InputMask = "")]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXString]
  public virtual string Path
  {
    get => this._Path;
    set => this._Path = value;
  }

  [PXString(InputMask = "")]
  public virtual string Icon
  {
    get => this._Icon;
    set => this._Icon = value;
  }

  [PXInt]
  [PXDefault(0)]
  public virtual int? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  /// <exclude />
  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheEntityItem.key>
  {
  }

  /// <exclude />
  public abstract class subKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheEntityItem.subKey>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheEntityItem.name>
  {
  }

  /// <exclude />
  public abstract class path : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheEntityItem.path>
  {
  }

  /// <exclude />
  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheEntityItem.icon>
  {
  }

  /// <exclude />
  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CacheEntityItem.number>
  {
  }
}

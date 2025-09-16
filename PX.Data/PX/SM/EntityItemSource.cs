// Decompiled with JetBrains decompiler
// Type: PX.SM.EntityItemSource
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
public class EntityItemSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Key;
  protected string _SubKey;
  protected string _ScreenID;
  protected string _Name;
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

  [PXString]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXString(InputMask = "")]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXInt]
  [PXDefault(0)]
  public virtual int? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  public EntityItemSource()
  {
  }

  public EntityItemSource(PXSiteMapNode node, string subKey)
  {
    this._Key = node.Key;
    this._SubKey = subKey;
    this._ScreenID = node.ScreenID;
    this._Name = node.Title;
  }

  /// <exclude />
  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntityItemSource.key>
  {
  }

  /// <exclude />
  public abstract class subKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntityItemSource.subKey>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntityItemSource.screenID>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EntityItemSource.name>
  {
  }

  /// <exclude />
  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EntityItemSource.number>
  {
  }
}

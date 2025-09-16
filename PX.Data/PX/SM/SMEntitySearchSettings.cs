// Decompiled with JetBrains decompiler
// Type: PX.SM.SMEntitySearchSettings
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
[Obsolete]
[Serializable]
public class SMEntitySearchSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _EntityType;
  protected string _EntityName;
  private bool? _Selected;

  [PXDBString(IsKey = true)]
  public virtual string EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Entity Name")]
  public virtual string EntityName
  {
    get => this._EntityName;
    set => this._EntityName = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <exclude />
  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMEntitySearchSettings.entityType>
  {
  }

  /// <exclude />
  public abstract class entityName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMEntitySearchSettings.entityName>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMEntitySearchSettings.selected>
  {
  }
}

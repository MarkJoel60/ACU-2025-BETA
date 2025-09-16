// Decompiled with JetBrains decompiler
// Type: PX.SM.UPHistoryComponents
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Update History Components")]
[Serializable]
public class UPHistoryComponents : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _UpdateComponentID;
  protected int? _UpdateID;
  protected 
  #nullable disable
  string _ComponentName;
  protected string _ComponentType;
  protected string _FromVersion;
  protected string _ToVersion;

  [PXUIField(DisplayName = "Maintenance Components ID", Enabled = false)]
  [PXDBIdentity(IsKey = true)]
  public virtual int? UpdateComponentID
  {
    get => this._UpdateComponentID;
    set => this._UpdateComponentID = value;
  }

  [PXUIField(DisplayName = "Maintenance ID", Enabled = false)]
  [PXDBIdentity(IsKey = false)]
  public virtual int? UpdateID
  {
    get => this._UpdateID;
    set => this._UpdateID = value;
  }

  [PXUIField(DisplayName = "Component Name", Enabled = false)]
  [PXDBString(255 /*0xFF*/)]
  [PXDefault("")]
  public virtual string ComponentName
  {
    get => this._ComponentName;
    set => this._ComponentName = value;
  }

  [PXUIField(DisplayName = "Component Type", Enabled = false)]
  [PXDBString(1)]
  [PXDefault("")]
  public virtual string ComponentType
  {
    get => this._ComponentType;
    set => this._ComponentType = value;
  }

  [PXUIField(DisplayName = "From Version", Enabled = false)]
  [PXDBString(20)]
  [PXDefault("")]
  public virtual string FromVersion
  {
    get => this._FromVersion;
    set => this._FromVersion = value;
  }

  [PXUIField(DisplayName = "To Version", Enabled = false)]
  [PXDBString(20)]
  [PXDefault("")]
  public virtual string ToVersion
  {
    get => this._ToVersion;
    set => this._ToVersion = value;
  }

  public abstract class updateComponentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UPHistoryComponents.updateComponentID>
  {
  }

  public abstract class updateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPHistoryComponents.updateID>
  {
  }

  public abstract class componentName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPHistoryComponents.componentName>
  {
  }

  public abstract class componentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPHistoryComponents.componentType>
  {
  }

  public abstract class fromVersion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UPHistoryComponents.fromVersion>
  {
  }

  public abstract class toVersion : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPHistoryComponents.toVersion>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.VersionFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Version Filter")]
[Serializable]
public class VersionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Key;
  protected bool? _RefreshBuildsRequired;
  protected bool? _ServerAvailable;

  [PXUIField(DisplayName = "Restriction Key", Enabled = true)]
  [PXString(44)]
  public virtual string Key
  {
    get => this._Key;
    set => this._Key = value;
  }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? RefreshBuildsRequired
  {
    get => this._RefreshBuildsRequired;
    set => this._RefreshBuildsRequired = value;
  }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? ServerAvailable
  {
    get => this._ServerAvailable;
    set => this._ServerAvailable = value;
  }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VersionFilter.key>
  {
  }

  public abstract class refreshBuildsRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VersionFilter.refreshBuildsRequired>
  {
  }

  public abstract class serverAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VersionFilter.serverAvailable>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UpdateABCAssignmentSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class UpdateABCAssignmentSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected 
  #nullable disable
  string _FinPeriodID;

  [PXDefault]
  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [INClosedPeriod(typeof (AccessInfo.businessDate))]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UpdateABCAssignmentSettings.siteID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateABCAssignmentSettings.finPeriodID>
  {
  }
}

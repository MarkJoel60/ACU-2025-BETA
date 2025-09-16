// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteFilter
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
public class INSiteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected DateTime? _PendingStdCostDate;
  protected bool? _RevalueInventory;

  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Max. Pending Cost Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? PendingStdCostDate
  {
    get => this._PendingStdCostDate;
    set => this._PendingStdCostDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Revalue Inventory")]
  public virtual bool? RevalueInventory
  {
    get => this._RevalueInventory;
    set => this._RevalueInventory = value;
  }

  public abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INSiteFilter.siteID>
  {
  }

  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteFilter.pendingStdCostDate>
  {
  }

  public abstract class revalueInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSiteFilter.revalueInventory>
  {
  }
}

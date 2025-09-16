// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIInvtSiteLoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INPIHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INPIDetail>.On<INPIDetail.FK.PIHeader>>>.AggregateTo<GroupBy<INPIHeader.pIID>, GroupBy<INPIDetail.inventoryID>, GroupBy<INPIHeader.siteID>, GroupBy<INPIDetail.locationID>>))]
public class PIInvtSiteLoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (INPIHeader.pIID))]
  public virtual 
  #nullable disable
  string PIID { get; set; }

  [StockItem(IsKey = true, BqlField = typeof (INPIDetail.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INPIHeader.siteID))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [Location(typeof (PIInvtSiteLoc.siteID), IsKey = true, BqlField = typeof (INPIDetail.locationID))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIInvtSiteLoc.pIID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIInvtSiteLoc.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIInvtSiteLoc.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIInvtSiteLoc.locationID>
  {
  }
}

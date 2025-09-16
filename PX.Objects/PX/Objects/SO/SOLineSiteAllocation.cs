// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSiteAllocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
[PXProjection(typeof (SelectFromBase<SOLineSplitAllocation, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<SOLineSplitAllocation.orderType>, GroupBy<SOLineSplitAllocation.orderNbr>, GroupBy<SOLineSplitAllocation.lineNbr>, GroupBy<SOLineSplitAllocation.siteID>, Sum<SOLineSplitAllocation.qtyAllocated>, Sum<SOLineSplitAllocation.qtyUnallocated>, Sum<SOLineSplitAllocation.lotSerialQtyAllocated>>), Persistent = false)]
public class SOLineSiteAllocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLineSplitAllocation.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLineSplitAllocation.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplitAllocation.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplitAllocation.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSplitAllocation.qtyAllocated))]
  public virtual Decimal? QtyAllocated { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSplitAllocation.qtyUnallocated))]
  public virtual Decimal? QtyUnallocated { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSplitAllocation.lotSerialQtyAllocated))]
  public virtual Decimal? LotSerialQtyAllocated { get; set; }

  public class PK : 
    PrimaryKeyOf<SOLineSiteAllocation>.By<SOLineSiteAllocation.orderType, SOLineSiteAllocation.orderNbr, SOLineSiteAllocation.lineNbr, SOLineSiteAllocation.siteID>
  {
    public static SOLineSiteAllocation Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      int? siteID)
    {
      return (SOLineSiteAllocation) PrimaryKeyOf<SOLineSiteAllocation>.By<SOLineSiteAllocation.orderType, SOLineSiteAllocation.orderNbr, SOLineSiteAllocation.lineNbr, SOLineSiteAllocation.siteID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) siteID, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLineSiteAllocation>.By<SOLineSiteAllocation.orderType, SOLineSiteAllocation.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOLineSiteAllocation>.By<SOLineSiteAllocation.orderType, SOLineSiteAllocation.orderNbr, SOLineSiteAllocation.lineNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSiteAllocation.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSiteAllocation.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSiteAllocation.lineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSiteAllocation.siteID>
  {
  }

  public abstract class qtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSiteAllocation.qtyAllocated>
  {
  }

  public abstract class qtyUnallocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSiteAllocation.qtyUnallocated>
  {
  }

  public abstract class lotSerialQtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSiteAllocation.lotSerialQtyAllocated>
  {
  }
}

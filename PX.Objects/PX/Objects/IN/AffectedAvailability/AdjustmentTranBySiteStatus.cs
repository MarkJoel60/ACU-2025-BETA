// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.AdjustmentTranBySiteStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

/// <exclude />
[PXCacheName("Adjustment Transactions grouped by SiteStatus")]
[PXProjection(typeof (Select5<AdjTranBySiteLotSerial, InnerJoin<INRegister, On<AdjTranBySiteLotSerial.docType, Equal<INRegister.docType>, And<AdjTranBySiteLotSerial.refNbr, Equal<INRegister.refNbr>>>, InnerJoin<INSiteStatusGroup, On<INSiteStatusGroup.inventoryID, Equal<AdjTranBySiteLotSerial.inventoryID>, And<INSiteStatusGroup.siteID, Equal<AdjTranBySiteLotSerial.siteID>>>>>, Aggregate<GroupBy<AdjTranBySiteLotSerial.docType, GroupBy<AdjTranBySiteLotSerial.refNbr, GroupBy<AdjTranBySiteLotSerial.inventoryID, GroupBy<AdjTranBySiteLotSerial.siteID, Max<INSiteStatusGroup.qtyHardAvail, Sum<AdjTranBySiteLotSerial.qtyAdjusted, Sum2<Switch<Case<Where<AdjTranBySiteLotSerial.qtyHardAvail, IsNull>, decimal0, Case<Where<INRegister.released, Equal<True>, And<AdjTranBySiteLotSerial.qtyHardAvail, Less<decimal0>>>, Minus<AdjTranBySiteLotSerial.qtyHardAvail>, Case<Where<INRegister.released, NotEqual<True>, And<Add<AdjTranBySiteLotSerial.qtyHardAvail, AdjTranBySiteLotSerial.qtyAdjusted>, Less<decimal0>>>, Minus<Add<AdjTranBySiteLotSerial.qtyHardAvail, AdjTranBySiteLotSerial.qtyAdjusted>>>>>, decimal0>>>>>>>>>>))]
public class AdjustmentTranBySiteStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsFixed = true, BqlField = typeof (AdjTranBySiteLotSerial.docType), IsKey = true)]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (AdjTranBySiteLotSerial.refNbr), IsKey = true)]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<INDocType.adjustment>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBBool(BqlField = typeof (INRegister.released))]
  public virtual bool? Released { get; set; }

  [StockItem(BqlField = typeof (AdjTranBySiteLotSerial.inventoryID), IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [Site(BqlField = typeof (AdjTranBySiteLotSerial.siteID), IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa", BqlField = typeof (AdjTranBySiteLotSerial.baseUnit))]
  public virtual string BaseUnit { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteStatusGroup.qtyHardAvail))]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (AdjTranBySiteLotSerial.qtyAdjusted))]
  public virtual Decimal? QtyAdjusted { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<AdjTranBySiteLotSerial.qtyHardAvail, IsNull>, decimal0, Case<Where<INRegister.released, Equal<True>, And<AdjTranBySiteLotSerial.qtyHardAvail, Less<decimal0>>>, Minus<AdjTranBySiteLotSerial.qtyHardAvail>, Case<Where<INRegister.released, NotEqual<True>, And<Add<AdjTranBySiteLotSerial.qtyHardAvail, AdjTranBySiteLotSerial.qtyAdjusted>, Less<decimal0>>>, Minus<Add<AdjTranBySiteLotSerial.qtyHardAvail, AdjTranBySiteLotSerial.qtyAdjusted>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? LSQtyToDeallocate { get; set; }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AdjustmentTranBySiteStatus.refNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AdjustmentTranBySiteStatus.released>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AdjustmentTranBySiteStatus.siteID>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.baseUnit>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.qtyHardAvail>
  {
  }

  public abstract class qtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.qtyAdjusted>
  {
  }

  public abstract class lsQtyToDeallocate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AdjustmentTranBySiteStatus.lsQtyToDeallocate>
  {
  }
}

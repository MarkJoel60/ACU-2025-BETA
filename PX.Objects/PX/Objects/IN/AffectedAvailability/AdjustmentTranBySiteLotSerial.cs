// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.AdjustmentTranBySiteLotSerial
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

/// <exclude />
[PXCacheName("Adjustment Transactions grouped by SiteLotSerial")]
[PXProjection(typeof (Select5<INTran, InnerJoin<InventoryItem, On<INTran.FK.InventoryItem>, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>, InnerJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>, InnerJoin<INSiteLotSerial, On<INSiteLotSerial.inventoryID, Equal<INTran.inventoryID>, And<INSiteLotSerial.siteID, Equal<INTran.siteID>, And<INSiteLotSerial.lotSerialNbr, Equal<INTran.lotSerialNbr>>>>>>>>, Where<INTran.docType, Equal<INDocType.adjustment>, And<INLotSerClass.lotSerTrack, In3<INLotSerTrack.serialNumbered, INLotSerTrack.lotNumbered>, And<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>>>>, Aggregate<GroupBy<INTran.docType, GroupBy<INTran.refNbr, GroupBy<INTran.inventoryID, GroupBy<INTran.siteID, GroupBy<INTran.lotSerialNbr, Max<INSiteLotSerial.qtyHardAvail, Sum<INTran.baseQty>>>>>>>>>))]
public class AdjustmentTranBySiteLotSerial : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.docType), IsKey = true)]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTran.refNbr), IsKey = true)]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<INDocType.adjustment>>>))]
  public virtual string RefNbr { get; set; }

  [StockItem(BqlField = typeof (INTran.inventoryID), IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [Site(BqlField = typeof (INTran.siteID), IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "", BqlField = typeof (INTran.lotSerialNbr), IsKey = true)]
  public virtual string LotSerialNbr { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa", BqlField = typeof (InventoryItem.baseUnit))]
  public virtual string BaseUnit { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteLotSerial.qtyHardAvail))]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INTran.baseQty))]
  public virtual Decimal? QtyAdjusted { get; set; }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.refNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AdjustmentTranBySiteLotSerial.siteID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.lotSerialNbr>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.baseUnit>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.qtyHardAvail>
  {
  }

  public abstract class qtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AdjustmentTranBySiteLotSerial.qtyAdjusted>
  {
  }
}

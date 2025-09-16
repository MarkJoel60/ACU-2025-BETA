// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineBillingRevision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Line Billing Revision")]
[Serializable]
public class POLineBillingRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string APDocType { get; set; }

  [PXDefault]
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual string APRefNbr { get; set; }

  [PXDefault]
  [PXDBString(2, IsKey = true, IsFixed = true)]
  public virtual string OrderType { get; set; }

  [PXDefault]
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual string OrderNbr { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public virtual int? OrderLineNbr { get; set; }

  [PXDefault]
  [PXDBString(2, IsFixed = true)]
  public virtual string LineType { get; set; }

  [PXDefault]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  public virtual string CuryID { get; set; }

  [PXDBInt]
  public virtual int? InventoryID { get; set; }

  [PXDefault]
  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string UOM { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? OrderQty { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDefault]
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  public virtual Decimal? RcptQtyMax { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? UnbilledQty { get; set; }

  [PXDefault]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlQty>))]
  public virtual Decimal? BaseUnbilledQty { get; set; }

  [PXDefault]
  [PXDBCury(typeof (POLineBillingRevision.curyID))]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDefault]
  [PXDBBaseCury]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXDefault]
  [PXDBPriceCost]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDefault]
  [PXDBPriceCost]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  public abstract class apDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineBillingRevision.apDocType>
  {
  }

  public abstract class apRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineBillingRevision.apRefNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineBillingRevision.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineBillingRevision.orderNbr>
  {
  }

  public abstract class orderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POLineBillingRevision.orderLineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineBillingRevision.lineType>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineBillingRevision.curyID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineBillingRevision.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineBillingRevision.uOM>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.baseOrderQty>
  {
  }

  public abstract class receivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.baseReceivedQty>
  {
  }

  public abstract class rcptQtyMax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.rcptQtyMax>
  {
  }

  public abstract class unbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.baseUnbilledQty>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.unbilledAmt>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.curyUnitCost>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineBillingRevision.unitCost>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLineBillingRevision.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLineBillingRevision.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineBillingRevision.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLineBillingRevision.createdDateTime>
  {
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualStatusSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (Select4<POAccrualStatus, Where<POAccrualStatus.type, Equal<POAccrualType.receipt>>, Aggregate<GroupBy<POAccrualStatus.orderType, GroupBy<POAccrualStatus.orderNbr, GroupBy<POAccrualStatus.orderLineNbr, Sum<POAccrualStatus.receivedQty, Sum<POAccrualStatus.baseReceivedQty, Sum<POAccrualStatus.receivedCost, Sum<POAccrualStatus.billedQty, Sum<POAccrualStatus.baseBilledQty, Sum<POAccrualStatus.curyBilledAmt, Sum<POAccrualStatus.billedAmt, Sum<POAccrualStatus.curyBilledCost, Sum<POAccrualStatus.billedCost, Sum<POAccrualStatus.billedDiscAmt, Sum<POAccrualStatus.curyBilledDiscAmt, Sum<POAccrualStatus.pPVAmt>>>>>>>>>>>>>>>>>), Persistent = false)]
[Serializable]
public class POAccrualStatusSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, BqlField = typeof (POAccrualStatus.lineType))]
  public virtual 
  #nullable disable
  string LineType { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POAccrualStatus.orderType))]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualStatus.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(BqlField = typeof (POAccrualStatus.orderLineNbr))]
  public virtual int? OrderLineNbr { get; set; }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (POAccrualStatus.origUOM))]
  public virtual string OrigUOM { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.origQty))]
  public virtual Decimal? OrigQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.baseOrigQty))]
  public virtual Decimal? BaseOrigQty { get; set; }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (POAccrualStatus.origCuryID))]
  public virtual string OrigCuryID { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyOrigAmt))]
  public virtual Decimal? CuryOrigAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.origAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyOrigCost))]
  public virtual Decimal? CuryOrigCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.origCost))]
  public virtual Decimal? OrigCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyOrigDiscAmt))]
  public virtual Decimal? CuryOrigDiscAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.origDiscAmt))]
  public virtual Decimal? OrigDiscAmt { get; set; }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (POAccrualStatus.receivedUOM))]
  public virtual string ReceivedUOM { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.receivedQty))]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.baseReceivedQty))]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.receivedCost))]
  public virtual Decimal? ReceivedCost { get; set; }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (POAccrualStatus.billedUOM))]
  public virtual string BilledUOM { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.billedQty))]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualStatus.baseBilledQty))]
  public virtual Decimal? BaseBilledQty { get; set; }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (POAccrualStatus.billCuryID))]
  public virtual string BillCuryID { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyBilledAmt))]
  public virtual Decimal? CuryBilledAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.billedAmt))]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyBilledCost))]
  public virtual Decimal? CuryBilledCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.billedCost))]
  public virtual Decimal? BilledCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.curyBilledDiscAmt))]
  public virtual Decimal? CuryBilledDiscAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.billedDiscAmt))]
  public virtual Decimal? BilledDiscAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualStatus.pPVAmt))]
  public virtual Decimal? PPVAmt { get; set; }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatusSummary.lineType>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatusSummary.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatusSummary.orderNbr>
  {
  }

  public abstract class orderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualStatusSummary.orderLineNbr>
  {
  }

  public abstract class origUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatusSummary.origUOM>
  {
  }

  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatusSummary.origQty>
  {
  }

  public abstract class baseOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.baseOrigQty>
  {
  }

  public abstract class origCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatusSummary.origCuryID>
  {
  }

  public abstract class curyOrigAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyOrigAmt>
  {
  }

  public abstract class origAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatusSummary.origAmt>
  {
  }

  public abstract class curyOrigCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyOrigCost>
  {
  }

  public abstract class origCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.origCost>
  {
  }

  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyOrigDiscAmt>
  {
  }

  public abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.origDiscAmt>
  {
  }

  public abstract class receivedUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatusSummary.receivedUOM>
  {
  }

  public abstract class receivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.baseReceivedQty>
  {
  }

  public abstract class receivedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.receivedCost>
  {
  }

  public abstract class billedUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatusSummary.billedUOM>
  {
  }

  public abstract class billedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.billedQty>
  {
  }

  public abstract class baseBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.baseBilledQty>
  {
  }

  public abstract class billCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatusSummary.billCuryID>
  {
  }

  public abstract class curyBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyBilledAmt>
  {
  }

  public abstract class billedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.billedAmt>
  {
  }

  public abstract class curyBilledCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyBilledCost>
  {
  }

  public abstract class billedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.billedCost>
  {
  }

  public abstract class curyBilledDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.curyBilledDiscAmt>
  {
  }

  public abstract class billedDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatusSummary.billedDiscAmt>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatusSummary.pPVAmt>
  {
  }
}

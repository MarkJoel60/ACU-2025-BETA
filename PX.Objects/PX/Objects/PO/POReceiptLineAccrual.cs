// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineAccrual
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (SelectFromBase<POAccrualSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.iNReleased, Equal<True>>>>>.And<POAccrualSplit.FK.ReceiptLine>>>>.Where<BqlOperand<POAccrualSplit.finPeriodID, IBqlString>.IsLessEqual<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>.AggregateTo<GroupBy<POAccrualSplit.pOReceiptType>, GroupBy<POAccrualSplit.pOReceiptNbr>, GroupBy<POAccrualSplit.pOReceiptLineNbr>, Sum<POAccrualSplit.accruedCost>, Sum<POAccrualSplit.pPVAmt>, Sum<POAccrualSplit.taxAccruedCost>, Sum<POAccrualSplit.baseAccruedQty>>), Persistent = false)]
public class POReceiptLineAccrual : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POAccrualSplit.pOReceiptType))]
  public virtual 
  #nullable disable
  string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POAccrualSplit.pOReceiptNbr))]
  public virtual string POReceiptNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POAccrualSplit.pOReceiptLineNbr))]
  public virtual int? POReceiptLineNbr { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.accruedCost))]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.pPVAmt))]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.taxAccruedCost))]
  public virtual Decimal? TaxAccruedCost { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualSplit.baseAccruedQty))]
  public virtual Decimal? BaseAccruedQty { get; set; }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineAccrual.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineAccrual.pOReceiptNbr>
  {
  }

  public abstract class pOReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineAccrual.pOReceiptLineNbr>
  {
  }

  public abstract class accruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineAccrual.accruedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineAccrual.pPVAmt>
  {
  }

  public abstract class taxAccruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineAccrual.taxAccruedCost>
  {
  }

  public abstract class baseAccruedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineAccrual.baseAccruedQty>
  {
  }
}

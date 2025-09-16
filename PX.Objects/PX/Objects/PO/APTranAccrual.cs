// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.APTranAccrual
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
[PXProjection(typeof (SelectFromBase<POAccrualSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.iNReleased, Equal<True>>>>>.And<POAccrualSplit.FK.ReceiptLine>>>>.Where<BqlOperand<POAccrualSplit.finPeriodID, IBqlString>.IsLessEqual<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>.AggregateTo<GroupBy<POAccrualSplit.aPDocType>, GroupBy<POAccrualSplit.aPRefNbr>, GroupBy<POAccrualSplit.aPLineNbr>, Sum<POAccrualSplit.accruedCost>, Sum<POAccrualSplit.pPVAmt>, Sum<POAccrualSplit.taxAccruedCost>, Sum<POAccrualSplit.baseAccruedQty>>), Persistent = false)]
public class APTranAccrual : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PX.Objects.AP.APDocType.List]
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (POAccrualSplit.aPDocType))]
  public virtual 
  #nullable disable
  string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POAccrualSplit.aPRefNbr))]
  public virtual string APRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POAccrualSplit.aPLineNbr))]
  public virtual int? APLineNbr { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.accruedCost))]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.pPVAmt))]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualSplit.taxAccruedCost))]
  public virtual Decimal? TaxAccruedCost { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POAccrualSplit.baseAccruedQty))]
  public virtual Decimal? BaseAccruedQty { get; set; }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranAccrual.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranAccrual.aPRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranAccrual.aPLineNbr>
  {
  }

  public abstract class accruedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranAccrual.accruedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranAccrual.pPVAmt>
  {
  }

  public abstract class taxAccruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranAccrual.taxAccruedCost>
  {
  }

  public abstract class baseAccruedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranAccrual.baseAccruedQty>
  {
  }
}

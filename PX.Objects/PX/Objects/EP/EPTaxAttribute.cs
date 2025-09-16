// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPTaxAttribute : EPTaxBaseAttribute
{
  public override bool IsTaxTipAttribute() => false;

  public EPTaxAttribute()
    : base(typeof (EPExpenseClaimDetails), typeof (EPTax), typeof (EPTaxTran), typeof (EPExpenseClaimDetails.taxCalcMode))
  {
    this.DocDate = typeof (EPExpenseClaimDetails.expenseDate);
    this.CuryLineTotal = typeof (EPExpenseClaimDetails.curyTaxableAmt);
    this.CuryTranAmt = typeof (EPExpenseClaimDetails.curyTaxableAmt);
    this.CuryDocBal = typeof (EPExpenseClaimDetails.curyAmountWithTaxes);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    sender.SetValueExt<EPExpenseClaimDetails.curyExtCost>(child, (object) value);
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<EPExpenseClaimDetails.curyTaxableAmtFromTax>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<EPExpenseClaimDetails.curyTaxAmt>(row, (object) value);
  }
}

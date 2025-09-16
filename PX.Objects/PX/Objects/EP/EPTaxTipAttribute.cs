// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTaxTipAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.EP;

public class EPTaxTipAttribute : EPTaxBaseAttribute
{
  public override bool IsTaxTipAttribute() => true;

  public EPTaxTipAttribute()
    : base(typeof (EPExpenseClaimDetails), typeof (EPTax), typeof (EPTaxTran), typeof (EPExpenseClaimDetails.taxCalcMode))
  {
    this.DocDate = typeof (EPExpenseClaimDetails.expenseDate);
    this.CuryLineTotal = typeof (EPExpenseClaimDetails.curyTipAmt);
    this.CuryTranAmt = typeof (EPExpenseClaimDetails.curyTipAmt);
    this.CuryDocBal = typeof (EPExpenseClaimDetails.curyTipAmt);
    this.TaxCategoryID = typeof (EPExpenseClaimDetails.taxTipCategoryID);
    this.CuryTaxTotal = typeof (EPExpenseClaimDetails.curyTaxTipTotal);
    this.CuryTaxRoundDiff = typeof (EPExpenseClaimDetails.curyTaxTipTotal);
    this.TaxRoundDiff = typeof (EPExpenseClaimDetails.curyTaxTipTotal);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentChargeSelect`7
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;

#nullable disable
namespace PX.Objects.AR;

public class ARPaymentChargeSelect<PaymentTable, PaymentMethodID, CashAccountID, DocDate, TranPeriodID, PMInstanceID, WhereSelect>(
  PXGraph graph) : 
  PaymentChargeSelect<PaymentTable, PaymentMethodID, CashAccountID, DocDate, TranPeriodID, ARPaymentChargeTran, ARPaymentChargeTran.entryTypeID, ARPaymentChargeTran.docType, ARPaymentChargeTran.refNbr, ARPaymentChargeTran.cashAccountID, ARPaymentChargeTran.finPeriodID, ARPaymentChargeTran.tranDate, WhereSelect>(graph)
  where PaymentTable : class, IBqlTable, new()
  where PaymentMethodID : IBqlField
  where CashAccountID : IBqlField
  where DocDate : IBqlField
  where TranPeriodID : IBqlField
  where PMInstanceID : IBqlField
  where WhereSelect : IBqlWhere, new()
{
  protected override void RelatedFieldsDefaulting(PXCache sender, PaymentTable payment)
  {
    object obj;
    sender.RaiseFieldDefaulting<PMInstanceID>((object) payment, ref obj);
    sender.SetValue<PMInstanceID>((object) payment, obj);
  }

  public void ReverseCharges(IRegister oldDoc, IRegister newDoc)
  {
    this.ReverseCharges(oldDoc, newDoc, ARPaymentType.DrCr(oldDoc.DocType) == ARPaymentType.DrCr(newDoc.DocType));
  }

  /// <summary>
  /// Check if the negative sign of the <see cref="P:PX.Objects.AR.ARPaymentChargeTran.CuryTranAmt" /> of the charge is allowed by document type.
  /// </summary>
  /// <param name="charge">The charge.</param>
  /// <returns />
  protected override bool IsAllowedNegativeSign(ARPaymentChargeTran charge)
  {
    return ARPaymentType.VoidAppl(charge.DocType) || charge.DocType == "REF";
  }
}

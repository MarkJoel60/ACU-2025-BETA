// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentChargeSelect`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentChargeSelect<PaymentTable, PaymentMethodID, CashAccountID, DocDate, TranPeriodID, WhereSelect>(
  PXGraph graph) : 
  PaymentChargeSelect<PaymentTable, PaymentMethodID, CashAccountID, DocDate, TranPeriodID, APPaymentChargeTran, APPaymentChargeTran.entryTypeID, APPaymentChargeTran.docType, APPaymentChargeTran.refNbr, APPaymentChargeTran.cashAccountID, APPaymentChargeTran.finPeriodID, APPaymentChargeTran.tranDate, WhereSelect>(graph)
  where PaymentTable : class, IBqlTable, new()
  where PaymentMethodID : IBqlField
  where CashAccountID : IBqlField
  where DocDate : IBqlField
  where TranPeriodID : IBqlField
  where WhereSelect : IBqlWhere, new()
{
  public void ReverseCharges(IRegister oldDoc, IRegister newDoc)
  {
    this.ReverseCharges(oldDoc, newDoc, APPaymentType.DrCr(oldDoc.DocType) == APPaymentType.DrCr(newDoc.DocType));
  }

  /// <summary>
  /// Check if the negative sign of the <see cref="P:PX.Objects.AP.APPaymentChargeTran.CuryTranAmt" /> of the charge is allowed by document type.
  /// </summary>
  /// <param name="charge">The charge.</param>
  /// <returns />
  protected override bool IsAllowedNegativeSign(APPaymentChargeTran charge)
  {
    return APPaymentType.VoidAppl(charge.DocType) || charge.DocType == "RQC";
  }
}

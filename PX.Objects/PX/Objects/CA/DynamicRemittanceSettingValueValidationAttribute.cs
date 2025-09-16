// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DynamicRemittanceSettingValueValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// This attribute allows to provide a dynamic validation rules for the field.<br />
/// The rule is defined as regexp and may be stored in some external field.<br />
/// In the costructor, one should provide a search method for this rule. <br />
/// </summary>
/// <example>
/// <code>
/// [DynamicValueValidation(typeof(Search&lt;PaymentMethodDetail.validRegexp,
/// 	Where&lt;PaymentMethodDetail.paymentMethodID, Equal&lt;Current&lt;VendorPaymentMethodDetail.paymentMethodID&gt;&gt;,
/// 	And&lt;PaymentMethodDetail.detailID, Equal&lt;Current&lt;VendorPaymentMethodDetail.detailID&gt;&gt;&gt;&gt;&gt;))]
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class DynamicRemittanceSettingValueValidationAttribute(Type aRegexpSearch) : 
  DynamicValueValidationAttribute(aRegexpSearch),
  IPXRowPersistingSubscriber,
  IPXRowUpdatedSubscriber
{
  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    CashAccountPaymentMethodDetail row = (CashAccountPaymentMethodDetail) e.Row;
    string detailValue = row.DetailValue;
    PXSetPropertyException ex;
    this.TryToVerifyDetailValue(cache, row, out ex);
    cache.RaiseExceptionHandling<CashAccountPaymentMethodDetail.detailValue>((object) row, (object) row.DetailValue, (Exception) ex);
  }

  private bool TryToVerifyDetailValue(
    PXCache cache,
    CashAccountPaymentMethodDetail row,
    out PXSetPropertyException ex)
  {
    ex = (PXSetPropertyException) null;
    string detailValue = row.DetailValue;
    if (!string.IsNullOrEmpty(detailValue))
    {
      string validationRegexp = this.FindValidationRegexp(cache, (object) row, out int? _);
      if (!this.ValidateValue(detailValue, validationRegexp))
      {
        PaymentMethodDetail paymentMethodDetail = PaymentMethodDetail.PK.Find(cache.Graph, row.PaymentMethodID, row.DetailID);
        ex = new PXSetPropertyException((IBqlTable) row, "The provided value does not pass the validation rule defined for this box on the Remittance Setting tab of the Payment Methods(CA204000) form: {0}.", new object[1]
        {
          (object) paymentMethodDetail.Descr
        });
        return false;
      }
    }
    return true;
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CashAccountPaymentMethodDetail row = (CashAccountPaymentMethodDetail) e.Row;
    PXSetPropertyException ex;
    if (!string.IsNullOrEmpty(row.DetailValue) && !this.TryToVerifyDetailValue(sender, row, out ex) && sender.RaiseExceptionHandling<CashAccountPaymentMethodDetail.detailValue>(e.Row, (object) row.DetailValue, (Exception) ex))
      throw new PXRowPersistingException(typeof (CashAccountPaymentMethodDetail.detailValue).Name, (object) row.DetailValue, ((Exception) ex).Message);
  }
}

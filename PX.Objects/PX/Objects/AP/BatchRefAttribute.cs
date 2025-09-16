// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BatchRefAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// This attribute implements auto-generation of the next Batch sequential number for CABatch Document<br />
/// according to the settings in the CashAccount and PaymentMethod. <br />
/// Depends upon CashAccountID, PaymentMethodID fields of the row.<br />
/// </summary>
public class BatchRefAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXFieldDefaultingSubscriber
{
  protected System.Type _CashAccountID;
  protected System.Type _PaymentTypeID;
  protected System.Type _ClassType;
  protected bool _UpdateCashManager = true;

  /// <summary>
  /// This flag defines wether the field is defaulted from the PaymentMethodAccount record by the next check number<br />
  /// If it set to false - the field on which attribute is set will not be initialized by the next value.<br />
  /// This flag doesn't affect persisting behavior - if user enter next number manually, it will be saved.<br />
  /// </summary>
  public bool UpdateCashManager
  {
    get => this._UpdateCashManager;
    set => this._UpdateCashManager = value;
  }

  private PaymentMethodAccount GetCashAccountDetail(PXCache sender, object row)
  {
    object obj1 = sender.GetValue(row, this._CashAccountID.Name);
    object obj2 = sender.GetValue(row, this._PaymentTypeID.Name);
    if (!this._UpdateCashManager || obj1 == null || obj2 == null)
      return (PaymentMethodAccount) null;
    PXSelectBase<PaymentMethodAccount> pxSelectBase = (PXSelectBase<PaymentMethodAccount>) new PXSelectReadonly<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>>>>(sender.Graph);
    PaymentMethodAccount cashAccountDetail = (PaymentMethodAccount) pxSelectBase.Select(obj1, obj2);
    pxSelectBase.View.Clear();
    if (cashAccountDetail != null && cashAccountDetail.APBatchLastRefNbr == null)
      cashAccountDetail.APBatchLastRefNbr = string.Empty;
    return cashAccountDetail;
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._FieldOrdinal);
    PaymentMethodAccount cashAccountDetail = this.GetCashAccountDetail(sender, e.Row);
    if (cashAccountDetail == null || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update || objA == null)
      return;
    string objB = AutoNumberAttribute.NextNumber(cashAccountDetail.APBatchLastRefNbr);
    if (!object.Equals((object) (string) objA, (object) objB))
      return;
    PXDatabase.Update<PaymentMethodAccount>((PXDataFieldParam) new PXDataFieldAssign("APBatchLastRefNbr", (object) objB), (PXDataFieldParam) new PXDataFieldRestrict("CashAccountID", (object) cashAccountDetail.CashAccountID), (PXDataFieldParam) new PXDataFieldRestrict("PaymentMethodID", (object) cashAccountDetail.PaymentMethodID), (PXDataFieldParam) new PXDataFieldRestrict("APBatchLastRefNbr", (object) cashAccountDetail.APBatchLastRefNbr), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
  }

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount cashAccountDetail = this.GetCashAccountDetail(sender, e.Row);
    if (cashAccountDetail != null)
    {
      try
      {
        e.NewValue = (object) AutoNumberAttribute.NextNumber(cashAccountDetail.APBatchLastRefNbr);
      }
      catch (Exception ex)
      {
        sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PaymentMethodAccount.aPBatchLastRefNbr>(sender.Graph.Caches[typeof (PaymentMethodAccount)])
        }));
      }
    }
    else
      e.NewValue = (object) null;
  }

  public BatchRefAttribute(System.Type CashAccountID, System.Type PaymentTypeID)
  {
    this._CashAccountID = CashAccountID;
    this._PaymentTypeID = PaymentTypeID;
  }

  private void DefaultRef(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetValue(e.Row, this._FieldName, (object) null);
    sender.SetDefaultExt(e.Row, this._FieldName);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ClassType = sender.GetItemType();
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._CashAccountID), this._CashAccountID.Name, new PXFieldUpdated(this.DefaultRef));
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._PaymentTypeID), this._PaymentTypeID.Name, new PXFieldUpdated(this.DefaultRef));
  }

  /// <summary>
  /// Sets IsUpdateCashManager flag for each instances of the Attibute specifed on the on the cache.
  /// </summary>
  /// <typeparam name="Field"> field, on which attribute is set</typeparam>
  /// <param name="cache"></param>
  /// <param name="data">Row. If omited, Field is set as altered for the cache</param>
  /// <param name="isUpdateCashManager">Value of the flag</param>
  public static void SetUpdateCashManager<Field>(
    PXCache cache,
    object data,
    bool isUpdateCashManager)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PaymentRefAttribute)
        ((PaymentRefAttribute) attribute).UpdateCashManager = isUpdateCashManager;
    }
  }
}

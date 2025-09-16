// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PaymentRefAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// This attribute implements auto-generation of the next check sequential number for ARPayment Document<br />
/// according to the settings in the CashAccount and PaymentMethod. <br />
/// </summary>
public class PaymentRefAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected Type _CashAccountID;
  protected Type _PaymentTypeID;
  protected Type _UpdateNextNumber;
  protected Type _IsMigratedRecord;
  protected Type _ClassType;
  protected string _TargetDisplayName;
  protected bool _UpdateCashManager = true;
  protected bool _AllowAskUpdateLastRefNbr = true;
  protected Type _Table;

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

  public bool AllowAskUpdateLastRefNbr
  {
    get => this._AllowAskUpdateLastRefNbr;
    set => this._AllowAskUpdateLastRefNbr = value;
  }

  private PaymentMethodAccount GetCashAccountDetail(PXCache sender, object row)
  {
    object obj1 = sender.GetValue(row, this._CashAccountID.Name);
    object obj2 = sender.GetValue(row, this._PaymentTypeID.Name);
    if (!this._UpdateCashManager || obj1 == null || obj2 == null)
      return (PaymentMethodAccount) null;
    PXSelectBase<PaymentMethodAccount> pxSelectBase = (PXSelectBase<PaymentMethodAccount>) new PXSelectReadonly<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>(sender.Graph);
    PaymentMethodAccount cashAccountDetail = PXResultset<PaymentMethodAccount>.op_Implicit(pxSelectBase.Select(new object[2]
    {
      obj1,
      obj2
    }));
    ((PXSelectBase) pxSelectBase).View.Clear();
    if (cashAccountDetail != null && cashAccountDetail.ARLastRefNbr == null)
    {
      cashAccountDetail.ARLastRefNbr = string.Empty;
      cashAccountDetail.ARLastRefNbrIsNull = new bool?(true);
    }
    return cashAccountDetail;
  }

  private void GetPaymentMethodSettings(
    PXCache sender,
    object row,
    out PX.Objects.CA.PaymentMethod aPaymentMethod,
    out PaymentMethodAccount aPMAccount)
  {
    aPaymentMethod = (PX.Objects.CA.PaymentMethod) null;
    aPMAccount = (PaymentMethodAccount) null;
    object obj1 = sender.GetValue(row, this._CashAccountID.Name);
    object obj2 = sender.GetValue(row, this._PaymentTypeID.Name);
    if (!this._UpdateCashManager || obj1 == null || obj2 == null)
      return;
    PXSelectBase<PaymentMethodAccount> pxSelectBase = (PXSelectBase<PaymentMethodAccount>) new PXSelectReadonly2<PaymentMethodAccount, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>(sender.Graph);
    PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod> pxResult = (PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod>) PXResultset<PaymentMethodAccount>.op_Implicit(pxSelectBase.Select(new object[2]
    {
      obj1,
      obj2
    }));
    aPaymentMethod = PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod>.op_Implicit(pxResult);
    ((PXSelectBase) pxSelectBase).View.Clear();
    PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod>.op_Implicit(pxResult);
    if (paymentMethodAccount != null && paymentMethodAccount.ARLastRefNbr == null)
    {
      paymentMethodAccount.ARLastRefNbr = string.Empty;
      paymentMethodAccount.ARLastRefNbrIsNull = new bool?(true);
    }
    aPMAccount = paymentMethodAccount;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this._AllowAskUpdateLastRefNbr)
      return;
    PaymentMethodAccount cashAccountDetail = this.GetCashAccountDetail(sender, e.Row);
    if (cashAccountDetail == null)
      return;
    bool? nullable1 = cashAccountDetail.ARAutoNextNbr;
    if (!nullable1.GetValueOrDefault())
      return;
    string objA = (string) sender.GetValue(e.Row, this._FieldOrdinal);
    bool? nullable2;
    if (!(this._IsMigratedRecord == (Type) null))
    {
      nullable2 = (bool?) sender.GetValue(e.Row, this._IsMigratedRecord.Name);
    }
    else
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    if (nullable2.GetValueOrDefault() || string.IsNullOrEmpty(objA) || string.IsNullOrEmpty((string) e.NewValue))
      return;
    if (object.Equals((object) objA, e.NewValue))
      return;
    try
    {
      if (!((Dictionary<string, PXView>) sender.Graph.Views).ContainsKey("Document") || !(sender.Graph.Views["Document"].CacheGetItemType() == sender.GetItemType()) || sender.Graph.Views["Document"].Ask(e.Row, "Confirmation", "Do you want to update Last Reference Number with entered number?", (MessageButtons) 4, (MessageIcon) 2) != 6)
        return;
      sender.SetValue(e.Row, this._UpdateNextNumber.Name, (object) true);
    }
    catch (PXException ex)
    {
      if (!(ex is PXDialogRequiredException))
        return;
      throw;
    }
  }

  /// <summary>
  /// Defines a table, from where oldValue of the field is taken from.<br />
  /// If not set - the table associated with the sender will be used<br />
  /// </summary>
  public Type Table
  {
    get => this._Table;
    set => this._Table = value;
  }

  protected virtual string GetOldField(PXCache sender, object Row)
  {
    List<PXDataField> pxDataFieldList = new List<PXDataField>();
    pxDataFieldList.Add(new PXDataField(this._FieldName));
    foreach (string key in (IEnumerable<string>) sender.Keys)
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(key, sender.GetValue(Row, key)));
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(this._Table, pxDataFieldList.ToArray()))
    {
      if (pxDataRecord != null)
        return pxDataRecord.GetString(0);
    }
    return (string) null;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._FieldOrdinal);
    bool valueOrDefault = ((bool?) sender.GetValue(e.Row, this._UpdateNextNumber.Name)).GetValueOrDefault();
    PX.Objects.CA.PaymentMethod aPaymentMethod;
    PaymentMethodAccount aPMAccount;
    this.GetPaymentMethodSettings(sender, e.Row, out aPaymentMethod, out aPMAccount);
    if (aPMAccount == null || aPaymentMethod == null || (e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    string str = (string) null;
    if (objA == null)
      return;
    if (!aPMAccount.ARAutoNextNbr.GetValueOrDefault() || !valueOrDefault && !object.Equals((object) (string) objA, (object) (str = AutoNumberAttribute.NextNumber(aPMAccount.ARLastRefNbr))))
      return;
    try
    {
      PXDatabase.Update<PaymentMethodAccount>(new PXDataFieldParam[5]
      {
        (PXDataFieldParam) new PXDataFieldAssign("ARLastRefNbr", objA),
        (PXDataFieldParam) new PXDataFieldRestrict("CashAccountID", (object) aPMAccount.CashAccountID),
        (PXDataFieldParam) new PXDataFieldRestrict("PaymentMethodID", (object) aPMAccount.PaymentMethodID),
        (PXDataFieldParam) new PXDataFieldRestrict("ARLastRefNbr", (object) aPMAccount.ARLastRefNbr),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      });
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      if (str == null)
        str = AutoNumberAttribute.NextNumber(aPMAccount.APBatchLastRefNbr);
      PXDatabase.Insert<PaymentMethodAccount>(new PXDataFieldAssign[9]
      {
        new PXDataFieldAssign("CashAccountID", (object) aPMAccount.CashAccountID),
        new PXDataFieldAssign("PaymentMethodID", (object) aPMAccount.PaymentMethodID),
        new PXDataFieldAssign("UseForAR", (object) aPMAccount.UseForAR),
        new PXDataFieldAssign("ARLastRefNbr", (object) str),
        new PXDataFieldAssign("ARAutoNextNbr", (object) aPMAccount.ARAutoNextNbr),
        new PXDataFieldAssign("ARIsDefault", (object) aPMAccount.ARIsDefault),
        new PXDataFieldAssign("UseForAP", (object) aPMAccount.UseForAP),
        new PXDataFieldAssign("APIsDefault", (object) aPMAccount.APIsDefault),
        new PXDataFieldAssign("APAutoNextNbr", (object) aPMAccount.APIsDefault)
      });
    }
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod aPaymentMethod;
    PaymentMethodAccount aPMAccount;
    this.GetPaymentMethodSettings(sender, e.Row, out aPaymentMethod, out aPMAccount);
    e.NewValue = (object) null;
    if (aPaymentMethod == null || aPMAccount == null || !aPMAccount.ARAutoNextNbr.GetValueOrDefault())
      return;
    int num = 0;
    PXGraph graph;
    object[] objArray1;
    object[] objArray2;
    do
    {
      try
      {
        e.NewValue = (object) AutoNumberAttribute.NextNumber(aPMAccount.ARLastRefNbr, ++num);
      }
      catch (Exception ex)
      {
        sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new AutoNumberException("Cannot generate the next number for the {0} sequence.", new object[1]
        {
          (object) this._TargetDisplayName
        }));
      }
      if (num > 100)
      {
        e.NewValue = (object) null;
        AutoNumberException autoNumberException = new AutoNumberException("Cannot generate the next number for the {0} sequence.", new object[1]
        {
          (object) this._TargetDisplayName
        });
      }
      graph = sender.Graph;
      objArray1 = new object[1]{ (object) aPMAccount };
      objArray2 = new object[1]{ e.NewValue };
    }
    while (PXSelectBase<CashAccountCheck, PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Current<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Current<PaymentMethodAccount.paymentMethodID>>, And<CashAccountCheck.checkNbr, Equal<Required<CashAccountCheck.checkNbr>>>>>>.Config>.SelectSingleBound(graph, objArray1, objArray2).Count == 1);
    if (num <= 1)
      return;
    sender.SetValue(e.Row, this._UpdateNextNumber.Name, (object) true);
  }

  public PaymentRefAttribute(Type CashAccountID, Type PaymentTypeID, Type UpdateNextNumber)
  {
    this._CashAccountID = CashAccountID;
    this._PaymentTypeID = PaymentTypeID;
    this._UpdateNextNumber = UpdateNextNumber;
  }

  public PaymentRefAttribute(
    Type CashAccountID,
    Type PaymentTypeID,
    Type UpdateNextNumber,
    Type IsMigratedRecord)
  {
    this._CashAccountID = CashAccountID;
    this._PaymentTypeID = PaymentTypeID;
    this._UpdateNextNumber = UpdateNextNumber;
    this._IsMigratedRecord = IsMigratedRecord;
  }

  protected virtual void DefaultRef(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj = sender.GetValue(e.Row, this._FieldName);
    sender.SetValue(e.Row, this._FieldName, (object) null);
    sender.SetDefaultExt(e.Row, this._FieldName, (object) null);
    if (sender.GetValue(e.Row, this._FieldName) != null || sender.Graph.IsCopyPasteContext)
      return;
    sender.SetValue(e.Row, this._FieldName, obj);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ClassType = sender.GetItemType();
    if (this._Table == (Type) null)
      this._Table = sender.BqlTable;
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type itemType1 = BqlCommand.GetItemType(this._CashAccountID);
    string name1 = this._CashAccountID.Name;
    PaymentRefAttribute paymentRefAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) paymentRefAttribute1, __vmethodptr(paymentRefAttribute1, DefaultRef));
    fieldUpdated1.AddHandler(itemType1, name1, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
    Type itemType2 = BqlCommand.GetItemType(this._PaymentTypeID);
    string name2 = this._PaymentTypeID.Name;
    PaymentRefAttribute paymentRefAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) paymentRefAttribute2, __vmethodptr(paymentRefAttribute2, DefaultRef));
    fieldUpdated2.AddHandler(itemType2, name2, pxFieldUpdated2);
    this._TargetDisplayName = PXUIFieldAttribute.GetDisplayName<PaymentMethodAccount.aRLastRefNbr>(sender.Graph.Caches[typeof (PaymentMethodAccount)]);
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

  public static void SetAllowAskUpdateLastRefNbr<Field>(
    PXCache cache,
    bool AllowAskUpdateLastRefNbr)
    where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PaymentRefAttribute)
        ((PaymentRefAttribute) attribute).AllowAskUpdateLastRefNbr = AllowAskUpdateLastRefNbr;
    }
  }
}

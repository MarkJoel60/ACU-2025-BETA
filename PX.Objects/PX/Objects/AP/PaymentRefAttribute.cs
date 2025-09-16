// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentRefAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// This attribute implements auto-generation of the next check sequential number for APPayment Document<br />
/// according to the settings in the CashAccount and PaymentMethod. <br />
/// It's also creates and inserts a related record into the CashAccountCheck table <br />
/// and keeps it in synch. with AP Payment (delets it if the the payment is deleted.<br />
/// Depends upon CashAccountID, PaymentMethodID, StubCntr fields of the row.<br />
/// Cache(s) for the CashAccountCheck must be present in the graph <br />
/// </summary>
public class PaymentRefAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected System.Type _CashAccountID;
  protected System.Type _PaymentTypeID;
  protected System.Type _StubCntr;
  protected System.Type _ClassType;
  protected string _TargetDisplayName;
  protected bool _UpdateCashManager = true;
  protected System.Type _Table;
  private static readonly HashSet<char> digits = new HashSet<char>()
  {
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9'
  };

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

  private static void GetPaymentMethodSettings(
    PXGraph graph,
    int? CashAccountID,
    string PaymentTypeID,
    out PX.Objects.CA.PaymentMethod paymentMethod,
    out PaymentMethodAccount pmAccount)
  {
    paymentMethod = (PX.Objects.CA.PaymentMethod) null;
    pmAccount = (PaymentMethodAccount) null;
    if (!CashAccountID.HasValue || string.IsNullOrEmpty(PaymentTypeID))
      return;
    PXSelectBase<PaymentMethodAccount> pxSelectBase = (PXSelectBase<PaymentMethodAccount>) new PXSelectReadonly2<PaymentMethodAccount, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>(graph);
    PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod> pxResult = (PXResult<PaymentMethodAccount, PX.Objects.CA.PaymentMethod>) (PXResult<PaymentMethodAccount>) pxSelectBase.Select((object) CashAccountID, (object) PaymentTypeID);
    paymentMethod = (PX.Objects.CA.PaymentMethod) pxResult;
    pxSelectBase.View.Clear();
    PaymentMethodAccount paymentMethodAccount = (PaymentMethodAccount) pxResult;
    if (paymentMethodAccount != null && paymentMethodAccount.APLastRefNbr == null)
    {
      paymentMethodAccount.APLastRefNbr = string.Empty;
      paymentMethodAccount.APLastRefNbrIsNull = new bool?(true);
    }
    pmAccount = paymentMethodAccount;
  }

  private void GetPaymentMethodSettings(
    PXCache sender,
    object row,
    out PX.Objects.CA.PaymentMethod paymentMethod,
    out PaymentMethodAccount pmAccount)
  {
    paymentMethod = (PX.Objects.CA.PaymentMethod) null;
    pmAccount = (PaymentMethodAccount) null;
    int? CashAccountID = sender.GetValue(row, this._CashAccountID.Name) as int?;
    string PaymentTypeID = sender.GetValue(row, this._PaymentTypeID.Name) as string;
    if (!this._UpdateCashManager)
      return;
    PaymentRefAttribute.GetPaymentMethodSettings(sender.Graph, CashAccountID, PaymentTypeID, out paymentMethod, out pmAccount);
  }

  public static bool IsNextNumberDuplicated(
    PXGraph graph,
    int? cashAccountID,
    string paymentMethodID,
    string nextNumber)
  {
    return (CashAccountCheck) PXSelectBase<CashAccountCheck, PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Required<PrintChecksFilter.payAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Required<PrintChecksFilter.payTypeID>>, And<CashAccountCheck.checkNbr, Equal<Required<PrintChecksFilter.nextCheckNbr>>>>>>.Config>.Select(graph, (object) cashAccountID, (object) paymentMethodID, (object) nextNumber) != null;
  }

  public static bool IsNextNumberDuplicatedExceptSame(
    PXGraph graph,
    int? cashAccountID,
    string paymentMethodID,
    string nextNumber,
    string docType,
    string refNbr)
  {
    CashAccountCheck cashAccountCheck = (CashAccountCheck) PXSelectBase<CashAccountCheck, PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Required<PrintChecksFilter.payAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Required<PrintChecksFilter.payTypeID>>, And<CashAccountCheck.checkNbr, Equal<Required<PrintChecksFilter.nextCheckNbr>>>>>>.Config>.Select(graph, (object) cashAccountID, (object) paymentMethodID, (object) nextNumber);
    if (cashAccountCheck == null)
      return false;
    return cashAccountCheck.DocType != docType || cashAccountCheck.RefNbr != refNbr;
  }

  void IPXFieldVerifyingSubscriber.FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.UpdateCashManager)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod;
    PaymentMethodAccount pmAccount;
    this.GetPaymentMethodSettings(sender, e.Row, out paymentMethod, out pmAccount);
    string newValue = (string) e.NewValue;
    if (e.Row is APPayment row && APPaymentEntry.IsCheckReallyPrinted((IPrintCheckControlable) row) && PaymentRefAttribute.IsMultistubCheck(sender.Graph, row))
      throw new PXSetPropertyException((IBqlTable) row, "The Payment Ref. number cannot be edited because the check consists of multiple stubs. To change the Payment Ref. number, use the Reprint with New Number action on the Release Payments (AP505200) form.");
    if (paymentMethod != null && pmAccount != null && e.Row != null && PaymentRefAttribute.PaymentRefMustBeUnique(paymentMethod) && PaymentRefAttribute.IsNextNumberDuplicatedExceptSame(sender.Graph, pmAccount.CashAccountID, pmAccount.PaymentMethodID, newValue, (string) sender.GetValue(e.Row, "DocType"), (string) sender.GetValue(e.Row, "RefNbr")))
      throw new PXSetPropertyException((IBqlTable) row, "A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
      {
        (object) newValue
      });
    sender.RaiseExceptionHandling(this.FieldName, e.Row, (object) newValue, (Exception) null);
  }

  public static string GetAutonumberPrefix(string number)
  {
    if (number == null)
      return (string) null;
    for (int length = number.Length; length > 0; --length)
    {
      if (!Regex.IsMatch(number[length - 1].ToString(), "^[0-9]"))
        return number.Substring(0, length);
    }
    return string.Empty;
  }

  /// <summary>
  /// Defines a table, from where oldValue of the field is taken from.<br />
  /// If not set - the table associated with the sender will be used<br />
  /// </summary>
  public System.Type Table
  {
    get => this._Table;
    set => this._Table = value;
  }

  protected virtual string GetOldValue(PXCache sender, object Row)
  {
    return sender.GetValueOriginal(Row, this._FieldName) as string;
  }

  protected virtual void InsertCheck(
    PXCache sender,
    PaymentMethodAccount det,
    APRegister payment,
    string CheckNbr)
  {
    this.InsertCheck(sender, payment, det.CashAccountID.GetValueOrDefault(), det.PaymentMethodID, CheckNbr);
  }

  protected virtual void InsertCheck(
    PXCache sender,
    APRegister payment,
    int CashAccountID,
    string PaymentMethodID,
    string CheckNbr)
  {
    PXCache<CashAccountCheck> pxCache = sender.Graph.Caches<CashAccountCheck>();
    CashAccountCheck cashAccountCheck = new CashAccountCheck();
    List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
    Dictionary<string, object> dictionary = new Dictionary<string, object>()
    {
      {
        typeof (CashAccountCheck.cashAccountID).Name.ToLower(),
        (object) CashAccountID
      },
      {
        typeof (CashAccountCheck.paymentMethodID).Name.ToLower(),
        (object) PaymentMethodID
      },
      {
        typeof (CashAccountCheck.checkNbr).Name.ToLower(),
        (object) CheckNbr
      },
      {
        typeof (CashAccountCheck.docType).Name.ToLower(),
        (object) payment.DocType
      },
      {
        typeof (CashAccountCheck.refNbr).Name.ToLower(),
        (object) payment.RefNbr
      },
      {
        typeof (CashAccountCheck.finPeriodID).Name.ToLower(),
        (object) payment.FinPeriodID
      },
      {
        typeof (CashAccountCheck.docDate).Name.ToLower(),
        (object) payment.DocDate
      },
      {
        typeof (CashAccountCheck.vendorID).Name.ToLower(),
        (object) payment.VendorID
      },
      {
        typeof (CashAccountCheck.Tstamp).Name.ToLower(),
        PXCache.NotSetValue
      }
    };
    foreach (string field in (List<string>) pxCache.Fields)
    {
      object newValue;
      if (!dictionary.TryGetValue(field.ToLower(), out newValue))
      {
        pxCache.RaiseFieldDefaulting(field, (object) cashAccountCheck, out newValue);
        if (newValue == null)
        {
          pxCache.RaiseRowInserting(cashAccountCheck);
          newValue = pxCache.GetValue((object) cashAccountCheck, field);
        }
      }
      if (newValue != PXCache.NotSetValue)
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        pxCache.RaiseCommandPreparing(field, (object) cashAccountCheck, newValue, PXDBOperation.Insert, typeof (CashAccountCheck), out description);
        if (description?.Expr != null)
          pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
      }
    }
    PXDatabase.Insert<CashAccountCheck>(pxDataFieldAssignList.ToArray());
  }

  protected virtual object GetStubCntr(PXCache sender, object Row)
  {
    object stubCntr = (object) null;
    if (this._StubCntr != (System.Type) null)
      stubCntr = sender.GetValue(Row, this._StubCntr.Name);
    if (stubCntr == null || (int) stubCntr == 0)
      stubCntr = (object) 1;
    return stubCntr;
  }

  public static string GetMaxCashAccountCheckNumberByPrefix(
    PXGraph graph,
    int? cashAccountID,
    string paymentMethodID,
    string prefix)
  {
    return PXSelectBase<CashAccountCheck, PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Required<CashAccountCheck.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Required<CashAccountCheck.paymentMethodID>>, And<CashAccountCheck.checkNbr, Like<Required<CashAccountCheck.checkNbr>>>>>, PX.Data.OrderBy<Desc<CashAccountCheck.checkNbr>>>.Config>.Select(graph, (object) cashAccountID, (object) paymentMethodID, (object) (prefix + PXDatabase.Provider.SqlDialect.WildcardAnything)).RowCast<CashAccountCheck>().FirstOrDefault<CashAccountCheck>((Func<CashAccountCheck, bool>) (cashAccountCheck => PaymentRefAttribute.digits.Contains(cashAccountCheck.CheckNbr.Skip<char>(prefix.Length).FirstOrDefault<char>())))?.CheckNbr;
  }

  public static bool PaymentRefMustBeUnique(PX.Objects.CA.PaymentMethod paymentMethod)
  {
    if (paymentMethod == null)
      return false;
    if (!paymentMethod.APCreateBatchPayment.GetValueOrDefault() && !paymentMethod.APPrintChecks.GetValueOrDefault() && paymentMethod.APRequirePaymentRef.GetValueOrDefault())
      return true;
    return !paymentMethod.APCreateBatchPayment.GetValueOrDefault() && paymentMethod.APPrintChecks.GetValueOrDefault();
  }

  public static bool IsMultistubCheck(PXGraph graph, APPayment check)
  {
    return PXSelectBase<CashAccountCheck, PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.docType, Equal<Required<APPayment.docType>>, And<CashAccountCheck.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select(graph, (object) check.DocType, (object) check.RefNbr).Count > 1;
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod paymentMethod;
    PaymentMethodAccount pmAccount;
    this.GetPaymentMethodSettings(sender, e.Row, out paymentMethod, out pmAccount);
    if (pmAccount == null || paymentMethod == null || !(e.Row is IPrintCheckControlable row))
      return;
    bool? nullable1 = row.IsPrintingProcess;
    if (nullable1.GetValueOrDefault())
      return;
    string str1 = sender.GetValue(e.Row, this._FieldOrdinal) as string;
    string oldNumber = this.GetOldValue(sender, e.Row);
    if (oldNumber == null)
      pmAccount = (PaymentMethodAccount) sender.Graph.Caches[typeof (PaymentMethodAccount)].Locate((object) pmAccount) ?? pmAccount;
    string str2 = sender.GetValue(e.Row, this._PaymentTypeID.Name) as string;
    string oldPaymentMethodID = sender.GetValueOriginal(e.Row, this._PaymentTypeID.Name) as string;
    int? nullable2 = sender.GetValue(e.Row, this._CashAccountID.Name) as int?;
    int? oldCashAccountID = sender.GetValueOriginal(e.Row, this._CashAccountID.Name) as int?;
    if (this.SkipUpdateOfAPLastRefNumber(sender.Graph, e.Row, str1))
      return;
    string a = pmAccount?.APLastRefNbr;
    if (string.IsNullOrEmpty(str1))
    {
      if (APPaymentEntry.IsCheckReallyPrinted(row))
        a = ((CashAccountCheck) PXSelectBase<CashAccountCheck, PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Optional<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Optional<PaymentMethodAccount.paymentMethodID>>>>, PX.Data.OrderBy<Desc<CashAccountCheck.Tstamp>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
        {
          (object) pmAccount
        }))?.CheckNbr ?? a;
    }
    else
    {
      nullable1 = row.IsMigratedRecord;
      if (!nullable1.GetValueOrDefault() && char.IsDigit(str1[str1.Length - 1]) && !string.Equals(oldNumber, str1) && !string.Equals(a, str1))
      {
        nullable1 = pmAccount.APAutoNextNbr;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row.IsReleaseCheckProcess;
          int num;
          if (!nullable1.GetValueOrDefault() && !sender.Graph.UnattendedMode && !sender.Graph.IsContractBasedAPI)
            num = (int) sender.Graph.Views[sender.Graph.PrimaryView].Ask(e.Row, "Confirmation", PXMessages.LocalizeFormatNoPrefix("Do you want the system to update the AP Last Reference Number on the Cash Accounts (CA202000) form with entered number '{0}'?", (object) str1), MessageButtons.YesNo, MessageIcon.Question);
          else
            num = 6;
          if (num == 6)
            a = str1;
        }
      }
    }
    pmAccount.APLastRefNbr = a;
    sender.Graph.Caches<PaymentMethodAccount>().Update(pmAccount);
    nullable1 = paymentMethod.PrintOrExport;
    if (!nullable1.GetValueOrDefault() && !PaymentRefAttribute.PaymentRefMustBeUnique(paymentMethod))
      return;
    int? nullable3;
    int? nullable4;
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
    {
      if (string.Equals(oldNumber, str1) && string.Equals(oldPaymentMethodID, str2))
      {
        nullable3 = oldCashAccountID;
        nullable4 = nullable2;
        if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
          goto label_22;
      }
      else
        goto label_22;
    }
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete || !(row.DocType != "VCK") || !(row.DocType != "VQC"))
      goto label_24;
label_22:
    if (!string.IsNullOrEmpty(oldNumber) && !string.IsNullOrEmpty(oldPaymentMethodID) && oldCashAccountID.HasValue)
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      CashAccountCheck data = PXSelectBase<CashAccountCheck, PXViewOf<CashAccountCheck>.BasedOn<SelectFromBase<CashAccountCheck, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select(sender.Graph).Select<PXResult<CashAccountCheck>, CashAccountCheck>(Expression.Lambda<Func<PXResult<CashAccountCheck>, CashAccountCheck>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).SingleOrDefault<CashAccountCheck>((Expression<Func<CashAccountCheck, bool>>) (_ => _.CashAccountID == oldCashAccountID && _.PaymentMethodID == oldPaymentMethodID && _.CheckNbr == oldNumber));
      PXCache<CashAccountCheck> pxCache = sender.Graph.Caches<CashAccountCheck>();
      pxCache.Delete(data);
      pxCache.Persist(PXDBOperation.Delete);
    }
label_24:
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
    {
      if (string.Equals(oldNumber, str1) && string.Equals(oldPaymentMethodID, str2))
      {
        nullable4 = oldCashAccountID;
        nullable3 = nullable2;
        if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
          goto label_28;
      }
      else
        goto label_28;
    }
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || !(row.DocType != "VCK") || !(row.DocType != "VQC"))
      return;
label_28:
    if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
      return;
    if (!nullable2.HasValue)
      return;
    try
    {
      this.InsertCheck(sender, (APRegister) e.Row, nullable2.Value, str2, str1);
    }
    catch (PXDatabaseException ex)
    {
      if (PaymentRefAttribute.PaymentRefMustBeUnique(paymentMethod))
        throw new PXCommandPreparingException(this._FieldName, (object) str1, "A check with the number '{0}' already exists in the system. Please enter another number.", new object[1]
        {
          (object) str1
        });
    }
  }

  protected virtual bool SkipUpdateOfAPLastRefNumber(PXGraph graph, object row, string newNumber)
  {
    if (!(row is APPayment))
      return false;
    APPayment apPayment = (APPayment) row;
    CABankTran caBankTran = (CABankTran) PXSelectBase<CABankTran, PXSelectReadonly<CABankTran, Where<CABankTran.noteID, Equal<Required<APRegister.refNoteID>>>>.Config>.Select(graph, (object) apPayment.RefNoteID);
    return !string.IsNullOrEmpty(caBankTran?.ExtRefNbr) && caBankTran.ExtRefNbr.Equals(newNumber);
  }

  public static string GetNextPaymentRef(PXGraph graph, int? CashAccountID, string PaymentTypeID)
  {
    PX.Objects.CA.PaymentMethod paymentMethod;
    PaymentMethodAccount pmAccount;
    PaymentRefAttribute.GetPaymentMethodSettings(graph, CashAccountID, PaymentTypeID, out paymentMethod, out pmAccount);
    if (paymentMethod == null || pmAccount == null || !pmAccount.APAutoNextNbr.GetValueOrDefault() || string.IsNullOrEmpty(pmAccount.APLastRefNbr))
      return string.Empty;
    string number = AutoNumberAttribute.NextNumber(pmAccount.APLastRefNbr, 1);
    if (PXSelectBase<CashAccountCheck, PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Current<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Current<PaymentMethodAccount.paymentMethodID>>, And<CashAccountCheck.checkNbr, Equal<Required<CashAccountCheck.checkNbr>>>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) pmAccount
    }, (object) number).Count == 1)
    {
      string checkNumberByPrefix = PaymentRefAttribute.GetMaxCashAccountCheckNumberByPrefix(graph, pmAccount.CashAccountID, pmAccount.PaymentMethodID, PaymentRefAttribute.GetAutonumberPrefix(number));
      number = string.IsNullOrEmpty(checkNumberByPrefix) ? (string) null : AutoNumberAttribute.NextNumber(checkNumberByPrefix, 1);
    }
    return number;
  }

  protected virtual string GetNextPaymentRef(PXCache sender, object row)
  {
    int? CashAccountID = sender.GetValue(row, this._CashAccountID.Name) as int?;
    string PaymentTypeID = sender.GetValue(row, this._PaymentTypeID.Name) as string;
    return PaymentRefAttribute.GetNextPaymentRef(sender.Graph, CashAccountID, PaymentTypeID);
  }

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.PaymentMethod paymentMethod;
    PaymentMethodAccount pmAccount;
    this.GetPaymentMethodSettings(sender, e.Row, out paymentMethod, out pmAccount);
    if (pmAccount != null && paymentMethod != null)
    {
      if (!paymentMethod.PrintOrExport.GetValueOrDefault())
      {
        try
        {
          e.NewValue = (object) this.GetNextPaymentRef(sender, e.Row);
          if (e.NewValue == null)
            return;
          pmAccount.APLastRefNbr = e.NewValue.ToString();
          sender.Graph.Caches<PaymentMethodAccount>().Update(pmAccount);
          return;
        }
        catch (PXException ex)
        {
          sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new AutoNumberException("Cannot generate the next number for the {0} sequence.", new object[1]
          {
            (object) this._TargetDisplayName
          }));
          return;
        }
      }
    }
    sender.RaiseExceptionHandling(this._FieldName, e.Row, e.NewValue, (Exception) null);
  }

  public PaymentRefAttribute(System.Type CashAccountID, System.Type PaymentTypeID, System.Type StubCntr)
  {
    this._CashAccountID = CashAccountID;
    this._PaymentTypeID = PaymentTypeID;
    this._StubCntr = StubCntr;
  }

  private void DefaultRef(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj = sender.GetValue(e.Row, this._FieldName);
    sender.SetValue(e.Row, this._FieldName, (object) null);
    sender.SetDefaultExt(e.Row, this._FieldName);
    if (!string.IsNullOrEmpty((string) sender.GetValue(e.Row, this._FieldName)))
      return;
    sender.SetValueExt(e.Row, this._FieldName, obj);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ClassType = sender.GetItemType();
    if (this._Table == (System.Type) null)
      this._Table = sender.BqlTable;
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._CashAccountID), this._CashAccountID.Name, new PXFieldUpdated(this.DefaultRef));
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._PaymentTypeID), this._PaymentTypeID.Name, new PXFieldUpdated(this.DefaultRef));
    this._TargetDisplayName = PXUIFieldAttribute.GetDisplayName<PaymentMethodAccount.aPLastRefNbr>(sender.Graph.Caches[typeof (PaymentMethodAccount)]);
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
      if (attribute is PaymentRefAttribute paymentRefAttribute)
        paymentRefAttribute.UpdateCashManager = isUpdateCashManager;
    }
  }

  public class LastCashAccountCheckSelect : 
    PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Optional<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Optional<PaymentMethodAccount.paymentMethodID>>>>, PX.Data.OrderBy<Desc<CashAccountCheck.Tstamp>>>
  {
    public LastCashAccountCheckSelect(PXGraph graph)
      : base(graph)
    {
    }

    public LastCashAccountCheckSelect(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}

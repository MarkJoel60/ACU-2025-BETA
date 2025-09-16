// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.CustomerCreditHold.CustomerCreditExtension`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Automation;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.Extensions.CustomerCreditHold;

/// <summary>A generic graph extension that defines the credit helper functionality.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
public abstract class CustomerCreditExtension<TGraph, TDoc, TCustomerIDField, TCreditHoldField, TReleasedField, TStatusField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDoc : class, IBqlTable, new()
  where TCustomerIDField : IBqlField
  where TCreditHoldField : IBqlField
  where TReleasedField : IBqlField
  where TStatusField : IBqlField
{
  protected bool _InternalCall;

  [InjectDependency]
  protected IWorkflowService WorkflowService { get; set; }

  protected virtual void _(PX.Data.Events.RowPersisting<TDoc> e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    this.Verify(e.Cache, e.Row, (EventArgs) e.Args);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<TDoc> e)
  {
    if (this._InternalCall)
      return;
    try
    {
      this._InternalCall = true;
      if (!e.Cache.ObjectsEqual<TCustomerIDField>((object) e.Row, (object) e.OldRow))
      {
        e.Cache.RaiseExceptionHandling<TCustomerIDField>((object) e.Row, (object) null, (Exception) null);
        e.Cache.RaiseExceptionHandling<TCreditHoldField>((object) e.Row, (object) null, (Exception) null);
      }
      this.Verify(e.Cache, e.Row, (EventArgs) e.Args);
    }
    finally
    {
      this._InternalCall = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<TDoc> e)
  {
    this.Verify(e.Cache, e.Row, (EventArgs) e.Args);
  }

  protected virtual Decimal? GetDocumentBalance(PXCache cache, TDoc Row)
  {
    if (!(cache.Current is ARBalances current) || cache.GetStatus((object) current) != PXEntryStatus.Inserted)
      return new Decimal?(0M);
    Decimal? currentBal = current.CurrentBal;
    Decimal? unreleasedBal = current.UnreleasedBal;
    Decimal? nullable = currentBal.HasValue & unreleasedBal.HasValue ? new Decimal?(currentBal.GetValueOrDefault() + unreleasedBal.GetValueOrDefault()) : new Decimal?();
    Decimal? totalOpenOrders = current.TotalOpenOrders;
    return !(nullable.HasValue & totalOpenOrders.HasValue) ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() + totalOpenOrders.GetValueOrDefault());
  }

  protected virtual void GetCustomerBalance(
    PXCache cache,
    PX.Objects.AR.Customer customer,
    out Decimal? CustomerBal,
    out System.DateTime? OldInvoiceDate)
  {
    ARBalances customerBalances;
    using (new PXConnectionScope())
      customerBalances = CustomerMaint.GetCustomerBalances<PX.Objects.AR.Override.Customer.sharedCreditCustomerID>(cache.Graph, customer.SharedCreditCustomerID);
    CustomerBal = new Decimal?(0M);
    if (cache.Current is ARBalances current && cache.GetStatus((object) current) == PXEntryStatus.Inserted)
    {
      foreach (ARBalances arBalances in cache.Inserted)
      {
        int? customerId1 = arBalances.CustomerID;
        int? customerId2 = current.CustomerID;
        if (customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue)
        {
          ref Decimal? local = ref CustomerBal;
          Decimal? nullable1 = CustomerBal;
          Decimal? currentBal = arBalances.CurrentBal;
          Decimal? nullable2 = arBalances.UnreleasedBal;
          Decimal? nullable3 = currentBal.HasValue & nullable2.HasValue ? new Decimal?(currentBal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable4 = arBalances.TotalOpenOrders;
          Decimal? nullable5;
          if (!(nullable3.HasValue & nullable4.HasValue))
          {
            nullable2 = new Decimal?();
            nullable5 = nullable2;
          }
          else
            nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault());
          Decimal? nullable6 = nullable5;
          Decimal? nullable7;
          if (!(nullable1.HasValue & nullable6.HasValue))
          {
            nullable4 = new Decimal?();
            nullable7 = nullable4;
          }
          else
            nullable7 = new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault());
          local = nullable7;
        }
      }
    }
    OldInvoiceDate = new System.DateTime?();
    if (customerBalances == null)
      return;
    ref Decimal? local1 = ref CustomerBal;
    Decimal? nullable8 = CustomerBal;
    Decimal? nullable9 = customerBalances.CurrentBal;
    Decimal valueOrDefault1 = nullable9.GetValueOrDefault();
    Decimal? nullable10;
    if (!nullable8.HasValue)
    {
      nullable9 = new Decimal?();
      nullable10 = nullable9;
    }
    else
      nullable10 = new Decimal?(nullable8.GetValueOrDefault() + valueOrDefault1);
    local1 = nullable10;
    ref Decimal? local2 = ref CustomerBal;
    nullable8 = CustomerBal;
    nullable9 = customerBalances.UnreleasedBal;
    Decimal valueOrDefault2 = nullable9.GetValueOrDefault();
    Decimal? nullable11;
    if (!nullable8.HasValue)
    {
      nullable9 = new Decimal?();
      nullable11 = nullable9;
    }
    else
      nullable11 = new Decimal?(nullable8.GetValueOrDefault() + valueOrDefault2);
    local2 = nullable11;
    ref Decimal? local3 = ref CustomerBal;
    nullable8 = CustomerBal;
    nullable9 = customerBalances.TotalOpenOrders;
    Decimal valueOrDefault3 = nullable9.GetValueOrDefault();
    Decimal? nullable12;
    if (!nullable8.HasValue)
    {
      nullable9 = new Decimal?();
      nullable12 = nullable9;
    }
    else
      nullable12 = new Decimal?(nullable8.GetValueOrDefault() + valueOrDefault3);
    local3 = nullable12;
    ref Decimal? local4 = ref CustomerBal;
    nullable8 = CustomerBal;
    nullable9 = customerBalances.TotalShipped;
    Decimal valueOrDefault4 = nullable9.GetValueOrDefault();
    Decimal? nullable13;
    if (!nullable8.HasValue)
    {
      nullable9 = new Decimal?();
      nullable13 = nullable9;
    }
    else
      nullable13 = new Decimal?(nullable8.GetValueOrDefault() + valueOrDefault4);
    local4 = nullable13;
    ref Decimal? local5 = ref CustomerBal;
    nullable8 = CustomerBal;
    nullable9 = customerBalances.TotalPrepayments;
    Decimal valueOrDefault5 = nullable9.GetValueOrDefault();
    Decimal? nullable14;
    if (!nullable8.HasValue)
    {
      nullable9 = new Decimal?();
      nullable14 = nullable9;
    }
    else
      nullable14 = new Decimal?(nullable8.GetValueOrDefault() - valueOrDefault5);
    local5 = nullable14;
    OldInvoiceDate = customerBalances.OldInvoiceDate;
  }

  protected virtual bool? GetHoldValue(PXCache sender, TDoc Row)
  {
    return (bool?) sender.GetValue<TCreditHoldField>((object) Row);
  }

  protected virtual bool? GetReleasedValue(PXCache sender, TDoc Row)
  {
    return (bool?) sender.GetValue<TReleasedField>((object) Row);
  }

  protected virtual bool? GetCreditCheckError(PXCache sender, TDoc Row)
  {
    return this.GetARSetup()?.CreditCheckError;
  }

  protected virtual bool? IsMigrationMode(PXCache sender) => this.GetARSetup()?.MigrationMode;

  protected virtual void PlaceOnHold(
    PXCache sender,
    TDoc Row,
    CreditVerificationResult.EnforceType enforceType)
  {
    sender.RaiseExceptionHandling<TStatusField>((object) Row, (object) true, (Exception) new PXSetPropertyException("Document status is 'On Credit Hold'.", PXErrorLevel.Warning));
  }

  public virtual void Verify(PXCache sender, TDoc Row, EventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (ARBalances)];
    ARBalances current = (ARBalances) cach.Current;
    PX.Objects.AR.Customer customer = this.EnsureCustomer(sender, Row);
    CustomerClass customerclass = this.EnsureCustomerClass(sender, customer);
    if (customer == null || this.IsMigrationMode(sender).GetValueOrDefault())
      return;
    if (current != null)
    {
      int? baccountId = customer.BAccountID;
      int? customerId = current.CustomerID;
      if (!(baccountId.GetValueOrDefault() == customerId.GetValueOrDefault() & baccountId.HasValue == customerId.HasValue))
        cach.Current = (object) null;
    }
    if (customer == null || customerclass == null)
      return;
    CreditVerificationResult res = this.VerifyByCreditRules(sender, Row, customer, customerclass);
    this.ApplyCreditVerificationResult(sender, Row, e, customer, res);
  }

  protected virtual CreditVerificationResult VerifyByCreditRules(
    PXCache sender,
    TDoc Row,
    PX.Objects.AR.Customer customer,
    CustomerClass customerclass)
  {
    CreditVerificationResult verificationResult = new CreditVerificationResult();
    verificationResult.Hold = this.GetHoldValue(sender, Row).GetValueOrDefault() || this.GetReleasedValue(sender, Row).GetValueOrDefault();
    if (customer.CreditRule == "N")
      return verificationResult;
    PXCache cach = sender.Graph.Caches[typeof (ARBalances)];
    Decimal? CustomerBal;
    System.DateTime? OldInvoiceDate;
    this.GetCustomerBalance(cach, customer, out CustomerBal, out OldInvoiceDate);
    TimeSpan timeSpan = sender.Graph.Accessinfo.BusinessDate.Value - (OldInvoiceDate ?? sender.Graph.Accessinfo.BusinessDate).Value;
    verificationResult.Failed = this.GetReleasedValue(sender, Row).GetValueOrDefault();
    if (!verificationResult.Failed && (customer.CreditRule == "B" || customer.CreditRule == "D"))
    {
      int days1 = timeSpan.Days;
      short? creditDaysPastDue1 = customer.CreditDaysPastDue;
      int? nullable = creditDaysPastDue1.HasValue ? new int?((int) creditDaysPastDue1.GetValueOrDefault()) : new int?();
      int valueOrDefault1 = nullable.GetValueOrDefault();
      if (days1 > valueOrDefault1 & nullable.HasValue)
        verificationResult.ErrorMessage = "The customer's Days Past Due number of days has been exceeded!";
      if (this.IsPutOnCreditHoldAllowed(cach, Row))
      {
        int days2 = timeSpan.Days;
        short? creditDaysPastDue2 = customer.CreditDaysPastDue;
        nullable = creditDaysPastDue2.HasValue ? new int?((int) creditDaysPastDue2.GetValueOrDefault()) : new int?();
        int valueOrDefault2 = nullable.GetValueOrDefault();
        if (days2 > valueOrDefault2 & nullable.HasValue)
          verificationResult.Failed = true;
      }
    }
    if (!verificationResult.Failed && (customer.CreditRule == "B" || customer.CreditRule == "C"))
    {
      Decimal? nullable1 = CustomerBal;
      Decimal? nullable2 = customer.CreditLimit;
      if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        verificationResult.ErrorMessage = "The customer's credit limit has been exceeded.";
      if (this.IsPutOnCreditHoldAllowed(cach, Row))
      {
        nullable2 = CustomerBal;
        Decimal? creditLimit = customer.CreditLimit;
        Decimal? overLimitAmount = customerclass.OverLimitAmount;
        nullable1 = creditLimit.HasValue & overLimitAmount.HasValue ? new Decimal?(creditLimit.GetValueOrDefault() + overLimitAmount.GetValueOrDefault()) : new Decimal?();
        if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          verificationResult.Failed = true;
      }
    }
    if (!verificationResult.Failed && customer.Status == "C")
    {
      verificationResult.ErrorMessage = "The customer status is 'Credit Hold'.";
      if (this.IsPutOnCreditHoldAllowed(cach, Row))
      {
        verificationResult.Enforce = CreditVerificationResult.EnforceType.AdminHold;
        verificationResult.Failed = true;
      }
    }
    if (!verificationResult.Failed && customer.Status == "H")
    {
      verificationResult.ErrorMessage = "The customer status is 'On Hold'.";
      verificationResult.Failed = true;
      verificationResult.Enforce = CreditVerificationResult.EnforceType.AdminHold;
    }
    if (!verificationResult.Failed && customer.Status == "I")
    {
      verificationResult.ErrorMessage = "The customer status is 'Inactive'.";
      verificationResult.Failed = true;
      verificationResult.Enforce = CreditVerificationResult.EnforceType.AdminHold;
    }
    return verificationResult;
  }

  public virtual void ApplyCreditVerificationResult(
    PXCache sender,
    TDoc Row,
    EventArgs e,
    PX.Objects.AR.Customer customer,
    CreditVerificationResult res)
  {
    PXCache cach = sender.Graph.Caches[typeof (ARBalances)];
    if (!string.IsNullOrEmpty(res.ErrorMessage) && string.IsNullOrEmpty(PXUIFieldAttribute.GetError(sender, (object) Row, res.ErrorField)))
    {
      object newValue = sender.GetValue((object) Row, res.ErrorField);
      sender.RaiseExceptionHandling(res.ErrorField, (object) Row, newValue, (Exception) new PXSetPropertyException(res.ErrorMessage, PXErrorLevel.Warning));
    }
    if (!res.Failed || res.Hold || !this.IsCreditVerificationEnabled())
      return;
    switch (e)
    {
      case PXRowUpdatedEventArgs _ when res.Enforce != CreditVerificationResult.EnforceType.None || this.GetCreditCheckError(sender, Row).GetValueOrDefault():
        this.ApplyCreditVerificationResult(sender, Row, res, cach);
        break;
      case PXRowPersistingEventArgs _ when (((PXRowPersistingEventArgs) e).Operation & PXDBOperation.Delete) != PXDBOperation.Delete && (res.Enforce != CreditVerificationResult.EnforceType.None || this.GetCreditCheckError(sender, Row).GetValueOrDefault()) && !string.IsNullOrEmpty(res.ErrorMessage):
        TDoc copy1 = (TDoc) sender.CreateCopy((object) Row);
        sender.SetValueExt<TCreditHoldField>((object) Row, (object) true);
        this.UpdateARBalances(sender, Row, copy1);
        Decimal? documentBalance = this.GetDocumentBalance(cach, Row);
        TDoc copy2 = (TDoc) sender.CreateCopy((object) Row);
        sender.SetValueExt<TCreditHoldField>((object) Row, (object) false);
        this.UpdateARBalances(sender, Row, copy2);
        Decimal? nullable = documentBalance;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
          break;
        throw new PXException(res.ErrorMessage);
    }
  }

  protected virtual void ApplyCreditVerificationResult(
    PXCache sender,
    TDoc row,
    CreditVerificationResult res,
    PXCache arbalancescache)
  {
    TDoc copy1 = (TDoc) sender.CreateCopy((object) row);
    sender.SetValueExt<TCreditHoldField>((object) row, (object) true);
    this.UpdateARBalances(sender, row, copy1);
    Decimal? documentSign = this.GetDocumentSign(sender, row);
    Decimal num = (Decimal) 1;
    if (!(documentSign.GetValueOrDefault() == num & documentSign.HasValue))
    {
      TDoc copy2 = (TDoc) sender.CreateCopy((object) row);
      sender.SetValueExt<TCreditHoldField>((object) row, (object) false);
      this.UpdateARBalances(sender, row, copy2);
    }
    else
      this.PlaceOnHold(sender, row, res.Enforce);
  }

  protected virtual Decimal? GetDocumentSign(PXCache sender, TDoc row)
  {
    return ARDocType.SignBalance((string) sender.GetValue<ARRegister.docType>((object) row));
  }

  protected virtual bool IsPutOnCreditHoldAllowed(PXCache cache, TDoc Row)
  {
    Decimal? documentBalance = this.GetDocumentBalance(cache, Row);
    Decimal num = 0M;
    return documentBalance.GetValueOrDefault() > num & documentBalance.HasValue;
  }

  protected virtual PX.Objects.AR.Customer EnsureCustomer(PXCache sender, TDoc Row)
  {
    PXCache cach = sender.Graph.Caches[typeof (PX.Objects.AR.Customer)];
    int? objB = (int?) sender.GetValue<TCustomerIDField>((object) Row);
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) cach.Current;
    if (!object.Equals((object) (int?) customer?.BAccountID, (object) objB))
    {
      cach.Current = (object) null;
      customer = (PX.Objects.AR.Customer) PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object) objB);
    }
    return customer;
  }

  protected virtual CustomerClass EnsureCustomerClass(PXCache sender, PX.Objects.AR.Customer customer)
  {
    PXCache cach = sender.Graph.Caches[typeof (CustomerClass)];
    CustomerClass customerClass = (CustomerClass) cach.Current;
    if (customer != null && !this.IsMigrationMode(sender).GetValueOrDefault() && !object.Equals((object) customerClass?.CustomerClassID, (object) customer.CustomerClassID))
    {
      cach.Current = (object) null;
      customerClass = (CustomerClass) PXSelectBase<CustomerClass, PXSelect<CustomerClass, Where<CustomerClass.customerClassID, Equal<Required<CustomerClass.customerClassID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object) customer.CustomerClassID);
    }
    return customerClass;
  }

  public abstract void UpdateARBalances(PXCache cache, TDoc newRow, TDoc oldRow);

  protected abstract ARSetup GetARSetup();

  protected virtual bool IsCreditVerificationEnabled() => true;
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARInvoiceCustomerCreditExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Extensions.CustomerCreditHold;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.GraphExtensions;

/// <summary>A mapped generic graph extension that defines the AR invoice credit helper functionality.</summary>
public abstract class ARInvoiceCustomerCreditExtension<TGraph> : 
  CustomerCreditExtension<TGraph, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.customerID, PX.Objects.AR.ARInvoice.creditHold, PX.Objects.AR.ARInvoice.released, PX.Objects.AR.ARInvoice.status>
  where TGraph : PXGraph
{
  private Dictionary<Type, List<PXRowUpdated>> _preupdatedevents = new Dictionary<Type, List<PXRowUpdated>>();

  protected override bool? GetHoldValue(PXCache sender, PX.Objects.AR.ARInvoice Row)
  {
    return new bool?(Row != null && Row.Hold.GetValueOrDefault() || Row != null && Row.PendingProcessing.GetValueOrDefault() || Row != null && Row.CreditHold.GetValueOrDefault());
  }

  protected override bool? GetCreditCheckError(PXCache sender, PX.Objects.AR.ARInvoice Row)
  {
    return Row.OrigModule == "SO" ? this.GetSOSetup()?.CreditCheckError : this.GetARSetup()?.CreditCheckError;
  }

  protected override void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice> e)
  {
    PX.Objects.AR.ARInvoice row = e.Row;
    PX.Objects.AR.ARInvoice oldRow = e.OldRow;
    if (row == null)
      return;
    if (e.Row != null && e.OldRow != null)
    {
      using (new ReadOnlyScope(new PXCache[2]
      {
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache,
        this.Base.Caches[typeof (ARBalances)]
      }))
        this.UpdateARBalances(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache, e.Row, e.OldRow);
    }
    if (this._InternalCall)
      return;
    List<PXRowUpdated> pxRowUpdatedList;
    if (this._preupdatedevents.TryGetValue(typeof (PX.Objects.AR.ARInvoice), out pxRowUpdatedList))
    {
      foreach (PXRowUpdated pxRowUpdated in pxRowUpdatedList)
      {
        PXRowUpdatedEventArgs updatedEventArgs1 = new PXRowUpdatedEventArgs((object) row, (object) oldRow, e.ExternalCall);
        PXCache cache = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache;
        PXRowUpdatedEventArgs updatedEventArgs2 = updatedEventArgs1;
        pxRowUpdated.Invoke(cache, updatedEventArgs2);
      }
    }
    if (oldRow != null)
    {
      bool? creditHold = row.CreditHold;
      bool? nullable = oldRow.CreditHold;
      if (!(creditHold.GetValueOrDefault() == nullable.GetValueOrDefault() & creditHold.HasValue == nullable.HasValue))
      {
        nullable = row.CreditHold;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = row.Hold;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          {
            nullable = row.PendingProcessing;
            bool flag3 = false;
            if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
            {
              object origDocAmt = (object) row.OrigDocAmt;
              ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.SetValue<PX.Objects.AR.ARInvoice.approvedCredit>((object) row, (object) true);
              ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.SetValue<PX.Objects.AR.ARInvoice.approvedCreditAmt>((object) row, origDocAmt);
              int? captureFailedCntr = row.CaptureFailedCntr;
              int num = 0;
              if (captureFailedCntr.GetValueOrDefault() > num & captureFailedCntr.HasValue)
                ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.SetValue<PX.Objects.AR.ARInvoice.approvedCaptureFailed>((object) row, (object) true);
              if (this.IsPrepaymentRequired(this.EnsureCustomer(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache, row), row))
                ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.SetValue<PX.Objects.AR.ARInvoice.approvedPrepaymentRequired>((object) row, (object) true);
            }
          }
        }
      }
    }
    base._(e);
    if (!(this.Base.PrimaryItemType != typeof (PX.Objects.AR.ARInvoice)) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.ObjectsEqual<PX.Objects.AR.ARInvoice.creditHold>((object) e.Row, (object) e.OldRow))
      return;
    PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.FireOnPropertyChanged<PX.Objects.AR.ARInvoice.creditHold>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.Graph, (PX.Objects.AR.ARInvoice.Events) e.Row);
  }

  public virtual void AppendPreUpdatedEvent(Type entity, PXRowUpdated del)
  {
    List<PXRowUpdated> pxRowUpdatedList;
    if (!this._preupdatedevents.TryGetValue(entity, out pxRowUpdatedList))
      pxRowUpdatedList = new List<PXRowUpdated>();
    pxRowUpdatedList.Add(del);
    this._preupdatedevents[entity] = pxRowUpdatedList;
  }

  public virtual void RemovePreUpdatedEvent(Type entity, PXRowUpdated del)
  {
    List<PXRowUpdated> pxRowUpdatedList;
    if (!this._preupdatedevents.TryGetValue(entity, out pxRowUpdatedList))
      return;
    pxRowUpdatedList.Remove(del);
  }

  protected override Decimal? GetDocumentBalance(PXCache cache, PX.Objects.AR.ARInvoice row)
  {
    Decimal? documentBalance = new Decimal?(0M);
    if (cache.Current is ARBalances current && cache.GetStatus((object) current) == 2)
      documentBalance = current.UnreleasedBal;
    Decimal? nullable = documentBalance;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue && this.IsFullAmountApproved(row))
      documentBalance = new Decimal?(0M);
    return documentBalance;
  }

  protected override void PlaceOnHold(
    PXCache sender,
    PX.Objects.AR.ARInvoice row,
    CreditVerificationResult.EnforceType enforceType)
  {
    if (enforceType == CreditVerificationResult.EnforceType.AdminHold)
    {
      sender.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.hold>((object) row, (object) true, (Exception) new PXSetPropertyException("Document status is 'On Hold'.", (PXErrorLevel) 2));
      object copy = sender.CreateCopy((object) row);
      sender.SetValueExt<PX.Objects.AR.ARInvoice.creditHold>((object) row, (object) false);
      sender.SetValueExt<PX.Objects.AR.ARInvoice.hold>((object) row, (object) true);
      sender.RaiseRowUpdated((object) row, copy);
    }
    else
      base.PlaceOnHold(sender, row, CreditVerificationResult.EnforceType.CreditHold);
    sender.SetValue<PX.Objects.AR.ARInvoice.approvedCredit>((object) row, (object) false);
    sender.SetValue<PX.Objects.AR.ARInvoice.approvedCreditAmt>((object) row, (object) 0M);
    sender.SetValue<PX.Objects.AR.ARInvoice.approvedCaptureFailed>((object) row, (object) false);
    sender.SetValue<PX.Objects.AR.ARInvoice.approvedPrepaymentRequired>((object) row, (object) false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.AR.ARInvoice>>) e).Cache, e.Row, (PX.Objects.AR.ARInvoice) null);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.AR.ARInvoice>>) e).Cache, (PX.Objects.AR.ARInvoice) null, e.Row);
  }

  public override void UpdateARBalances(PXCache cache, PX.Objects.AR.ARInvoice newRow, PX.Objects.AR.ARInvoice oldRow)
  {
    if (oldRow != null)
    {
      PXGraph graph = cache.Graph;
      PX.Objects.AR.ARInvoice ardoc = oldRow;
      Decimal? origDocAmt = oldRow.OrigDocAmt;
      Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
      ARReleaseProcess.UpdateARBalances(graph, ardoc, BalanceAmt);
    }
    if (newRow == null)
      return;
    ARReleaseProcess.UpdateARBalances(cache.Graph, newRow, newRow.OrigDocAmt);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.hold> eventArgs)
  {
    PX.Objects.AR.ARInvoice row = eventArgs.Row;
    if ((row != null ? (row.Hold.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !eventArgs.Row.Released.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.hold>>) eventArgs).Cache.SetValue<PX.Objects.AR.ARInvoice.creditHold>((object) eventArgs.Row, (object) false);
    if (!(eventArgs.Row?.Status == "W"))
      return;
    PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.FireOnPropertyChanged<PX.Objects.AR.ARInvoice.creditHold>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.hold>>) eventArgs).Cache.Graph, (PX.Objects.AR.ARInvoice.Events) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.pendingProcessing> eventArgs)
  {
    PX.Objects.AR.ARInvoice row = eventArgs.Row;
    if ((row != null ? (row.PendingProcessing.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !eventArgs.Row.Released.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.pendingProcessing>>) eventArgs).Cache.SetValue<PX.Objects.AR.ARInvoice.creditHold>((object) eventArgs.Row, (object) false);
    if (!(eventArgs.Row?.Status == "W"))
      return;
    PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.FireOnPropertyChanged<PX.Objects.AR.ARInvoice.creditHold>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.pendingProcessing>>) eventArgs).Cache.Graph, (PX.Objects.AR.ARInvoice.Events) eventArgs.Row);
  }

  protected virtual bool IsFullAmountApproved(PX.Objects.AR.ARInvoice row)
  {
    if (!row.ApprovedCredit.GetValueOrDefault())
      return false;
    Decimal? approvedCreditAmt = row.ApprovedCreditAmt;
    Decimal? origDocAmt = row.OrigDocAmt;
    return approvedCreditAmt.GetValueOrDefault() >= origDocAmt.GetValueOrDefault() & approvedCreditAmt.HasValue & origDocAmt.HasValue;
  }

  protected override CreditVerificationResult VerifyByCreditRules(
    PXCache sender,
    PX.Objects.AR.ARInvoice Row,
    PX.Objects.AR.Customer customer,
    CustomerClass customerclass)
  {
    CreditVerificationResult verificationResult = base.VerifyByCreditRules(sender, Row, customer, customerclass);
    bool? nullable;
    if (!verificationResult.Failed && Row.OrigModule == "SO")
    {
      int? captureFailedCntr = Row.CaptureFailedCntr;
      int num = 0;
      if (captureFailedCntr.GetValueOrDefault() > num & captureFailedCntr.HasValue)
      {
        nullable = Row.ApprovedCaptureFailed;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          verificationResult.Failed = true;
          verificationResult.Enforce = CreditVerificationResult.EnforceType.CreditHold;
        }
      }
    }
    if (!verificationResult.Failed && !verificationResult.Hold && Row.OrigModule == "SO")
    {
      nullable = Row.ApprovedPrepaymentRequired;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue && this.IsPrepaymentRequired(customer, Row))
      {
        verificationResult.Failed = true;
        verificationResult.Enforce = CreditVerificationResult.EnforceType.CreditHold;
      }
    }
    return verificationResult;
  }

  public virtual bool IsPrepaymentRequired(PX.Objects.AR.Customer customer, PX.Objects.AR.ARInvoice invoice)
  {
    if (!(invoice.OrigModule != "SO") && !EnumerableExtensions.IsIn<string>(invoice.DocType, "CSL", "RCS"))
    {
      Decimal? curyUnpaidBalance = invoice.CuryUnpaidBalance;
      Decimal num1 = 0M;
      if (!(curyUnpaidBalance.GetValueOrDefault() <= num1 & curyUnpaidBalance.HasValue) && invoice.TermsID != null)
      {
        PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) this.Base, invoice.TermsID);
        if (terms != null && terms.PrepaymentRequired.GetValueOrDefault())
        {
          Decimal? prepaymentPct = terms.PrepaymentPct;
          Decimal num2 = 0M;
          if (!(prepaymentPct.GetValueOrDefault() <= num2 & prepaymentPct.HasValue))
            return PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.AR.ARInvoice[1]
            {
              invoice
            }, Array.Empty<object>())) != null;
        }
        return false;
      }
    }
    return false;
  }

  protected abstract SOSetup GetSOSetup();
}

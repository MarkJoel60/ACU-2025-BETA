// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.ARPaymentCustomerCreditExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.CustomerCreditHold;
using System;

#nullable disable
namespace PX.Objects.AR.GraphExtensions;

/// <summary>A mapped generic graph extension that defines the AR payment credit helper functionality.</summary>
public class ARPaymentCustomerCreditExtension : 
  CustomerCreditExtension<ARPaymentEntry, ARPayment, ARPayment.customerID, ARPayment.hold, ARPayment.released, ARPayment.status>
{
  protected virtual void _(Events.RowInserted<ARPayment> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<ARPayment>>) e).Cache, e.Row, (ARPayment) null);
  }

  protected override void _(Events.RowUpdated<ARPayment> e)
  {
    if (e.Row != null && e.OldRow != null)
    {
      using (new ReadOnlyScope(new PXCache[2]
      {
        ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<ARPayment>>) e).Cache,
        ((PXGraph) this.Base).Caches[typeof (ARBalances)]
      }))
        this.UpdateARBalances(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<ARPayment>>) e).Cache, e.Row, e.OldRow);
    }
    base._(e);
  }

  protected virtual void _(Events.RowDeleted<ARPayment> e)
  {
    if (e.Row == null)
      return;
    this.UpdateARBalances(((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<ARPayment>>) e).Cache, (ARPayment) null, e.Row);
  }

  public override void UpdateARBalances(PXCache cache, ARPayment newRow, ARPayment oldRow)
  {
    if (oldRow != null)
    {
      PXGraph graph = cache.Graph;
      ARPayment ardoc = oldRow;
      Decimal? origDocAmt = oldRow.OrigDocAmt;
      Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
      ARReleaseProcess.UpdateARBalances(graph, (ARRegister) ardoc, BalanceAmt);
    }
    if (newRow == null)
      return;
    ARReleaseProcess.UpdateARBalances(cache.Graph, (ARRegister) newRow, newRow.OrigDocAmt);
  }

  protected override ARSetup GetARSetup() => ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;

  protected override CreditVerificationResult VerifyByCreditRules(
    PXCache sender,
    ARPayment Row,
    Customer customer,
    CustomerClass customerclass)
  {
    CreditVerificationResult verificationResult = base.VerifyByCreditRules(sender, Row, customer, customerclass);
    if (verificationResult.Failed && customer.Status == "H")
    {
      verificationResult.Failed = false;
      verificationResult.Enforce = CreditVerificationResult.EnforceType.None;
    }
    return verificationResult;
  }
}

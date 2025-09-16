// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.Level3Graph`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CC.PaymentProcessing;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CC.GraphExtensions;

public abstract class Level3Graph<TGraph, TPrimary, TDerived> : PXGraphExtension<
#nullable disable
TGraph>
  where TGraph : PXGraph, new()
  where TPrimary : class, IBqlTable, new()
  where TDerived : Level3Graph<TGraph, TPrimary, TDerived>
{
  public PXSelectExtension<PX.Objects.Extensions.PaymentTransaction.Payment> PaymentDoc;
  public PXSelectExtension<ExternalTransactionDetail> ExternalTransaction;
  private ICCPaymentProcessingRepository _repo;
  public PXAction<TPrimary> updateLevel3DataCCPayment;

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable UpdateLevel3DataCCPayment(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Level3Graph<TGraph, TPrimary, TDerived>.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new Level3Graph<TGraph, TPrimary, TDerived>.\u003C\u003Ec__DisplayClass4_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.level3Processing = new Level3Processing(this.GetPaymentProcessingRepo());
    TGraph instance = PXGraph.CreateInstance<TGraph>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.level3Graph = instance.GetExtension<TDerived>();
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) cDisplayClass40.level3Graph.PaymentDoc).Current = ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation<TGraph>((PXGraphExtension<TGraph>) this, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CUpdateLevel3DataCCPayment\u003Eb__0)));
    return adapter.Get();
  }

  public IEnumerable<ExternalTransactionDetail> GetExtTranDetails()
  {
    if (this.ExternalTransaction != null)
    {
      foreach (ExternalTransactionDetail extTranDetail in GraphHelper.RowCast<ExternalTransactionDetail>((IEnumerable) ((PXSelectBase<ExternalTransactionDetail>) this.ExternalTransaction).Select(Array.Empty<object>())))
        yield return extTranDetail;
    }
  }

  public IEnumerable<IExternalTransaction> GetExtTrans()
  {
    foreach (IExternalTransaction extTranDetail in this.GetExtTranDetails())
      yield return extTranDetail;
  }

  protected virtual ICCPaymentProcessingRepository GetPaymentProcessingRepo()
  {
    if (this._repo == null)
      this._repo = CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    return this._repo;
  }

  protected abstract PX.Objects.CC.Utility.PaymentMapping GetPaymentMapping();

  protected abstract PX.Objects.CC.Utility.ExternalTransactionDetailMapping GetExternalTransactionMapping();
}

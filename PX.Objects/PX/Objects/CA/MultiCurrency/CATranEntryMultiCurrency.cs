// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CATranEntryMultiCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

public sealed class CATranEntryMultiCurrency : CAMultiCurrencyGraph<CATranEntry, CAAdj>
{
  protected int? AccountProcessing;

  protected override MultiCurrencyGraph<CATranEntry, CAAdj>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<CATranEntry, CAAdj>.CurySourceMapping(typeof (CashAccount))
    {
      CuryID = typeof (CashAccount.curyID),
      CuryRateTypeID = typeof (CashAccount.curyRateTypeID)
    };
  }

  protected override MultiCurrencyGraph<CATranEntry, CAAdj>.DocumentMapping GetDocumentMapping()
  {
    return new MultiCurrencyGraph<CATranEntry, CAAdj>.DocumentMapping(typeof (CAAdj))
    {
      CuryID = typeof (CAAdj.curyID),
      CuryInfoID = typeof (CAAdj.curyInfoID),
      BAccountID = typeof (CAAdj.cashAccountID),
      DocumentDate = typeof (CAAdj.tranDate),
      BranchID = typeof (CAAdj.branchID)
    };
  }

  protected override PXSelectBase[] GetChildren()
  {
    return new PXSelectBase[4]
    {
      (PXSelectBase) this.Base.CAAdjRecords,
      (PXSelectBase) this.Base.CASplitRecords,
      (PXSelectBase) this.Base.Tax_Rows,
      (PXSelectBase) this.Base.Taxes
    };
  }

  protected string DocumentStatus => ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current?.Status;

  protected override string Module => "CA";

  protected override PX.Objects.Extensions.MultiCurrency.CurySource CurrentSourceSelect()
  {
    return PXResultset<PX.Objects.Extensions.MultiCurrency.CurySource>.op_Implicit(((PXSelectBase<PX.Objects.Extensions.MultiCurrency.CurySource>) this.CurySource).Select(new object[1]
    {
      (object) this.AccountProcessing
    }));
  }

  protected void _(Events.FieldUpdated<CAAdj, CAAdj.cashAccountID> e)
  {
    this.SourceFieldUpdated<CAAdj.curyInfoID, CAAdj.curyID, CAAdj.tranDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CAAdj, CAAdj.cashAccountID>>) e).Cache, (IBqlTable) e.Row);
  }

  protected void _(Events.FieldUpdated<CAAdj, CAAdj.tranDate> e)
  {
    if (e.Row == null)
      return;
    this.DateFieldUpdated<CAAdj.curyInfoID, CAAdj.tranDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CAAdj, CAAdj.tranDate>>) e).Cache, (IBqlTable) e.Row);
  }

  protected bool ShouldBeDisabledDueToDocStatus()
  {
    string documentStatus = this.DocumentStatus;
    return documentStatus == "R" || documentStatus == "P";
  }

  protected override bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, PX.Objects.Extensions.MultiCurrency.CurySource source)
  {
    return !this.ShouldBeDisabledDueToDocStatus() && base.AllowOverrideRate(sender, info, source);
  }

  protected void _(Events.RowInserting<CASplit> e, PXRowInserting baseMethod)
  {
    this.UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CASplit>>) e).Cache, (ICATranDetail) new CASplit(), (ICATranDetail) e.Row);
    baseMethod.Invoke(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CASplit>>) e).Cache, ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CASplit>>) e).Args);
  }

  protected void _(Events.RowUpdating<CASplit> e, PXRowUpdating baseMethod)
  {
    this.UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CASplit>>) e).Cache, (ICATranDetail) e.Row, (ICATranDetail) e.NewRow);
    baseMethod.Invoke(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CASplit>>) e).Cache, ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CASplit>>) e).Args);
  }
}

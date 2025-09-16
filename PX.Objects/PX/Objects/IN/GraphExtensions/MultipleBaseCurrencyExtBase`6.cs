// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.MultipleBaseCurrencyExtBase`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class MultipleBaseCurrencyExtBase<TGraph, TDocument, TLine, TDocumentBranch, TLineBranch, TLineSite> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
  where TDocumentBranch : class, IBqlField
  where TLineBranch : class, IBqlField
  where TLineSite : class, IBqlField
{
  protected virtual void _(PX.Data.Events.RowUpdated<TDocument> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TDocument>>) e).Cache.ObjectsEqual<TDocumentBranch>((object) e.OldRow, (object) e.Row) || string.Equals(((PX.Objects.GL.Branch) PXSelectorAttribute.Select<TDocumentBranch>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TDocument>>) e).Cache, (object) e.Row))?.BaseCuryID, ((PX.Objects.GL.Branch) PXSelectorAttribute.Select<TDocumentBranch>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TDocument>>) e).Cache, (object) e.OldRow))?.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      return;
    this.OnDocumentBaseCuryChanged(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TDocument>>) e).Cache, e.Row);
  }

  protected virtual void OnDocumentBaseCuryChanged(PXCache cache, TDocument row)
  {
    PXSelectBase<TLine> transactionView = this.GetTransactionView();
    foreach (PXResult<TLine> pxResult in transactionView.Select(Array.Empty<object>()))
    {
      TLine row1 = PXResult<TLine>.op_Implicit(pxResult);
      PXCache cache1 = ((PXSelectBase) transactionView).Cache;
      GraphHelper.MarkUpdated(cache1, (object) row1, true);
      cache1.VerifyFieldAndRaiseException<TLineBranch>((object) row1);
      cache1.VerifyFieldAndRaiseException<TLineSite>((object) row1);
    }
  }

  protected abstract PXSelectBase<TLine> GetTransactionView();

  protected virtual void _(PX.Data.Events.FieldUpdated<TLine, TLineBranch> eventArgs)
  {
    if (string.Equals(PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TLine, TLineBranch>>) eventArgs).Cache.GetValue<TLineBranch>((object) eventArgs.Row))?.BaseCuryID, PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<TLine, TLineBranch>, TLine, object>) eventArgs).OldValue)?.BaseCuryID, StringComparison.OrdinalIgnoreCase))
      return;
    this.OnLineBaseCuryChanged(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TLine, TLineBranch>>) eventArgs).Cache, eventArgs.Row);
  }

  protected virtual void OnLineBaseCuryChanged(PXCache cache, TLine row)
  {
    cache.SetDefaultExt<TLineSite>((object) row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TLine> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TLine>>) e).Cache.VerifyFieldAndRaiseException<TLineBranch>((object) e.Row);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TLine>>) e).Cache.VerifyFieldAndRaiseException<TLineSite>((object) e.Row);
  }

  protected virtual void SetDefaultBaseCurrency<TCuryID, TCuryInfoID, TDocDate>(
    PXCache cache,
    TDocument document,
    bool resetCuryID)
    where TCuryID : class, IBqlField
    where TCuryInfoID : class, IBqlField
    where TDocDate : class, IBqlField
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<TCuryInfoID>(cache, (object) document, resetCuryID);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(this.Base.Caches[typeof (PX.Objects.CM.CurrencyInfo)], (object) currencyInfo);
    if (!string.IsNullOrEmpty(error))
    {
      object obj = cache.GetValue<TDocDate>((object) document);
      cache.RaiseExceptionHandling<TDocDate>((object) document, obj, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
    }
    if (currencyInfo == null)
      return;
    cache.SetValue<TCuryID>((object) document, (object) currencyInfo.CuryID);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SyncOriginalValues`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public abstract class SyncOriginalValues<TOrigLine> : PXGraphExtension<SOShipmentEntry> where TOrigLine : class, IBqlTable, new()
{
  protected virtual void _(Events.RowUpdated<TOrigLine> e)
  {
    if (!this.CalculateFullOpenQtyCalculationOnTheFly() || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<SOShipment>) this.Base.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    this.ShippedQtyUpdated(e.Row, this.GetBaseShippedQty(e.OldRow), true);
  }

  protected virtual bool CalculateFullOpenQtyCalculationOnTheFly()
  {
    return !((PXGraph) this.Base).UnattendedMode && !((PXGraph) this.Base).IsContractBasedAPI;
  }

  protected virtual void ShippedQtyUpdated(
    TOrigLine row,
    Decimal? originalBaseShippedQty,
    bool refreshUI = false)
  {
    Decimal? baseShippedQty = this.GetBaseShippedQty(row);
    Decimal? nullable = originalBaseShippedQty;
    if (baseShippedQty.GetValueOrDefault() == nullable.GetValueOrDefault() & baseShippedQty.HasValue == nullable.HasValue)
      return;
    Decimal? openQty = this.GetOpenQty(row);
    Decimal? baseOpenQty = this.GetBaseOpenQty(row);
    foreach (SOShipLine soShipLine in GraphHelper.RowCast<SOShipLine>((IEnumerable) this.SelectAffectedSOShipLines(row)).ToList<SOShipLine>())
    {
      ((PXSelectBase) this.Base.Transactions).Cache.SetValue<SOShipLine.fullOpenQty>((object) soShipLine, (object) openQty);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValue<SOShipLine.baseFullOpenQty>((object) soShipLine, (object) baseOpenQty);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) soShipLine);
    }
    if (!refreshUI)
      return;
    ((PXSelectBase) this.Base.Transactions).View.RequestRefresh();
  }

  [PXOverride]
  public virtual bool PrePersist(Func<bool> baseMethod)
  {
    if (this.CalculateFullOpenQtyCalculationOnTheFly())
      return baseMethod();
    foreach (TOrigLine origLine in GraphHelper.Caches<TOrigLine>((PXGraph) this.Base).Rows.Updated)
      this.ShippedQtyUpdated(origLine, this.GetOriginalBaseShippedQty(origLine));
    return baseMethod();
  }

  protected abstract Decimal? GetOpenQty(TOrigLine line);

  protected abstract Decimal? GetBaseOpenQty(TOrigLine line);

  protected abstract Decimal? GetOriginalBaseShippedQty(TOrigLine line);

  protected abstract Decimal? GetBaseShippedQty(TOrigLine line);

  protected abstract PXResultset<SOShipLine> SelectAffectedSOShipLines(TOrigLine row);
}

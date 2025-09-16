// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INUnitLotSerClassBase`9
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class INUnitLotSerClassBase<TGraph, TUnitID, TUnitType, TParent, TParentID, TParentBaseUnit, TParentSalesUnit, TParentPurchaseUnit, TParentLotSerClassID> : 
  INUnitValidatorBase<TGraph, TUnitID, TUnitType, TParent, TParentID, TParentBaseUnit, TParentSalesUnit, TParentPurchaseUnit>
  where TGraph : PXGraph
  where TUnitID : class, IBqlField
  where TUnitType : class, IConstant, new()
  where TParent : class, IBqlTable, new()
  where TParentID : class, IBqlField
  where TParentBaseUnit : class, IBqlField
  where TParentSalesUnit : class, IBqlField
  where TParentPurchaseUnit : class, IBqlField
  where TParentLotSerClassID : class, IBqlField
{
  protected virtual void _(Events.RowPersisting<INUnit> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    PXCache pxCache = (PXCache) GraphHelper.Caches<INLotSerClass>((PXGraph) this.Base);
    if (pxCache.Current == null || !(((INLotSerClass) pxCache.Current).LotSerTrack == "S") || !INUnitAttribute.IsFractional(e.Row))
      return;
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INUnit>>) e).Cache.RaiseExceptionHandling<INUnit.unitMultDiv>((object) e.Row, (object) e.Row.UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
  }

  protected virtual void _(Events.RowSelected<INUnit> e)
  {
    if (this.ReadLotSerClass()?.LotSerTrack == "S" && INUnitAttribute.IsFractional(e.Row))
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INUnit>>) e).Cache.RaiseExceptionHandling<INUnit.unitMultDiv>((object) e.Row, (object) e.Row.UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
    else
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INUnit>>) e).Cache.RaiseExceptionHandling<INUnit.unitMultDiv>((object) e.Row, (object) null, (Exception) null);
  }

  protected virtual void _(
    Events.FieldVerifying<TParent, TParentLotSerClassID> e)
  {
    if (!(INLotSerClass.PK.FindDirty((PXGraph) this.Base, (string) ((Events.FieldVerifyingBase<Events.FieldVerifying<TParent, TParentLotSerClassID>, TParent, object>) e).NewValue)?.LotSerTrack == "S"))
      return;
    foreach (INUnit selectOwnConversion in this.SelectOwnConversions((string) null))
    {
      if (INUnitAttribute.IsFractional(selectOwnConversion))
      {
        GraphHelper.MarkUpdated((PXCache) this.UnitCache, (object) selectOwnConversion, true);
        ((PXCache) this.UnitCache).RaiseExceptionHandling<INUnit.unitMultDiv>((object) selectOwnConversion, (object) selectOwnConversion.UnitMultDiv, (Exception) new PXSetPropertyException("Fractional unit conversions not supported for serial numbered items"));
      }
    }
  }

  protected INLotSerClass ReadLotSerClass()
  {
    PXCache cach = this.Base.Caches[BqlCommand.GetItemType(typeof (TParentLotSerClassID))];
    return INLotSerClass.PK.FindDirty((PXGraph) this.Base, (string) cach.GetValue(cach.Current, typeof (TParentLotSerClassID).Name));
  }
}

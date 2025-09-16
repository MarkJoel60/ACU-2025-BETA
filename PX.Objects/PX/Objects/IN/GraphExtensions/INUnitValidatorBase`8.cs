// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INUnitValidatorBase`8
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class INUnitValidatorBase<TGraph, TUnitID, TUnitType, TParent, TParentID, TParentBaseUnit, TParentSalesUnit, TParentPurchaseUnit> : 
  UnitsOfMeasureBase<TGraph, TUnitID, TUnitType, TParent, TParentID, TParentBaseUnit, TParentSalesUnit, TParentPurchaseUnit>
  where TGraph : PXGraph
  where TUnitID : class, IBqlField
  where TUnitType : class, IConstant, new()
  where TParent : class, IBqlTable, new()
  where TParentID : class, IBqlField
  where TParentBaseUnit : class, IBqlField
  where TParentSalesUnit : class, IBqlField
  where TParentPurchaseUnit : class, IBqlField
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Base.OnBeforeCommit += (Action<PXGraph>) (graph => graph.Apply<PXGraph>((Action<PXGraph>) (g => this.ValidateUnitConversions(this.ParentCurrent))));
  }

  protected abstract void ValidateUnitConversions(TParent validatedItem);

  protected virtual void _(Events.RowPersisted<INUnit> e)
  {
    if (e.TranStatus == null && EnumerableExtensions.IsIn<PXDBOperation>((PXDBOperation) (e.Operation & 3), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      int? nullable1 = (int?) ((PXCache) this.UnitCache).GetValue<TUnitID>((object) e.Row);
      if (nullable1.HasValue)
      {
        int? nullable2 = nullable1;
        int num = 0;
        if (!(nullable2.GetValueOrDefault() < num & nullable2.HasValue))
          return;
      }
      throw new PXRowPersistedException(typeof (TUnitID).Name, (object) nullable1, "'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<TUnitID>(((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<INUnit>>) e).Cache)
      });
    }
  }
}

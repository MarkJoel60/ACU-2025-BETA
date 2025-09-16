// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.PriceRecalcExt`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Discount;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.RQ;

public abstract class PriceRecalcExt<TGraph, TMaster, TDetail, TPriceField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : class, IBqlTable, new()
  where TDetail : class, IBqlTable, new()
  where TPriceField : IBqlField
{
  public PXFilter<RecalcDiscountsParamFilter> recalcPricesFilter;
  public PXAction<TMaster> recalculatePricesAction;
  public PXAction<TMaster> recalculatePricesActionOk;

  [PXButton]
  [PXUIField]
  public virtual IEnumerable RecalculatePricesAction(PXAdapter adapter)
  {
    if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcPricesFilter).AskExt() == 1)
    {
      PXCache cache = ((PXSelectBase) this.DetailSelect).Cache;
      foreach (PXResult<TDetail> pxResult in this.DetailSelect.Select(Array.Empty<object>()))
      {
        TDetail line = PXResult<TDetail>.op_Implicit(pxResult);
        TDetail copy = (TDetail) cache.CreateCopy((object) line);
        PriceRecalcExt<TGraph, TMaster, TDetail, TPriceField>.IPricedLine pricedLine = this.WrapLine(line);
        bool? nullable1 = ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcPricesFilter).Current.OverrideManualPrices;
        if (nullable1.GetValueOrDefault())
          pricedLine.ManualPrice = new bool?(false);
        if (pricedLine.InventoryID.HasValue)
        {
          nullable1 = pricedLine.ManualPrice;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcPricesFilter).Current.RecalcUnitPrices;
            if (!nullable1.GetValueOrDefault())
            {
              Decimal? nullable2 = pricedLine.CuryUnitPrice;
              Decimal num1 = 0M;
              if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
              {
                nullable2 = pricedLine.CuryExtPrice;
                Decimal num2 = 0M;
                if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                {
                  nullable1 = ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcPricesFilter).Current.OverrideManualPrices;
                  bool flag = false;
                  if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
                    continue;
                }
                else
                  continue;
              }
              else
                continue;
            }
            cache.RaiseFieldUpdated<TPriceField>((object) line, (object) 0M);
            cache.SetDefaultExt<TPriceField>((object) line);
            cache.IsDirty = true;
            cache.RaiseRowUpdated((object) line, (object) copy);
            GraphHelper.MarkUpdated(cache, (object) line, true);
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable RecalculatePricesActionOk(PXAdapter adapter) => adapter.Get();

  protected abstract PXSelectBase<TDetail> DetailSelect { get; }

  protected abstract PriceRecalcExt<TGraph, TMaster, TDetail, TPriceField>.IPricedLine WrapLine(
    TDetail line);

  protected interface IPricedLine
  {
    bool? ManualPrice { get; set; }

    int? InventoryID { get; set; }

    Decimal? CuryUnitPrice { get; set; }

    Decimal? CuryExtPrice { get; set; }
  }
}

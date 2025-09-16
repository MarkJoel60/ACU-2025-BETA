// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ARPriceWorksheetMaintASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.DR;

public class ARPriceWorksheetMaintASC606 : PXGraphExtension<ARPriceWorksheetMaint>
{
  private bool CustomerDiscountsFeatureEnabled;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  public virtual void Initialize()
  {
    this.CustomerDiscountsFeatureEnabled = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    PXUIFieldAttribute.SetVisible<ARPriceWorksheet.isFairValue>(((PXSelectBase) this.Base.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARPriceWorksheet.isProrated>(((PXSelectBase) this.Base.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARPriceWorksheet.discountable>(((PXSelectBase) this.Base.Document).Cache, (object) null, this.CustomerDiscountsFeatureEnabled);
  }

  protected virtual void _(
    Events.FieldVerifying<ARPriceWorksheet, ARPriceWorksheet.isPromotional> e)
  {
    ARPriceWorksheet row = e.Row;
    if ((row != null ? (row.IsFairValue.GetValueOrDefault() ? 1 : 0) : 0) != 0 && ((bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<ARPriceWorksheet, ARPriceWorksheet.isPromotional>, ARPriceWorksheet, object>) e).NewValue).GetValueOrDefault())
      throw new PXSetPropertyException("The price cannot be both promotional and fair value.", (PXErrorLevel) 4);
  }

  protected virtual void _(
    Events.FieldVerifying<ARPriceWorksheet, ARPriceWorksheet.isFairValue> e)
  {
    if (!((bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<ARPriceWorksheet, ARPriceWorksheet.isFairValue>, ARPriceWorksheet, object>) e).NewValue).GetValueOrDefault())
      return;
    foreach (PXResult<ARPriceWorksheetDetail> pxResult in ((PXSelectBase<ARPriceWorksheetDetail>) this.Base.Details).Select(Array.Empty<object>()))
    {
      if (PXResult<ARPriceWorksheetDetail>.op_Implicit(pxResult).SkipLineDiscounts.GetValueOrDefault())
        throw new PXSetPropertyException("The Ignore Automatic Line Discounts and Fair Value check boxes cannot be selected simultaneously.", (PXErrorLevel) 4);
    }
  }

  protected virtual void _(Events.RowSelected<ARPriceWorksheet> e)
  {
    ARPriceWorksheet row = e.Row;
    if (row == null)
      return;
    PXCache cache1 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARPriceWorksheet>>) e).Cache;
    ARPriceWorksheet arPriceWorksheet1 = row;
    bool? nullable = row.IsFairValue;
    int num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.skipLineDiscounts>(cache1, (object) arPriceWorksheet1, num1 != 0);
    PXCache cache2 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARPriceWorksheet>>) e).Cache;
    ARPriceWorksheet arPriceWorksheet2 = row;
    nullable = row.IsFairValue;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.isPromotional>(cache2, (object) arPriceWorksheet2, num2 != 0);
    PXCache cache3 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARPriceWorksheet>>) e).Cache;
    ARPriceWorksheet arPriceWorksheet3 = row;
    nullable = row.SkipLineDiscounts;
    int num3;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.IsPromotional;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.isFairValue>(cache3, (object) arPriceWorksheet3, num3 != 0);
  }

  protected virtual void _(Events.RowSelected<ARPriceWorksheetDetail> e)
  {
    ARPriceWorksheetDetail row = e.Row;
    if (row == null)
      return;
    ARPriceWorksheet current = ((PXSelectBase<ARPriceWorksheet>) this.Base.Document).Current;
    if (current == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheetDetail.skipLineDiscounts>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARPriceWorksheetDetail>>) e).Cache, (object) row, !current.IsFairValue.GetValueOrDefault());
  }

  protected virtual void _(
    Events.FieldUpdated<ARPriceWorksheet, ARPriceWorksheet.isFairValue> e)
  {
    if (!((bool?) ((Events.FieldUpdatedBase<Events.FieldUpdated<ARPriceWorksheet, ARPriceWorksheet.isFairValue>, ARPriceWorksheet, object>) e).OldValue).GetValueOrDefault())
      return;
    ARPriceWorksheet row = e.Row;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      bool? isFairValue = row.IsFairValue;
      bool flag = false;
      num = isFairValue.GetValueOrDefault() == flag & isFairValue.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    e.Row.IsProrated = new bool?(false);
    e.Row.Discountable = new bool?(false);
  }

  protected virtual void _(
    Events.FieldUpdated<CopyPricesFilter, CopyPricesFilter.isFairValue> e)
  {
    if (!((bool?) ((Events.FieldUpdatedBase<Events.FieldUpdated<CopyPricesFilter, CopyPricesFilter.isFairValue>, CopyPricesFilter, object>) e).OldValue).GetValueOrDefault())
      return;
    CopyPricesFilter row = e.Row;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      bool? isFairValue = row.IsFairValue;
      bool flag = false;
      num = isFairValue.GetValueOrDefault() == flag & isFairValue.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    e.Row.IsProrated = new bool?(false);
    e.Row.Discountable = new bool?(false);
  }

  protected virtual void _(Events.RowSelected<CopyPricesFilter> e)
  {
    CopyPricesFilter row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.isFairValue>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache, (object) row, true);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.isProrated>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache, (object) row, true);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.discountable>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache, (object) row, this.CustomerDiscountsFeatureEnabled);
    PXCache cache1 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache;
    CopyPricesFilter copyPricesFilter1 = row;
    bool? nullable = row.IsFairValue;
    int num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.isPromotional>(cache1, (object) copyPricesFilter1, num1 != 0);
    PXCache cache2 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache;
    CopyPricesFilter copyPricesFilter2 = row;
    nullable = row.IsFairValue;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.isProrated>(cache2, (object) copyPricesFilter2, num2 != 0);
    PXCache cache3 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache;
    CopyPricesFilter copyPricesFilter3 = row;
    nullable = row.IsPromotional;
    int num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.isFairValue>(cache3, (object) copyPricesFilter3, num3 != 0);
    PXCache cache4 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CopyPricesFilter>>) e).Cache;
    CopyPricesFilter copyPricesFilter4 = row;
    nullable = row.IsFairValue;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.discountable>(cache4, (object) copyPricesFilter4, num4 != 0);
  }
}

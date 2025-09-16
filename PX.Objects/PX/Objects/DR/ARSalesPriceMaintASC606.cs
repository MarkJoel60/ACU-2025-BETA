// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ARSalesPriceMaintASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class ARSalesPriceMaintASC606 : PXGraphExtension<ARSalesPriceMaint>
{
  private bool CustomerDiscountsFeatureEnabled;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  public virtual void Initialize()
  {
    this.CustomerDiscountsFeatureEnabled = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    PXUIFieldAttribute.SetVisible<ARSalesPrice.isFairValue>(((PXSelectBase) this.Base.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARSalesPrice.isProrated>(((PXSelectBase) this.Base.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisibility<ARSalesPrice.isFairValue>(((PXSelectBase) this.Base.Records).Cache, (object) null, (PXUIVisibility) 7);
    PXUIFieldAttribute.SetVisibility<ARSalesPrice.isProrated>(((PXSelectBase) this.Base.Records).Cache, (object) null, (PXUIVisibility) 7);
    PXUIFieldAttribute.SetVisible<ARSalesPrice.discountable>(((PXSelectBase) this.Base.Records).Cache, (object) null, this.CustomerDiscountsFeatureEnabled);
    PXUIFieldAttribute.SetVisibility<ARSalesPrice.discountable>(((PXSelectBase) this.Base.Records).Cache, (object) null, (PXUIVisibility) 7);
  }

  protected virtual void _(Events.RowSelected<ARSalesPrice> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.isPromotionalPrice>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARSalesPrice>>) e).Cache, (object) e.Row, !e.Row.IsFairValue.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.skipLineDiscounts>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARSalesPrice>>) e).Cache, (object) e.Row, !e.Row.IsFairValue.GetValueOrDefault());
    PXCache cache1 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARSalesPrice>>) e).Cache;
    ARSalesPrice row1 = e.Row;
    bool? nullable = e.Row.IsPromotionalPrice;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.SkipLineDiscounts;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.isFairValue>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARSalesPrice>>) e).Cache;
    ARSalesPrice row2 = e.Row;
    nullable = e.Row.IsFairValue;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.isProrated>(cache2, (object) row2, num2 != 0);
    PXCache cache3 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ARSalesPrice>>) e).Cache;
    ARSalesPrice row3 = e.Row;
    nullable = e.Row.IsFairValue;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.discountable>(cache3, (object) row3, num3 != 0);
  }

  protected virtual void _(
    Events.FieldUpdated<ARSalesPrice, ARSalesPrice.isFairValue> e)
  {
    if (e.Row == null)
      return;
    bool? promotionalPrice = e.Row.IsPromotionalPrice;
    bool flag = false;
    if (!(promotionalPrice.GetValueOrDefault() == flag & promotionalPrice.HasValue) || !(bool) ((Events.FieldUpdatedBase<Events.FieldUpdated<ARSalesPrice, ARSalesPrice.isFairValue>, ARSalesPrice, object>) e).OldValue)
      return;
    e.Row.IsProrated = new bool?(false);
  }
}

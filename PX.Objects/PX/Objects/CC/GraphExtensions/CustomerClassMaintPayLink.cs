// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.CustomerClassMaintPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class CustomerClassMaintPayLink : PXGraphExtension<CustomerClassMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  protected virtual void _(Events.RowSelected<CustomerClass> e)
  {
    CustomerClass row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CustomerClass>>) e).Cache;
    if (row == null)
      return;
    bool valueOrDefault = cache.GetExtension<CustomerClassPayLink>((object) row).DisablePayLink.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CustomerClassPayLink.allowOverrideDeliveryMethod>(cache, (object) row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<CustomerClassPayLink.deliveryMethod>(cache, (object) row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<CustomerClassPayLink.payLinkPaymentMethod>(cache, (object) row, !valueOrDefault);
  }
}

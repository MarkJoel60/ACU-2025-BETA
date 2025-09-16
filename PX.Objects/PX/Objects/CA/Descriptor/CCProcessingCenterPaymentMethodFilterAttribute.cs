// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Descriptor.CCProcessingCenterPaymentMethodFilterAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CA.Descriptor;

internal class CCProcessingCenterPaymentMethodFilterAttribute : PXCustomSelectorAttribute
{
  private Type search;

  public CCProcessingCenterPaymentMethodFilterAttribute(Type type)
    : base(typeof (CCProcessingCenter.processingCenterID), new Type[1]
    {
      typeof (CCProcessingCenter.processingCenterID)
    })
  {
    this.search = type;
  }

  public IEnumerable GetRecords()
  {
    CCProcessingCenterPaymentMethodFilterAttribute methodFilterAttribute = this;
    string paymentMethod = ((PaymentMethod) ((PXCache) GraphHelper.Caches<PaymentMethod>(methodFilterAttribute._Graph)).Current).PaymentType;
    BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
    {
      methodFilterAttribute.search
    });
    foreach (object obj in new PXView(methodFilterAttribute._Graph, false, instance).SelectMulti(Array.Empty<object>()))
    {
      CCProcessingCenter ccProcessingCenter = PXResult.Unwrap<CCProcessingCenter>(obj);
      if (paymentMethod != "EFT")
        yield return (object) ccProcessingCenter;
      else if (CCProcessingFeatureHelper.IsFeatureSupported(ccProcessingCenter, CCProcessingFeature.EFTSupport, false))
        yield return (object) ccProcessingCenter;
    }
  }
}

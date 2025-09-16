// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCProcessingCenterSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.CA;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class CCProcessingCenterSelectorAttribute : PXCustomSelectorAttribute
{
  private CCProcessingFeature feature;

  public CCProcessingCenterSelectorAttribute(CCProcessingFeature feature)
    : base(typeof (CCProcessingCenter.processingCenterID), new Type[1]
    {
      typeof (CCProcessingCenter.processingCenterID)
    })
  {
    this.feature = feature;
  }

  public IEnumerable GetRecords()
  {
    CCProcessingCenterSelectorAttribute selectorAttribute = this;
    foreach (PXResult<CCProcessingCenter> pxResult in ((PXSelectBase<CCProcessingCenter>) new PXSelectReadonly<CCProcessingCenter>(selectorAttribute._Graph)).Select(Array.Empty<object>()))
    {
      CCProcessingCenter record = PXResult<CCProcessingCenter>.op_Implicit(pxResult);
      string processingCenterId = record.ProcessingCenterID;
      CCProcessingCenter ccProcessingCenter = CCProcessingCenter.PK.Find(selectorAttribute._Graph, processingCenterId);
      if (CCProcessingFeatureHelper.IsFeatureSupported(ccProcessingCenter, selectorAttribute.feature, false))
      {
        if (selectorAttribute.feature == CCProcessingFeature.ExtendedProfileManagement)
        {
          bool? allowSaveProfile = ccProcessingCenter.AllowSaveProfile;
          bool flag = false;
          if (allowSaveProfile.GetValueOrDefault() == flag & allowSaveProfile.HasValue)
            continue;
        }
        yield return (object) record;
      }
    }
  }
}

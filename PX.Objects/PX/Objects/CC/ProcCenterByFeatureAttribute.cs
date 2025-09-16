// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.ProcCenterByFeatureAttribute
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
namespace PX.Objects.CC;

/// <summary>Retrive list of Processing Centers by Feature</summary>
public class ProcCenterByFeatureAttribute : PXCustomSelectorAttribute
{
  private CCProcessingFeature[] _featureList;

  public ProcCenterByFeatureAttribute(Type type, CCProcessingFeature[] featureList)
    : base(type)
  {
    this._featureList = featureList;
  }

  protected virtual IEnumerable GetRecords()
  {
    ProcCenterByFeatureAttribute featureAttribute = this;
    foreach (PXResult<CCProcessingCenter> pxResult in PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter>.Config>.Select(featureAttribute._Graph, Array.Empty<object>()))
    {
      CCProcessingCenter processingCenter = PXResult<CCProcessingCenter>.op_Implicit(pxResult);
      CCProcessingFeature[] processingFeatureArray = featureAttribute._featureList;
      for (int index = 0; index < processingFeatureArray.Length; ++index)
      {
        if (CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, processingFeatureArray[index]))
          yield return (object) processingCenter;
      }
      processingFeatureArray = (CCProcessingFeature[]) null;
      processingCenter = (CCProcessingCenter) null;
    }
  }
}

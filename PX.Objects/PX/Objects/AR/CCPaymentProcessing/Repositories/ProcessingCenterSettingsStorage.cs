// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.ProcessingCenterSettingsStorage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using PX.Objects.CA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class ProcessingCenterSettingsStorage : IProcessingCenterSettingsStorage
{
  private readonly PXGraph _graph;
  private readonly string _processingCenterID;

  public ProcessingCenterSettingsStorage(PXGraph graph, string processingCenterID)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (string.IsNullOrEmpty(processingCenterID))
      throw new ArgumentNullException(nameof (processingCenterID));
    this._graph = graph;
    this._processingCenterID = processingCenterID;
  }

  void IProcessingCenterSettingsStorage.ReadSettings(Dictionary<string, string> aSettings)
  {
    foreach (PXResult<CCProcessingCenterDetail> pxResult in ((PXSelectBase<CCProcessingCenterDetail>) new PXSelect<CCProcessingCenterDetail, Where<CCProcessingCenterDetail.processingCenterID, Equal<Required<CCProcessingCenterDetail.processingCenterID>>>>(this._graph)).Select(new object[1]
    {
      (object) this._processingCenterID
    }))
    {
      CCProcessingCenterDetail processingCenterDetail = PXResult<CCProcessingCenterDetail>.op_Implicit(pxResult);
      aSettings[processingCenterDetail.DetailID] = processingCenterDetail.Value;
    }
  }
}

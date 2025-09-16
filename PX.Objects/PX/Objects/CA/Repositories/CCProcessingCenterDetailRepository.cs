// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Repositories.CCProcessingCenterDetailRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA.Repositories;

public class CCProcessingCenterDetailRepository
{
  protected readonly PXGraph _graph;

  public CCProcessingCenterDetailRepository(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public PXResultset<CCProcessingCenterDetail> FindAllProcessingCenterDetails(
    string processingCenterID)
  {
    return PXSelectBase<CCProcessingCenterDetail, PXSelect<CCProcessingCenterDetail, Where<CCProcessingCenterDetail.processingCenterID, Equal<Required<CCProcessingCenterDetail.processingCenterID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) processingCenterID
    });
  }
}

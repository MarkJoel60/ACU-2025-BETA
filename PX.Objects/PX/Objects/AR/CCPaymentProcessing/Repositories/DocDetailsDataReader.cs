// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.DocDetailsDataReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class DocDetailsDataReader : IDocDetailsDataReader
{
  private PXGraph _graph;
  private string _docType;
  private string _refNbr;

  public DocDetailsDataReader(PXGraph graph, string docType, string refNbr)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (string.IsNullOrEmpty(docType))
      throw new ArgumentNullException(nameof (docType));
    if (string.IsNullOrEmpty(refNbr))
      throw new ArgumentNullException(nameof (refNbr));
    this._graph = graph;
    this._docType = docType;
    this._refNbr = refNbr;
  }

  void IDocDetailsDataReader.ReadData(List<DocDetailInfo> aData)
  {
    foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) new PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.voided, Equal<False>>>>>(this._graph)).Select(new object[2]
    {
      (object) this._docType,
      (object) this._refNbr
    }))
    {
      ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult);
      aData.Add(new DocDetailInfo()
      {
        ItemID = $"{arAdjust.AdjdDocType}{arAdjust.AdjdRefNbr}",
        ItemName = $"{arAdjust.AdjdDocType} {arAdjust.AdjdRefNbr}",
        ItemDescription = $"Payment of invoice {arAdjust.AdjdDocType}{arAdjust.AdjdRefNbr} - {arAdjust.CuryAdjgAmt}",
        Quantity = 1M,
        IsTaxable = new bool?(),
        Price = arAdjust.CuryAdjgAmt.Value
      });
    }
  }
}

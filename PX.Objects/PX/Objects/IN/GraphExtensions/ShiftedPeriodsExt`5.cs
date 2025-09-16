// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ShiftedPeriodsExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ShiftedPeriodsExt<TGraph, TDocument, THeaderDocDate, THeaderTranPeriodID, TDocumentLine> : 
  DocumentWithLinesGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDocument : IBqlTable
  where THeaderDocDate : IBqlField
  where THeaderTranPeriodID : IBqlField
  where TDocumentLine : IBqlTable
{
  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (TDocument))
    {
      HeaderTranPeriodID = typeof (THeaderTranPeriodID),
      HeaderDocDate = typeof (THeaderDocDate)
    };
  }

  protected override DocumentLineMapping GetDocumentLineMapping()
  {
    return new DocumentLineMapping(typeof (TDocumentLine));
  }
}

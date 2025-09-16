// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.GraphExtensions.AffectedSubcontractsByAPRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.GraphExtensions;

public class AffectedSubcontractsByAPRelease : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedSubcontractsByAPRelease, APReleaseProcess, PX.Objects.PO.POOrder, SubcontractEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.distributionModule>();

  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(PX.Objects.PO.POOrder entity)
  {
    PXCache<PX.Objects.PO.POOrder> pxCache = this.Base.Caches<PX.Objects.PO.POOrder>();
    int? valueOriginal1 = (int?) pxCache.GetValueOriginal<PX.Objects.PO.POOrder.linesToCloseCntr>((object) entity);
    int? valueOriginal2 = (int?) pxCache.GetValueOriginal<PX.Objects.PO.POOrder.linesToCompleteCntr>((object) entity);
    if (object.Equals((object) valueOriginal1, (object) entity.LinesToCloseCntr) && object.Equals((object) valueOriginal2, (object) entity.LinesToCompleteCntr))
      return false;
    int? nullable1 = valueOriginal1;
    int num1 = 0;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
    {
      int? nullable2 = valueOriginal2;
      int num2 = 0;
      if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
      {
        int? linesToCloseCntr = entity.LinesToCloseCntr;
        int num3 = 0;
        if (!(linesToCloseCntr.GetValueOrDefault() == num3 & linesToCloseCntr.HasValue))
        {
          int? linesToCompleteCntr = entity.LinesToCompleteCntr;
          int num4 = 0;
          return linesToCompleteCntr.GetValueOrDefault() == num4 & linesToCompleteCntr.HasValue;
        }
      }
    }
    return true;
  }

  protected override IEnumerable<PX.Objects.PO.POOrder> GetAffectedEntities()
  {
    return base.GetAffectedEntities().Where<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (x => x.OrderType == "RS"));
  }

  protected override void ProcessAffectedEntity(SubcontractEntry primaryGraph, PX.Objects.PO.POOrder entity)
  {
    primaryGraph.UpdateDocumentState(entity);
  }

  protected override PX.Objects.PO.POOrder ActualizeEntity(
    SubcontractEntry primaryGraph,
    PX.Objects.PO.POOrder entity)
  {
    return (PX.Objects.PO.POOrder) PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) primaryGraph, (object) entity.OrderType, (object) entity.OrderNbr);
  }
}

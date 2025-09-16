// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.DR.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public static class ScheduleHelper
{
  /// <summary>
  /// Checks if deferral code has been changed or removed from the line.
  /// If so, ensures the removal of any associated deferral schedules.
  /// </summary>
  public static void DeleteAssociatedScheduleIfDeferralCodeChanged(
    PXGraph graph,
    IDocumentLine documentLine)
  {
    IDocumentLine original;
    switch (documentLine.Module)
    {
      case "AR":
        original = (IDocumentLine) ScheduleHelper.GetOriginal<ARTran>(graph.Caches[typeof (ARTran)], (object) (documentLine as ARTran));
        break;
      case "AP":
        original = (IDocumentLine) ScheduleHelper.GetOriginal<APTran>(graph.Caches[typeof (APTran)], (object) (documentLine as APTran));
        break;
      default:
        throw new PXException("The document line module code is unexpected; it should be AR or AP.");
    }
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged(graph, documentLine, original);
  }

  /// <summary>
  /// Checks if deferral code has been changed or removed from the line.
  /// If so, ensures the removal of any associated deferral schedules.
  /// </summary>
  public static void DeleteAssociatedScheduleIfDeferralCodeChanged(
    PXCache cache,
    ARTran documentLine)
  {
    ARTran original = ScheduleHelper.GetOriginal<ARTran>(cache, (object) documentLine);
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged(cache.Graph, (IDocumentLine) documentLine, (IDocumentLine) original);
  }

  /// <summary>
  /// Checks if deferral code has been changed or removed from the line.
  /// If so, ensures the removal of any associated deferral schedules.
  /// </summary>
  public static void DeleteAssociatedScheduleIfDeferralCodeChanged(
    PXCache cache,
    APTran documentLine)
  {
    APTran original = ScheduleHelper.GetOriginal<APTran>(cache, (object) documentLine);
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged(cache.Graph, (IDocumentLine) documentLine, (IDocumentLine) original);
  }

  private static T GetOriginal<T>(PXCache cache, object row) where T : class, IBqlTable, new()
  {
    if (cache.GetStatus(row) == 2 || cache.GetStatus(row) == 4)
      return default (T);
    T original = new T();
    EnumerableExtensions.ForEach<string>((IEnumerable<string>) cache.Keys, (Action<string>) (s => cache.SetValue((object) original, s, cache.GetValueOriginal(row, s))));
    if (!cache.IsKeysFilled((object) (T) original))
      return default (T);
    EnumerableExtensions.ForEach<string>(((IEnumerable<string>) cache.Fields).Except<string>((IEnumerable<string>) cache.Keys), (Action<string>) (s => cache.SetValue((object) original, s, cache.GetValueOriginal(row, s))));
    return original;
  }

  private static void DeleteAssociatedScheduleIfDeferralCodeChanged(
    PXGraph graph,
    IDocumentLine documentLine,
    IDocumentLine oldLine)
  {
    if (((documentLine.DeferredCode != null ? 0 : (oldLine?.DeferredCode != null ? 1 : 0)) | (oldLine == null || oldLine.DeferredCode == null || documentLine.DeferredCode == null ? (false ? 1 : 0) : (oldLine.DeferredCode != documentLine.DeferredCode ? 1 : 0))) == 0)
    {
      int? branchId1 = documentLine.BranchID;
      int? branchId2 = (int?) oldLine?.BranchID;
      if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue || documentLine.DeferredCode == null)
        return;
    }
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<Required<DRSchedule.module>>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>>.Config>.Select(graph, new object[4]
    {
      (object) documentLine.Module,
      (object) documentLine.TranType,
      (object) documentLine.RefNbr,
      (object) documentLine.LineNbr
    }));
    if (drSchedule == null)
      return;
    DraftScheduleMaint instance = PXGraph.CreateInstance<DraftScheduleMaint>();
    ((PXSelectBase<DRSchedule>) instance.Schedule).Delete(drSchedule);
    ((PXAction) instance.Save).Press();
  }
}

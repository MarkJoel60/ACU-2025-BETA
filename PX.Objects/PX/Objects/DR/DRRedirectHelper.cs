// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRedirectHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.DR;

public static class DRRedirectHelper
{
  /// <summary>
  /// Tries to perform a redirect to a deferral schedule by its ID.
  /// Does nothing if the provided ID value is <c>null</c>.
  /// </summary>
  /// <param name="sourceGraph">
  /// A graph through which the redirect will be processed.
  /// </param>
  /// <param name="scheduleID">
  /// The unique identifier of a <see cref="T:PX.Objects.DR.DRSchedule" /> record which
  /// the user should be redirected to.</param>
  public static void NavigateToDeferralSchedule(PXGraph sourceGraph, int? scheduleID)
  {
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select(sourceGraph, new object[1]
    {
      (object) scheduleID
    }));
    if (drSchedule == null)
      return;
    PXRedirectHelper.TryRedirect(sourceGraph.Caches[typeof (DRSchedule)], (object) drSchedule, "ViewDocument", (PXRedirectHelper.WindowMode) 3);
  }

  /// <summary>
  /// Tries to perform a redirect to the original AR or AP document of a given
  /// <see cref="T:PX.Objects.DR.DRSchedule" /> record.
  /// </summary>
  /// <param name="sourceGraph">
  /// A graph through which the redirect will be processed.
  /// </param>
  /// <param name="scheduleDetail">
  /// The <see cref="T:PX.Objects.DR.DRSchedule" /> record containing the document type and
  /// document reference number necessary for the redirect.
  /// </param>
  public static void NavigateToOriginalDocument(PXGraph sourceGraph, DRSchedule schedule)
  {
    IBqlTable ibqlTable = (IBqlTable) null;
    if (schedule.Module == "AR")
      ibqlTable = (IBqlTable) PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<DRSchedule.docType>>, And<ARRegister.refNbr, Equal<Required<DRSchedule.refNbr>>>>>.Config>.Select(sourceGraph, new object[2]
      {
        (object) schedule.DocType,
        (object) schedule.RefNbr
      }));
    else if (schedule.Module == "AP")
      ibqlTable = (IBqlTable) PXResultset<APRegister>.op_Implicit(PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<Required<DRSchedule.docType>>, And<APRegister.refNbr, Equal<Required<DRSchedule.refNbr>>>>>.Config>.Select(sourceGraph, new object[2]
      {
        (object) schedule.DocType,
        (object) schedule.RefNbr
      }));
    if (ibqlTable == null)
      return;
    PXRedirectHelper.TryRedirect(sourceGraph.Caches[ibqlTable.GetType()], (object) ibqlTable, "ViewDocument", (PXRedirectHelper.WindowMode) 3);
  }

  /// <summary>
  /// Tries to perform a redirect to the original AR or AP document of a given
  /// <see cref="T:PX.Objects.DR.DRScheduleDetail" />.
  /// </summary>
  /// <param name="sourceGraph">
  /// A graph through which the redirect will be processed.
  /// </param>
  /// <param name="scheduleDetail">
  /// The <see cref="T:PX.Objects.DR.DRScheduleDetail" /> object containing the document type and
  /// document reference number necessary for the redirect.
  /// </param>
  public static void NavigateToOriginalDocument(
    PXGraph sourceGraph,
    DRScheduleDetail scheduleDetail)
  {
    DRSchedule schedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>>>.Config>.Select(sourceGraph, new object[1]
    {
      (object) scheduleDetail.ScheduleID
    }));
    DRRedirectHelper.NavigateToOriginalDocument(sourceGraph, schedule);
  }

  /// <summary>
  /// Tries to perform a redirect to the original AR or AP document referenced by
  /// the <see cref="T:PX.Objects.DR.DRSchedule" /> that owns the given <see cref="T:PX.Objects.DR.DRScheduleTran" />.
  /// </summary>
  /// <param name="sourceGraph">
  /// A graph through which the redirect will be processed.
  /// </param>
  /// <param name="scheduleTransaction">
  /// The <see cref="T:PX.Objects.DR.DRScheduleDetail" /> object from which the corresponding schedule
  /// will be selected.
  /// </param>
  public static void NavigateToOriginalDocument(
    PXGraph sourceGraph,
    DRScheduleTran scheduleTransaction)
  {
    DRSchedule schedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRScheduleTran.scheduleID>>>>.Config>.Select(sourceGraph, new object[1]
    {
      (object) scheduleTransaction.ScheduleID
    }));
    DRRedirectHelper.NavigateToOriginalDocument(sourceGraph, schedule);
  }
}

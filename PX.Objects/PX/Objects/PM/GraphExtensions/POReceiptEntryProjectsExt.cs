// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.POReceiptEntryProjectsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO.Models;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class POReceiptEntryProjectsExt : PXGraphExtension<POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// <summary>
  /// Override of <see cref="M:PX.Objects.PO.POReceiptEntry.GetDropshipReceiptsSelectCommand(PX.Objects.SO.Models.PostReceiptArgs)" />
  /// </summary>
  [PXOverride]
  public virtual BqlCommand GetDropshipReceiptsSelectCommand(
    PostReceiptArgs args,
    POReceiptEntryProjectsExt.GetDropshipReceiptsSelectCommandDelegate baseMethod)
  {
    return BqlCommand.AppendJoin<LeftJoin<PX.Objects.PM.Lite.PMProject, On<PX.Objects.PM.Lite.PMProject.contractID, Equal<POReceiptLine.projectID>>, LeftJoin<PX.Objects.PM.Lite.PMTask, On<PX.Objects.PM.Lite.PMTask.projectID, Equal<POReceiptLine.projectID>, And<PX.Objects.PM.Lite.PMTask.taskID, Equal<POReceiptLine.taskID>>>>>>(baseMethod(args));
  }

  [PXOverride]
  public virtual void TryToGetProjectAndTask(
    PXResult<POReceiptLine> res,
    POReceiptLine line,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task,
    POReceiptEntryProjectsExt.TryToGetProjectAndTaskDelegate baseMethod)
  {
    project = (PX.Objects.PM.Lite.PMProject) null;
    task = (PX.Objects.PM.Lite.PMTask) null;
    baseMethod(res, line, out project, out task);
    if (!line.ProjectID.HasValue)
      return;
    int? projectId = line.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      return;
    project = ((PXResult) res).GetItem<PX.Objects.PM.Lite.PMProject>();
    if (!line.TaskID.HasValue)
      return;
    task = ((PXResult) res).GetItem<PX.Objects.PM.Lite.PMTask>();
  }

  public delegate BqlCommand GetDropshipReceiptsSelectCommandDelegate(PostReceiptArgs args);

  public delegate void TryToGetProjectAndTaskDelegate(
    PXResult<POReceiptLine> res,
    POReceiptLine line,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task);
}

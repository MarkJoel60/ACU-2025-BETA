// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.INReleaseProcessProjectsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class INReleaseProcessProjectsExt : PXGraphExtension<INReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// <summary>
  /// Override of <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.GetMainSelect(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase<INTran> GetMainSelect(
    INRegister doc,
    INReleaseProcessProjectsExt.GetMainSelectDelegate baseMethod)
  {
    PXSelectBase<INTran> mainSelect = baseMethod(doc);
    mainSelect.Join<LeftJoin<PX.Objects.PM.Lite.PMProject, On<INTran.FK.ProjectL>, LeftJoin<PX.Objects.PM.Lite.PMTask, On<INTran.FK.TaskL>>>>();
    return mainSelect;
  }

  [PXOverride]
  public virtual void TryToGetProjectAndTask(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task,
    INReleaseProcessProjectsExt.TryToGetProjectAndTaskDelegate baseMethod)
  {
    baseMethod(item, out project, out task);
    INTran inTran = item.GetItem<INTran>();
    if (!inTran.ProjectID.HasValue)
      return;
    int? projectId = inTran.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      return;
    project = item.GetItem<PX.Objects.PM.Lite.PMProject>();
    nullable = inTran.TaskID;
    if (!nullable.HasValue)
      return;
    task = item.GetItem<PX.Objects.PM.Lite.PMTask>();
  }

  public delegate PXSelectBase<INTran> GetMainSelectDelegate(INRegister doc);

  public delegate void TryToGetProjectAndTaskDelegate(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task);
}

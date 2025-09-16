// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.EP.GraphExtensions.EmployeeActivitiesEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.EP.GraphExtensions;

public class EmployeeActivitiesEntryExt : PXGraphExtension<EmployeeActivitiesEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new Type[] {})]
  [PXFormula(typeof (Validate<EmployeeActivitiesEntry.PMTimeActivityFilter.projectID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EmployeeActivitiesEntry.PMTimeActivityFilter.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Validate<EPActivityApprove.projectID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPActivityApprove.projectTaskID> e)
  {
  }
}

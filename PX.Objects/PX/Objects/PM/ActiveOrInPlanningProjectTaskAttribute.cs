// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ActiveOrInPlanningProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

[PXRestrictor(typeof (Where<PMTask.isCancelled, Equal<False>>), "Task is Canceled and cannot be used for data entry.", new Type[] {typeof (PMTask.taskCD)})]
[PXRestrictor(typeof (Where<PMTask.isCompleted, Equal<False>>), "Task is Completed and cannot be used for data entry.", new Type[] {typeof (PMTask.taskCD)})]
public class ActiveOrInPlanningProjectTaskAttribute : BaseProjectTaskAttribute
{
  public ActiveOrInPlanningProjectTaskAttribute(Type projectID)
    : base(projectID)
  {
    this.Filterable = true;
  }

  public ActiveOrInPlanningProjectTaskAttribute(Type projectID, string Module)
    : base(projectID, Module)
  {
    this.Filterable = true;
  }
}

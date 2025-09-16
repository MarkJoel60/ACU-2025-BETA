// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public static class PMProjectHelper
{
  public static void TryToGetProjectAndTask(
    PXGraph graph,
    int? projectID,
    int? taskID,
    out PMProject project,
    out PMTask task)
  {
    project = (PMProject) null;
    task = (PMTask) null;
    if (!projectID.HasValue)
      return;
    int? nullable1 = projectID;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    project = PMProject.PK.Find(graph, projectID);
    if (!taskID.HasValue)
      return;
    task = PMTask.PK.Find(graph, projectID, taskID);
  }
}

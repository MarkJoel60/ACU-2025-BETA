// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.ProjectTaskEntryClockIn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class ProjectTaskEntryClockIn : ClockInClockOutBase<ProjectTaskEntry, PMTask>
{
  public new static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  public override TimeLogData GetTimeLogData(Guid? noteID)
  {
    TimeLogData timeLogData = base.GetTimeLogData(noteID);
    PMTask data = (PMTask) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.noteID, Equal<Required<PMTask.noteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) noteID);
    if (data != null)
    {
      timeLogData.ProjectID = data.ProjectID;
      timeLogData.TaskID = data.TaskID;
      timeLogData.Summary = data.Description;
      string str = PXSelectorAttribute.Select<PMTask.projectID>(this.Base.Task.Cache, (object) data) is PMProject pmProject ? $"{pmProject.ContractCD.TrimEnd()}, {data.TaskCD.TrimEnd()}" : data.TaskCD;
      timeLogData.DocumentNbr = str.Substring(0, System.Math.Min((int) byte.MaxValue, str.Length));
    }
    return timeLogData;
  }

  public override Guid? SelectEntityID(Dictionary<string, string> fields)
  {
    string str1;
    string str2;
    if (!fields.TryGetValue("ProjectID", out str1) || !fields.TryGetValue("TaskCD", out str2))
      return new Guid?();
    return ((PMTask) PXSelectBase<PMTask, PXSelectJoin<PMTask, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMTask.projectID>>>, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) this.PrepareKeyCD(str1), (object) this.PrepareKeyCD(str2)))?.NoteID;
  }

  private string PrepareKeyCD(string value) => value.Replace('+', ' ').TrimEnd();

  public override System.Type DbTable => typeof (PMTask);

  public override string NoteIdField => typeof (PMTask.noteID).Name;
}

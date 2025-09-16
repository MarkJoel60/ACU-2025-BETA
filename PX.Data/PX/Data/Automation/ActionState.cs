// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.ActionState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;

#nullable disable
namespace PX.Data.Automation;

public class ActionState
{
  public ActionState()
  {
  }

  public ActionState(AUWorkflowStateAction actionState)
  {
    this.ActionName = actionState.ActionName;
    this.StateName = actionState.StateName;
    this.StateActionLineNbr = actionState.StateActionLineNbr;
    this.Connotation = actionState.Connotation;
    this.IsDisabled = actionState.IsDisabled.GetValueOrDefault();
    this.IsHide = actionState.IsHide.GetValueOrDefault();
    this.IsTopLevel = actionState.IsTopLevel.GetValueOrDefault();
  }

  public string ActionName { get; internal set; }

  public string StateName { get; internal set; }

  public int? StateActionLineNbr { get; internal set; }

  public string Connotation { get; internal set; }

  public bool IsDisabled { get; internal set; }

  public bool IsHide { get; internal set; }

  public bool IsTopLevel { get; internal set; }
}

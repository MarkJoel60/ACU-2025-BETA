// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ActivityCommandExecutor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class ActivityCommandExecutor : IActivityCommandExecutor
{
  private readonly Dictionary<string, Action<string, object>> _commandHandlers = new Dictionary<string, Action<string, object>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public ActivityCommandExecutor(IActivityService activityService)
  {
    this.RegisterActivityExecuteHandlers(activityService);
  }

  public void Execute(string commandName, string commandArgs, object refNoteId)
  {
    Action<string, object> action;
    if (!this._commandHandlers.TryGetValue(commandName, out action))
      return;
    action(commandArgs, refNoteId);
  }

  private void RegisterActivityExecuteHandlers(IActivityService activityService)
  {
    this._commandHandlers.Add(ActivityCommands.Refresh, (Action<string, object>) ((_1, _2) => { }));
    this._commandHandlers.Add(ActivityCommands.OpenItem, (Action<string, object>) ((commandArgs, _) => activityService.Open(commandArgs)));
    this._commandHandlers.Add(ActivityCommands.Complete, (Action<string, object>) ((commandArgs, _) => activityService.Complete(commandArgs)));
    this._commandHandlers.Add(ActivityCommands.Cancel, (Action<string, object>) ((commandArgs, _) => activityService.Cancel(commandArgs)));
    this._commandHandlers.Add(ActivityCommands.NewTask, (Action<string, object>) ((_, refNoteId) =>
    {
      if (refNoteId == null)
        return;
      activityService.CreateTask(refNoteId);
    }));
    this._commandHandlers.Add(ActivityCommands.NewEvent, (Action<string, object>) ((_, refNoteId) =>
    {
      if (refNoteId == null)
        return;
      activityService.CreateEvent(refNoteId);
    }));
    this._commandHandlers.Add(ActivityCommands.NewEmail, (Action<string, object>) ((_, refNoteId) =>
    {
      if (refNoteId == null)
        return;
      activityService.CreateEmailActivity(refNoteId, 0);
    }));
    this._commandHandlers.Add(ActivityCommands.NewActivity, (Action<string, object>) ((commandArgs, refNoteId) => activityService.CreateActivity(refNoteId, commandArgs)));
    this._commandHandlers.Add(ActivityCommands.ShowAll, (Action<string, object>) ((_, refNoteId) =>
    {
      if (refNoteId == null)
        return;
      activityService.ShowAll(refNoteId);
    }));
    this._commandHandlers.Add(ActivityCommands.Dismiss, (Action<string, object>) ((commandArgs, _) => activityService.Dismiss(commandArgs)));
    this._commandHandlers.Add(ActivityCommands.Snooze, (Action<string, object>) ((commandArgs, _) =>
    {
      int length = commandArgs.IndexOf('|');
      if (length <= -1)
        return;
      activityService.Defer(commandArgs.Substring(0, length), int.Parse(commandArgs.Length == length - 1 ? "0" : commandArgs.Substring(length + 1)));
    }));
  }
}

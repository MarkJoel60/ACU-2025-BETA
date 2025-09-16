// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpdateExecutionObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Points;
using PX.DbServices.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Update;

public class UpdateExecutionObserver : SimpleExecutionObserver
{
  public readonly List<PXUpdateEvent> errors;

  public UpdateExecutionObserver(List<PXUpdateEvent> errors) => this.errors = errors;

  public virtual ActionOnException Problem(Exception ex)
  {
    ScriptExecutionException executionException = ex as ScriptExecutionException;
    PXUpdateEvent message = new PXUpdateEvent(EventLogEntryType.Error, ex.Message, ex, executionException == null ? "" : executionException.ScriptAt.ToString());
    PXUpdateLog.WriteMessage(message);
    this.errors.Add(message);
    return (ActionOnException) 4;
  }
}

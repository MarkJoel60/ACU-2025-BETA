// Decompiled with JetBrains decompiler
// Type: PX.SM.IWorkflowConditionEvaluateService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Automation.State;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <summary>
/// Service that used to evaluate conditions in workflow process
/// </summary>
internal interface IWorkflowConditionEvaluateService
{
  IReadOnlyDictionary<string, Lazy<bool>> EvaluateConditions(
    PXGraph graph,
    object row,
    Screen screen,
    string form,
    IReadOnlyDictionary<string, object> formValues,
    PXCache cache = null,
    string screenId = null);
}

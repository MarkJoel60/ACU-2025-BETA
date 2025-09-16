// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.SM;

internal static class WorkflowHelper
{
  public const string DEFAULT_WORKFLOW = "DEFAULT";

  public static string GetWorkflowPseudoGuid(string flowId, string flowSubId)
  {
    return !string.IsNullOrEmpty(flowSubId) && !(flowSubId == "DEFAULT") ? $"{flowId ?? "DEFAULT"}__{flowSubId}" : flowId ?? "DEFAULT";
  }
}

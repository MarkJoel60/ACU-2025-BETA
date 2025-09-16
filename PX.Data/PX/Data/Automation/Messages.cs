// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Automation;

[PXLocalizable]
public static class Messages
{
  public const string ActionMethodDoesNotExist = "Method '{0}' specified for the action '{1}' does not exist.";
  public const string ConditionDoesNotExist = "Condition '{0}' does not exist.";
  public const string FilterPreview = "Transition Parameters";
  public const string WorkflowTransition = "Transition";
  public const string TargetStateDoesNotExist = "The further processing cannot be performed because the flow was configured incorrectly. Please contact your system administrator.";
  public const string ConditionOfTransitionDoesNotExist = "The condition '{0}' for the transition '{1}' does not exist.";
}

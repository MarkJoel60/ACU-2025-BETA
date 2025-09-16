// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.ComboBoxValuesSource
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>A source of combo box values in dialog forms.</summary>
public enum ComboBoxValuesSource
{
  /// <summary>
  /// Take combo box values from the workflow state property of the current entity state.
  /// </summary>
  SourceState,
  /// <summary>
  /// Take combo box values from the workflow state property of the target entity state—that is, the entity state after the transition.
  /// </summary>
  TargetState,
  /// <summary>
  /// Take combo box values from the field settings in the dialog form.
  /// </summary>
  ExplicitValues,
}

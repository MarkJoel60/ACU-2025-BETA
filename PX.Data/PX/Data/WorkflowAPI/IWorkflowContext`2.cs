// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.IWorkflowContext`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.WorkflowAPI;

public interface IWorkflowContext<TGraph, TTable>
  where TGraph : PXGraph
  where TTable : class, IBqlTable, new()
{
  /// <summary>A helper object that allows you to create reusable configurations of dialog boxes and provides access to already configured dialog boxes.</summary>
  BoundedTo<TGraph, TTable>.Form.FormBuilder Forms { get; }

  /// <summary>A helper object that allows you to create reusable configuration of an action state.</summary>
  BoundedTo<TGraph, TTable>.ActionState.ActionStateBuilder ActionStates { get; }

  /// <summary>A helper object that allows you to create reusable action configurations and provides access to already configured actions.</summary>
  BoundedTo<TGraph, TTable>.ActionDefinition.ActionDefinitionBuilder ActionDefinitions { get; }

  /// <summary>A helper object that allows you to create reusable categories.</summary>
  BoundedTo<TGraph, TTable>.ActionCategory.ActionCategoryBuilder Categories { get; }

  /// <summary>A helper object that allows you to create reusable workflow event handlers.</summary>
  BoundedTo<TGraph, TTable>.WorkflowEventHandlerDefinition.EventHandlerBuilder WorkflowEventHandlers { get; }

  /// <summary>A helper object that allows you to create reusable assignments for fields and action parameters.</summary>
  BoundedTo<TGraph, TTable>.Assignment.AssignmentBuilder Assignments { get; }

  /// <summary>A helper object that allows you to create reusable shared conditions and provides access to already defined conditions.</summary>
  BoundedTo<TGraph, TTable>.Condition.ConditionBuilder Conditions { get; }

  /// <summary>A helper object that allows you to create reusable configurations of a field state.</summary>
  BoundedTo<TGraph, TTable>.FieldState.FieldStateBuilder FieldStates { get; }

  /// <summary>A helper object that allows you to create reusable configurations of a flow state.</summary>
  BoundedTo<TGraph, TTable>.FlowState.FlowStateBuilder FlowStates { get; }

  /// <summary>A helper object that allows you to create reusable workflows.</summary>
  BoundedTo<TGraph, TTable>.Workflow.WorkflowBuilder Workflows { get; }

  /// <summary>A helper object that allows you to create reusable navigation actions.</summary>
  BoundedTo<TGraph, TTable>.NavigationDefinition.NavigationDefinitionBuilder Navigation { get; }

  /// <summary>Adds a new configuration of the screen.</summary>
  /// <param name="config">A function that specifies the screen configuration.</param>
  /// <remarks>A screen can contain only one screen configuration.</remarks>
  void AddScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured> config);

  /// <summary>Replaces an existing screen configuration with the provided one.</summary>
  /// <param name="config">A function that specifies the screen configuration.</param>
  void ReplaceScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured> config);

  /// <summary>Overrides an existing screen configuration.</summary>
  /// <param name="config">A function that overrides the screen configuration.</param>
  void UpdateScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen> config);

  /// <summary>Removes the screen configuration.</summary>
  void RemoveScreenConfigurationFor();
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.WorkflowContext`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>Contains conditions, workflows, dialog boxes, action and field configuration for the screen.</summary>
public class WorkflowContext<TGraph, TTable> : IWorkflowContext, IWorkflowContext<TGraph, TTable>
  where TGraph : PXGraph
  where TTable : class, IBqlTable, new()
{
  private BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen _configurator = new BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen();

  Readonly.ScreenConfiguration IWorkflowContext.Result => this._configurator.Result.AsReadonly();

  internal BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen Configurator
  {
    get => this._configurator;
  }

  /// <summary>A helper object that allows you to create reusable configurations of dialog boxes and provides access to already configured dialog boxes.</summary>
  public BoundedTo<TGraph, TTable>.Form.FormBuilder Forms { get; }

  /// <summary>A helper object that allows you to create reusable configuration of an action state.</summary>
  public BoundedTo<TGraph, TTable>.ActionState.ActionStateBuilder ActionStates { get; } = new BoundedTo<TGraph, TTable>.ActionState.ActionStateBuilder();

  /// <summary>A helper object that allows you to create reusable action configurations and provides access to already configured actions.</summary>
  public BoundedTo<TGraph, TTable>.ActionDefinition.ActionDefinitionBuilder ActionDefinitions { get; }

  /// <summary>A helper object that allows you to create reusable categories.</summary>
  public BoundedTo<TGraph, TTable>.ActionCategory.ActionCategoryBuilder Categories { get; }

  /// <summary>A helper object that allows you to create reusable workflow event handlers.</summary>
  public BoundedTo<TGraph, TTable>.WorkflowEventHandlerDefinition.EventHandlerBuilder WorkflowEventHandlers { get; } = new BoundedTo<TGraph, TTable>.WorkflowEventHandlerDefinition.EventHandlerBuilder();

  /// <summary>A helper object that allows you to create reusable assignments for fields and action parameters.</summary>
  public BoundedTo<TGraph, TTable>.Assignment.AssignmentBuilder Assignments { get; } = new BoundedTo<TGraph, TTable>.Assignment.AssignmentBuilder();

  /// <summary>A helper object that allows you to create reusable shared conditions and provides access to already defined conditions.</summary>
  public BoundedTo<TGraph, TTable>.Condition.ConditionBuilder Conditions { get; }

  /// <summary>A helper object that allows you to create reusable configurations of a field state.</summary>
  public BoundedTo<TGraph, TTable>.FieldState.FieldStateBuilder FieldStates { get; } = new BoundedTo<TGraph, TTable>.FieldState.FieldStateBuilder();

  /// <summary>A helper object that allows you to create reusable configurations of a flow state.</summary>
  public BoundedTo<TGraph, TTable>.FlowState.FlowStateBuilder FlowStates { get; } = new BoundedTo<TGraph, TTable>.FlowState.FlowStateBuilder();

  /// <summary>A helper object that allows you to create reusable workflows.</summary>
  public BoundedTo<TGraph, TTable>.Workflow.WorkflowBuilder Workflows { get; } = new BoundedTo<TGraph, TTable>.Workflow.WorkflowBuilder((List<BoundedTo<TGraph, TTable>.Workflow>) null);

  /// <summary>A helper object that allows you to create reusable navigation actions.</summary>
  public BoundedTo<TGraph, TTable>.NavigationDefinition.NavigationDefinitionBuilder Navigation { get; } = new BoundedTo<TGraph, TTable>.NavigationDefinition.NavigationDefinitionBuilder();

  public WorkflowContext()
  {
    this.Forms = new BoundedTo<TGraph, TTable>.Form.FormBuilder(this);
    this.ActionDefinitions = new BoundedTo<TGraph, TTable>.ActionDefinition.ActionDefinitionBuilder(this);
    this.Categories = new BoundedTo<TGraph, TTable>.ActionCategory.ActionCategoryBuilder(this);
    this.Conditions = new BoundedTo<TGraph, TTable>.Condition.ConditionBuilder(new BoundedTo<TGraph, TTable>.Condition.ContainerAdjusterConditions(this));
  }

  public WorkflowContext(
    BoundedTo<TGraph, TTable>.ScreenConfiguration configuration)
    : this()
  {
    this._configurator = new BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen(configuration);
  }

  /// <summary>Adds a new configuration of the screen.</summary>
  /// <param name="config">A function that specifies the screen configuration.</param>
  /// <remarks>A screen can contain only one screen configuration.</remarks>
  public void AddScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured> config)
  {
    BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen configuratorScreen = new BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen();
    BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured configured = config((BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen) configuratorScreen);
    configuratorScreen.Result.SharedConditions.AddRange((IEnumerable<BoundedTo<TGraph, TTable>.ISharedCondition>) this._configurator.Result.SharedConditions);
    this._configurator = configuratorScreen;
  }

  /// <summary>Replaces an existing screen configuration with the provided one.</summary>
  /// <param name="config">A function that specifies the screen configuration.</param>
  public void ReplaceScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured> config)
  {
    BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen configuratorScreen = new BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen();
    BoundedTo<TGraph, TTable>.ScreenConfiguration.IConfigured configured = config((BoundedTo<TGraph, TTable>.ScreenConfiguration.IStartConfigScreen) configuratorScreen);
    this._configurator = configuratorScreen;
  }

  /// <summary>Overrides an existing screen configuration.</summary>
  /// <param name="config">A function that overrides the screen configuration.</param>
  public void UpdateScreenConfigurationFor(
    Func<BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen> config)
  {
    BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen configuratorScreen = config(this._configurator);
  }

  /// <summary>Removes the screen configuration.</summary>
  public void RemoveScreenConfigurationFor()
  {
    this._configurator = new BoundedTo<TGraph, TTable>.ScreenConfiguration.ConfiguratorScreen();
  }
}

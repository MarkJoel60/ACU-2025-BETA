// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.ActionPersistOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>Specifies if and when the <see cref="M:PX.Data.PXGraph.Persist" /> method is invoked by the system in relation to an action execution.</summary>
public enum ActionPersistOptions
{
  /// <summary>A new or existing entity will be saved after an action is executed.
  /// A new entity will not be saved if the action is explicitly declared to be of type <see cref="T:PX.Data.WorkflowAPI.PXAutoAction`1" /> and is executed automatically by the workflow.</summary>
  Auto,
  /// <summary>A new or existing entity will not be saved when the action is executed.
  /// If the action being executed triggers a transition, then the transition, by default, will cause the entity to be saved to the database.
  /// In this case, specifying <see cref="F:PX.Data.WorkflowAPI.ActionPersistOptions.NoPersist" /> in that action would essentially have no effect.
  /// To prevent the transition from saving the entity to the database, call the <see cref="M:PX.Data.WorkflowAPI.BoundedTo`2.Transition.ConfiguratorTransition.DoesNotPersist" /> method in the transition.</summary>
  NoPersist,
  /// <summary>A new entity will be saved before the action is executed. An existing entity will be saved after the action is executed.</summary>
  PersistBeforeAction,
}

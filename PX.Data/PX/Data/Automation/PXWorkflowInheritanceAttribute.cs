// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXWorkflowInheritanceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Automation;

/// <summary>An indicator that the graph should inherit all workflow definitions of the parent graph.
/// If you specify this attribute on a graph declaration, the graph will utilize all workflow extensions of its parent.
/// All workflows defined for this graph, will be applied after the all the extensions of the parent graph.
/// <remarks>The attribute should be used on graph extensions.</remarks>
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class PXWorkflowInheritanceAttribute : Attribute
{
}

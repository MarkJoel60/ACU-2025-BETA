// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>A first-level graph extension. To declare an extension of a graph,
/// you derive a class from this class.</summary>
/// <typeparam name="Graph">The base graph.</typeparam>
/// <example>
/// The example below shows a declaration of a first-level graph extension.
/// <code>class BaseGraphExtension : PXGraphExtension&lt;BaseGraph&gt;
/// {
///     public void SomeMethod()
///     {
///         BaseGraph baseGraph = Base;
///     }
/// }</code></example>
public abstract class PXGraphExtension<Graph> : PXGraphExtension, IExtends<Graph> where Graph : PXGraph
{
  internal Graph _Base;

  /// <summary>
  /// The read-only property, which returns an instance of the base graph.
  /// </summary>
  protected Graph Base => this._Base;
}

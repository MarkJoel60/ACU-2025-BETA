// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>A second-level graph extension.</summary>
/// <typeparam name="Extension1">The first-level or previous-level graph extension.</typeparam>
/// <typeparam name="Graph">The base graph.</typeparam>
/// <remarks><para>You can use this class to define a higher-level graph extension
/// by passing an extension of the previous level to the first type parameter.</para>
/// <para>Conceptually, a graph extension is a substitution of the base graph.
/// The base graph is replaced at run time with the merged result of the base graph and every extension the platform found.
/// The higher level of a declaration an extension has, the higher priority it obtains in the merge operation.</para></remarks>
/// <example>
///   <code>//The example below shows a declaration of a second-level graph extension.
/// class BaseGraphExtensionOnExtension :
///     PXGraphExtension&lt;BaseGraphExtension, BaseGraph&gt;
/// {
///     public void SomeMethod()
///     {
///         BaseGraph baseGraph = Base;
///         BaseGraphExtension ext = Base1;
///     }
/// }</code>
///   <code>//The example below shows a declaration of a third- or higher-level
/// //graph extension that is derived from the PXGraphExtension generic
/// //class with two type parameters.
/// class BaseGraphMultiExtensionOnExtension :
///     PXGraphExtension&lt;BaseGraphExtensionOnExtension, BaseGraph&gt;
/// {
///     public void SomeMethod()
///     {
///         //The instance of the base graph
///         BaseGraph graph = Base;
///         //The instance of the graph extension from the previous level
///         BaseGraphExtensionOnExtension prevExt = Base1;
///     }
/// }</code>
/// </example>
public abstract class PXGraphExtension<Extension1, Graph> : 
  PXGraphExtension<Graph>,
  IExtends<Extension1>
  where Extension1 : PXGraphExtension<Graph>
  where Graph : PXGraph
{
  internal Extension1 _Base1;

  protected Extension1 Base1 => this._Base1;
}

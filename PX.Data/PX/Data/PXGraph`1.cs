// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraph`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The type that is used to derive business logic controllers
/// (graphs) in the application.
/// This type extends the <tt>PXGraph</tt> type with the ability to automatically initialize data views, actions, and event handlers that are defined as members in the current graph or in its base graphs.
/// </summary>
/// <remarks>
/// <para>This type extends the <see cref="T:PX.Data.PXGraph">PXGraph</see> type with the ability to
/// automatically initialize data views, actions, and event handlers that
/// are defined as members in the current graph or in its base
/// graphs.</para>
/// <para>In a graph, you can define the following members:</para>
/// <list type="bullet">
/// <item>
/// <description>Data views as objects of the <see cref="T:PX.Data.PXSelect`1">PXSelect&lt;&gt;</see> type or its
/// variant. The type of a data view is the BQL expression which can be
/// executed by invoking the <tt>Select()</tt> or <tt>Search()</tt>
/// methods.</description>
/// </item>
/// <item>
/// <description>Actions as objects of
/// the <tt>PXAction</tt> type and paired by the implementation
/// method.</description>
/// </item>
/// <item>
/// <description>Event handlers.</description>
/// </item>
/// </list>
/// <para>For a data view you can also define the optional method that
/// will be executed by the Select() method to retrieve the data instead
/// of the standard logic of retreiving the data.</para>
/// <para>Data views and actions must be declared as <tt>public</tt>. When
/// you declare data views and actions, you do not initialize them. The
/// graph initializes them automatically. The <tt>PXView</tt> objects
/// initialized by the data views are available through the <see cref="F:PX.Data.PXGraph.Views">Views</see> collection of the graph. The
/// actions are available through the <see cref="F:PX.Data.PXGraph.Actions">Actions</see> collection of the
/// graph.</para>
/// <para>Event handlers and methods can be declared as <tt>public</tt>,
/// <tt>protected</tt>, or <tt>internal</tt>. The <tt>protected
/// virtual</tt> is the recommended modifier. Event handlers of particular
/// type are available through the corresponding collections.</para>
/// <para>You can derive a graph from the <see cref="T:PX.Data.PXGraph`2">PXGraph&lt;TGraph,
/// TPrimary&gt;</see> type to add pre-defined actions to the graph.</para>
/// </remarks>
/// <example>
/// <code title="Example" lang="CS">
/// // The code below declares a graph.
/// public class ARDocumentEnq : PXGraph&lt;ARDocumentEnq&gt;
/// {
/// }</code>
/// <code title="Example2" lang="CS">
/// // The type parameter is set to the graph itself.
/// // The code below declares a graph with a data view, an action, and an event handler.
/// public class ARDocumentEnq : PXGraph&lt;ARDocumentEnq&gt;
/// {
///     // The data view declaration
///     public PXSelectOrderBy&lt;ARDocumentResult,
///                 OrderBy&lt;Desc&lt;ARDocumentResult.docDate&gt;&gt;&gt; Documents;
/// 
///     // The action declaration
///     public PXAction&lt;ARDocumentFilter&gt; previousPeriod;
///     [PXUIField(DisplayName = "Prev")]
///     [PXPreviousButton]
///     public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
///     {
///         ...
///     }
/// 
///     // The event handler declaration
///     public virtual void ARDocumentFilter_RowSelected(
///         PXCache cache, PXRowSelectedEventArgs e)
///     {
///         ...
///     }
/// }</code>
/// </example>
public class PXGraph<TGraph> : PXGraph where TGraph : PXGraph
{
}

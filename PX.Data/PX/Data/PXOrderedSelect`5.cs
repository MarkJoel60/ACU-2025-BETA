// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOrderedSelect`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>The class that implements the functionality that allows users to drag rows, cut and paste rows, and insert new rows in the middle of the grid. The data view
/// defined with this class can include <tt>Join</tt>, <tt>Where</tt> and <tt>OrderBy</tt> clauses.</summary>
/// <typeparam name="Primary">The primary DAC in the graph.</typeparam>
/// <typeparam name="Table">The DAC of the grid rows.</typeparam>
/// <typeparam name="Join">The <tt>Join</tt> clause.</typeparam>
/// <typeparam name="Where">The <tt>Where</tt> clause.</typeparam>
/// <typeparam name="OrderBy">The <tt>OrderBy</tt> clause.</typeparam>
/// <remarks>This class can be used in a graph for the grid's view.</remarks>
[PXDynamicButton(new string[] {"PasteLine", "ResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (Messages))]
public class PXOrderedSelect<Primary, Table, Join, Where, OrderBy> : 
  PXOrderedSelectBase<Primary, Table>
  where Primary : class, IBqlTable, new()
  where Table : class, IBqlTable, ISortOrder, new()
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <summary>Initializes a new instance of a data view bound to the specified graph.</summary>
  /// <param name="graph">The graph with which the data view is associated.</param>
  public PXOrderedSelect(PXGraph graph)
  {
    this._Graph = graph;
    this.Initialize();
    this.View = new PXView(graph, false, (BqlCommand) new Select2<Table, Join, Where, OrderBy>());
  }

  /// <summary>Initializes a new instance of a data view that is bound to the specified graph and uses the provided method to retrieve data.</summary>
  /// <param name="graph">The graph with which the data view is associated.</param>
  /// <param name="handler">The delegate of the method that is used to retrieve the data from the database (or other source). This method is invoked when one of the <tt>Select()</tt> methods is
  /// called.</param>
  public PXOrderedSelect(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this.Initialize();
    this.View = new PXView(graph, false, (BqlCommand) new Select2<Table, Join, Where, OrderBy>(), handler);
  }
}

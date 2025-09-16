// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelect`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// <para>Defines a data view for retrieving a particular data set from the database and provides the interface to the cache for inserting, updating, and deleting the
/// data records.</para>
/// <para>This class and other classes derived from <tt>PXSelectBase</tt> are used as a basis for building BQL statements. These classes are translated into the SQL
/// <tt>SELECT</tt> statements.</para>
/// </summary>
/// <typeparam name="Table">Specifies a data access class (DAC) bound to a database table from which the resulting SQL query will select records.</typeparam>
/// <remarks>
/// <para>A <tt>PXSelect&lt;Table&gt;</tt> object wraps the <tt>Select&lt;Table&gt;</tt> object, which represents the BQL command, and the <see cref="T:PX.Data.PXView">PXView</see> object,
/// which executes this BQL command. The <tt>PXSelect&lt;Table&gt;</tt> object also holds the reference of the <see cref="T:PX.Data.PXCache`1">cache</see> of the <tt>Table</tt> data
/// records and the graph.</para>
/// <para>The <tt>PXSelect&lt;Table&gt;</tt> type provides interfaces to the <tt>PXView</tt> object and the cache. So you can execute the underlying BQL command and
/// invoke cache methods through the methods of the <tt>PXSelect&lt;Table&gt;</tt> class.</para>
/// </remarks>
/// <example><para>The code below shows the declaration of a data view in a graph and execution of this data view.</para>
/// <code title="Example" lang="CS">
/// public class VendorClassMaint : PXGraph&lt;VendorClassMaint&gt;
/// {
///     public PXSelect&lt;Vendor,
///         Where&lt;Vendor.vendorClassID, Equal&lt;Current&lt;VendorClass.vendorClassID&gt;&gt;&gt;&gt;
///     Vendors;
///     ...
///     public void SomeMethod()
///     {
///         // Data view execution
///         foreach (Vendor vend in Vendors.Select())
///             ...
///     }
/// }</code>
/// </example>
public class PXSelect<Table> : PXSelectBase<Table, PXSelect<Table>.Config> where Table : class, IBqlTable, new()
{
  /// <summary>Initializes a new instance of a data view bound to the
  /// specified graph.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  public PXSelect(PXGraph graph)
    : base(graph)
  {
  }

  /// <summary>Initializes a new instance of a data view that is bound to
  /// the specified graph and uses the provided method to retrieve
  /// data.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  /// <param name="handler">The delegate of the method that is used to
  /// retrieve the data from the database (or other source). This method is
  /// invoked when one of the <tt>Select()</tt> methods is called.</param>
  public PXSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : PXSelectBase<Table, PXSelect<Table>.Config>.IViewConfig, IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<Table>();

    public bool IsReadOnly => false;
  }
}

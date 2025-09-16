// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelect`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from one table filtered by an expression set in <tt>Where</tt>. The result set is merged with the modified data records kept in the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
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
public class PXSelect<Table, Where> : PXSelectBase<Table, PXSelect<Table, Where>.Config>
  where Table : class, IBqlTable, new()
  where Where : IBqlWhere, new()
{
  /// <exclude />
  public PXSelect(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelect<Table, Where>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<Table, Where>();

    public bool IsReadOnly => false;
  }
}

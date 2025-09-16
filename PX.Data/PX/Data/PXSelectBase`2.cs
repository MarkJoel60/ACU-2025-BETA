// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data;

public abstract class PXSelectBase<Table, TViewConfig> : PXSelectBase<Table>
  where Table : class, IBqlTable, new()
  where TViewConfig : PXSelectBase<Table, TViewConfig>.IViewConfig, new()
{
  private static readonly TViewConfig ViewConfig = new TViewConfig();

  /// <summary>Returns the <tt>BqlCommand</tt> object representing the BLQ statement.</summary>
  public static BqlCommand GetCommand() => PXSelectBase<Table, TViewConfig>.ViewConfig.GetCommand();

  private static bool IsReadOnly => PXSelectBase<Table, TViewConfig>.ViewConfig.IsReadOnly;

  /// <summary>Initializes a new instance of a data view bound to the
  /// specified graph.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  protected PXSelectBase(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(graph, PXSelectBase<Table, TViewConfig>.IsReadOnly, PXSelectBase<Table, TViewConfig>.GetCommand());
  }

  /// <summary>Initializes a new instance of a data view that is bound to
  /// the specified graph and uses the provided method to retrieve
  /// data.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  /// <param name="handler">The delegate of the method that is used to
  /// retrieve the data from the database (or other source). This method is
  /// invoked when one of the <tt>Select()</tt> methods is called.</param>
  protected PXSelectBase(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this.View = new PXView(graph, PXSelectBase<Table, TViewConfig>.IsReadOnly, PXSelectBase<Table, TViewConfig>.GetCommand(), handler);
  }

  /// <summary>Retrieves the specified number of data records starting from
  /// the given position.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> SelectWindowed(
    PXGraph graph,
    int startRow,
    int totalRows,
    params object[] pars)
  {
    return PXSelectBase<Table, TViewConfig>.SelectWindowed<PXResultset<Table>>(graph, startRow, totalRows, pars);
  }

  /// <summary>Executes the BQL statement and retrieves all matching data
  /// records.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Select(PXGraph graph, params object[] pars)
  {
    return PXSelectBase<Table, TViewConfig>.Select<PXResultset<Table>>(graph, pars);
  }

  /// <summary>Executes the BQL statement and retrieves all matching data
  /// records.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  internal static IPXAsyncResultset<PXResult<Table>> SelectAsync(
    PXGraph graph,
    CancellationToken cancellationToken,
    params object[] pars)
  {
    return PXSelectBase<Table>.selectBoundAsync(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, 0, 0, (object[]) null, cancellationToken, pars);
  }

  /// <summary>Executes BQL command with specified parameters and currents. Selects single row.</summary>
  /// <param name="graph">Graph is used to cache selected results and to merge modified rows</param>
  /// <param name="currents">Overrides values from PXCache.Current</param>
  /// <param name="pars">Parameters referenced from "Optional" or "Required" BQL statement</param>
  /// <returns></returns>
  public static PXResultset<Table> SelectSingleBound(
    PXGraph graph,
    object[] currents,
    params object[] pars)
  {
    return PXSelectBase<Table>.selectBound<PXResultset<Table>>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, 0, 1, currents, pars);
  }

  /// <summary>Executes BQL command with specified parameters and currents. Selects single row.
  /// A specific <tt>PXResultset&lt;&gt;</tt> type can be specified
  /// in the type parameter. To wrap the retrieved data records, the non-
  /// generic <tt>Select()</tt> method uses the
  /// <tt>PXResultset&lt;Table&gt;</tt> type, where <tt>Table</tt> is the
  /// first DAC specified in the BQL statement.</summary>
  /// <param name="graph">Graph is used to cache selected results and to merge modified rows</param>
  /// <param name="currents">Overrides values from PXCache.Current</param>
  /// <param name="pars">Parameters referenced from "Optional" or "Required" BQL statement</param>
  /// <returns></returns>
  public static Resultset SelectSingleBound<Resultset>(
    PXGraph graph,
    object[] currents,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
  {
    return PXSelectBase<Table>.selectBound<Resultset>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, 0, 1, currents, pars);
  }

  /// <summary>Executes the BQL statement with the specified values to
  /// substitute current object and retrieves all matching data
  /// records.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="currents">The objects to be used instead of the data
  /// records referenced by the <tt>Current</tt> property of the
  /// caches.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> SelectMultiBound(
    PXGraph graph,
    object[] currents,
    params object[] pars)
  {
    return PXSelectBase<Table>.selectBound<PXResultset<Table>>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, 0, 0, currents, pars);
  }

  /// <summary>Executes the BQL statement with the specified values to
  /// substitute current object and retrieves all matching data
  /// records. A specific <tt>PXResultset&lt;&gt;</tt> type can be specified
  /// in the type parameter. To wrap the retrieved data records, the non-
  /// generic <tt>Select()</tt> method uses the
  /// <tt>PXResultset&lt;Table&gt;</tt> type, where <tt>Table</tt> is the
  /// first DAC specified in the BQL statement.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="currents">The objects to be used instead of the data
  /// records referenced by the <tt>Current</tt> property of the
  /// caches.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static Resultset SelectMultiBound<Resultset>(
    PXGraph graph,
    object[] currents,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
  {
    return PXSelectBase<Table>.selectBound<Resultset>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, 0, 0, currents, pars);
  }

  /// <summary>Executes the BQL statement and retrieves all matching data
  /// records. A specific <tt>PXResultset&lt;&gt;</tt> type can be specified
  /// in the type parameter. To wrap the retrieved data records, the non-
  /// generic <tt>Select()</tt> method uses the
  /// <tt>PXResultset&lt;Table&gt;</tt> type, where <tt>Table</tt> is the
  /// first DAC specified in the BQL statement.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static Resultset Select<Resultset>(PXGraph graph, params object[] pars) where Resultset : PXResultset<Table>, new()
  {
    return PXSelectBase<Table, TViewConfig>.SelectWindowed<Resultset>(graph, 0, 0, pars);
  }

  /// <summary>Retrieves the specified number of data records starting from
  /// the given position. A specific <tt>PXResultset&lt;&gt;</tt> type can
  /// be specified in the type parameter.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="pars">The values to substitute BQL parameters, such as
  /// <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static Resultset SelectWindowed<Resultset>(
    PXGraph graph,
    int startRow,
    int totalRows,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
  {
    return PXSelectBase<Table>.select<Resultset>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, startRow, totalRows, pars);
  }

  /// <summary>Searches for a data record by the value of specified field in
  /// the data set that corresponds to the BQL statement. The method extends
  /// the BQL statement with filtering and ordering by the specified field
  /// and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0>(
    PXGraph graph,
    object field0,
    params object[] arguments)
    where Field0 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0>>(graph, new object[1]
    {
      field0
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  /// <example>
  /// The code below checks whether a duplicate of the <tt>APInvoice</tt>
  /// data record exists by searching by the key fields.
  /// <code>
  /// APInvoice duplicate = PXSelect&lt;APInvoice&gt;.
  ///     Search&lt;APInvoice.docType, APInvoice.refNbr&gt;(
  ///         this, invoice.DocType, invoice.OrigRefNbr);
  /// 
  /// // If the data record exists, throw an exception
  /// if (duplicate != null)
  ///     throw new PXException(ErrorMessages.RecordExists);</code>
  /// </example>
  public static PXResultset<Table> Search<Field0, Field1>(
    PXGraph graph,
    object field0,
    object field1,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1>>>(graph, new object[2]
    {
      field0,
      field1
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2>>>>(graph, new object[3]
    {
      field0,
      field1,
      field2
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3>>>>>(graph, new object[4]
    {
      field0,
      field1,
      field2,
      field3
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4>>>>>>(graph, new object[5]
    {
      field0,
      field1,
      field2,
      field3,
      field4
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field5">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5>>>>>>>(graph, new object[6]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field5">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field6">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6>>>>>>>>(graph, new object[7]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field5">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field6">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field7">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7>>>>>>>>>(graph, new object[8]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field5">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field6">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field7">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field8">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8>>>>>>>>>>(graph, new object[9]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8
    }, 0, 1, arguments);
  }

  /// <summary>Searches for a data record by the values of specified fields
  /// in the data set that corresponds to the BQL statement. The method
  /// extends the BQL statement with filtering and ordering by the specified
  /// fields and retrieves the top data record.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="field0">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field1">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field2">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field3">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field4">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field5">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field6">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field7">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field8">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="field9">The value by which the data
  /// set is filtered and sorted.</param>
  /// <param name="arguments">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> Search<Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9>(
    PXGraph graph,
    object field0,
    object field1,
    object field2,
    object field3,
    object field4,
    object field5,
    object field6,
    object field7,
    object field8,
    object field9,
    params object[] arguments)
    where Field0 : IBqlField
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
    where Field9 : IBqlField
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<PXResultset<Table>, Asc<Field0, Asc<Field1, Asc<Field2, Asc<Field3, Asc<Field4, Asc<Field5, Asc<Field6, Asc<Field7, Asc<Field8, Asc<Field9>>>>>>>>>>>(graph, new object[10]
    {
      field0,
      field1,
      field2,
      field3,
      field4,
      field5,
      field6,
      field7,
      field8,
      field9
    }, 0, 1, arguments);
  }

  /// <summary>Searches the data set that corresponds to the BQL statement
  /// for all data records whose fields have the specified values. The
  /// fields are specified in the type parameter. The method extends the BQL
  /// statement with filtering and ordering by the fields and retrieves all
  /// data records from the resulting data set.</summary>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="pars">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static PXResultset<Table> SearchAll<Sort>(
    PXGraph graph,
    object[] searchValues,
    params object[] pars)
    where Sort : IBqlSortColumn
  {
    return PXSelectBase<Table, TViewConfig>.SearchAll<PXResultset<Table>, Sort>(graph, searchValues, pars);
  }

  /// <summary>Searches the data set that corresponds to the BQL statement
  /// for all data records whose fields have the specified values.</summary>
  /// <remarks>The fields are specified in the <tt>Sort</tt> type parameter.
  /// The method extends the BQL statement with filtering and ordering by
  /// the fields and retrieves all data records from the resulting data set.
  /// A specific <tt>PXResultset&lt;&gt;</tt> type can be specified in the
  /// <tt>Resultset</tt> type parameter.</remarks>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="pars">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static Resultset SearchAll<Resultset, Sort>(
    PXGraph graph,
    object[] searchValues,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
    where Sort : IBqlSortColumn
  {
    return PXSelectBase<Table, TViewConfig>.SearchWindowed<Resultset, Sort>(graph, searchValues, 0, 0, pars);
  }

  /// <summary>Searches the data set that corresponds to the BQL statement
  /// for the data records whose fields have the specified values. Retrieves
  /// the specified number of such data records starting from the given
  /// position.</summary>
  /// <remarks>The fields are specified in the <tt>Sort</tt> type parameter.
  /// The method extends the BQL statement with filtering and ordering by
  /// the fields and retrieves all data records from the resulting data set.
  /// A specific <tt>PXResultset&lt;&gt;</tt> type can be specified in the
  /// <tt>Resultset</tt> type parameter.</remarks>
  /// <param name="graph">The graph that is used to cache the retrieved data
  /// record and merge them with the modified data records.</param>
  /// <param name="searchValues">The values of fields referenced in
  /// <tt>Sort</tt> by which the data set is filtered and sorted.</param>
  /// <param name="startRow">The 0-based index of the first data record to
  /// retrieve.</param>
  /// <param name="totalRows">The number of data records to
  /// retrieve.</param>
  /// <param name="pars">The values to substitute BQL parameters, such
  /// as <tt>Optional</tt>, <tt>Required</tt>, and <tt>Argument</tt>, in the
  /// BQL statement.</param>
  public static Resultset SearchWindowed<Resultset, Sort>(
    PXGraph graph,
    object[] searchValues,
    int startRow,
    int totalRows,
    params object[] pars)
    where Resultset : PXResultset<Table>, new()
    where Sort : IBqlSortColumn
  {
    return PXSelectBase<Table>.search<Resultset, Sort>(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, searchValues, startRow, totalRows, pars);
  }

  /// <summary>Clears the results of BQL statement execution stored in the
  /// provided graph.</summary>
  /// <param name="graph">The graph where the data is cleared.</param>
  /// <example><para>The code below clears the query cache to load the records directly from the database (the data records are still merged with the modifications stored in the PXCache object).</para>
  /// <code title="Example" lang="CS">
  /// // Clearing the query cache
  /// PXSelect&lt;CRMergeCriteria,
  ///     Where&lt;CRMergeCriteria.mergeID, Equal&lt;Required&lt;CRMerge.mergeID&gt;&gt;&gt;&gt;.
  ///     Clear(this);
  /// // Selecting data records directly from the database (not from the query
  /// // cache) and merging with the PXCache&lt;&gt; object
  /// foreach (CRMergeCriteria item in
  ///      PXSelect&lt;CRMergeCriteria,
  ///          Where&lt;CRMergeCriteria.mergeID, Equal&lt;Required&lt;CRMerge.mergeID&gt;&gt;&gt;&gt;.
  ///          Select(this, document.MergeID))
  /// {
  ///     Criteria.Cache.Delete(item);
  /// }</code>
  /// </example>
  public static void Clear(PXGraph graph)
  {
    PXSelectBase<Table>.Clear(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph);
  }

  /// <summary>Stores in the caches the results of BQL statement
  /// execution.</summary>
  /// <param name="graph">The graph object whose caches are used to store
  /// the data records.</param>
  /// <param name="queryKey"></param>
  /// <param name="records"></param>
  public static void StoreCached(PXGraph graph, PXCommandKey queryKey, List<object> records)
  {
    PXSelectBase<Table>.StoreCached(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, queryKey, records);
  }

  public static void StoreResult(
    PXGraph graph,
    List<object> selectResult,
    PXQueryParameters queryParameters)
  {
    PXSelectBase<Table>.StoreResult(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, selectResult, queryParameters);
  }

  public static void StoreResult(
    PXGraph graph,
    List<object> selectResult,
    PXQueryParameters queryParameters,
    bool clearView)
  {
    PXSelectBase<Table>.StoreResult(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, selectResult, queryParameters, clearView);
  }

  public static void StoreResult(PXGraph graph, IBqlTable selectResult)
  {
    PXSelectBase<Table>.StoreResult(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, selectResult);
  }

  public static void StoreResult(PXGraph graph, PXResult selectResult)
  {
    PXSelectBase<Table>.StoreResult(PXSelectBase<Table, TViewConfig>.GetCommand(), PXSelectBase<Table, TViewConfig>.IsReadOnly, graph, selectResult);
  }

  public interface IViewConfig : IViewConfigBase
  {
  }
}

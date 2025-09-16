// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAdapter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>A class that is used to support various operations,
/// such as navigation, redirects, and mass-processing operations.
/// The class contains information about the invoked operation,
/// the data query for retrieval of the records being processed,
/// and the state of the screen.</summary>
public sealed class PXAdapter
{
  /// <summary>A Boolean value that indicates (if set to <see lagword="true" />)
  /// that the current operation has been started through the UI or the web services API.</summary>
  public bool ExternalCall;
  internal bool InternalCall;
  private bool _forceRedirect;
  /// <summary>
  /// A <see cref="T:PX.Data.PXView" /> object that defines the data query for retrieval of the records being processed.
  /// </summary>
  public readonly PXView View;

  /// <summary>Returns the non-generic list of records that are being processed.</summary>
  /// <example>
  /// The following example shows one of the standard action declarations
  /// that is used when the action initiates a background operation or is called from a processing form.
  /// <code>[PXButton]
  /// [PXUIField(DisplayName = "Release")]
  /// protected virtual IEnumerable release(PXAdapter adapter)
  /// {
  ///     ...
  ///     return adapter.Get();
  /// }</code></example>
  public IEnumerable Get() => (IEnumerable) this.SelectData();

  /// <summary>Returns the list of records of a specified type that are being processed.</summary>
  /// <typeparam name="Table">A DAC.</typeparam>
  /// <example>
  /// <code>List&lt;SOOrder&gt; list = adapter.Get&lt;SOOrder&gt;().ToList();</code></example>
  public IEnumerable<Table> Get<Table>()
  {
    return this.SelectData().Select<object, Table>((Func<object, Table>) (row => row is PXResult pxResult ? (Table) pxResult[typeof (Table)] : (Table) row));
  }

  private IEnumerable<object> SelectData()
  {
    if (this.View.Graph.TimeStamp == null)
      this.View.Graph.SelectTimeStamp();
    int startRow = this.StartRow;
    int totalRows = this.TotalRequired ? -1 : 0;
    List<object> objectList = this.View.Select(this.Currents, this.Parameters, this.Searches, this.SortColumns, this.Descendings, this.Filters, ref startRow, this.MaximumRows, ref totalRows);
    this.StartRow = startRow;
    this.TotalRows = totalRows;
    return (IEnumerable<object>) objectList;
  }

  /// <summary>Initializes a new instance of the <see cref="T:PX.Data.PXAdapter" /> class
  /// for the specified <see cref="T:PX.Data.PXView" />.</summary>
  /// <param name="view">A <see cref="T:PX.Data.PXView" /> instance.</param>
  /// <example>
  /// In the following example, a new instance of <see cref="T:PX.Data.PXAdapter" /> is created for a dummy view.
  /// <code>adapter = new PXAdapter(PXView.Dummy.For&lt;SOShipment&gt;(this))
  /// {
  ///     MassProcess = true,
  ///     Arguments =
  ///             {
  ///                 [nameof(IPrintable.PrintWithDeviceHub)] = true,
  ///                 [nameof(IPrintable.DefinePrinterManually)] = false
  ///             }
  /// };</code></example>
  public PXAdapter(PXView view)
  {
    this.View = view ?? throw new PXArgumentException(nameof (view), "The argument cannot be null.");
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Data.PXAdapter" /> class
  /// for the specified <see cref="T:PX.Data.PXSelectBase" />-derived class instance and its parameters.
  /// </summary>
  /// <param name="indexer">A <see cref="T:PX.Data.PXSelectBase" />-derived class instance.</param>
  /// <param name="pars">The parameters to be passed to <paramref name="indexer" />.</param>
  public PXAdapter(PXSelectBase indexer, params object[] pars)
    : this(indexer?.View)
  {
    this.Parameters = pars;
  }

  /// <summary>
  /// Creates a new <see cref="T:PX.Data.PXAdapter" /> instance that is related to another <paramref name="graph" />, by cloning the state of the current <see cref="T:PX.Data.PXAdapter" /> instance.
  /// </summary>
  /// <param name="graph">An instance of <see cref="T:PX.Data.PXGraph" /> for which a copy should be created.</param>
  public PXAdapter CloneTo(PXGraph graph)
  {
    PXView view = this.View;
    PXAdapter target = new PXAdapter(string.IsNullOrEmpty(view.Name) || !graph.Views.ContainsKey(view.Name) ? graph.TypedViews.GetView(view.BqlSelect, view.IsReadOnly) : graph.Views[view.Name]);
    PXAdapter.Copy(this, target);
    return target;
  }

  /// <summary>Copies one instance of <see cref="T:PX.Data.PXAdapter" /> to another.</summary>
  /// <param name="source">The source <see cref="T:PX.Data.PXAdapter" /> instance.</param>
  /// <param name="target">The target <see cref="T:PX.Data.PXAdapter" /> instance.</param>
  public static void Copy(PXAdapter source, PXAdapter target)
  {
    target.ExternalCall = source.ExternalCall;
    target.InternalCall = source.InternalCall;
    target._forceRedirect = source._forceRedirect;
    target.ForceButtonEnabledCheck = source.ForceButtonEnabledCheck;
    target.StartRow = source.StartRow;
    target.MaximumRows = source.MaximumRows;
    target.TotalRows = source.TotalRows;
    target.TotalRequired = source.TotalRequired;
    target.Menu = source.Menu;
    target.MassProcess = source.MassProcess;
    target.QuickProcessFlow = source.QuickProcessFlow;
    target.ImportFlag = source.ImportFlag;
    target.CommandArguments = source.CommandArguments;
    PXAdapter pxAdapter1 = target;
    object[] currents = source.Currents;
    object[] array1 = currents != null ? ((IEnumerable<object>) currents).ToArray<object>() : (object[]) null;
    pxAdapter1.Currents = array1;
    PXAdapter pxAdapter2 = target;
    object[] parameters = source.Parameters;
    object[] array2 = parameters != null ? ((IEnumerable<object>) parameters).ToArray<object>() : (object[]) null;
    pxAdapter2.Parameters = array2;
    PXAdapter pxAdapter3 = target;
    object[] searches = source.Searches;
    object[] array3 = searches != null ? ((IEnumerable<object>) searches).ToArray<object>() : (object[]) null;
    pxAdapter3.Searches = array3;
    PXAdapter pxAdapter4 = target;
    string[] sortColumns = source.SortColumns;
    string[] array4 = sortColumns != null ? ((IEnumerable<string>) sortColumns).ToArray<string>() : (string[]) null;
    pxAdapter4.SortColumns = array4;
    PXAdapter pxAdapter5 = target;
    bool[] descendings = source.Descendings;
    bool[] array5 = descendings != null ? ((IEnumerable<bool>) descendings).ToArray<bool>() : (bool[]) null;
    pxAdapter5.Descendings = array5;
    PXAdapter pxAdapter6 = target;
    PXFilterRow[] filters = source.Filters;
    PXFilterRow[] array6 = filters != null ? ((IEnumerable<PXFilterRow>) filters).ToArray<PXFilterRow>() : (PXFilterRow[]) null;
    pxAdapter6.Filters = array6;
    PXAdapter pxAdapter7 = target;
    Dictionary<string, object> arguments = source.Arguments;
    Dictionary<string, object> dictionary = arguments != null ? arguments.ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (t => t.Key), (Func<KeyValuePair<string, object>, object>) (t => t.Value)) : (Dictionary<string, object>) null;
    pxAdapter7.Arguments = dictionary;
  }

  /// <summary>A Boolean value that indicates (if set to <see lagword="true" />)
  /// that a redirect (such as redirect to a form or report) can be performed during the current operation.</summary>
  public bool AllowRedirect
  {
    get => this._forceRedirect | this.ExternalCall;
    set => this._forceRedirect = value;
  }

  internal bool ForceButtonEnabledCheck { get; set; }

  /// <summary>
  /// The collection of current values, which substitute graph current records
  /// for calculation of the parameters of the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> and other purposes.
  /// </summary>
  public object[] Currents { get; set; }

  /// <summary>
  /// The parameters of the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see>.
  /// </summary>
  public object[] Parameters { get; set; }

  /// <summary>
  /// The field values by which the records being processed can be retrieved
  /// from the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see>.
  /// </summary>
  public object[] Searches { get; set; }

  /// <summary>
  /// The fields by which the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> results are sorted.
  /// </summary>
  public string[] SortColumns { get; set; }

  /// <summary>
  /// The list of Boolean values each of which indicates (if set to <see langword="true" />)
  /// that the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> results are sorted descending by the corresponding sort field.
  /// </summary>
  public bool[] Descendings { get; set; }

  /// <summary>
  /// The filter conditions that are applied to the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> results.
  /// </summary>
  public PXFilterRow[] Filters { get; set; }

  /// <summary>
  /// The starting row position in the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> results
  /// from which the adapter processes records.
  /// </summary>
  /// <value>The 0 value indicates that the starting position is the beginning of the result set.</value>
  /// <remarks>The adapter can process a subset of records for paging
  /// if the <see cref="P:PX.Data.PXAdapter.StartRow" /> and <see cref="P:PX.Data.PXAdapter.MaximumRows" /> properties are specified.</remarks>
  public int StartRow { get; set; }

  /// <summary>
  /// The maximum number of records that the adapter processes during the operation.
  /// </summary>
  /// <value>The 0 value indicates that all possible records are processed.</value>
  /// <inheritdoc cref="P:PX.Data.PXAdapter.StartRow" path="/remarks" />
  public int MaximumRows { get; set; }

  /// <summary>
  /// The total number of rows in the adapter's <see cref="F:PX.Data.PXAdapter.View">data query</see> results.
  /// </summary>
  public int TotalRows { get; private set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />)
  /// that the adapter should retrieve the <see cref="P:PX.Data.PXAdapter.TotalRows" /> value during the processing operation.
  /// </summary>
  public bool TotalRequired { get; set; }

  /// <summary>
  /// The name of the command in a toolbar menu that corresponds to the operation
  /// that triggered the processing.
  /// </summary>
  public string Menu { get; set; }

  /// <summary>
  /// The dictionary of arguments, which populate arguments of the action delegate.
  /// </summary>
  /// <remarks>The first argument must be <see cref="T:PX.Data.PXAdapter" />. The maximum number of arguments is 10.</remarks>
  public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();

  /// <summary>A Boolean value that indicates (if set to <see langword="true" />)
  /// that a mass-processing operation is executed.</summary>
  public bool MassProcess { get; set; }

  /// <summary>
  /// The flow of the operation that is performed as a <see cref="T:PX.Data.PXQuickProcess">quick process</see>.
  /// </summary>
  public PXQuickProcess.ActionFlow QuickProcessFlow { get; internal set; }

  /// <summary>A Boolean value that indicates (if set to <see langword="true" />)
  /// that an import operation is executed.</summary>
  public bool ImportFlag { get; set; }

  /// <summary>A string that contains the command arguments.</summary>
  /// <remarks>The property is used to pass service information from the UI. It is mostly used in drag-and-drop operations.</remarks>
  public string CommandArguments { get; set; }
}

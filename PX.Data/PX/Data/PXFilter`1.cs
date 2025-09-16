// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilter`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>A data view that is used to provide parameters for data selection on pages.</summary>
/// <typeparam name="Table">The DAC that provides the filtering parameters.</typeparam>
/// <remarks>
///   <para>The data view of this type always returns one record that consists of the filtering parameters specified by the user in the UI. The <tt>PXFilter</tt> data
/// view never requests data from the database.</para>
/// </remarks>
/// <example><para>The following code shows the definition of the &lt;tt&gt;SalesOrderFilter&lt;/tt&gt; DAC and the &lt;tt&gt;Filter&lt;/tt&gt; data view that uses this DAC.</para>
///   <code title="Example" lang="CS">
/// [Serializable]
/// public class SalesOrderFilter : PXBqlTable, IBqlTable
/// {
///   #region CustomerID
///   public abstract class customerID : PX.Data.BQL.BqlInt.Field&lt;customerID&gt;
///   {
///   }
///   [PXInt]
///   [PXDefault]
///   [PXUIField(DisplayName = "Customer ID")]
///   [PXSelector(
///   typeof(Customer.customerID),
///   typeof(Customer.customerCD),
///   typeof(Customer.companyName),
///   SubstituteKey = typeof(Customer.customerCD))]
///   public virtual int? CustomerID { get; set; }
///   #endregion
///   #region Status
///   public abstract class status : PX.Data.BQL.BqlString.Field&lt;status&gt;
///   {
///   }
///   [PXString(1, IsFixed = true)]
///   [PXUIField(DisplayName = "Status")]
///   [PXStringList(
///   new string[]
///   {
///     OrderStatus.Open,
///     OrderStatus.Hold,
///     OrderStatus.Approved,
///     OrderStatus.Completed
///   },
///   new string[]
///   {
///     OrderStatus.UI.Open,
///     OrderStatus.UI.Hold,
///     OrderStatus.UI.Approved,
///     OrderStatus.UI.Completed
///   })]
///   public virtual string Status { get; set; }
///   #endregion
/// }
/// 
/// public PXFilter&lt;SalesOrderFilter&gt; Filter;</code>
/// </example>
public class PXFilter<Table> : PXSelectBase<Table> where Table : class, IBqlTable, new()
{
  private Table current;
  private bool _inserting;

  /// <exclude />
  public PXFilter(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(graph, false, (BqlCommand) new PX.Data.Select<Table>(), (Delegate) new PXSelectDelegate(this.Get));
    System.Type type = this._Graph.Prototype.Memoize<System.Type>((Func<System.Type>) (() => this._Graph.Caches[typeof (Table)].GetItemType()), (object) typeof (Table));
    this._Graph.Caches.ProcessCacheMapping(this._Graph, type);
    this._Graph.Defaults[type] = new PXGraph.GetDefaultDelegate(this.getFilter);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._Graph.RowPersisting.AddHandler(typeof (Table), PXFilter<Table>.\u003C\u003EO.\u003C0\u003E__persisting ?? (PXFilter<Table>.\u003C\u003EO.\u003C0\u003E__persisting = new PXRowPersisting(PXFilter<Table>.persisting)));
    this._Graph.OnReuseInitialize += new System.Action(this.ResetState);
  }

  /// <exclude />
  public PXFilter(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this.View = (PXView) new PXFilter<Table>.swfilterview(graph, handler);
    System.Type type = this._Graph.Prototype.Memoize<System.Type>((Func<System.Type>) (() => this._Graph.Caches[typeof (Table)].GetItemType()), (object) typeof (Table));
    this._Graph.Caches.ProcessCacheMapping(this._Graph, type);
    this._Graph.Defaults[type] = new PXGraph.GetDefaultDelegate(this.getFilter);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._Graph.RowPersisting.AddHandler(typeof (Table), PXFilter<Table>.\u003C\u003EO.\u003C0\u003E__persisting ?? (PXFilter<Table>.\u003C\u003EO.\u003C0\u003E__persisting = new PXRowPersisting(PXFilter<Table>.persisting)));
  }

  /// <exclude />
  [PXDependToCache(new System.Type[] {})]
  public IEnumerable Get()
  {
    PXCache cache = this._Graph.Caches[typeof (Table)];
    cache._AllowInsert = true;
    cache._AllowUpdate = true;
    object obj = cache.Current;
    if (obj != null)
    {
      if (cache.Locate(obj) == null)
      {
        try
        {
          obj = cache.Insert(obj);
        }
        catch
        {
          cache.SetStatus(obj, PXEntryStatus.Inserted);
        }
      }
    }
    yield return obj;
    cache.IsDirty = false;
    cache.Version = 0;
  }

  /// <exclude />
  public void Reset()
  {
    this.Cache.Clear();
    this.Cache.Current = (object) null;
  }

  /// <exclude />
  public bool VerifyRequired() => this.VerifyRequired(false);

  /// <exclude />
  public bool AskExtRequired(WebDialogResult result = WebDialogResult.OK, bool reset = true)
  {
    return this.AskExtRequired((PXView.InitializePanel) ((g, c) => { }), result, reset);
  }

  private PXView.InitializePanel GetResetHandler(
    PXView.InitializePanel initializeHandler,
    bool reset = true)
  {
    return (PXView.InitializePanel) ((graph, viewName) =>
    {
      if (reset)
        this.Reset();
      initializeHandler(graph, viewName);
    });
  }

  /// <exclude />
  public bool AskExtRequired(
    PXView.InitializePanel initializeHandler,
    WebDialogResult result = WebDialogResult.OK,
    bool reset = true)
  {
    return this.AskExt(this.GetResetHandler(initializeHandler, reset)) == result && this.VerifyRequired();
  }

  private bool AskExtWithValidation(
    PXView.InitializePanel initializeHandler,
    PXFilter<Table>.Validator validator,
    DialogAnswerType resultType,
    bool reset = true)
  {
    return resultType == this.AskExt(this.GetResetHandler(initializeHandler, reset)).GetAnswerType() && validator();
  }

  /// <exclude />
  public bool AskExtRequired(
    PXView.InitializePanel initializeHandler,
    DialogAnswerType resultType,
    bool reset = true)
  {
    return this.AskExtWithValidation(initializeHandler, new PXFilter<Table>.Validator(this.VerifyRequired), resultType, reset);
  }

  /// <summary>Displays the dialog box, which is configured by the <tt>PXSmartPanel</tt> control, compares the answer returned by the dialog box with the <tt>resultType</tt>
  /// parameter, and returns the Boolean result of comparison. The method can initialize the values in the dialog box.</summary>
  /// <param name="initializeHandler">A handler for initialization of the data for the dialog box.</param>
  /// <param name="resultType">The value that is compared with the answer returned by the dialog box.</param>
  /// <param name="reset">A value that indicates (if set to <tt>true</tt>) that the data of the dialog box must be cleared before the dialog box is displayed. By default, the value
  /// is <tt>true</tt>.</param>
  /// <returns>A Boolean result of comparison of the dialog box answer and <tt>resultType</tt>.</returns>
  /// <example><para>The following example shows calling of the dialog box and processing of the returned result.</para>
  ///   <code title="Example" lang="CS">
  /// [PXUIField(DisplayName = "Create Ledger", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  /// [PXButton]
  /// public virtual IEnumerable CreateLedger(PXAdapter adapter)
  /// {
  ///     if (OrganizationView.Current == null) return adapter.Get();
  /// 
  ///     if (CreateLedgerView.AskExtFullyValid(
  ///         (graph, viewName) =&gt;
  ///         {
  ///             CreateLedgerView.Current.OrganizationID = OrganizationView.Current.OrganizationID;
  ///             CreateLedgerView.Current.Descr = String.Format(Messages.ActualLedgerDescription, OrganizationView.Current.OrganizationCD.Trim());
  ///         },
  ///         DialogAnswerType.Positive))
  ///     {
  ///         Save.Press();
  ///         CreateLeadgerProc(CreateLedgerView.Current);
  ///         throw new PXRefreshException();
  ///     }
  /// 
  ///     return adapter.Get();
  /// }</code>
  /// </example>
  public bool AskExtFullyValid(
    PXView.InitializePanel initializeHandler,
    DialogAnswerType resultType,
    bool reset = true)
  {
    return this.AskExtWithValidation(initializeHandler, new PXFilter<Table>.Validator(this.VerifyFullyValid), resultType, reset);
  }

  /// <summary>Displays the dialog box, which is configured by the <tt>PXSmartPanel</tt> control, compares the answer returned by the dialog box with the <tt>resultType</tt>
  /// parameter, and returns the Boolean result of comparison.</summary>
  /// <param name="reset">A value that indicates (if set to <tt>true</tt>) that the data of the dialog box must be cleared before the dialog box is displayed.</param>
  /// <param name="resultType">The value that is compared with the answer returned by the dialog box.</param>
  /// <returns>A Boolean result of comparison of the dialog box answer and <tt>resultType</tt>.</returns>
  public bool AskExtFullyValid(DialogAnswerType resultType, bool reset = true)
  {
    return this.AskExtWithValidation((PXView.InitializePanel) ((g, c) => { }), new PXFilter<Table>.Validator(this.VerifyFullyValid), resultType, reset);
  }

  /// <exclude />
  public virtual bool HasError<Field>() where Field : IBqlField
  {
    return this.HasError(typeof (Field).Name);
  }

  /// <exclude />
  public virtual bool HasError(string fieldName)
  {
    return !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly(this.Cache, this.Cache.Current, fieldName));
  }

  /// <exclude />
  public virtual bool VerifyRequired(bool suppressError)
  {
    this.Cache.RaiseRowSelected(this.Cache.Current);
    bool flag1 = true;
    PXRowPersistingEventArgs e = new PXRowPersistingEventArgs(PXDBOperation.Insert, this.Cache.Current);
    foreach (string field in (List<string>) this.Cache.Fields)
    {
      if (this.HasError(field))
        return false;
      foreach (PXDefaultAttribute defaultAttribute in this.Cache.GetAttributesReadonly(this.Cache.Current, field).OfType<PXDefaultAttribute>().Where<PXDefaultAttribute>((Func<PXDefaultAttribute, bool>) (defaultAttribute => defaultAttribute.PersistingCheck != PXPersistingCheck.Nothing)))
      {
        defaultAttribute.RowPersisting(this.Cache, e);
        bool flag2 = this.HasError(field);
        if (flag2)
          flag1 = false;
        if (suppressError & flag2)
        {
          this.Cache.RaiseExceptionHandling(field, this.Cache.Current, (object) null, (Exception) null);
          return false;
        }
      }
    }
    return flag1;
  }

  /// <exclude />
  public virtual bool VerifyFullyValid() => this.VerifyFullyValid(false);

  /// <exclude />
  public virtual bool VerifyFullyValid(bool suppressError) => this.VerifyRequired(suppressError);

  private object getFilter()
  {
    PXCache cach = this._Graph.Caches[typeof (Table)];
    if (!this._inserting)
    {
      try
      {
        this._inserting = true;
        if ((object) this.current == null)
        {
          this.current = (Table) (cach.Insert() ?? cach.Locate(cach.CreateInstance()));
          cach.IsDirty = false;
          cach.Version = 0;
        }
        else if (cach.Locate((object) this.current) == null)
        {
          try
          {
            this.current = (Table) cach.Insert((object) this.current);
          }
          catch
          {
            cach.SetStatus((object) this.current, PXEntryStatus.Inserted);
          }
          cach.IsDirty = false;
          cach.Version = 0;
        }
      }
      finally
      {
        this._inserting = false;
      }
    }
    return (object) this.current;
  }

  private static void persisting(PXCache sender, PXRowPersistingEventArgs e) => e.Cancel = true;

  private void ResetState() => this.current = default (Table);

  protected delegate bool Validator() where Table : class, IBqlTable, new();

  /// <exclude />
  private class swfilterview(PXGraph graph, Delegate handler) : PXView(graph, false, (BqlCommand) new PX.Data.Select<Table>(), handler)
  {
    public override List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      List<object> objectList = base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
      if (objectList.Count > 0 && objectList[0] is Table && this.Cache.Locate(objectList[0]) == null && this.Cache.AllowInsert)
      {
        this.Cache.Insert(objectList[0]);
        this.Cache.IsDirty = false;
        this.Cache.Version = 0;
      }
      return objectList;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImportAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

#nullable enable
namespace PX.Data;

/// <summary>Adds the table toolbar button that allows the user to load data from a file to the table. The attribute is placed on the view that is used to obtain the data
/// for the table.</summary>
/// <remarks>
///   <para>The attribute is placed on the view declaration in the graph. As a result, the table that uses the view as a data provider will include the button that
/// opens the data import wizard. By using this wizard, a user can load data from an Excel or CSV file to the table.</para>
///   <para>You can control all steps of the data import if you make the graph implement the following interfaces: <see cref="T:PX.Data.PXImportAttribute.IPrepare" />, <see cref="T:PX.Data.PXImportAttribute.IImport" /> and <see cref="T:PX.Data.PXImportAttribute.IConfirm" />.</para>
/// </remarks>
/// <example>
/// <para>The following attribute adds the upload button to the toolbar of the table that uses the Transactions view for data retrieval. In this example, the primary view DAC is INRegister, and this DAC is passed to the attribute as a parameter. </para>
/// <code lang="CS">
/// // Primary view declaration
/// public PXSelect&lt;INRegister,
///   Where&lt;INRegister.docType, Equal&lt;INDocType.adjustment&gt;&gt;&gt; adjustment;
///  ...
/// 
/// [PXImport(typeof(INRegister))]
/// public PXSelect&lt;INTran,
///   Where&lt;INTran.docType, Equal&lt;Current&lt;INRegister.docType&gt;&gt;,
///     And&lt;INTran.refNbr, Equal&lt;Current&lt;INRegister.refNbr&gt;&gt;&gt;&gt;&gt;
///     Transactions;
/// </code>
/// </example>
/// <example>
/// <para>In the following example, the graph implements the PXImportAttribute.IPrepare interface to control the data import.</para>
/// <code lang="CS">
/// public class APInvoiceEntry : APDataEntryGraph&lt;APInvoiceEntry, APInvoice&gt;, PXImportAttribute.IPrepare
/// {
///     ...
///     // The attribute is placed on the view declaration
///     [PXImport(typeof(APInvoice))]
///     public PXSelectJoin&lt;APTran,
///         LeftJoin&lt;POReceiptLine,
///             On&lt;POReceiptLine.receiptNbr, Equal&lt;APTran.receiptNbr&gt;,
///             And&lt;POReceiptLine.lineNbr, Equal&lt;APTran.receiptLineNbr&gt;&gt;&gt;&gt;,
///         Where&lt;APTran.tranType, Equal&lt;Current&lt;APInvoice.docType&gt;&gt;,
///             And&lt;APTran.refNbr, Equal&lt;Current&lt;APInvoice.refNbr&gt;&gt;&gt;&gt;,
///         OrderBy&lt;Asc&lt;APTran.tranType,
///                 Asc&lt;APTran.refNbr, Asc&lt;APTran.lineNbr&gt;&gt;&gt;&gt;&gt;
///         Transactions;
///     ...
/// 
///     // Implementation of the PXImportAttribute.IPrepare methods
///     public virtual bool PrepareImportRow(
///         string viewName, IDictionary keys, IDictionary values)
///     {
///         if (string.Compare(viewName, nameof(Transactions), true) == 0)
///         {
///             if (values.Contains(nameof(APTran.tranType))) values[nameof(APTran.tranType)] =
///                 Document.Current.DocType;
///             else values.Add(nameof(APTran.tranType), Document.Current.DocType);
///             if (values.Contains(nameof(APTran.tranType))) values[nameof(APTran.refNbr)] =
///                 Document.Current.RefNbr;
///             else values.Add(nameof(APTran.refNbr), Document.Current.RefNbr);
///         }
///         return true;
///     }
///     ...
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PXImportAttribute : PXViewExtensionAttribute
{
  private const 
  #nullable disable
  string _IMPORT_MESSAGE = "Import";
  /// <exclude />
  public const string _RUNWIZARD_ACTION_NAME = "$ImportWizardAction";
  /// <exclude />
  public const string _IMPORT_ACTION_NAME = "$ImportAction";
  /// <exclude />
  public const string _UPLOADFILE_ACTION_NAME = "$UploadFileAction";
  /// <exclude />
  public const string ImportCSVSettingsName = "$ImportCSVSettings";
  /// <exclude />
  public const string ImportXLSXSettingsName = "$ImportXLSXSettings";
  /// <exclude />
  public const string ImportColumnsSettingsName = "$ImportColumns";
  /// <exclude />
  public const string ImportContentBagName = "$ImportContentBag";
  /// <exclude />
  public const string _DONT_UPDATE_EXIST_RECORDS = "_DONT_UPDATE_EXIST_RECORDS";
  private const string _LOADED_FIELD_NAME = "Loaded";
  private const string _DATA_FIELD_NAME = "Data";
  private const string _FILEEXTENSION_FIELD_NAME = "FileExtension";
  protected System.Type _table;
  protected PXImportAttribute.IPXImportWizard _importer;
  protected PXCache _itemsCache;
  protected string _itemsViewName;
  protected PXView _importContentBag;
  /// <exclude />
  public static readonly string ImportFlag = Guid.NewGuid().ToString();

  /// <exclude />
  public event EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs> MappingPropertiesInit
  {
    add
    {
      if (this._importer == null)
        return;
      this._importer.MappingPropertiesInit += value;
    }
    remove
    {
      if (this._importer == null)
        return;
      this._importer.MappingPropertiesInit -= value;
    }
  }

  public event EventHandler<PXImportAttribute.CommonSettingsDialogShowingEventArgs> CommonSettingsDialogShowing
  {
    add
    {
      if (this._importer == null)
        return;
      this._importer.CommonSettingsDialogShowing += value;
    }
    remove
    {
      if (this._importer == null)
        return;
      this._importer.CommonSettingsDialogShowing -= value;
    }
  }

  public event EventHandler<PXImportAttribute.MappingDialogShowingEventArgs> MappingDialogShowing
  {
    add
    {
      if (this._importer == null)
        return;
      this._importer.MappingDialogShowing += value;
    }
    remove
    {
      if (this._importer == null)
        return;
      this._importer.MappingDialogShowing -= value;
    }
  }

  public event EventHandler<PXImportAttribute.FileUploadingEventArgs> FileUploading
  {
    add
    {
      if (this._importer == null)
        return;
      this._importer.FileUploading += value;
    }
    remove
    {
      if (this._importer == null)
        return;
      this._importer.FileUploading -= value;
    }
  }

  public event EventHandler<PXImportAttribute.RowImportingEventArgs> RowImporting
  {
    add
    {
      if (this._importer == null)
        return;
      this._importer.RowImporting += value;
    }
    remove
    {
      if (this._importer == null)
        return;
      this._importer.RowImporting -= value;
    }
  }

  /// <summary>Initializes a new instance of the attribute. The primary view of the graph will be detected based on PXGraph.PrimaryView.</summary>
  public PXImportAttribute() => this._table = (System.Type) null;

  /// <summary>Initializes a new instance of the attribute. The parameter sets the primary view DAC.</summary>
  /// <param name="primaryTable">The first DAC that is referenced by the primary view of the graph where the current view is declared.</param>
  public PXImportAttribute(System.Type primaryTable) => this._table = primaryTable;

  /// <summary>Initializes a new instance of the attribute. The first
  /// parameter is the primary table of the view the attribute is attached
  /// to.</summary>
  /// <param name="primaryTable">The first table that is referenced in the
  /// view (primary table).</param>
  /// <param name="importer">An object that implements the <tt>IPXImportWizard</tt> interface.</param>
  public PXImportAttribute(System.Type primaryTable, PXImportAttribute.IPXImportWizard importer)
    : this(primaryTable)
  {
    this._importer = importer;
  }

  /// <exclude />
  public override void ViewCreated(PXGraph graph, string viewName)
  {
    System.Type type = this._table;
    if ((object) type == null)
      type = graph.Views[graph.PrimaryView].GetItemType();
    System.Type table = type;
    PXView view = graph.Views[viewName];
    this._itemsCache = this._itemsCache ?? graph.Views[viewName].Cache;
    this._itemsViewName = this._itemsViewName ?? viewName;
    graph.Views.GetExternalMember(view).Attributes.Add((Attribute) this);
    PXImportAttribute.AddAction(PXImportAttribute.GetImportActionName(viewName), table, graph, new PXButtonDelegate(this.Import));
    PXImportAttribute.AddAction(PXImportAttribute.GetRunWizardActionName(viewName), table, graph, new PXButtonDelegate(this.ImportWizard));
    PXImportAttribute.AddAction(PXImportAttribute.GetUploadFileActionName(viewName), table, graph, new PXButtonDelegate(this.UploadFile));
    PXView contentBagView = this.GetOrCreateContentBagView();
    PXImportAttribute.IPXImportWizard importer = this.GetOrCreateImporter();
    if (importer == null)
      return;
    PXImportAttribute.TryUploadData(importer, contentBagView);
  }

  /// <summary>Enables or disables the control that the attribute adds to the table.</summary>
  /// <param name="graph">The graph where the view marked with the attribute
  /// is defined.</param>
  /// <param name="viewName">The name of the view that is marked with the
  /// attribute.</param>
  /// <param name="isEnabled">The value that indicates whether the method
  /// enables or disables the control.</param>
  public static void SetEnabled(PXGraph graph, string viewName, bool isEnabled)
  {
    if (graph == null)
      throw new PXArgumentException(nameof (graph));
    string name = !string.IsNullOrEmpty(viewName) ? PXImportAttribute.GetRunWizardActionName(viewName) : throw new PXArgumentException(nameof (viewName));
    graph.Actions[name]?.SetEnabled(isEnabled);
    string importActionName = PXImportAttribute.GetImportActionName(viewName);
    graph.Actions[importActionName]?.SetEnabled(isEnabled);
  }

  public static bool GetEnabled(PXGraph graph, string viewName)
  {
    if (graph == null)
      throw new PXArgumentException(nameof (graph));
    string name = !string.IsNullOrEmpty(viewName) ? PXImportAttribute.GetImportActionName(viewName) : throw new PXArgumentException(nameof (viewName));
    PXAction action = graph.Actions[name];
    return action != null && action.GetEnabled();
  }

  private static string GetImportActionName(string viewName) => viewName + "$ImportAction";

  private static string GetRunWizardActionName(string viewName) => viewName + "$ImportWizardAction";

  private static string GetUploadFileActionName(string viewName) => viewName + "$UploadFileAction";

  /// <exclude />
  public bool RollbackPreviousImport { get; set; }

  protected PXView GetOrCreateContentBagView()
  {
    if (this._importContentBag == null && this._itemsCache != null && !string.IsNullOrEmpty(this._itemsViewName))
      this._importContentBag = PXImportAttribute.AddView(this._itemsCache.Graph, this._itemsViewName + "$ImportContentBag", typeof (PXImportAttribute.PXContentBag), (System.Action) (() => this._itemsCache.Graph.RowUpdated.AddHandler<PXImportAttribute.PXContentBag>((PXRowUpdated) ((sender, args) =>
      {
        PXImportAttribute.IPXImportWizard importer = this.GetOrCreateImporter();
        if (importer != null && this._importContentBag != null && !PXImportAttribute.TryUploadData(importer, this._importContentBag))
          throw new PXDialogRequiredException(this._itemsViewName, (object) null, "Validation failed", "The file, which is being uploaded, contains incorrect data.", MessageButtons.OK, MessageIcon.Error);
      }))));
    return this._importContentBag;
  }

  protected PXImportAttribute.IPXImportWizard GetOrCreateImporter()
  {
    if (this._importer == null)
    {
      PXView contentBagView = this.GetOrCreateContentBagView();
      if (contentBagView != null)
      {
        PXImportAttribute.PXContentBag current = contentBagView.Cache.Current as PXImportAttribute.PXContentBag;
        object[] parameters = new object[4]
        {
          current == null ? (object) (string) null : (object) current.FileExtension,
          (object) this._itemsCache,
          (object) this._itemsViewName,
          (object) this.RollbackPreviousImport
        };
        this._importer = PXImportAttribute.MakeGenericType(typeof (PXImportAttribute.PXImporter<>), this._itemsCache.GetItemType()).GetMethod("Create").Invoke((object) null, parameters) as PXImportAttribute.IPXImportWizard;
        if (this._importer != null)
        {
          this._importer.OnCreateImportRow += (PXImportAttribute.CreateImportRowEventHandler) ((args, prepareItemsHandler) =>
          {
            using (PXExecutionContext.Scope.Instantiate(new PXExecutionContext()))
            {
              PXExecutionContext.Current.Bag["_DONT_UPDATE_EXIST_RECORDS"] = (object) (bool) (args.Mode == PXImportAttribute.ImportMode.Value.BypassExisting ? 1 : (args.Mode == PXImportAttribute.ImportMode.Value.InsertAllRecords ? 1 : 0));
              args.Cancel = !this._importer_OnImportRow(args.Keys, args.Values, prepareItemsHandler);
            }
          });
          this._importer.OnRowImporting += (PXImportAttribute.RowImportingEventHandler) ((args, prepareItemsHandler) => args.Cancel = !this._importer_OnRowImporting(args.Row, prepareItemsHandler));
          this._importer.OnRowImported += (PXImportAttribute.RowImportedEventHandler) ((args, prepareItemsHandler) => args.Cancel = !this._importer_OnRowImported(args.Row, args.OldRow, prepareItemsHandler));
          this._importer.OnImportDone += (PXImportAttribute.ImportDoneEventHandler) ((args, importProcess) => this._importer_OnImportDone(args.Mode, importProcess));
        }
      }
    }
    return this._importer;
  }

  protected static PXAction AddAction(
    string name,
    string viewName,
    System.Type table,
    PXGraph graph,
    PXButtonDelegate handler)
  {
    return PXImportAttribute.AddAction(viewName + name, table, graph, handler);
  }

  protected static PXAction AddAction(
    string actionName,
    System.Type table,
    PXGraph graph,
    PXButtonDelegate handler)
  {
    PXAction instance = (PXAction) Activator.CreateInstance(PXImportAttribute.MakeGenericType(typeof (PXNamedAction<>), table), (object) graph, (object) actionName, (object) handler);
    instance.SetVisible(false);
    graph.Actions[actionName] = instance;
    return instance;
  }

  protected static PXView AddView(
    PXGraph graph,
    string viewName,
    System.Type itemType,
    System.Action afterCreated = null)
  {
    return PXImportAttribute.AddView(graph, viewName, itemType, (System.Type) null, (System.Type) null, afterCreated);
  }

  protected static PXView AddView(
    PXGraph graph,
    string viewName,
    System.Type itemType,
    System.Type whereType,
    System.Type orderType,
    System.Action afterCreated = null)
  {
    if (graph.Views.ContainsKey(viewName))
      return graph.Views[viewName];
    System.Type type = PXImportAttribute.MakeGenericType(typeof (PXNotCleanableCache<>), itemType);
    if (!graph.Caches.ContainsKey(itemType) || !type.IsAssignableFrom(graph.Caches[itemType].GetType()))
    {
      PXCache instance = (PXCache) Activator.CreateInstance(type, (object) graph);
      instance.Load();
      graph.Caches[itemType] = instance;
      graph.SynchronizeByItemType(instance);
    }
    PXImportAttribute.PXSelectInsertedHandler selectInsertedHandler = new PXImportAttribute.PXSelectInsertedHandler();
    BqlCommand select = BqlCommand.CreateInstance(typeof (Select<>), itemType);
    if (whereType != (System.Type) null)
      select = select.WhereNew(whereType);
    if (orderType != (System.Type) null)
      select = select.OrderByNew(orderType);
    selectInsertedHandler.View = new PXView(graph, false, select, (Delegate) new PXSelectDelegate(selectInsertedHandler.Select));
    graph.Views.Add(viewName, selectInsertedHandler.View);
    graph.RowUpdated.AddHandler(itemType, new PXRowUpdated(PXImportAttribute.ClearDirtyOnRowUpdated));
    graph.RowInserted.AddHandler(itemType, new PXRowInserted(PXImportAttribute.ClearDirtyOnRowInserted));
    if (afterCreated != null)
      afterCreated();
    return selectInsertedHandler.View;
  }

  private static bool TryUploadData(PXImportAttribute.IPXImportWizard importer, PXView contentBag)
  {
    return contentBag.Cache.Current is PXImportAttribute.PXContentBag current && current.Loaded.HasValue && current.Loaded.Value && importer.TryUploadData((byte[]) current.Data, current.FileExtension);
  }

  private static void ClearDirtyOnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.IsDirty = false;
  }

  private static void ClearDirtyOnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    sender.IsDirty = false;
  }

  private bool _importer_OnImportRow(
    IDictionary keys,
    IDictionary values,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler)
  {
    return prepareItemsHandler.PrepareImportRow(this._itemsViewName, keys, values);
  }

  private bool _importer_OnRowImporting(
    object row,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler)
  {
    return prepareItemsHandler.RowImporting(this._itemsViewName, row);
  }

  private bool _importer_OnRowImported(
    object row,
    object oldRow,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler)
  {
    return prepareItemsHandler.RowImported(this._itemsViewName, row, oldRow);
  }

  private void _importer_OnImportDone(
    PXImportAttribute.ImportMode.Value mode,
    PXImportAttribute.IPXProcess importProcess)
  {
    importProcess.ImportDone(mode);
  }

  [PXUIField(DisplayName = "Import", Visible = false)]
  [PXButton(CommitChanges = true)]
  private IEnumerable UploadFile(PXAdapter adapter)
  {
    if (adapter.Arguments.Count > 0)
    {
      PXView contentBagView = this.GetOrCreateContentBagView();
      if (contentBagView != null)
      {
        string str1 = adapter.Arguments.Keys.FirstOrDefault<string>();
        string str2 = Path.GetExtension(str1).ToLower().TrimStart('.');
        object obj = adapter.Arguments[str1];
        PXCache cache = contentBagView.Cache;
        if (cache.Current == null)
        {
          cache.Current = cache.Insert();
          cache.IsDirty = false;
        }
        cache.SetValueExt(cache.Current, "Loaded", (object) false);
        cache.SetValueExt(cache.Current, "Data", obj);
        cache.SetValueExt(cache.Current, "FileExtension", (object) str2);
        cache.SetValueExt(cache.Current, "Loaded", (object) true);
        cache.Update(cache.Current);
        PXImportAttribute.IPXImportWizard importer = this.GetOrCreateImporter();
        if (importer != null)
        {
          PXImportAttribute.TryUploadData(importer, contentBagView);
          this._itemsCache.Graph.Unload();
        }
      }
    }
    return (IEnumerable) new List<object>();
  }

  [PXUIField(Visible = false)]
  [PXButton(CommitChanges = true)]
  private IEnumerable ImportWizard(PXAdapter adapter)
  {
    PXImportAttribute.IPXImportWizard importer = this.GetOrCreateImporter();
    if (importer != null)
    {
      importer.PreRunWizard();
      importer.RunWizard();
      this._itemsCache.Graph.Views[this._itemsViewName].RequestRefresh();
      adapter.View.Graph.Views[this._itemsViewName] = (PXView) PXImportAttribute.viewErrorInterceptor.FromView(adapter.View.Graph.Views[this._itemsViewName]);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Import", Enabled = true)]
  [PXButton(ImageKey = "Process", CommitChanges = true)]
  private IEnumerable Import(PXAdapter adapter)
  {
    PXImportAttribute.CompositePrepareItems.CreateFor(this._itemsCache.Graph)?.PrepareItems(this._itemsViewName, this.GetImportedItems());
    this.SafetyPersistInserted(this._itemsCache, this.GetImportedItems());
    return adapter.Get();
  }

  private IEnumerable GetImportedItems()
  {
    foreach (object importedItem in this._itemsCache.Inserted)
    {
      if (importedItem is IPXSelectable pxSelectable)
      {
        bool? selected = pxSelectable.Selected;
        bool flag = true;
        if (!(selected.GetValueOrDefault() == flag & selected.HasValue))
          continue;
      }
      yield return importedItem;
    }
  }

  private void SafetyPersistInserted(PXCache cache, IEnumerable items)
  {
    bool isAborted = false;
    PXTransactionScope transactionScope = (PXTransactionScope) null;
    List<object> objectList = new List<object>();
    try
    {
      transactionScope = new PXTransactionScope();
      foreach (object row in items)
      {
        cache.PersistInserted(row);
        objectList.Add(row);
      }
      transactionScope.Complete(cache.Graph);
    }
    catch (Exception ex)
    {
      isAborted = true;
      throw;
    }
    finally
    {
      transactionScope?.Dispose();
      cache.Normalize();
      cache.Persisted(isAborted);
      cache.Graph.Views[this._itemsViewName].Clear();
    }
  }

  private static System.Type MakeGenericType(params System.Type[] types)
  {
    int index = 0;
    return PXImportAttribute.MakeGenericType(types, ref index);
  }

  private static System.Type MakeGenericType(System.Type[] types, ref int index)
  {
    if (types == null)
      throw new ArgumentNullException(nameof (types));
    if (types.Length == 0)
      throw new ArgumentException("The types list is empty.");
    if (index >= types.Length)
      throw new ArgumentOutOfRangeException(nameof (types), "The types list is not correct.");
    System.Type type = types[index];
    ++index;
    if (!type.IsGenericTypeDefinition)
      return type;
    System.Type[] typeArray = new System.Type[type.GetGenericArguments().Length];
    for (int index1 = 0; index1 < typeArray.Length; ++index1)
      typeArray[index1] = PXImportAttribute.MakeGenericType(types, ref index);
    return type.MakeGenericType(typeArray);
  }

  /// <exclude />
  public class ImportMode
  {
    public const string UPDATE_EXISTING = "U";
    public const string BYPASS_EXISTING = "B";
    public const string INSERT_ALL_RECORDS = "I";
    private static readonly Dictionary<string, PXImportAttribute.ImportMode.Value> _mapping = new Dictionary<string, PXImportAttribute.ImportMode.Value>()
    {
      {
        "U",
        PXImportAttribute.ImportMode.Value.UpdateExisting
      },
      {
        "B",
        PXImportAttribute.ImportMode.Value.BypassExisting
      },
      {
        "I",
        PXImportAttribute.ImportMode.Value.InsertAllRecords
      }
    };

    public static PXImportAttribute.ImportMode.Value Parse(string value)
    {
      PXImportAttribute.ImportMode.Value obj;
      if (PXImportAttribute.ImportMode._mapping.TryGetValue(value, out obj))
        return obj;
      throw new PXException("Import mode cannot be parsed. The string value is invalid.");
    }

    /// <exclude />
    public enum Value
    {
      UpdateExisting,
      BypassExisting,
      InsertAllRecords,
    }
  }

  /// <exclude />
  public sealed class MappingPropertiesInitEventArgs : EventArgs
  {
    public List<string> Names { get; set; }

    public List<string> DisplayNames { get; set; }

    public MappingPropertiesInitEventArgs(List<string> names, List<string> displayNames)
    {
      this.Names = names;
      this.DisplayNames = displayNames;
    }
  }

  /// <exclude />
  public sealed class CommonSettingsDialogShowingEventArgs : CancelEventArgs
  {
    public PXImportAttribute.PXImportSettings Settings { get; set; }

    public CommonSettingsDialogShowingEventArgs(PXImportAttribute.PXImportSettings settings)
    {
      this.Settings = settings;
    }
  }

  /// <exclude />
  public sealed class MappingDialogShowingEventArgs : CancelEventArgs
  {
    public List<PXImportAttribute.PXImportColumnsSettings> Settings { get; set; }

    public MappingDialogShowingEventArgs(
      List<PXImportAttribute.PXImportColumnsSettings> settings)
    {
      this.Settings = settings;
    }
  }

  /// <exclude />
  public sealed class FileUploadingEventArgs : CancelEventArgs
  {
    public byte[] Data { get; set; }

    public string FileExtension { get; set; }

    public FileUploadingEventArgs(byte[] data, string fileExtension)
    {
      this.Data = data;
      this.FileExtension = fileExtension;
    }
  }

  /// <exclude />
  public sealed class RowImportingEventArgs : CancelEventArgs
  {
    public IDictionary Keys { get; set; }

    public IDictionary Values { get; set; }

    public PXImportAttribute.ImportMode.Value Mode { get; private set; }

    public RowImportingEventArgs(
      IDictionary keys,
      IDictionary values,
      PXImportAttribute.ImportMode.Value mode)
    {
      this.Keys = keys;
      this.Values = values;
      this.Mode = mode;
    }
  }

  /// <exclude />
  [Serializable]
  public class PXContentBag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _FileExtension;
    protected object _Data;
    protected bool? _Loaded;

    [PXString]
    public virtual string FileExtension
    {
      get => this._FileExtension;
      set => this._FileExtension = value;
    }

    [PXDBVariant]
    public virtual object Data
    {
      get => this._Data;
      set => this._Data = value;
    }

    [PXBool]
    [PXUIField(Visible = false)]
    public virtual bool? Loaded
    {
      get => this._Loaded;
      set => this._Loaded = value;
    }

    /// <exclude />
    public abstract class fileExtension : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXContentBag.fileExtension>
    {
    }

    /// <exclude />
    public abstract class data : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXImportAttribute.PXContentBag.data>
    {
    }

    /// <exclude />
    public abstract class loaded : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXImportAttribute.PXContentBag.loaded>
    {
    }
  }

  /// <exclude />
  [Serializable]
  public class PXImportSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private int? _Index;
    protected string _ViewName;
    protected string _FileExtension;
    protected object _Data;
    protected string _NullValue;
    protected int? _Culture;

    [PXDBIdentity(IsKey = true)]
    [PXUIField(Visible = false)]
    public int? Index
    {
      get => this._Index;
      set => this._Index = value;
    }

    [PXString]
    [PXUIField(Visible = false)]
    public virtual string ViewName
    {
      get => this._ViewName;
      set => this._ViewName = value;
    }

    [PXString]
    [PXUIField(DisplayName = "File Extension", Visible = false)]
    public virtual string FileExtension
    {
      get => this._FileExtension;
      set => this._FileExtension = value;
    }

    [PXDBVariant]
    [PXUIField(Visible = false)]
    public virtual object Data
    {
      get => this._Data;
      set => this._Data = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Null Value")]
    public virtual string NullValue
    {
      get => this._NullValue;
      set => this._NullValue = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Culture")]
    public int? Culture
    {
      get => this._Culture;
      set => this._Culture = value;
    }

    [PXString]
    [PXStringList(new string[] {"U", "B", "I"}, new string[] {"Update Existing", "Bypass Existing", "Insert All Records"})]
    [PXUIField(DisplayName = "Mode")]
    public virtual string Mode { get; set; }

    /// <exclude />
    public abstract class index : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.index>
    {
    }

    /// <exclude />
    public abstract class viewName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.viewName>
    {
    }

    /// <exclude />
    public abstract class fileExtension : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.fileExtension>
    {
    }

    /// <exclude />
    public abstract class data : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.data>
    {
    }

    /// <exclude />
    public abstract class nullValue : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.nullValue>
    {
    }

    /// <exclude />
    public abstract class culture : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.culture>
    {
    }

    /// <exclude />
    public abstract class mode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportSettings.mode>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [PXVirtual]
  [Serializable]
  public sealed class CSVSettings : PXImportAttribute.PXImportSettings
  {
    private string _Separator;
    private int? _CodePage;

    [PXString(50)]
    [PXUIField(DisplayName = "Separator Chars")]
    public string Separator
    {
      get => this._Separator;
      set => this._Separator = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Encoding")]
    public int? CodePage
    {
      get => this._CodePage;
      set => this._CodePage = value;
    }

    /// <exclude />
    public new abstract class viewName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.CSVSettings.viewName>
    {
    }

    /// <exclude />
    public abstract class separator : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.CSVSettings.separator>
    {
    }

    /// <exclude />
    public abstract class codePage : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXImportAttribute.CSVSettings.codePage>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [PXVirtual]
  [Serializable]
  public sealed class XLSXSettings : PXImportAttribute.PXImportSettings
  {
    /// <exclude />
    public new abstract class viewName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.XLSXSettings.viewName>
    {
    }
  }

  /// <exclude />
  [PXVirtual]
  [Serializable]
  public class PXImportColumnsSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private int? _Index;
    protected string _ViewName;
    private int? _ColumnIndex;
    private string _ColumnName;
    private string _PropertyName;

    [PXDBIdentity(IsKey = true)]
    [PXUIField(Visible = false)]
    public int? Index
    {
      get => this._Index;
      set => this._Index = value;
    }

    [PXString]
    [PXUIField(Visible = false)]
    public virtual string ViewName
    {
      get => this._ViewName;
      set => this._ViewName = value;
    }

    [PXDBIdentity]
    [PXUIField(Visible = false)]
    public int? ColumnIndex
    {
      get => this._ColumnIndex;
      set => this._ColumnIndex = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Column Name", Enabled = false)]
    public string ColumnName
    {
      get => this._ColumnName;
      set => this._ColumnName = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Property Name")]
    public string PropertyName
    {
      get => this._PropertyName;
      set => this._PropertyName = value;
    }

    /// <exclude />
    public abstract class index : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXImportAttribute.PXImportColumnsSettings.index>
    {
    }

    /// <exclude />
    public abstract class viewName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportColumnsSettings.viewName>
    {
    }

    /// <exclude />
    public abstract class columnIndex : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXImportAttribute.PXImportColumnsSettings.columnIndex>
    {
    }

    /// <exclude />
    public abstract class columnName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportColumnsSettings.columnName>
    {
    }

    /// <exclude />
    public abstract class propertyName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXImportAttribute.PXImportColumnsSettings.propertyName>
    {
    }
  }

  /// <exclude />
  public struct CommonSettings(byte[] content, string nullValue, string culture, string mode)
  {
    private readonly byte[] _content = content;
    private readonly string _nullValue = nullValue;
    private readonly string _culture = culture;
    private readonly string _mode = mode;

    public byte[] Content => this._content;

    public string NullValue => this._nullValue;

    public string Culture => this._culture;

    public string Mode => this._mode;
  }

  /// <exclude />
  public interface IPXImportWizard
  {
    bool TryUploadData(byte[] content, string ext);

    void RunWizard();

    void PreRunWizard();

    event PXImportAttribute.CreateImportRowEventHandler OnCreateImportRow;

    event PXImportAttribute.RowImportingEventHandler OnRowImporting;

    event PXImportAttribute.RowImportedEventHandler OnRowImported;

    event PXImportAttribute.ImportDoneEventHandler OnImportDone;

    event EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs> MappingPropertiesInit;

    event EventHandler<PXImportAttribute.CommonSettingsDialogShowingEventArgs> CommonSettingsDialogShowing;

    event EventHandler<PXImportAttribute.MappingDialogShowingEventArgs> MappingDialogShowing;

    event EventHandler<PXImportAttribute.FileUploadingEventArgs> FileUploading;

    event EventHandler<PXImportAttribute.RowImportingEventArgs> RowImporting;
  }

  /// <exclude />
  public sealed class CreateImportRowEventArguments
  {
    private readonly IDictionary _keys;
    private readonly IDictionary _values;
    private readonly PXImportAttribute.ImportMode.Value _mode;

    public CreateImportRowEventArguments(IDictionary keys, IDictionary values, string mode)
    {
      if (keys == null)
        throw new ArgumentNullException(nameof (keys));
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this._keys = keys;
      this._values = values;
      this._mode = PXImportAttribute.ImportMode.Parse(mode);
    }

    public IDictionary Keys => this._keys;

    public IDictionary Values => this._values;

    public bool Cancel { get; set; }

    public PXImportAttribute.ImportMode.Value Mode => this._mode;
  }

  /// <exclude />
  public sealed class RowImportingEventArguments
  {
    private readonly object _row;

    public RowImportingEventArguments(object row) => this._row = row;

    public bool Cancel { get; set; }

    public object Row => this._row;
  }

  /// <exclude />
  public sealed class RowImportedEventArguments
  {
    private readonly object _row;
    private readonly object _oldRow;

    public RowImportedEventArguments(object row, object oldRow)
    {
      this._row = row != null ? row : throw new ArgumentNullException(nameof (row));
      this._oldRow = oldRow;
    }

    public bool Cancel { get; set; }

    public object Row => this._row;

    public object OldRow => this._oldRow;
  }

  /// <exclude />
  public sealed class ImporDoneEventArguments
  {
    private readonly PXImportAttribute.ImportMode.Value _mode;

    public ImporDoneEventArguments(string mode)
    {
      this._mode = mode != null ? PXImportAttribute.ImportMode.Parse(mode) : throw new ArgumentNullException(nameof (mode));
    }

    public ImporDoneEventArguments(PXImportAttribute.ImportMode.Value mode) => this._mode = mode;

    public PXImportAttribute.ImportMode.Value Mode => this._mode;
  }

  /// <exclude />
  public delegate void CreateImportRowEventHandler(
    PXImportAttribute.CreateImportRowEventArguments args,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler);

  /// <exclude />
  public delegate void RowImportingEventHandler(
    PXImportAttribute.RowImportingEventArguments args,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler);

  /// <exclude />
  public delegate void RowImportedEventHandler(
    PXImportAttribute.RowImportedEventArguments args,
    PXImportAttribute.IPXPrepareItems prepareItemsHandler);

  /// <exclude />
  public delegate void ImportDoneEventHandler(
    PXImportAttribute.ImporDoneEventArguments args,
    PXImportAttribute.IPXProcess importProcess);

  /// <exclude />
  public sealed class CSVImporter<TBatchTable> : PXImportAttribute.PXImporter<TBatchTable> where TBatchTable : class, IBqlTable, new()
  {
    private static int[] _codePages;
    private static string[] _codePagesNames;

    static CSVImporter() => PXImportAttribute.CSVImporter<TBatchTable>.InitCodePagesInfo();

    public CSVImporter(PXCache itemsCache, string viewName, bool rollbackPreviousImport)
      : base(itemsCache, viewName, typeof (PXImportAttribute.CSVSettings), typeof (PXImportAttribute.CSVSettings.viewName), "$ImportCSVSettings", rollbackPreviousImport)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      itemsCache.Graph.FieldSelecting.AddHandler<PXImportAttribute.CSVSettings.codePage>(PXImportAttribute.CSVImporter<TBatchTable>.\u003C\u003EO.\u003C0\u003E__GetCodePages ?? (PXImportAttribute.CSVImporter<TBatchTable>.\u003C\u003EO.\u003C0\u003E__GetCodePages = new PXFieldSelecting(PXImportAttribute.CSVImporter<TBatchTable>.GetCodePages)));
    }

    private static void GetCodePages(PXCache sender, PXFieldSelectingEventArgs args)
    {
      args.ReturnState = (object) PXIntState.CreateInstance(args.ReturnState, "CodePage", new bool?(), new int?(), new int?(), new int?(), PXImportAttribute.CSVImporter<TBatchTable>._codePages, PXImportAttribute.CSVImporter<TBatchTable>._codePagesNames, typeof (int), new int?(Encoding.ASCII.CodePage));
    }

    private static void InitCodePagesInfo()
    {
      List<int> intList = new List<int>();
      List<string> stringList = new List<string>();
      List<EncodingInfo> encodingInfoList = new List<EncodingInfo>((IEnumerable<EncodingInfo>) Encoding.GetEncodings());
      encodingInfoList.Sort((Comparison<EncodingInfo>) ((x, y) => string.Compare(x.DisplayName, y.DisplayName, true)));
      foreach (EncodingInfo encodingInfo in encodingInfoList)
      {
        intList.Add(encodingInfo.CodePage);
        stringList.Add(encodingInfo.DisplayName);
      }
      PXImportAttribute.CSVImporter<TBatchTable>._codePages = intList.ToArray();
      PXImportAttribute.CSVImporter<TBatchTable>._codePagesNames = stringList.ToArray();
    }

    protected override IContentReader GetReader(byte[] content)
    {
      PXImportAttribute.CSVSettings importSettingsCurrent = (PXImportAttribute.CSVSettings) this.ImportSettingsCurrent;
      return (IContentReader) new CSVReader(content, importSettingsCurrent.CodePage ?? Encoding.ASCII.CodePage)
      {
        Separator = importSettingsCurrent.Separator
      };
    }

    protected override PXImportAttribute.PXImportSettings CreateDefaultCommonSettings()
    {
      return (PXImportAttribute.PXImportSettings) new PXImportAttribute.CSVSettings()
      {
        Separator = ",",
        CodePage = new int?(Encoding.ASCII.CodePage)
      };
    }
  }

  /// <exclude />
  public sealed class XLSXImporter<TBatchTable>(
    PXCache itemsCache,
    string viewName,
    bool rollbackPreviousImport) : PXImportAttribute.PXImporter<TBatchTable>(itemsCache, viewName, typeof (PXImportAttribute.XLSXSettings), typeof (PXImportAttribute.XLSXSettings.viewName), "$ImportXLSXSettings", rollbackPreviousImport)
    where TBatchTable : class, IBqlTable, new()
  {
    protected override IContentReader GetReader(byte[] content)
    {
      return (IContentReader) new XLSXReader(content);
    }

    protected override PXImportAttribute.PXImportSettings CreateDefaultCommonSettings()
    {
      PXImportAttribute.XLSXSettings defaultCommonSettings = new PXImportAttribute.XLSXSettings();
      defaultCommonSettings.Culture = new int?(CultureInfo.CurrentCulture.LCID);
      return (PXImportAttribute.PXImportSettings) defaultCommonSettings;
    }
  }

  /// <exclude />
  public abstract class PXImporter<TBatchTable> : PXImportAttribute.IPXImportWizard where TBatchTable : class, IBqlTable, new()
  {
    private const string _CSV_EXT_NAME = "csv";
    private const string _XLSX_EXT_NAME = "xlsx";
    private const string _ERRORS_MESSAGE = "Import errors";
    private static readonly bool _isCRSelectable = typeof (IPXSelectable).IsAssignableFrom(typeof (TBatchTable));
    private readonly bool _rollbackPreviousOperation;
    private readonly PXCache _cache;
    private readonly PXCache _backupCache;
    private readonly PXCache _importedCache;
    private readonly PXView _importCommonSettings;
    private readonly PXView _importColumnsSettings;
    private string[] _propertiesNames;
    private string[] _propertiesDisplayNames;
    private ICollection<string> _lineProperties;
    private Dictionary<string, int> _languages = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private ICollection<string> _shortLineProperties;
    private readonly bool _suppressLongRun;
    private readonly IDictionary<System.Type, IList<Exception>> _exceptions;
    private readonly PXImportAttribute.PXImporter<TBatchTable>.PXCachedView _cachedItems;
    private readonly string _viewName;
    private static int[] _cultures;
    private static string[] _culturesNames;

    public event EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs> MappingPropertiesInit;

    public event EventHandler<PXImportAttribute.CommonSettingsDialogShowingEventArgs> CommonSettingsDialogShowing;

    public event EventHandler<PXImportAttribute.MappingDialogShowingEventArgs> MappingDialogShowing;

    public event EventHandler<PXImportAttribute.FileUploadingEventArgs> FileUploading;

    public event EventHandler<PXImportAttribute.RowImportingEventArgs> RowImporting;

    static PXImporter() => PXImportAttribute.PXImporter<TBatchTable>.InitCulturesInfo();

    protected PXImporter(
      PXCache itemsCache,
      string viewName,
      System.Type commonSettingsType,
      System.Type commonSettingsViewNameField,
      string commonSettingsViewName,
      bool rollbackPreviousImport)
    {
      PXImportAttribute.PXImporter<TBatchTable>.AssertType<TBatchTable>(itemsCache);
      PXImportAttribute.PXImporter<TBatchTable>.AssertType<PXImportAttribute.PXImportSettings>(commonSettingsType);
      this._rollbackPreviousOperation = rollbackPreviousImport;
      this._exceptions = (IDictionary<System.Type, IList<Exception>>) new Dictionary<System.Type, IList<Exception>>();
      this._cache = itemsCache;
      this._viewName = viewName;
      this._cachedItems = new PXImportAttribute.PXImporter<TBatchTable>.PXCachedView(itemsCache);
      if (this._rollbackPreviousOperation)
      {
        this._backupCache = (PXCache) new PXCache<TBatchTable>(itemsCache.Graph);
        this._importedCache = (PXCache) new PXCache<TBatchTable>(itemsCache.Graph);
      }
      this._importCommonSettings = PXImportAttribute.AddView(itemsCache.Graph, viewName + commonSettingsViewName, commonSettingsType, BqlCommand.Compose(typeof (Where<,>), commonSettingsViewNameField, typeof (Equal<>), typeof (Required<>), commonSettingsViewNameField), (System.Type) null);
      this._importColumnsSettings = PXImportAttribute.AddView(itemsCache.Graph, viewName + "$ImportColumns", typeof (PXImportAttribute.PXImportColumnsSettings), typeof (Where<PXImportAttribute.PXImportColumnsSettings.viewName, Equal<Required<PXImportAttribute.PXImportColumnsSettings.viewName>>>), typeof (OrderBy<Asc<PXImportAttribute.PXImportColumnsSettings.index>>));
      this._cache.Graph.FieldSelecting.AddHandler<PXImportAttribute.PXImportColumnsSettings.propertyName>(new PXFieldSelecting(this.GetTBatchTableProperties));
      this._cache.Graph.FieldUpdating.AddHandler<PXImportAttribute.PXImportColumnsSettings.propertyName>(new PXFieldUpdating(this.CorrectColumnAssociations));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this._cache.Graph.FieldSelecting.AddHandler(commonSettingsType, "Culture", PXImportAttribute.PXImporter<TBatchTable>.\u003C\u003EO.\u003C0\u003E__GetCultures ?? (PXImportAttribute.PXImporter<TBatchTable>.\u003C\u003EO.\u003C0\u003E__GetCultures = new PXFieldSelecting(PXImportAttribute.PXImporter<TBatchTable>.GetCultures)));
    }

    private static void GetCultures(PXCache sender, PXFieldSelectingEventArgs args)
    {
      args.ReturnState = (object) PXIntState.CreateInstance(args.ReturnState, "Culture", new bool?(), new int?(), new int?(), new int?(), PXImportAttribute.PXImporter<TBatchTable>._cultures, PXImportAttribute.PXImporter<TBatchTable>._culturesNames, typeof (int), new int?(Encoding.ASCII.CodePage));
    }

    private static void InitCulturesInfo()
    {
      List<int> intList = new List<int>();
      List<string> stringList = new List<string>();
      List<CultureInfo> cultureInfoList = new List<CultureInfo>((IEnumerable<CultureInfo>) CultureInfo.GetCultures(CultureTypes.AllCultures));
      cultureInfoList.Sort((Comparison<CultureInfo>) ((x, y) => string.Compare(x.DisplayName, y.DisplayName, true)));
      foreach (CultureInfo cultureInfo in cultureInfoList)
      {
        if (cultureInfo.LCID != 4096 /*0x1000*/)
        {
          intList.Add(cultureInfo.LCID);
          stringList.Add(cultureInfo.DisplayName);
        }
      }
      PXImportAttribute.PXImporter<TBatchTable>._cultures = intList.ToArray();
      PXImportAttribute.PXImporter<TBatchTable>._culturesNames = stringList.ToArray();
    }

    private void InitPropertiesInfo()
    {
      if (this._propertiesNames != null && this._propertiesDisplayNames != null && this._lineProperties != null && this._shortLineProperties != null)
        return;
      this.ForceInitPropertiesInfo(this._cache);
    }

    private void ForceInitPropertiesInfo(PXCache cache)
    {
      List<KeyValuePair<string, string>> keyValuePairList1 = new List<KeyValuePair<string, string>>();
      List<KeyValuePair<string, string>> collection = new List<KeyValuePair<string, string>>();
      this._lineProperties = (ICollection<string>) new List<string>(cache.Fields.Count);
      this._shortLineProperties = (ICollection<string>) new List<string>(cache.Fields.Count);
      using (List<string>.Enumerator enumerator = cache.Fields.GetEnumerator())
      {
label_17:
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          if (cache.GetStateExt((object) null, current) is PXFieldState stateExt)
          {
            if (stateExt.Visible && stateExt.Enabled && !stateExt.IsReadOnly)
            {
              List<KeyValuePair<string, string>> keyValuePairList2 = stateExt.Visibility != PXUIVisibility.Invisible ? keyValuePairList1 : collection;
              if (stateExt is PXStringState pxStringState && !string.IsNullOrEmpty(pxStringState.Language) && this._cache.GetValueExt((object) null, current + "Translations") is string[] valueExt)
              {
                int num = 0;
                for (int index = 0; index < valueExt.Length; ++index)
                {
                  string str = valueExt[index];
                  keyValuePairList2.Add(new KeyValuePair<string, string>($"{current} {str.ToUpper()}", $"{stateExt.DisplayName} {str.ToUpper()}"));
                  this._languages[str.ToUpper()] = num;
                  ++num;
                }
              }
              else
                keyValuePairList2.Add(new KeyValuePair<string, string>(current, stateExt.DisplayName));
            }
            foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(current))
            {
              if (attribute is PXLineNbrAttribute || attribute is PXLineNbrMarkerAttribute)
              {
                switch (System.Type.GetTypeCode(stateExt.DataType))
                {
                  case TypeCode.Int16:
                    this._shortLineProperties.Add(current);
                    goto label_17;
                  case TypeCode.Int32:
                    this._lineProperties.Add(current);
                    goto label_17;
                  default:
                    goto label_17;
                }
              }
            }
          }
        }
      }
      keyValuePairList1.Sort((Comparison<KeyValuePair<string, string>>) ((x, y) => string.Compare(x.Value, y.Value, true)));
      collection.Sort((Comparison<KeyValuePair<string, string>>) ((x, y) => string.Compare(x.Value, y.Value, true)));
      keyValuePairList1.AddRange((IEnumerable<KeyValuePair<string, string>>) collection);
      this._propertiesNames = new string[keyValuePairList1.Count + 1];
      this._propertiesDisplayNames = new string[keyValuePairList1.Count + 1];
      this._propertiesNames[0] = string.Empty;
      this._propertiesDisplayNames[0] = string.Empty;
      for (int index = 1; index <= keyValuePairList1.Count; ++index)
      {
        KeyValuePair<string, string> keyValuePair = keyValuePairList1[index - 1];
        this._propertiesNames[index] = keyValuePair.Key;
        this._propertiesDisplayNames[index] = keyValuePair.Value;
      }
      PXImportAttribute.MappingPropertiesInitEventArgs e = new PXImportAttribute.MappingPropertiesInitEventArgs(new List<string>((IEnumerable<string>) this._propertiesNames), new List<string>((IEnumerable<string>) this._propertiesDisplayNames));
      this.OnMappingPropertiesInit(e);
      this._propertiesNames = e.Names.ToArray();
      this._propertiesDisplayNames = e.DisplayNames.ToArray();
    }

    public static PXImportAttribute.IPXImportWizard Create(
      string fileExt,
      PXCache itemsCache,
      string viewName,
      bool rollbackPreviousImport)
    {
      switch (fileExt)
      {
        case "csv":
          return (PXImportAttribute.IPXImportWizard) new PXImportAttribute.CSVImporter<TBatchTable>(itemsCache, viewName, rollbackPreviousImport);
        case "xlsx":
          return (PXImportAttribute.IPXImportWizard) new PXImportAttribute.XLSXImporter<TBatchTable>(itemsCache, viewName, rollbackPreviousImport);
        default:
          return (PXImportAttribute.IPXImportWizard) null;
      }
    }

    public bool RollbackPreviousOperation => this._rollbackPreviousOperation;

    public bool TryUploadData(byte[] content, string ext)
    {
      if (this._cache == null || content == null || string.IsNullOrEmpty(ext))
        return false;
      PXImportAttribute.FileUploadingEventArgs e = new PXImportAttribute.FileUploadingEventArgs(content, ext);
      this.OnFileUploading(e);
      if (e.Cancel)
        return false;
      this.SetContent(e.Data, e.FileExtension);
      return true;
    }

    public void RunWizard()
    {
      if (!this.AskCommonSettings())
        return;
      this.AskColumnAssociations();
    }

    public virtual void PreRunWizard()
    {
      this._cachedItems.Cache.Graph.Views[this._viewName].SelectMulti();
    }

    public event PXImportAttribute.CreateImportRowEventHandler OnCreateImportRow;

    public event PXImportAttribute.RowImportingEventHandler OnRowImporting;

    public event PXImportAttribute.RowImportedEventHandler OnRowImported;

    public event PXImportAttribute.ImportDoneEventHandler OnImportDone;

    public IEnumerable<KeyValuePair<System.Type, IList<Exception>>> Exceptions
    {
      get => (IEnumerable<KeyValuePair<System.Type, IList<Exception>>>) this._exceptions;
    }

    protected virtual void OnMappingPropertiesInit(
      PXImportAttribute.MappingPropertiesInitEventArgs e)
    {
      if (this.MappingPropertiesInit == null)
        return;
      this.MappingPropertiesInit((object) this, e);
    }

    protected virtual void OnCommonSettingsDialogShowing(
      PXImportAttribute.CommonSettingsDialogShowingEventArgs e)
    {
      if (this.CommonSettingsDialogShowing == null)
        return;
      this.CommonSettingsDialogShowing((object) this, e);
    }

    protected virtual void OnMappingDialogShowing(PXImportAttribute.MappingDialogShowingEventArgs e)
    {
      if (this.MappingDialogShowing == null)
        return;
      this.MappingDialogShowing((object) this, e);
    }

    protected virtual void OnFileUploading(PXImportAttribute.FileUploadingEventArgs e)
    {
      if (this.FileUploading == null)
        return;
      this.FileUploading((object) this, e);
    }

    protected virtual void OnBeforeRowImporting(PXImportAttribute.RowImportingEventArgs e)
    {
      if (this.RowImporting == null)
        return;
      this.RowImporting((object) this, e);
    }

    protected bool SaftyPerformOperation(
      PXImportAttribute.PXImporter<TBatchTable>.Operation handler)
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      bool flag = true;
      try
      {
        handler();
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        this.AddException(ex);
        flag = false;
      }
      return flag;
    }

    protected abstract IContentReader GetReader(byte[] content);

    protected virtual void SetValue(TBatchTable result, string fieldName, string value)
    {
      if (this._cache.GetStateExt((object) result, fieldName) is PXStringState stateExt && !string.IsNullOrEmpty(stateExt.InputMask))
        value = Mask.Parse(stateExt.InputMask, value);
      this.SaftyPerformOperation((PXImportAttribute.PXImporter<TBatchTable>.Operation) (() => this._cache.SetValueExt((object) result, fieldName, (object) value)));
    }

    protected void AddException(Exception e)
    {
      System.Type type = e.GetType();
      if (!this._exceptions.ContainsKey(type))
        this._exceptions.Add(type, (IList<Exception>) new List<Exception>());
      this._exceptions[type].Add(e);
    }

    protected PXImportAttribute.PXImportSettings ImportSettingsCurrent
    {
      get
      {
        object importSettingsCurrent = this._importCommonSettings.SelectSingle((object) this._viewName);
        if (importSettingsCurrent != null)
          return (PXImportAttribute.PXImportSettings) importSettingsCurrent;
        this.SetDefaultCommonSettings();
        return (PXImportAttribute.PXImportSettings) this._importCommonSettings.SelectSingle((object) this._viewName);
      }
    }

    private IEnumerable<KeyValuePair<IDictionary, IDictionary>> ReadItems(
      IContentReader reader,
      PXCache cache,
      CultureInfo culture)
    {
      reader.Reset();
      Dictionary<string, PXImportAttribute.PXImporter<TBatchTable>.StateInfo> statesCache = new Dictionary<string, PXImportAttribute.PXImporter<TBatchTable>.StateInfo>();
      int index = 1;
      while (reader.MoveNext())
      {
        KeyValuePair<IDictionary, IDictionary> result;
        if (this.GetKeysAndValues(reader, index, culture, cache, (IDictionary<string, PXImportAttribute.PXImporter<TBatchTable>.StateInfo>) statesCache, out result))
        {
          yield return result;
          ++index;
        }
      }
    }

    private bool GetKeysAndValues(
      IContentReader contentReader,
      int lineNumber,
      CultureInfo culture,
      PXCache cache,
      IDictionary<string, PXImportAttribute.PXImporter<TBatchTable>.StateInfo> statesCache,
      out KeyValuePair<IDictionary, IDictionary> result)
    {
      result = new KeyValuePair<IDictionary, IDictionary>((IDictionary) new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase), (IDictionary) new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase));
      PXImportAttribute.PXImportSettings importSettingsCurrent = this.ImportSettingsCurrent;
      string strB = importSettingsCurrent == null ? (string) null : importSettingsCurrent.NullValue ?? string.Empty;
      List<string> stringList1 = new List<string>((IEnumerable<string>) this._lineProperties);
      List<string> stringList2 = new List<string>((IEnumerable<string>) this._shortLineProperties);
      bool flag = false;
      PXView importColumnsSettings1 = this._importColumnsSettings;
      object[] objArray = new object[1]
      {
        (object) this._viewName
      };
      foreach (PXImportAttribute.PXImportColumnsSettings importColumnsSettings2 in importColumnsSettings1.SelectMulti(objArray))
      {
        if (!string.IsNullOrEmpty(importColumnsSettings2.PropertyName))
        {
          string str1 = contentReader.GetValue(importColumnsSettings2.ColumnIndex.Value).TrimEnd();
          if (string.Compare(str1, strB, false) == 0)
            str1 = (string) null;
          if (str1 != null)
          {
            flag |= str1.Trim() != string.Empty;
            string str2 = importColumnsSettings2.PropertyName;
            if (!cache.Fields.Contains(str2) && this._languages.Count > 0 && str2.Length > 3 && str2[str2.Length - 3] == ' ' && this._languages.ContainsKey(str2.Substring(str2.Length - 2)))
            {
              str2 = str2.Substring(0, str2.Length - 3);
              if (!cache.Fields.Contains(str2))
                str2 = importColumnsSettings2.PropertyName;
            }
            PXImportAttribute.PXImporter<TBatchTable>.StateInfo stateInfo;
            if (!statesCache.TryGetValue(str2, out stateInfo))
            {
              if (cache.GetStateExt((object) null, str2) is PXFieldState stateExt)
                stateInfo = new PXImportAttribute.PXImporter<TBatchTable>.StateInfo(stateExt);
              statesCache.Add(str2, stateInfo);
            }
            if (stateInfo != null)
            {
              if (string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(stateInfo.ViewName))
              {
                str1 = (string) null;
              }
              else
              {
                if (!string.IsNullOrEmpty(stateInfo.InputMask) && PXImportAttribute.PXImporter<TBatchTable>.IsMasked(stateInfo.InputMask, str1))
                  str1 = Mask.Parse(stateInfo.InputMask, str1);
                object obj;
                if (stateInfo.LabelValuePairs != null && stateInfo.LabelValuePairs.TryGetValue(str1, out obj))
                  str1 = obj?.ToString();
                else if (culture != null && stateInfo.DataType != (System.Type) null)
                  str1 = PXImportAttribute.PXImporter<TBatchTable>.ParseValue(str1, stateInfo.DataType, (IFormatProvider) culture);
              }
            }
            if (string.IsNullOrEmpty(str1))
              str1 = (string) null;
            if (cache.Keys.Contains(str2))
            {
              if (result.Key.Contains((object) str2))
                result.Key[(object) str2] = (object) str1;
              else
                result.Key.Add((object) str2, (object) str1);
            }
            if (str2 != importColumnsSettings2.PropertyName)
            {
              string key = str2 + "Translations";
              string[] strArray = (string[]) null;
              if (result.Value.Contains((object) key))
                strArray = result.Value[(object) key] as string[];
              if (strArray == null)
                strArray = new string[this._languages.Count];
              int index;
              if (this._languages.TryGetValue(importColumnsSettings2.PropertyName.Substring(importColumnsSettings2.PropertyName.Length - 2), out index))
                strArray[index] = str1;
              result.Value[(object) key] = (object) strArray;
            }
            else if (result.Value.Contains((object) str2))
              result.Value[(object) str2] = (object) str1;
            else
              result.Value.Add((object) str2, (object) str1);
          }
          stringList1.Remove(importColumnsSettings2.PropertyName);
          stringList2.Remove(importColumnsSettings2.PropertyName);
        }
      }
      if (!flag)
        return false;
      foreach (string str in stringList1)
      {
        if (cache.Keys.Contains(str))
          result.Key.Add((object) str, (object) lineNumber);
        else
          result.Value.Add((object) str, (object) lineNumber);
      }
      short num = (short) lineNumber;
      foreach (string str in stringList2)
      {
        if (cache.Keys.Contains(str))
          result.Key.Add((object) str, (object) num);
        else
          result.Value.Add((object) str, (object) num);
      }
      if (cache.Keys.Count > result.Key.Count)
      {
        object instance = cache.CreateInstance();
        foreach (string key in (IEnumerable<string>) cache.Keys)
        {
          if (!result.Key.Contains((object) key))
          {
            object newValue;
            if (!cache.RaiseFieldDefaulting(key, instance, out newValue))
            {
              cache.RaiseFieldSelecting(key, instance, ref newValue, false);
              newValue = PXFieldState.UnwrapValue(newValue);
            }
            result.Key.Add((object) key, newValue);
          }
        }
      }
      result.Value.Add((object) PXImportAttribute.ImportFlag, PXCache.NotSetValue);
      return true;
    }

    /// <summary>
    /// Check if <paramref name="text" /> is masked by <paramref name="mask" />
    /// </summary>
    /// <remarks>Differs from Mask.IsMasked as it returns true even if text is longer and starts with mask</remarks>
    /// <returns>True if <paramref name="text" /> is a part of <paramref name="mask" /> or
    /// <paramref name="text" /> is longer but it starts with the <paramref name="mask" /></returns>
    private static bool IsMasked(string mask, string text)
    {
      if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(mask))
        return false;
      string source = string.Join<char>(string.Empty, mask.Where<char>((Func<char, bool>) (x => Mask.Flag((int) x, 0) == 0)));
      if (source.Length == 0)
        return true;
      HashSet<char> fillersSet = source.ToHashSet<char>();
      string str = string.Join<char>(string.Empty, text.Where<char>((Func<char, bool>) (x => fillersSet.Contains(x))));
      if (str.Length == 0)
        return false;
      return source.StartsWith(str) || str.StartsWith(source);
    }

    private static string ParseValue(
      string contentValue,
      System.Type targetType,
      IFormatProvider formatProvider)
    {
      object obj = (object) null;
      bool flag = false;
      try
      {
        switch (System.Type.GetTypeCode(targetType))
        {
          case TypeCode.Boolean:
            if (contentValue == null)
              obj = (object) false;
            bool result1;
            if (flag = bool.TryParse(contentValue, out result1))
            {
              obj = (object) result1;
              break;
            }
            break;
          case TypeCode.SByte:
            if (contentValue == null)
              obj = (object) (sbyte) 0;
            sbyte result2;
            if (flag = sbyte.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result2))
            {
              obj = (object) result2;
              break;
            }
            break;
          case TypeCode.Byte:
            if (contentValue == null)
              obj = (object) (byte) 0;
            byte result3;
            if (flag = byte.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result3))
            {
              obj = (object) result3;
              break;
            }
            break;
          case TypeCode.Int16:
            if (contentValue == null)
              obj = (object) (short) 0;
            short result4;
            if (flag = short.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result4))
            {
              obj = (object) result4;
              break;
            }
            break;
          case TypeCode.UInt16:
            if (contentValue == null)
              obj = (object) (ushort) 0;
            ushort result5;
            if (flag = ushort.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result5))
            {
              obj = (object) result5;
              break;
            }
            break;
          case TypeCode.Int32:
            if (contentValue == null)
              obj = (object) 0;
            int result6;
            if (flag = int.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result6))
            {
              obj = (object) result6;
              break;
            }
            break;
          case TypeCode.UInt32:
            if (contentValue == null)
              obj = (object) 0U;
            uint result7;
            if (flag = uint.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result7))
            {
              obj = (object) result7;
              break;
            }
            break;
          case TypeCode.Int64:
            if (contentValue == null)
              obj = (object) 0L;
            long result8;
            if (flag = long.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result8))
            {
              obj = (object) result8;
              break;
            }
            break;
          case TypeCode.UInt64:
            if (contentValue == null)
              obj = (object) 0UL;
            ulong result9;
            if (flag = ulong.TryParse(contentValue, NumberStyles.Integer, formatProvider, out result9))
            {
              obj = (object) result9;
              break;
            }
            break;
          case TypeCode.Single:
            if (contentValue == null)
              obj = (object) 0.0f;
            float result10;
            if (flag = float.TryParse(contentValue, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result10))
            {
              obj = (object) result10;
              break;
            }
            break;
          case TypeCode.Double:
            if (contentValue == null)
              obj = (object) 0.0;
            double result11;
            if (flag = double.TryParse(contentValue, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider, out result11))
            {
              obj = (object) result11;
              break;
            }
            break;
          case TypeCode.Decimal:
            if (contentValue == null)
              obj = (object) 0M;
            Decimal result12;
            if (flag = Decimal.TryParse(contentValue, NumberStyles.Number, formatProvider, out result12))
            {
              obj = (object) result12;
              break;
            }
            break;
          case TypeCode.DateTime:
            if (contentValue == null)
              obj = (object) new System.DateTime();
            System.DateTime result13;
            if (flag = System.DateTime.TryParse(contentValue, formatProvider, DateTimeStyles.None, out result13))
            {
              obj = (object) result13;
              break;
            }
            break;
        }
      }
      catch (FormatException ex)
      {
      }
      catch (OverflowException ex)
      {
      }
      return flag && obj != null ? obj.ToString() : contentValue;
    }

    private void GetTBatchTableProperties(PXCache sender, PXFieldSelectingEventArgs args)
    {
      if (!(args.Row is PXImportAttribute.PXImportColumnsSettings row) || string.Compare(row.ViewName, this._viewName, true) != 0)
        return;
      this.InitPropertiesInfo();
      args.ReturnState = (object) PXStringState.CreateInstance(args.ReturnState, new int?(), new bool?(), "PropertyName", new bool?(), new int?(-1), (string) null, this._propertiesNames, this._propertiesDisplayNames, new bool?(true), (string) null);
    }

    private void CorrectColumnAssociations(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!(e.Row is PXImportAttribute.PXImportColumnsSettings row) || string.Compare(row.ViewName, this._viewName, true) != 0)
        return;
      string strB = e.NewValue == null ? string.Empty : e.NewValue.ToString();
      foreach (PXImportAttribute.PXImportColumnsSettings importColumnsSettings in sender.Cached)
      {
        if (!importColumnsSettings.Index.Equals((object) row.Index) && string.Compare(importColumnsSettings.PropertyName, strB, true) == 0)
        {
          e.Cancel = true;
          break;
        }
      }
    }

    private void SetDefaultColumnsAssociations(byte[] content)
    {
      IContentReader reader = this.GetReader(content);
      reader.Reset();
      if (!reader.MoveNext())
        throw new Exception("Data is absent");
      this.ForceInitPropertiesInfo(this._cache);
      PXView importColumnsSettings1 = this._importColumnsSettings;
      object[] objArray = new object[1]
      {
        (object) this._viewName
      };
      foreach (PXImportAttribute.PXImportColumnsSettings importColumnsSettings2 in importColumnsSettings1.SelectMulti(objArray))
        this._importColumnsSettings.Cache.Delete((object) importColumnsSettings2);
      foreach (KeyValuePair<int, string> indexKeyPair in (IEnumerable<KeyValuePair<int, string>>) reader.IndexKeyPairs)
      {
        PXImportAttribute.PXImportColumnsSettings importColumnsSettings3 = new PXImportAttribute.PXImportColumnsSettings()
        {
          ViewName = this._viewName,
          ColumnIndex = new int?(indexKeyPair.Key),
          ColumnName = indexKeyPair.Value
        };
        int index = Array.IndexOf<string>(this._propertiesDisplayNames, indexKeyPair.Value);
        if (index < 0)
          index = Array.IndexOf<string>(this._propertiesNames, indexKeyPair.Value);
        if (index < 0 && this._languages.Count > 0)
          index = Array.IndexOf<string>(this._propertiesNames, $"{indexKeyPair.Value} {this._languages.Keys.First<string>()}");
        if (index > -1)
          importColumnsSettings3.PropertyName = this._propertiesNames[index];
        this._importColumnsSettings.Cache.Insert((object) importColumnsSettings3);
      }
      this._importColumnsSettings.Cache.IsDirty = false;
    }

    private void SetDefaultCommonSettings()
    {
      PXView importCommonSettings = this._importCommonSettings;
      object[] objArray = new object[1]
      {
        (object) this._viewName
      };
      foreach (object obj in importCommonSettings.SelectMulti(objArray))
        this._importCommonSettings.Cache.Delete(obj);
      PXImportAttribute.PXImportSettings defaultCommonSettings = this.CreateDefaultCommonSettings();
      defaultCommonSettings.ViewName = this._viewName;
      defaultCommonSettings.Culture = new int?(Thread.CurrentThread.CurrentCulture.LCID);
      defaultCommonSettings.Mode = "U";
      this._importCommonSettings.Cache.Insert((object) defaultCommonSettings);
      this._importCommonSettings.Cache.IsDirty = false;
    }

    private void SetContent(byte[] content, string ext)
    {
      PXImportAttribute.PXImportSettings importSettingsCurrent = this.ImportSettingsCurrent;
      importSettingsCurrent.FileExtension = ext;
      importSettingsCurrent.Data = (object) content;
    }

    protected abstract PXImportAttribute.PXImportSettings CreateDefaultCommonSettings();

    private void RemoveLineNbrFields(IDictionary fields)
    {
      foreach (object key in fields.Cast<DictionaryEntry>().Where<DictionaryEntry>((Func<DictionaryEntry, bool>) (entry => ((IEnumerable<object>) this._lineProperties).Contains<object>(entry.Key) || ((IEnumerable<object>) this._shortLineProperties).Contains<object>(entry.Key))).Select<DictionaryEntry, object>((Func<DictionaryEntry, object>) (entry => entry.Key)).ToList<object>())
        fields.Remove(key);
    }

    private void ConvertData(byte[] content, PXCache cache, CultureInfo culture, string mode)
    {
      this._exceptions.Clear();
      PXImportAttribute.ImportMode.Value mode1 = PXImportAttribute.ImportMode.Parse(mode);
      if (this.RollbackPreviousOperation)
      {
        foreach (TBatchTable batchTable in this._backupCache.Cached)
          cache.Update((object) batchTable);
        this._backupCache.Clear();
        foreach (TBatchTable batchTable in this._importedCache.Cached)
          cache.Delete((object) batchTable);
        this._importedCache.Clear();
      }
      this.ForceInitPropertiesInfo(cache);
      PXImportAttribute.IPXPrepareItems prepareItemsHandler = PXImportAttribute.CompositePrepareItems.CreateFor(cache.Graph);
      PXImportAttribute.IPXProcess importProcess = PXImportAttribute.CompositeProcess.CreateFor(cache.Graph);
      using (IContentReader reader = this.GetReader(content))
      {
        foreach (KeyValuePair<IDictionary, IDictionary> readItem in this.ReadItems(reader, cache, culture))
        {
          IDictionary keys = readItem.Key;
          IDictionary values = readItem.Value;
          if (mode1 == PXImportAttribute.ImportMode.Value.InsertAllRecords)
          {
            this.RemoveLineNbrFields(keys);
            this.RemoveLineNbrFields(values);
          }
          if (PXImportAttribute.PXImporter<TBatchTable>._isCRSelectable && !values.Contains((object) "Selected"))
            values.Add((object) "Selected", (object) true);
          PXImportAttribute.RowImportingEventArgs e = new PXImportAttribute.RowImportingEventArgs(keys, values, mode1);
          this.OnBeforeRowImporting(e);
          if (!e.Cancel)
          {
            if (this.OnCreateImportRow != null && prepareItemsHandler != null)
            {
              PXImportAttribute.CreateImportRowEventArguments args = new PXImportAttribute.CreateImportRowEventArguments(keys, values, this.ImportSettingsCurrent.Mode);
              this.OnCreateImportRow(args, prepareItemsHandler);
              if (args.Cancel)
                continue;
            }
            object current1 = cache.Current;
            object obj = cache.Locate(keys) > 0 ? cache.Current : (object) null;
            bool flag1 = obj == null;
            if (mode1 != PXImportAttribute.ImportMode.Value.BypassExisting || flag1)
            {
              bool flag2 = mode1 == PXImportAttribute.ImportMode.Value.BypassExisting || mode1 == PXImportAttribute.ImportMode.Value.InsertAllRecords;
              if (flag2)
              {
                if (this.OnRowImporting != null && prepareItemsHandler != null)
                {
                  PXImportAttribute.RowImportingEventArguments args = new PXImportAttribute.RowImportingEventArguments(obj);
                  this.OnRowImporting(args, prepareItemsHandler);
                  if (args.Cancel)
                    continue;
                }
                if (obj != null)
                  obj = cache.CreateCopy(obj);
              }
              if (!this.SaftyPerformOperation((PXImportAttribute.PXImporter<TBatchTable>.Operation) (() =>
              {
                cache.Graph.ExecuteUpdate(this._viewName, keys, values);
                this.CheckForExceptions(values.Values);
              })))
              {
                cache.Current = current1;
              }
              else
              {
                object current2 = cache.Current;
                if (flag2)
                {
                  PXImportAttribute.RowImportedEventArguments args = new PXImportAttribute.RowImportedEventArguments(current2, obj);
                  if (this.OnRowImported != null && prepareItemsHandler != null)
                    this.OnRowImported(args, prepareItemsHandler);
                  else
                    args.Cancel = !flag1;
                  if (args.Cancel)
                  {
                    cache.Remove(cache.Current);
                    cache.Current = current1;
                  }
                }
                if (this.RollbackPreviousOperation)
                {
                  if (flag1)
                    PXImportAttribute.PXImporter<TBatchTable>.InsertHeldItem(this._importedCache, keys);
                  else
                    PXImportAttribute.PXImporter<TBatchTable>.InsertHeldItem(this._backupCache, keys);
                }
              }
            }
          }
        }
      }
      if (this.OnImportDone == null || importProcess == null)
        return;
      this.OnImportDone(new PXImportAttribute.ImporDoneEventArguments(mode1), importProcess);
    }

    private void CheckForExceptions(ICollection values)
    {
      PXFieldState pxFieldState = values.OfType<PXFieldState>().FirstOrDefault<PXFieldState>((Func<PXFieldState, bool>) (state => state.ErrorLevel == PXErrorLevel.RowError));
      if (pxFieldState != null)
        throw new PXException(pxFieldState.Error);
    }

    private static void InsertHeldItem(PXCache cache, IDictionary keys)
    {
      object instance = cache.CreateInstance();
      foreach (DictionaryEntry key in keys)
        cache.SetValueExt(instance, key.Key.ToString(), key.Value);
      object obj = cache.Locate(instance);
      cache.SetStatus(obj, PXEntryStatus.Held);
      cache.Insert(obj);
    }

    private bool AskCommonSettings()
    {
      PXImportAttribute.PXImportSettings importSettingsCurrent = this.ImportSettingsCurrent;
      if (importSettingsCurrent == null)
        return true;
      PXImportAttribute.CommonSettingsDialogShowingEventArgs e = new PXImportAttribute.CommonSettingsDialogShowingEventArgs((PXImportAttribute.PXImportSettings) null);
      if (this._importCommonSettings.Answer == WebDialogResult.None)
      {
        byte[] data = importSettingsCurrent.Data as byte[];
        string fileExtension = importSettingsCurrent.FileExtension;
        this.SetDefaultCommonSettings();
        this.SetContent(data, fileExtension);
        e.Settings = this.ImportSettingsCurrent;
        this.OnCommonSettingsDialogShowing(e);
      }
      return e.Cancel || this._importCommonSettings.AskExt() == WebDialogResult.OK;
    }

    private void AskColumnAssociations()
    {
      // ISSUE: unable to decompile the method.
    }

    private void MoveEventsToImportGraph(PXGraph importGraph)
    {
      PXImportAttribute.PXImporter<TBatchTable> pxImporter = (PXImportAttribute.PXImporter<TBatchTable>) null;
      foreach (object obj in ((IEnumerable<object>) new object[1]
      {
        (object) importGraph
      }).Union<object>((IEnumerable<object>) importGraph.Extensions ?? Enumerable.Empty<object>()))
      {
        System.Reflection.FieldInfo field = obj.GetType().GetField(this._viewName);
        if (!(field == (System.Reflection.FieldInfo) null))
        {
          PXImportAttribute attribute = (field.GetValue(obj) as PXSelectBase).GetAttribute<PXImportAttribute>();
          if (attribute != null)
          {
            pxImporter = (PXImportAttribute.PXImporter<TBatchTable>) attribute._importer;
            break;
          }
        }
      }
      if (pxImporter == null)
        return;
      if (this.RowImporting != null)
      {
        foreach (EventHandler<PXImportAttribute.RowImportingEventArgs> invocation in this.RowImporting.GetInvocationList())
          this.RowImporting -= invocation;
        foreach (EventHandler<PXImportAttribute.RowImportingEventArgs> invocation in pxImporter.RowImporting.GetInvocationList())
          this.RowImporting += invocation;
      }
      if (this.FileUploading != null)
      {
        foreach (EventHandler<PXImportAttribute.FileUploadingEventArgs> invocation in this.FileUploading.GetInvocationList())
          this.FileUploading -= invocation;
        foreach (EventHandler<PXImportAttribute.FileUploadingEventArgs> invocation in pxImporter.FileUploading.GetInvocationList())
          this.FileUploading += invocation;
      }
      if (this.MappingDialogShowing != null)
      {
        foreach (EventHandler<PXImportAttribute.MappingDialogShowingEventArgs> invocation in this.MappingDialogShowing.GetInvocationList())
          this.MappingDialogShowing -= invocation;
        foreach (EventHandler<PXImportAttribute.MappingDialogShowingEventArgs> invocation in pxImporter.MappingDialogShowing.GetInvocationList())
          this.MappingDialogShowing += invocation;
      }
      if (this.CommonSettingsDialogShowing != null)
      {
        foreach (EventHandler<PXImportAttribute.CommonSettingsDialogShowingEventArgs> invocation in this.CommonSettingsDialogShowing.GetInvocationList())
          this.CommonSettingsDialogShowing -= invocation;
        foreach (EventHandler<PXImportAttribute.CommonSettingsDialogShowingEventArgs> invocation in pxImporter.CommonSettingsDialogShowing.GetInvocationList())
          this.CommonSettingsDialogShowing += invocation;
      }
      if (this.MappingPropertiesInit == null)
        return;
      foreach (EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs> invocation in this.MappingPropertiesInit.GetInvocationList())
        this.MappingPropertiesInit -= invocation;
      foreach (EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs> invocation in pxImporter.MappingPropertiesInit.GetInvocationList())
        this.MappingPropertiesInit += invocation;
    }

    private void ShowExceptions()
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = 10;
      foreach (KeyValuePair<System.Type, IList<Exception>> exception1 in this.Exceptions)
      {
        foreach (Exception exception2 in (IEnumerable<Exception>) exception1.Value)
        {
          stringBuilder.AppendLine(exception2.Message);
          if (--num < 1)
            break;
        }
      }
      string message = stringBuilder.ToString();
      if (!string.IsNullOrEmpty(message))
        throw new PXDialogRequiredException(this._viewName, (object) null, "Import errors", message, MessageButtons.RetryCancel, MessageIcon.Error);
    }

    private static void AssertType<ItemType>(PXCache itemsCache)
    {
      PXImportAttribute.PXImporter<TBatchTable>.AssertType<ItemType>(itemsCache.GetItemType());
    }

    private static void AssertType<ItemType>(System.Type cacheItemType)
    {
      if (!typeof (ItemType).IsAssignableFrom(cacheItemType))
        throw new ArgumentException($"The items type '{cacheItemType}' of cache must be an inheritor of the '{typeof (ItemType)}' type.");
    }

    /// <exclude />
    protected class StateInfo
    {
      private readonly PXFieldState _state;
      private string _inputeMask;
      private Dictionary<string, object> _labelValuePairs;

      public StateInfo(PXFieldState state)
      {
        this._state = state != null ? state : throw new ArgumentNullException(nameof (state));
      }

      public string Language
      {
        get
        {
          return this._state is PXStringState ? ((PXStringState) this._state).Language : (string) null;
        }
      }

      public string InputMask
      {
        get
        {
          if (this._inputeMask == null && this._state is PXStringState)
          {
            this._inputeMask = ((PXStringState) this._state).InputMask;
            if (this._state is PXSegmentedState state && !string.IsNullOrEmpty(state.Wildcard))
            {
              string[] strArray = this._inputeMask.Split('|');
              if (strArray.Length < 2)
                this._inputeMask += "||";
              else if (strArray.Length < 3)
                this._inputeMask += "|";
              this._inputeMask += state.Wildcard;
            }
          }
          return this._inputeMask;
        }
      }

      public System.Type DataType => this._state.DataType;

      public string ViewName => this._state.ViewName;

      public Dictionary<string, object> LabelValuePairs
      {
        get
        {
          if (this._labelValuePairs == null)
          {
            this._labelValuePairs = new Dictionary<string, object>();
            if (this._state is PXStringState state1 && state1.AllowedValues != null)
            {
              for (int index = 0; index < state1.AllowedValues.Length; ++index)
                this._labelValuePairs[state1.AllowedLabels[index]] = (object) state1.AllowedValues[index];
            }
            if (this._state is PXIntState state2 && state2.AllowedValues != null)
            {
              for (int index = 0; index < state2.AllowedValues.Length; ++index)
                this._labelValuePairs[state2.AllowedLabels[index]] = (object) state2.AllowedValues[index];
            }
          }
          return this._labelValuePairs;
        }
      }
    }

    /// <exclude />
    private class PXCachedView(PXCache cache) : PXView(cache.Graph, true, BqlCommand.CreateInstance(typeof (Select<>), typeof (TBatchTable)), (Delegate) (() => cache.Cached))
    {
      public bool Contains(IDictionary keys)
      {
        foreach (TBatchTable data in this.SelectMulti())
        {
          bool flag = true;
          foreach (string key1 in (IEnumerable<string>) this._Cache.Keys)
          {
            object key2 = keys[(object) key1];
            this._Cache.RaiseFieldUpdating(key1, (object) null, ref key2);
            if (key2 == null || !this._Cache.GetValueExt((object) data, key1).Equals(key2))
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return true;
        }
        return false;
      }
    }

    /// <exclude />
    protected delegate void Operation() where TBatchTable : class, IBqlTable, new();
  }

  /// <exclude />
  public sealed class PXImportException : Exception
  {
    public readonly KeyValuePair<IDictionary, IDictionary> Row;

    public PXImportException(
      string message,
      KeyValuePair<IDictionary, IDictionary> row,
      Exception innerException)
      : base(message, innerException)
    {
      this.Row = row;
    }

    public PXImportException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      ReflectionSerializer.RestoreObjectProps<PXImportAttribute.PXImportException>(this, info);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ReflectionSerializer.GetObjectData<PXImportAttribute.PXImportException>(this, info);
      base.GetObjectData(info, context);
    }
  }

  /// <summary>Defines all methods that can be implemented by the graph to control the data import.</summary>
  /// <remarks>Prefer to use the following interfaces instead: <see cref="T:PX.Data.PXImportAttribute.IPrepare" />, <see cref="T:PX.Data.PXImportAttribute.IImport" />, <see cref="T:PX.Data.PXImportAttribute.IConfirm" />.</remarks>
  public interface IPXPrepareItems
  {
    /// <summary>Prepares a record from the imported file for conversion into a DAC instance.</summary>
    /// <param name="viewName">The name of the view that is marked with the attribute.</param>
    /// <param name="keys">The keys of the data to import.</param>
    /// <param name="values">The values corresponding to the keys.</param>
    bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values);

    /// <summary>Implements the logic executed before the insertion of a data record into the cache.</summary>
    /// <param name="viewName">The name of the view that is marked with the attribute.</param>
    /// <param name="row">The record to import as a DAC instance.</param>
    bool RowImporting(string viewName, object row);

    /// <summary>Implements the logic executed after the insertion of a data record into the cache.</summary>
    /// <param name="viewName">The name of the view that is marked with the attribute.</param>
    /// <param name="row">The imported record as a DAC instance.</param>
    bool RowImported(string viewName, object row, object oldRow);

    /// <summary>Verifies the imported items before they are saved in the database.</summary>
    /// <param name="viewName">The name of the view that is marked with the attribute.</param>
    /// <param name="items">The collection of objects to import as instances of the DAC.</param>
    void PrepareItems(string viewName, IEnumerable items);
  }

  /// <summary>Prepares a record from the imported file for conversion into a DAC instance.</summary>
  public interface IPrepare
  {
    /// <inheritdoc cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.PrepareImportRow(System.String,System.Collections.IDictionary,System.Collections.IDictionary)" />
    bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values);
  }

  /// <summary>Implements the logic executed on the insertion of a data record into the cache.</summary>
  public interface IImport
  {
    /// <inheritdoc cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.RowImporting(System.String,System.Object)" />
    bool RowImporting(string viewName, object row);

    /// <inheritdoc cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.RowImported(System.String,System.Object,System.Object)" />
    bool RowImported(string viewName, object row, object oldRow);
  }

  /// <summary>Verifies the imported items before they are saved in the database.</summary>
  public interface IConfirm
  {
    /// <inheritdoc cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.PrepareItems(System.String,System.Collections.IEnumerable)" />
    void PrepareItems(string viewName, IEnumerable items);
  }

  private class CompositePrepareItems : PXImportAttribute.IPXPrepareItems
  {
    private readonly IEnumerable<PXImportAttribute.IPXPrepareItems> _fulls;
    private readonly IEnumerable<PXImportAttribute.IPrepare> _prepares;
    private readonly IEnumerable<PXImportAttribute.IImport> _imports;
    private readonly IEnumerable<PXImportAttribute.IConfirm> _confirms;

    public static PXImportAttribute.IPXPrepareItems CreateFor(PXGraph graph)
    {
      if (graph == null)
        return (PXImportAttribute.IPXPrepareItems) null;
      List<PXImportAttribute.IPXPrepareItems> pxPrepareItemsList = new List<PXImportAttribute.IPXPrepareItems>();
      List<PXImportAttribute.IPrepare> prepareList = new List<PXImportAttribute.IPrepare>();
      List<PXImportAttribute.IImport> importList = new List<PXImportAttribute.IImport>();
      List<PXImportAttribute.IConfirm> confirmList = new List<PXImportAttribute.IConfirm>();
      if (graph is PXImportAttribute.IPXPrepareItems pxPrepareItems1)
        pxPrepareItemsList.Add(pxPrepareItems1);
      if (graph is PXImportAttribute.IPrepare prepare1)
        prepareList.Add(prepare1);
      if (graph is PXImportAttribute.IImport import1)
        importList.Add(import1);
      if (graph is PXImportAttribute.IConfirm confirm1)
        confirmList.Add(confirm1);
      if (graph.Extensions != null)
      {
        foreach (PXGraphExtension extension in graph.Extensions)
        {
          if (extension is PXImportAttribute.IPXPrepareItems pxPrepareItems2)
            pxPrepareItemsList.Add(pxPrepareItems2);
          if (extension is PXImportAttribute.IPrepare prepare2)
            prepareList.Add(prepare2);
          if (extension is PXImportAttribute.IImport import2)
            importList.Add(import2);
          if (extension is PXImportAttribute.IConfirm confirm2)
            confirmList.Add(confirm2);
        }
      }
      return pxPrepareItemsList.Count == 0 && prepareList.Count == 0 && importList.Count == 0 && confirmList.Count == 0 ? (PXImportAttribute.IPXPrepareItems) null : (PXImportAttribute.IPXPrepareItems) new PXImportAttribute.CompositePrepareItems(pxPrepareItemsList.Count == 0 ? Enumerable.Empty<PXImportAttribute.IPXPrepareItems>() : (IEnumerable<PXImportAttribute.IPXPrepareItems>) pxPrepareItemsList, prepareList.Count == 0 ? Enumerable.Empty<PXImportAttribute.IPrepare>() : (IEnumerable<PXImportAttribute.IPrepare>) prepareList, importList.Count == 0 ? Enumerable.Empty<PXImportAttribute.IImport>() : (IEnumerable<PXImportAttribute.IImport>) importList, confirmList.Count == 0 ? Enumerable.Empty<PXImportAttribute.IConfirm>() : (IEnumerable<PXImportAttribute.IConfirm>) confirmList);
    }

    private CompositePrepareItems(
      IEnumerable<PXImportAttribute.IPXPrepareItems> fulls,
      IEnumerable<PXImportAttribute.IPrepare> prepares,
      IEnumerable<PXImportAttribute.IImport> imports,
      IEnumerable<PXImportAttribute.IConfirm> confirms)
    {
      this._fulls = fulls;
      this._prepares = prepares;
      this._imports = imports;
      this._confirms = confirms;
    }

    bool PXImportAttribute.IPXPrepareItems.PrepareImportRow(
      string viewName,
      IDictionary keys,
      IDictionary values)
    {
      bool flag = true;
      foreach (PXImportAttribute.IPXPrepareItems full in this._fulls)
      {
        flag &= full.PrepareImportRow(viewName, keys, values);
        if (!flag)
          break;
      }
      foreach (PXImportAttribute.IPrepare prepare in this._prepares)
      {
        flag &= prepare.PrepareImportRow(viewName, keys, values);
        if (!flag)
          break;
      }
      return flag;
    }

    bool PXImportAttribute.IPXPrepareItems.RowImporting(string viewName, object row)
    {
      bool? nullable1 = new bool?();
      bool valueOrDefault;
      foreach (PXImportAttribute.IPXPrepareItems full in this._fulls)
      {
        valueOrDefault = nullable1.GetValueOrDefault();
        if (!nullable1.HasValue)
          nullable1 = new bool?(true);
        bool? nullable2 = nullable1;
        string viewName1 = viewName;
        object row1 = row;
        nullable1 = full.RowImporting(viewName1, row1) ? nullable2 : new bool?(false);
        nullable2 = nullable1;
        bool flag = false;
        if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          break;
      }
      foreach (PXImportAttribute.IImport import in this._imports)
      {
        valueOrDefault = nullable1.GetValueOrDefault();
        if (!nullable1.HasValue)
          nullable1 = new bool?(true);
        bool? nullable3 = nullable1;
        string viewName2 = viewName;
        object row2 = row;
        nullable1 = import.RowImporting(viewName2, row2) ? nullable3 : new bool?(false);
        nullable3 = nullable1;
        bool flag = false;
        if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
          break;
      }
      return nullable1 ?? row == null;
    }

    bool PXImportAttribute.IPXPrepareItems.RowImported(string viewName, object row, object oldRow)
    {
      bool? nullable1 = new bool?();
      bool valueOrDefault;
      foreach (PXImportAttribute.IPXPrepareItems full in this._fulls)
      {
        valueOrDefault = nullable1.GetValueOrDefault();
        if (!nullable1.HasValue)
          nullable1 = new bool?(true);
        bool? nullable2 = nullable1;
        string viewName1 = viewName;
        object row1 = row;
        object oldRow1 = oldRow;
        nullable1 = full.RowImported(viewName1, row1, oldRow1) ? nullable2 : new bool?(false);
        nullable2 = nullable1;
        bool flag = false;
        if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          break;
      }
      foreach (PXImportAttribute.IImport import in this._imports)
      {
        valueOrDefault = nullable1.GetValueOrDefault();
        if (!nullable1.HasValue)
          nullable1 = new bool?(true);
        bool? nullable3 = nullable1;
        string viewName2 = viewName;
        object row2 = row;
        object oldRow2 = oldRow;
        nullable1 = import.RowImported(viewName2, row2, oldRow2) ? nullable3 : new bool?(false);
        nullable3 = nullable1;
        bool flag = false;
        if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
          break;
      }
      return nullable1 ?? oldRow == null;
    }

    void PXImportAttribute.IPXPrepareItems.PrepareItems(string viewName, IEnumerable items)
    {
      foreach (PXImportAttribute.IPXPrepareItems full in this._fulls)
        full.PrepareItems(viewName, items);
      foreach (PXImportAttribute.IConfirm confirm in this._confirms)
        confirm.PrepareItems(viewName, items);
    }
  }

  /// <exclude />
  public interface IPXProcess
  {
    void ImportDone(PXImportAttribute.ImportMode.Value mode);
  }

  private class CompositeProcess : PXImportAttribute.IPXProcess
  {
    private readonly IEnumerable<PXImportAttribute.IPXProcess> _children;

    public static PXImportAttribute.IPXProcess CreateFor(PXGraph graph)
    {
      if (graph == null)
        return (PXImportAttribute.IPXProcess) null;
      List<PXImportAttribute.IPXProcess> children = new List<PXImportAttribute.IPXProcess>();
      if (graph is PXImportAttribute.IPXProcess pxProcess1)
        children.Add(pxProcess1);
      if (graph.Extensions != null)
      {
        foreach (PXGraphExtension extension in graph.Extensions)
        {
          if (extension is PXImportAttribute.IPXProcess pxProcess2)
            children.Add(pxProcess2);
        }
      }
      return children.Count == 0 ? (PXImportAttribute.IPXProcess) null : (PXImportAttribute.IPXProcess) new PXImportAttribute.CompositeProcess((IEnumerable<PXImportAttribute.IPXProcess>) children);
    }

    private CompositeProcess(IEnumerable<PXImportAttribute.IPXProcess> children)
    {
      this._children = children;
    }

    void PXImportAttribute.IPXProcess.ImportDone(PXImportAttribute.ImportMode.Value mode)
    {
      foreach (PXImportAttribute.IPXProcess child in this._children)
        child.ImportDone(mode);
    }
  }

  /// <exclude />
  private class PXSelectInsertedHandler
  {
    public PXView View;

    public IEnumerable Select()
    {
      this.View.Cache.IsDirty = false;
      return this.View.Cache.Inserted;
    }
  }

  /// <exclude />
  private sealed class viewErrorInterceptor : PXView
  {
    private PXView _View;

    private viewErrorInterceptor(PXGraph graph, bool isReadOnly, BqlCommand select)
      : base(graph, isReadOnly, select)
    {
    }

    private viewErrorInterceptor(
      PXGraph graph,
      bool isReadOnly,
      BqlCommand select,
      Delegate handler)
      : base(graph, isReadOnly, select, handler)
    {
    }

    public static PXImportAttribute.viewErrorInterceptor FromView(PXView view)
    {
      PXImportAttribute.viewErrorInterceptor errorInterceptor = (object) view.BqlDelegate == null ? new PXImportAttribute.viewErrorInterceptor(view.Graph, view.IsReadOnly, view.BqlSelect) : new PXImportAttribute.viewErrorInterceptor(view.Graph, view.IsReadOnly, view.BqlSelect, view.BqlDelegate);
      errorInterceptor._View = view;
      return errorInterceptor;
    }

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
      if (maximumRows == 0)
        return this._View.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
      int startRow1 = 0;
      int maximumRows1 = 0;
      List<object> objectList1 = this._View.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow1, maximumRows1, ref totalRows);
      List<object> objectList2 = new List<object>();
      int index1 = 0;
      while (index1 < objectList1.Count)
      {
        object data = objectList1[index1];
        if (data is PXResult)
          data = ((PXResult) data)[0];
        switch (this._View.Cache.GetStatus(data))
        {
          case PXEntryStatus.Updated:
          case PXEntryStatus.Inserted:
            if (PXUIFieldAttribute.GetErrors(this._View.Cache, data).Count > 0)
            {
              objectList2.Add(objectList1[index1]);
              objectList1.RemoveAt(index1);
              continue;
            }
            ++index1;
            continue;
          default:
            ++index1;
            continue;
        }
      }
      if (startRow < 0)
        startRow = 0;
      for (int index2 = startRow; index2 < objectList1.Count && objectList2.Count <= maximumRows; ++index2)
        objectList2.Add(objectList1[index2]);
      return objectList2;
    }
  }
}

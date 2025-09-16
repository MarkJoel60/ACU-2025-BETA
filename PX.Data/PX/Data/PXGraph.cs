// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Api.Models;
using PX.Async;
using PX.Common;
using PX.Common.Extensions;
using PX.Common.Session;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.BusinessProcess;
using PX.Data.DacDescriptorGeneration;
using PX.Data.DependencyInjection;
using PX.Data.Maintenance.GI;
using PX.Data.SQLTree;
using PX.Data.WorkflowAPI;
using PX.DbServices.Model.DataSet;
using PX.DbServices.Model.ImportExport;
using PX.DbServices.Model.ImportExport.Serialization;
using PX.DbServices.Model.Schema;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Licensing;
using PX.SM;
using PX.SP;
using Serilog;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

/// <summary>
/// The base type that defines the common interface of business logic controllers (graphs), which you should derive from either <see cref="T:PX.Data.PXGraph`1" />
/// or <see cref="T:PX.Data.PXGraph`2" />.
/// </summary>
/// <remarks>
/// Each webpage references a graph (through the
/// <tt>PXDatasource</tt> control). An instance of this graph is created
/// and destroyed on each user's request, while the modified data records
/// are preserved between requests in the session.
/// </remarks>
[DebuggerDisplay("{GetType(),nq} (TimeStamp = {TimeStamp == null ? \"<empty>\" : PX.Data.PXDBTimestampAttribute.ToString(TimeStamp),nq})")]
[DebuggerTypeProxy(typeof (PXGraph.PXDebugView))]
[PXSerializationSurrogate(typeof (PXGraphSerializationSurrogate))]
public class PXGraph
{
  internal const 
  #nullable disable
  string CacheAttachedPostfix = "_CacheAttached";
  internal const BindingFlags ActionMemberHendlerBindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
  private static readonly System.Type[] PXButtonDelegateGenerics = new System.Type[11]
  {
    typeof (PXButtonDelegate<>),
    typeof (PXButtonDelegate<,>),
    typeof (PXButtonDelegate<,,>),
    typeof (PXButtonDelegate<,,,>),
    typeof (PXButtonDelegate<,,,,>),
    typeof (PXButtonDelegate<,,,,,>),
    typeof (PXButtonDelegate<,,,,,,>),
    typeof (PXButtonDelegate<,,,,,,,>),
    typeof (PXButtonDelegate<,,,,,,,,>),
    typeof (PXButtonDelegate<,,,,,,,,,>),
    typeof (PXButtonDelegate<,,,,,,,,,,>)
  };
  private Dictionary<System.Type, PXCache.AlteredDescriptor> _AlteredDescriptors;
  internal Dictionary<string, System.Type> _InactiveViews;
  internal Dictionary<string, string> _InactiveActions;
  /// <exclude />
  public bool IsReusableGraph;
  /// <exclude />
  [Obsolete("Not used and will be removed", true)]
  public bool IsCreatedFromSession;
  /// <exclude />
  public int ReuseCount;
  private bool NeedFullUnload;
  /// <exclude />
  public bool ShouldSaveVersionModified;
  public bool IsInVersionModifiedState;
  /// <summary>Holds the time when the graph instance was created.</summary>
  public readonly System.DateTime CreateTime = System.DateTime.Now;
  [ThreadStatic]
  private static bool IsCreateInstance;
  internal bool _ForceUnattended;
  /// <summary>Indicates (if set to <tt>true</tt>) that the import and export engine is working. That is, the field is <tt>true</tt> during the submission or retrieval of
  /// data with the import scenarios, the export scenarios, the web services API, the copy-and-paste functionality, or the mobile application.</summary>
  public bool IsImport;
  /// <summary>Indicates (if set to <tt>true</tt>) that the export engine is working. That is, the field is <tt>true</tt> during the retrieval of data with the export
  /// scenarios, the web services API, the copy-and-paste functionality, or the mobile application.</summary>
  /// <remarks>
  ///   <para>During the data export, the <see cref="F:PX.Data.PXGraph.IsImport" /> field is <tt>true</tt> as well. Therefore, you need to check the following conditions to check which
  /// operation is performed:</para>
  ///   <list type="bullet">
  ///     <item><description>If you need to check that the data export is performed: <tt>(graph.IsImport &amp;&amp; graph.IsExport)</tt></description></item>
  ///     <item><description>If you need to check that the data import is performed: <tt>(graph.IsImport &amp;&amp; !graph.IsExport)</tt></description></item>
  ///     <item><description>If you need to check that either the data import or the data export is performed: <tt>(graph.IsImport)</tt></description></item>
  ///   </list>
  /// </remarks>
  public bool IsExport;
  private bool _isMobile;
  /// <summary>Indicates (if set to <tt>true</tt>) that the request has come from the contract-based API.</summary>
  public bool IsContractBasedAPI;
  /// <summary>Indicates (if set to <tt>true</tt>) that the request has come from the dac-based Odata API.</summary>
  public bool IsDacBasedOdataAPI;
  internal bool IsPageReload;
  internal bool IsPageGeneratorRequest;
  /// <summary>Indicates (if set to <tt>true</tt>) that the copy-and-paste functionality is working.</summary>
  public bool IsCopyPasteContext;
  private AccessInfo accessinfo;
  private object _UID;
  private CultureInfo culture = CultureInfo.InvariantCulture;
  private bool _QueriesLoaded;
  internal readonly PXGraphQueryCacheCollection QueryCache = new PXGraphQueryCacheCollection();
  /// <summary>
  /// Collection of the delegates to get default current items
  /// </summary>
  public Dictionary<System.Type, PXGraph.GetDefaultDelegate> Defaults;
  internal byte[] _VeryFirstTimeStamp;
  private byte[] _TimeStamp;
  /// <summary>
  /// The dictionary that maps DACs to the related cache objects.
  /// An access to the indexer [] of this collection implicitly
  /// adds an element to the dictionary if the appropriate element
  /// does not exist.
  /// </summary>
  /// <example>
  /// The example below shows how the <tt>Caches</tt> property can
  /// be used to access cache objects of particular DAC type.
  /// <code>
  /// // The constructor of the APReleaseProcess graph
  /// public APReleaseProcess()
  /// {
  ///     ...
  ///     PXDBDefaultAttribute.SetDefaultForUpdate&lt;APAdjust.adjgFinPeriodID&gt;(
  ///         Caches[typeof(APAdjust)], null, false);
  /// 
  ///     ...
  ///     PXDBDefaultAttribute.SetDefaultForUpdate&lt;APTran.finPeriodID&gt;(
  ///         Caches[typeof(APTran)], null, false);
  /// }
  /// </code>
  /// </example>
  public PXCacheCollection Caches;
  /// <summary>The collection of actions defined in the graph.</summary>
  /// <example>
  /// One of the typical uses of the <tt>Actions</tt> is to save
  /// changes to the database by invoking the <tt>PressSave()</tt>
  /// methods as the following code of an action shows. Here, you save current
  /// changes before starting a background operation.
  /// <code>
  /// public PXAction&lt;SalesOrder&gt; Approve;
  /// [PXProcessButton]
  /// [PXUIField(DisplayName = "Approve")]
  /// protected virtual IEnumerable approve(PXAdapter adapter)
  /// {
  ///     Actions.PressSave();
  ///     SalesOrder order = Orders.Current;
  ///     // Starting a background operation
  ///     PXLongOperation.StartOperation(this, delegate()
  ///     {
  ///         // Background code
  ///         SalesOrderEntry graph = PXGraph.CreateInstance&lt;SalesOrderEntry&gt;();
  ///         graph.ApproveOrder(order);
  ///     });
  ///     return adapter.Get();
  /// }
  /// </code>
  /// </example>
  public readonly PXActionCollection Actions;
  /// <summary>The collection of data views defined in the graph.</summary>
  public PXViewCollection Views;
  /// <summary>
  /// The dictionary that allows getting the name of the data view
  /// by the corresponding <tt>PXView</tt> object.
  /// </summary>
  public readonly Dictionary<PXView, string> ViewNames;
  internal PXGraphExtension[] Extensions;
  /// <summary>
  /// The collection of <tt>PXView</tt> objects indexed by the first
  /// DACs referenced by the corresponding BQL commands.
  /// </summary>
  public PXTypedViewCollection TypedViews;
  protected internal List<string> _viewNames;
  internal HashSet<string> _selectedViews = new HashSet<string>();
  protected internal byte[] _primaryRecordTimeStamp;
  /// <exclude />
  public string WorkflowID;
  /// <exclude />
  public string WorkflowStepID;
  /// <exclude />
  public IEnumerable<string> NavigationParams;
  internal bool AutomationHidden;
  internal bool AutomationInsertDisabled;
  internal bool AutomationUpdateDisabled;
  internal bool AutomationDeleteDisabled;
  /// <summary>
  /// UnattendedMode = !PXPreserveScope.IsScoped();
  /// Do not automatically loads data from session in constructor
  /// </summary>
  /// <exclude />
  public bool UnattendedMode;
  internal bool FullTrust;
  /// <summary>
  /// Do not allows graph to Unload or Clear data to session.
  /// </summary>
  /// <exclude />
  public bool IsSessionReadOnly;
  internal StringTable StringTable;
  /// <exclude />
  public static bool IsRestricted;
  private IGraphLongOperationManager _longOperationManager;
  public PXGraph.PXGraphPrototype Prototype;
  private string _PrimaryView;
  private System.Type _PrimaryItemType;
  protected Dictionary<System.Type, Tuple<object, Func<object>>> _CachedSlots = new Dictionary<System.Type, Tuple<object, Func<object>>>();
  internal bool _ReWriteUnloadData = true;
  internal bool stateLoading;
  internal string statePrefix = "";
  internal static string sessionLoading;
  internal bool _ReadonlyCacheCreation;
  internal bool _DoNotClearCustomInfo;
  [ThreadStatic]
  internal static PXGraphPrepareDelegate OnAfterConstructor;
  private PXGraph.RowSelectingEvents _RowSelectingEvents;
  private PXGraph.RowSelectedEvents _RowSelectedEvents;
  private PXGraph.RowInsertingEvents _RowInsertingEvents;
  private PXGraph.RowInsertedEvents _RowInsertedEvents;
  private PXGraph.RowUpdatingEvents _RowUpdatingEvents;
  private PXGraph.RowUpdatedEvents _RowUpdatedEvents;
  private PXGraph.RowDeletingEvents _RowDeletingEvents;
  private PXGraph.RowDeletedEvents _RowDeletedEvents;
  private PXGraph.RowPersistingEvents _RowPersistingEvents;
  private PXGraph.RowPersistedEvents _RowPersistedEvents;
  private PXGraph.CommandPreparingEvents _CommandPreparingEvents;
  private PXGraph.FieldDefaultingEvents _FieldDefaultingEvents;
  private PXGraph.FieldUpdatingEvents _FieldUpdatingEvents;
  private PXGraph.FieldVerifyingEvents _FieldVerifyingEvents;
  private PXGraph.FieldUpdatedEvents _FieldUpdatedEvents;
  private PXGraph.FieldSelectingEvents _FieldSelectingEvents;
  private PXGraph.ExceptionHandlingEvents _ExceptionHandlingEvents;
  /// <summary>
  /// The instance of <see cref="T:PX.Data.PXGraph.InstanceCreatedEvents">InstanceCreatedEvents</see>
  /// type representing the collection of <tt>InstanceCreated</tt> event handlers.
  /// </summary>
  public static PXGraph.InstanceCreatedEvents InstanceCreated = new PXGraph.InstanceCreatedEvents();
  private Lazy<ISqlDialect> _LazyDialect;
  private bool? canCopyPaste;
  internal bool _UnexpectedException;
  private const string _ImportRowExistDialog = "xmlImportRootRowExist";
  private const string _ImportDuplicatesExistDialog = "xmlImportDuplicatesExist";
  private bool _reuseRestricted;
  internal bool HasSidePanelConditions;
  internal VersionedState VersionedState;
  internal SessionRollback ExceptionRollback;

  [PXInternalUseOnly]
  public IDisposable UnderDifferentStatePrefix(string newPrefix)
  {
    return (IDisposable) new PXGraph.StatePrefixScope(this, newPrefix);
  }

  [PXInternalUseOnly]
  public string StatePrefix { get; private set; }

  public bool PreserveErrorInfo { get; set; }

  public PXErrorInfo[] NonUIErrors { get; set; }

  internal virtual PXCache.AlteredDescriptor GetAlteredAttributes(System.Type itemType)
  {
    PXCache.AlteredDescriptor alteredDescriptor;
    return this._AlteredDescriptors != null && this._AlteredDescriptors.TryGetValue(itemType, out alteredDescriptor) ? alteredDescriptor : (PXCache.AlteredDescriptor) null;
  }

  /// <summary>Initializes a new graph instance of the specified type and
  /// extension types if the customization exists. This method provides a
  /// preferred way of initializing a graph. The graph type is specified in
  /// the type parameter.</summary>
  /// <example>
  /// The code below initializes an instance of the <tt>JournalEntry</tt>
  /// graph.
  /// <code>
  /// JournalEntry graph = PXGraph.CreateInstance&lt;JournalEntry&gt;();</code>
  /// </example>
  public static Graph CreateInstance<Graph>() where Graph : PXGraph, new()
  {
    return (Graph) PXGraph.CreateInstance(typeof (Graph));
  }

  public static Lazy<Graph> CreateLazyInstance<Graph>() where Graph : PXGraph, new()
  {
    return Lazy.By<Graph>((Func<Graph>) (() => PXGraph.CreateInstance<Graph>()));
  }

  public IQueryable<T> Select<T>() where T : IBqlTable => (IQueryable<T>) new SQLQueryable<T>(this);

  public IQueryable<T> SelectReadOnly<T>() where T : IBqlTable => this.Select<T>().ReadOnly<T>();

  /// <exclude />
  public static Graph CreateInstance<Graph>(string prefix) where Graph : PXGraph, new()
  {
    return (Graph) PXGraph.CreateInstance(typeof (Graph), prefix);
  }

  /// <summary>Restricts query fields for view.</summary>
  /// <param name="view">view name</param>
  /// <param name="fields">bql fields</param>
  public void RestrictViewFields(string view, IEnumerable<System.Type> fields)
  {
    PXFieldScope.SetRestrictedFields(this.Views[view], fields);
  }

  /// <summary>Restricts query fields for view.</summary>
  /// <param name="view">view name</param>
  /// <param name="fields">bql fields</param>
  public void RestrictViewFields(string view, params System.Type[] fields)
  {
    this.RestrictViewFields(view, ((IEnumerable<System.Type>) fields).AsEnumerable<System.Type>());
  }

  /// <exclude />
  public void RestrictViewFields(string view, IEnumerable<string> fields, bool collectDependencies = true)
  {
    PXFieldScope.SetRestrictedFields(this.Views[view], fields, collectDependencies);
  }

  /// <summary>Initializes a new graph instance of the specified type and
  /// extension types if the customization exists. This method provides a
  /// preferred way of initializing a graph.</summary>
  /// <param name="graphType">A type derived from <tt>PXGraph</tt>.</param>
  public static PXGraph CreateInstance(System.Type graphType)
  {
    return PXGraph.CreateInstance(graphType, "");
  }

  internal static System.Type _GetWrapperType(System.Type gt)
  {
    try
    {
      System.Type wrapperType = PXGraph._Initialize(gt).Wrapper;
      if ((object) wrapperType == null)
        wrapperType = gt;
      return wrapperType;
    }
    catch
    {
      return gt;
    }
  }

  [PXInternalUseOnly]
  public static PXGraph CreateInstance(System.Type graphType, string prefix)
  {
    if (graphType == (System.Type) null)
      throw new ArgumentNullException(nameof (graphType));
    if (!typeof (PXGraph).IsAssignableFrom(graphType))
      throw new ArgumentException($"The type '{graphType.FullName}' must inherit the PX.Data.PXGraph type.", nameof (graphType));
    string typeName = !(graphType.GetConstructor(new System.Type[0]) == (ConstructorInfo) null) ? CustomizedTypeManager.GetCustomizedTypeFullName(graphType) : throw new ArgumentException($"The type '{graphType.FullName}' must contain a default constructor.", nameof (graphType));
    System.Type graphType1 = graphType;
    try
    {
      if (typeName != graphType.FullName)
      {
        System.Type type = PXBuildManager.GetType(typeName, false);
        if ((object) type == null)
          type = graphType;
        graphType1 = type;
      }
      PXGraph.GraphStaticInfo graphStaticInfo = PXGraph._Initialize(graphType1);
      System.Type type1 = graphStaticInfo.Wrapper;
      if ((object) type1 == null)
        type1 = graphType1;
      System.Type type2 = type1;
      System.Type instanceCreating = PXGraph.GraphInstanceCreating;
      PXGraph.GraphStaticInfo staticInfoCreating = PXGraph.GraphStaticInfoCreating;
      try
      {
        PXGraph.GraphStatePrefix = prefix;
        PXGraph.IsCreateInstance = true;
        PXGraph.GraphInstanceCreating = type2;
        PXGraph.GraphStaticInfoCreating = graphStaticInfo;
        PXGraph instance = (PXGraph) Activator.CreateInstance(type2);
        InjectMethods.InjectDependencies(instance, graphType, prefix);
        if (instance is IGraphWithInitialization withInitialization)
          withInitialization.Initialize();
        if (instance.Extensions != null && instance.Extensions.Length != 0)
        {
          foreach (PXGraphExtension extension in instance.Extensions)
            extension.Initialize();
        }
        if (instance.WorkflowService != null && (instance.WorkflowService.IsEnabled(instance) || instance.WorkflowService.IsWorkflowExists(instance)))
          instance.WorkflowService.Configure(instance);
        try
        {
          if (PXDatabase.Provider is PXDatabaseProviderBase)
            instance._VeryFirstTimeStamp = ((PXDatabaseProviderBase) PXDatabase.Provider).selectTimestamp()?.Item1;
        }
        catch
        {
        }
        return instance;
      }
      finally
      {
        PXGraph.GraphStatePrefix = "";
        PXGraph.IsCreateInstance = false;
        PXGraph.GraphInstanceCreating = instanceCreating;
        PXGraph.GraphStaticInfoCreating = staticInfoCreating;
      }
    }
    catch (TargetInvocationException ex)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
  }

  protected static System.Type GraphInstanceCreating
  {
    get => PXContext.GetSlot<System.Type>(nameof (GraphInstanceCreating));
    set => PXContext.SetSlot<System.Type>(nameof (GraphInstanceCreating), value);
  }

  internal static PXGraph.GraphStaticInfo GraphStaticInfoCreating
  {
    get => PXContext.GetSlot<PXGraph.GraphStaticInfo>(nameof (GraphStaticInfoCreating));
    set => PXContext.SetSlot<PXGraph.GraphStaticInfo>(nameof (GraphStaticInfoCreating), value);
  }

  internal static string GraphStatePrefix
  {
    get => PXContext.GetSlot<string>(nameof (GraphStatePrefix));
    set => PXContext.SetSlot<string>(nameof (GraphStatePrefix), value);
  }

  /// <summary>
  /// When owner web page created in some artifical context.
  /// For example WSDL generation or screen information extraction.
  /// </summary>
  public static bool ProxyIsActive
  {
    get => PXContext.GetSlot<bool>(nameof (ProxyIsActive));
    set => PXContext.SetSlot<bool>(nameof (ProxyIsActive), value);
  }

  internal static bool ExportIsActive
  {
    get => PXContext.GetSlot<bool>(nameof (ExportIsActive));
    set => PXContext.SetSlot<bool>(nameof (ExportIsActive), value);
  }

  /// <summary>
  /// When owner web page created in some artifical context.
  /// For example test proxy generation.
  /// </summary>
  public static bool GeneratorIsActive
  {
    get => PXContext.GetSlot<bool>(nameof (GeneratorIsActive));
    set => PXContext.SetSlot<bool>(nameof (GeneratorIsActive), value);
  }

  internal static List<System.Type> _GetExtensions(System.Type tgraph)
  {
    return PXExtensionManager.GetExtensions(tgraph, true);
  }

  /// <summary>
  /// Initializes and populates GraphStaticInfo, more specifically wrapper type and
  /// and method to initialize graph views, actions and to subscribe event handlers
  /// </summary>
  /// <param name="tgraph">Type of the business object</param>
  /// <param name="extensions">List of extension types for tgraph</param>
  /// <param name="inactiveViews">Inactive views for tgraph</param>
  /// <param name="inactiveActions">Inactive actions for tgraph</param>
  /// <returns>GraphStaticInfo</returns>
  internal static PXGraph.GraphStaticInfo InitGraphStaticInfo(
    System.Type tgraph,
    List<System.Type> extensions,
    Dictionary<string, System.Type> inactiveViews,
    Dictionary<string, string> inactiveActions)
  {
    PXGraph.GraphStaticInfo graphInfo = new PXGraph.GraphStaticInfo();
    graphInfo.Wrapper = PXExtensionManager.CreateWrapper(tgraph, extensions);
    graphInfo.InactiveViews = inactiveViews;
    graphInfo.InactiveActions = inactiveActions;
    graphInfo.Extensions = extensions;
    if (graphInfo.Wrapper == (System.Type) null && PXGraph.GraphStaticInfoCreating != null && PXGraph.GraphStaticInfoCreating.Wrapper == tgraph)
      extensions = PXGraph.GraphStaticInfoCreating.Extensions;
    DynamicMethod dynamicMethod;
    if (!PXGraph.IsRestricted)
      dynamicMethod = new DynamicMethod("_Initialize", (System.Type) null, new System.Type[1]
      {
        typeof (PXGraph)
      }, typeof (PXGraph), true);
    else
      dynamicMethod = new DynamicMethod("_Initialize", (System.Type) null, new System.Type[1]
      {
        typeof (PXGraph)
      }, true);
    ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
    List<System.Type> typeList1 = new List<System.Type>();
    List<System.Type> typeList2 = new List<System.Type>();
    PXExtensionManager.EmitExtensionsCreation(tgraph, extensions, ilGenerator);
    List<System.Type> readonlyCaches;
    PXGraph.ProcessFields(tgraph, extensions, typeList1, ilGenerator, out readonlyCaches, graphInfo);
    foreach (System.Type cls in ((IEnumerable<System.Type>) new System.Type[1]
    {
      tgraph
    }).Union<System.Type>((IEnumerable<System.Type>) extensions))
    {
      foreach (PropertyInfo property in cls.GetProperties())
      {
        if (!typeList2.Contains(property.PropertyType) && string.Compare(property.Name, property.PropertyType.Name, StringComparison.OrdinalIgnoreCase) == 0 && typeof (IBqlTable).IsAssignableFrom(property.PropertyType))
        {
          ilGenerator.Emit(OpCodes.Ldarg_0);
          ilGenerator.Emit(OpCodes.Ldfld, tgraph.GetField("Defaults"));
          ilGenerator.Emit(OpCodes.Ldtoken, property.PropertyType);
          ilGenerator.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
          {
            typeof (RuntimeTypeHandle)
          }));
          ilGenerator.Emit(OpCodes.Ldtoken, typeof (PXGraph.GetDefaultDelegate));
          ilGenerator.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
          {
            typeof (RuntimeTypeHandle)
          }));
          ilGenerator.Emit(OpCodes.Ldarg_0);
          if (cls != tgraph)
          {
            ilGenerator.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
            ilGenerator.Emit(OpCodes.Ldc_I4, extensions.IndexOf(cls));
            ilGenerator.Emit(OpCodes.Ldelem_Ref);
          }
          ilGenerator.Emit(OpCodes.Ldtoken, property.GetGetMethod());
          ilGenerator.Emit(OpCodes.Ldtoken, cls);
          ilGenerator.Emit(OpCodes.Call, typeof (MethodBase).GetMethod("GetMethodFromHandle", new System.Type[2]
          {
            typeof (RuntimeMethodHandle),
            typeof (RuntimeTypeHandle)
          }));
          ilGenerator.Emit(OpCodes.Castclass, typeof (MethodInfo));
          ilGenerator.Emit(OpCodes.Call, typeof (Delegate).GetMethod("CreateDelegate", new System.Type[3]
          {
            typeof (System.Type),
            typeof (object),
            typeof (MethodInfo)
          }));
          ilGenerator.Emit(OpCodes.Castclass, typeof (PXGraph.GetDefaultDelegate));
          ilGenerator.Emit(OpCodes.Callvirt, typeof (Dictionary<System.Type, PXGraph.GetDefaultDelegate>).GetMethod("Add", new System.Type[2]
          {
            typeof (System.Type),
            typeof (PXGraph.GetDefaultDelegate)
          }));
          typeList2.Add(property.PropertyType);
        }
      }
    }
    Dictionary<System.Type, PXGraph.AlteredState> alteredState;
    EventsAutoSubscription.Process(ilGenerator, tgraph, extensions, typeList1, readonlyCaches, out alteredState);
    if (alteredState.Count > 0)
    {
      graphInfo.AlteredSource = new Dictionary<System.Type, PXCache.AlteredSource>();
      foreach (KeyValuePair<System.Type, PXGraph.AlteredState> keyValuePair in alteredState)
        graphInfo.AlteredSource[keyValuePair.Key] = new PXCache.AlteredSource(keyValuePair.Value.Fields.Values.SelectMany<List<PXEventSubscriberAttribute>, PXEventSubscriberAttribute>((Func<List<PXEventSubscriberAttribute>, IEnumerable<PXEventSubscriberAttribute>>) (l => (IEnumerable<PXEventSubscriberAttribute>) l)).ToArray<PXEventSubscriberAttribute>(), keyValuePair.Value.Fields.Keys.ToHashSet<string>(), (PXCache._CacheAttachedDelegate) keyValuePair.Value.Method.CreateDelegate(typeof (PXCache._CacheAttachedDelegate)), keyValuePair.Key);
    }
    ilGenerator.Emit(OpCodes.Ret);
    PXGraph._InitializeDelegate initializeDelegate1 = (PXGraph._InitializeDelegate) dynamicMethod.CreateDelegate(typeof (PXGraph._InitializeDelegate));
    PXGraph._InitializeDelegate initializeDelegate2 = PXGraph.ProcessExtendableMethods(tgraph, extensions);
    graphInfo.InitializeDelegate = initializeDelegate1 + initializeDelegate2;
    return graphInfo;
  }

  private static string GetGenericTypeName(System.Type t)
  {
    return StringExtensions.FirstSegment(t.Name, '`');
  }

  private static PXGraph._InitializeDelegate ProcessExtendableMethods(
    System.Type tgraph,
    List<System.Type> graphextensions)
  {
    ParameterExpression parameterExpression1 = Expression.Parameter(typeof (PXGraph), "graph");
    ParameterExpression g = Expression.Variable(tgraph, "g");
    List<Expression> expressionList1 = new List<Expression>()
    {
      (Expression) Expression.Assign((Expression) g, (Expression) Expression.Convert((Expression) parameterExpression1, tgraph))
    };
    foreach (System.Reflection.FieldInfo field in ((IEnumerable<System.Reflection.FieldInfo>) tgraph.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (_ =>
    {
      if (!_.FieldType.IsGenericType)
        return false;
      return PXGraph.GetGenericTypeName(_.FieldType) == PXGraph.GetGenericTypeName(typeof (PXExtendableMethod<>)) || PXGraph.GetGenericTypeName(_.FieldType) == PXGraph.GetGenericTypeName(typeof (PXExtendableFunction<>));
    })).ToArray<System.Reflection.FieldInfo>())
    {
      string name = field.Name;
      System.Type fieldType = field.FieldType;
      string genericTypeName = PXGraph.GetGenericTypeName(fieldType);
      bool flag1 = genericTypeName == "PXExtendableFunction";
      System.Type[] source1 = fieldType.GetGenericArguments();
      System.Type type = (System.Type) null;
      if (flag1)
      {
        type = ((IEnumerable<System.Type>) source1).Last<System.Type>();
        source1 = ((IEnumerable<System.Type>) source1).Take<System.Type>(source1.Length - 1).ToArray<System.Type>();
      }
      List<MethodInfo> source2 = new List<MethodInfo>();
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      foreach (System.Type c1 in ((IEnumerable<System.Type>) new System.Type[1]
      {
        tgraph
      }).Union<System.Type>((IEnumerable<System.Type>) graphextensions))
      {
        MethodInfo method = c1.GetMethod(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method == (MethodInfo) null & flag1 && typeof (PXGraph).IsAssignableFrom(c1))
          throw new PXException($"The function {method?.Name} is not implemented in type {c1.FullName} for {genericTypeName}.");
        if (!(method == (MethodInfo) null))
        {
          System.Type[] array = ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (_ => _.ParameterType)).ToArray<System.Type>();
          ParameterInfo returnParameter = method.ReturnParameter;
          bool flag2 = returnParameter.ParameterType == typeof (void);
          bool flag3 = array.Length == source1.Length;
          bool flag4 = array.Length == source1.Length + 1;
          if (!flag3 && !flag4)
            throw new PXException($"Invalid number of arguments in the method {method.Name} in type {c1.FullName} for {genericTypeName}.");
          for (int index = 0; index < source1.Length; ++index)
          {
            System.Type c2 = source1[index];
            if (!array[index].IsAssignableFrom(c2))
              throw new PXException(string.Format("Incompatible parameter #{3} in the method {0} in type {1} for {2}", (object) method.Name, (object) c1.FullName, (object) genericTypeName, (object) index));
          }
          if (flag4 && ((IEnumerable<System.Type>) array).Last<System.Type>().ToString() != fieldType.ToString())
            throw new PXException($"Invalid delegate in the method {method.Name} in type {c1.FullName} for {genericTypeName}.");
          if (flag1)
          {
            if (flag4 & flag2 || ((!flag3 ? 0 : (typeof (PXGraph).IsAssignableFrom(c1) ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
              throw new PXException($"Required return value in the method {method.Name} in type {c1.FullName} for {genericTypeName}.");
            if (!flag2 && !type.IsAssignableFrom(returnParameter.ParameterType))
              throw new PXException($"Incompatible type of return value in the method {method.Name} in type {c1.FullName} for {genericTypeName}.");
          }
          else if (!flag2)
            throw new PXException($"Unexpected return value in the method {method.Name} in type {c1.FullName} for {genericTypeName}.");
          if (flag3)
            source2.Add(method);
          else
            methodInfoList.Add(method);
        }
      }
      ParameterExpression[] array1 = ((IEnumerable<System.Type>) source1).Select<System.Type, ParameterExpression>((Func<System.Type, int, ParameterExpression>) ((t, i) => Expression.Parameter(t, "p" + i.ToString()))).ToArray<ParameterExpression>();
      Expression[] arguments = (Expression[]) array1;
      Func<MethodInfo, Expression> GetTarget = (Func<MethodInfo, Expression>) (info =>
      {
        int num = graphextensions.IndexOf(info.DeclaringType);
        return num >= 0 ? (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) Expression.Field((Expression) g, "Extensions"), (Expression) Expression.Constant((object) num)), info.DeclaringType) : (Expression) g;
      });
      if (flag1)
      {
        ParameterExpression left = Expression.Variable(type, "ret");
        List<Expression> expressionList2 = new List<Expression>();
        foreach (MethodInfo method in source2)
        {
          int num = method.ReturnType != typeof (void) ? 1 : 0;
          Expression right = (Expression) Expression.Call(GetTarget(method), method, arguments);
          if (num != 0)
            right = (Expression) Expression.Assign((Expression) left, right);
          expressionList2.Add(right);
        }
        expressionList2.Add((Expression) left);
        Expression body = (Expression) Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[1]
        {
          left
        }, (IEnumerable<Expression>) expressionList2);
        foreach (MethodInfo method in methodInfoList)
        {
          LambdaExpression lambdaExpression = Expression.Lambda(fieldType, body, array1);
          body = (Expression) Expression.Call(GetTarget(method), method, EnumerableExtensions.Append<Expression>(arguments, (Expression) lambdaExpression));
        }
        expressionList1.Add((Expression) Expression.Assign((Expression) Expression.Field((Expression) g, field), (Expression) Expression.Lambda(fieldType, body, array1)));
      }
      else
      {
        Expression body = (Expression) Expression.Block((IEnumerable<Expression>) source2.Cast<MethodInfo>().Select<MethodInfo, MethodCallExpression>((Func<MethodInfo, MethodCallExpression>) (simpleMethod => Expression.Call(GetTarget(simpleMethod), simpleMethod, arguments))));
        foreach (MethodInfo method in methodInfoList)
        {
          LambdaExpression lambdaExpression = Expression.Lambda(fieldType, body, array1);
          body = (Expression) Expression.Call(GetTarget(method), method, EnumerableExtensions.Append<Expression>(arguments, (Expression) lambdaExpression));
        }
        expressionList1.Add((Expression) Expression.Assign((Expression) Expression.Field((Expression) g, field), (Expression) Expression.Lambda(fieldType, body, array1)));
      }
    }
    return ((Expression<PXGraph._InitializeDelegate>) (parameterExpression => Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[1]
    {
      g
    }, (IEnumerable<Expression>) expressionList1))).Compile();
  }

  private static System.Type FindSelectDacType(System.Type selectType)
  {
    for (; selectType != typeof (object); selectType = selectType.BaseType)
    {
      if (selectType.IsGenericType && selectType.GetGenericTypeDefinition() == typeof (PXSelectBase<>))
        return selectType.GetGenericArguments()[0];
    }
    return (System.Type) null;
  }

  private static void ProcessFields(
    System.Type tgraph,
    List<System.Type> graphextensions,
    List<System.Type> caches,
    ILGenerator il,
    out List<System.Type> readonlyCaches,
    PXGraph.GraphStaticInfo graphInfo)
  {
    readonlyCaches = new List<System.Type>();
    Dictionary<System.Type, (System.Type, System.Reflection.FieldInfo)> dictionary = new Dictionary<System.Type, (System.Type, System.Reflection.FieldInfo)>();
    foreach (System.Type type1 in ((IEnumerable<System.Type>) new System.Type[1]
    {
      tgraph
    }).Union<System.Type>((IEnumerable<System.Type>) graphextensions))
    {
      System.Type type = type1;
      string str = (type.GetCustomAttribute(typeof (PXPrefixMembersAttribute)) is PXPrefixMembersAttribute customAttribute ? customAttribute.Prefix : (string) null) ?? "";
      System.Reflection.FieldInfo[] array = ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields()).OrderBy<System.Reflection.FieldInfo, MemberInfo>((Func<System.Reflection.FieldInfo, MemberInfo>) (f => (MemberInfo) f), (IComparer<MemberInfo>) new PXGraph.FieldInfoComparer()).ToArray<System.Reflection.FieldInfo>();
      System.Reflection.FieldInfo[] destinationArray = new System.Reflection.FieldInfo[array.Length];
      int sourceIndex;
      for (int destinationIndex = 0; destinationIndex < destinationArray.Length; destinationIndex = array.Length - sourceIndex)
      {
        sourceIndex = array.Length - destinationIndex - 1;
        while (sourceIndex > 0 && array[array.Length - destinationIndex - 1].DeclaringType == array[sourceIndex - 1].DeclaringType)
          --sourceIndex;
        Array.Copy((Array) array, sourceIndex, (Array) destinationArray, destinationIndex, array.Length - destinationIndex - sourceIndex);
      }
      List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>();
      foreach (System.Reflection.FieldInfo fieldInfo in destinationArray)
      {
        System.Reflection.FieldInfo field = fieldInfo;
        if (field.FieldType.IsSubclassOf(typeof (PXSelectBase)))
        {
          if (!(field.DeclaringType != type) || (object) field == (object) type.GetField(field.Name))
          {
            System.Type fieldType = field.FieldType;
            if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof (PXSelectExtension<>))
            {
              fieldInfoList.Add(field);
            }
            else
            {
              System.Type selectDacType = PXGraph.FindSelectDacType(fieldType);
              if (selectDacType != (System.Type) null && !dictionary.ContainsKey(selectDacType))
                dictionary[selectDacType] = (type, field);
              MethodInfo methodInfo = type.GetMethod(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
              System.Type cls1 = type;
              if (methodInfo != (MethodInfo) null && Attribute.IsDefined((MemberInfo) methodInfo, typeof (PXOverrideAttribute)) && tgraph != type)
              {
                MethodInfo method = tgraph.GetMethod(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != (MethodInfo) null && method.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (_ => _.AttributeType == typeof (PXExtensionManager.PXCstOverridedAttribute))))
                {
                  methodInfo = method;
                  cls1 = tgraph;
                }
              }
              int num1 = 0;
              if (type != tgraph)
                num1 = graphextensions.IndexOf(type) + 1;
              for (int index = num1; index < graphextensions.Count; ++index)
              {
                MethodInfo method = graphextensions[index].GetMethod(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != (MethodInfo) null && graphextensions[index].GetField(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) == (System.Reflection.FieldInfo) null && !Attribute.IsDefined((MemberInfo) method, typeof (PXOverrideAttribute)))
                {
                  methodInfo = method;
                  cls1 = graphextensions[index];
                }
              }
              System.Type cls2 = (System.Type) null;
              if (methodInfo != (MethodInfo) null)
              {
                ParameterInfo[] parameters = methodInfo.GetParameters();
                int num2 = 0;
                for (int index = 0; index < parameters.Length; ++index)
                {
                  if (parameters[index].ParameterType.IsByRef)
                    ++num2;
                }
                if (num2 == 0 && parameters.Length == 0 && typeof (IEnumerable).IsAssignableFrom(methodInfo.ReturnType))
                  cls2 = typeof (PXSelectDelegate);
                else if (methodInfo.ReturnType == typeof (IEnumerable) && num2 == 0)
                {
                  switch (parameters.Length)
                  {
                    case 0:
                      cls2 = typeof (PXSelectDelegate);
                      break;
                    case 1:
                      cls2 = typeof (PXSelectDelegate<>).MakeGenericType(parameters[0].ParameterType);
                      break;
                    case 2:
                      cls2 = typeof (PXSelectDelegate<,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType);
                      break;
                    case 3:
                      cls2 = typeof (PXSelectDelegate<,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType);
                      break;
                    case 4:
                      cls2 = typeof (PXSelectDelegate<,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType);
                      break;
                    case 5:
                      cls2 = typeof (PXSelectDelegate<,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType);
                      break;
                    case 6:
                      cls2 = typeof (PXSelectDelegate<,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType);
                      break;
                    case 7:
                      cls2 = typeof (PXSelectDelegate<,,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType, parameters[6].ParameterType);
                      break;
                    case 8:
                      cls2 = typeof (PXSelectDelegate<,,,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType, parameters[6].ParameterType, parameters[7].ParameterType);
                      break;
                    case 9:
                      cls2 = typeof (PXSelectDelegate<,,,,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType, parameters[6].ParameterType, parameters[7].ParameterType, parameters[8].ParameterType);
                      break;
                    case 10:
                      cls2 = typeof (PXSelectDelegate<,,,,,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType, parameters[6].ParameterType, parameters[7].ParameterType, parameters[8].ParameterType, parameters[9].ParameterType);
                      break;
                    case 11:
                      cls2 = typeof (PXSelectDelegate<,,,,,,,,,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, parameters[5].ParameterType, parameters[6].ParameterType, parameters[7].ParameterType, parameters[8].ParameterType, parameters[9].ParameterType, parameters[10].ParameterType);
                      break;
                  }
                }
                else if (methodInfo.ReturnType == typeof (void) && num2 == parameters.Length)
                {
                  switch (parameters.Length)
                  {
                    case 0:
                      cls2 = typeof (PXPrepareDelegate);
                      break;
                    case 1:
                      cls2 = typeof (PXPrepareDelegate<>).MakeGenericType(parameters[0].ParameterType.GetElementType());
                      break;
                    case 2:
                      cls2 = typeof (PXPrepareDelegate<,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType());
                      break;
                    case 3:
                      cls2 = typeof (PXPrepareDelegate<,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType());
                      break;
                    case 4:
                      cls2 = typeof (PXPrepareDelegate<,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType());
                      break;
                    case 5:
                      cls2 = typeof (PXPrepareDelegate<,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType());
                      break;
                    case 6:
                      cls2 = typeof (PXPrepareDelegate<,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType());
                      break;
                    case 7:
                      cls2 = typeof (PXPrepareDelegate<,,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType(), parameters[6].ParameterType.GetElementType());
                      break;
                    case 8:
                      cls2 = typeof (PXPrepareDelegate<,,,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType(), parameters[6].ParameterType.GetElementType(), parameters[7].ParameterType.GetElementType());
                      break;
                    case 9:
                      cls2 = typeof (PXPrepareDelegate<,,,,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType(), parameters[6].ParameterType.GetElementType(), parameters[7].ParameterType.GetElementType(), parameters[8].ParameterType.GetElementType());
                      break;
                    case 10:
                      cls2 = typeof (PXPrepareDelegate<,,,,,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType(), parameters[6].ParameterType.GetElementType(), parameters[7].ParameterType.GetElementType(), parameters[8].ParameterType.GetElementType(), parameters[9].ParameterType.GetElementType());
                      break;
                    case 11:
                      cls2 = typeof (PXPrepareDelegate<,,,,,,,,,,>).MakeGenericType(parameters[0].ParameterType.GetElementType(), parameters[1].ParameterType.GetElementType(), parameters[2].ParameterType.GetElementType(), parameters[3].ParameterType.GetElementType(), parameters[4].ParameterType.GetElementType(), parameters[5].ParameterType.GetElementType(), parameters[6].ParameterType.GetElementType(), parameters[7].ParameterType.GetElementType(), parameters[8].ParameterType.GetElementType(), parameters[9].ParameterType.GetElementType(), parameters[10].ParameterType.GetElementType());
                      break;
                  }
                }
              }
              il.Emit(OpCodes.Ldarg_0);
              if (type != tgraph)
              {
                il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
                il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
                il.Emit(OpCodes.Ldelem_Ref);
              }
              il.Emit(OpCodes.Castclass, type);
              ConstructorInfo constructor = field.FieldType.GetConstructor(new System.Type[2]
              {
                typeof (PXGraph),
                typeof (Delegate)
              });
              if (cls2 == (System.Type) null || constructor == (ConstructorInfo) null)
              {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Newobj, field.FieldType.GetConstructor(new System.Type[1]
                {
                  typeof (PXGraph)
                }));
              }
              else
              {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldtoken, cls2);
                il.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
                {
                  typeof (RuntimeTypeHandle)
                }));
                if (!methodInfo.IsStatic)
                {
                  il.Emit(OpCodes.Ldarg_0);
                  if (cls1 != tgraph)
                  {
                    il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
                    il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(cls1));
                    il.Emit(OpCodes.Ldelem_Ref);
                  }
                  il.Emit(OpCodes.Castclass, cls1);
                }
                il.Emit(OpCodes.Ldtoken, methodInfo);
                il.Emit(OpCodes.Ldtoken, cls1);
                il.Emit(OpCodes.Call, typeof (MethodBase).GetMethod("GetMethodFromHandle", new System.Type[2]
                {
                  typeof (RuntimeMethodHandle),
                  typeof (RuntimeTypeHandle)
                }));
                il.Emit(OpCodes.Castclass, typeof (MethodInfo));
                if (!methodInfo.IsStatic)
                  il.Emit(OpCodes.Call, typeof (Delegate).GetMethod("CreateDelegate", new System.Type[3]
                  {
                    typeof (System.Type),
                    typeof (object),
                    typeof (MethodInfo)
                  }));
                else
                  il.Emit(OpCodes.Call, typeof (Delegate).GetMethod("CreateDelegate", new System.Type[2]
                  {
                    typeof (System.Type),
                    typeof (MethodInfo)
                  }));
                il.Emit(OpCodes.Castclass, cls2);
                il.Emit(OpCodes.Newobj, constructor);
              }
              il.Emit(OpCodes.Stfld, field);
              il.Emit(OpCodes.Ldarg_0);
              il.Emit(OpCodes.Ldfld, tgraph.GetField("Views"));
              il.Emit(OpCodes.Ldstr, str + field.Name);
              il.Emit(OpCodes.Ldarg_0);
              if (type != tgraph)
              {
                il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
                il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
                il.Emit(OpCodes.Ldelem_Ref);
              }
              il.Emit(OpCodes.Castclass, type);
              il.Emit(OpCodes.Ldfld, field);
              il.Emit(OpCodes.Callvirt, typeof (PXViewCollection).GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
              {
                typeof (string),
                typeof (PXSelectBase)
              }, (ParameterModifier[]) null));
              if (selectDacType != (System.Type) null && caches.IndexOf(selectDacType) < 0)
              {
                if (!typeof (IPXNonUpdateable).IsAssignableFrom(field.FieldType))
                {
                  il.Emit(OpCodes.Ldarg_0);
                  il.Emit(OpCodes.Ldfld, tgraph.GetField("Views"));
                  il.Emit(OpCodes.Ldfld, typeof (PXViewCollection).GetField("Caches"));
                  il.Emit(OpCodes.Ldtoken, selectDacType);
                  il.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
                  {
                    typeof (RuntimeTypeHandle)
                  }));
                  il.Emit(OpCodes.Callvirt, typeof (List<System.Type>).GetMethod("Add", new System.Type[1]
                  {
                    typeof (System.Type)
                  }));
                  caches.Add(selectDacType);
                }
                else if (readonlyCaches.IndexOf(selectDacType) < 0)
                  readonlyCaches.Add(selectDacType);
              }
              if (Attribute.IsDefined((MemberInfo) field, typeof (PXViewExtensionAttribute)))
              {
                LocalBuilder local1 = il.DeclareLocal(typeof (Attribute[]));
                LocalBuilder local2 = il.DeclareLocal(typeof (int));
                LocalBuilder local3 = il.DeclareLocal(typeof (PXViewExtensionAttribute[]));
                Label label1 = il.DefineLabel();
                Label label2 = il.DefineLabel();
                il.Emit(OpCodes.Ldtoken, field);
                il.Emit(OpCodes.Ldtoken, field.DeclaringType);
                il.Emit(OpCodes.Call, typeof (System.Reflection.FieldInfo).GetMethod("GetFieldFromHandle", new System.Type[2]
                {
                  typeof (RuntimeFieldHandle),
                  typeof (RuntimeTypeHandle)
                }));
                il.Emit(OpCodes.Ldtoken, typeof (PXViewExtensionAttribute));
                il.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
                {
                  typeof (RuntimeTypeHandle)
                }));
                il.Emit(OpCodes.Call, typeof (Attribute).GetMethod("GetCustomAttributes", new System.Type[2]
                {
                  typeof (MemberInfo),
                  typeof (System.Type)
                }));
                il.Emit(OpCodes.Stloc, local1);
                il.Emit(OpCodes.Ldloc, local1);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Conv_I4);
                il.Emit(OpCodes.Newarr, typeof (PXViewExtensionAttribute));
                il.Emit(OpCodes.Stloc, local3);
                il.Emit(OpCodes.Ldarg_0);
                if (type != tgraph)
                {
                  il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
                  il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
                  il.Emit(OpCodes.Ldelem_Ref);
                }
                il.Emit(OpCodes.Castclass, type);
                il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Ldfld, typeof (PXSelectBase).GetField("View"));
                il.Emit(OpCodes.Ldloc, local3);
                il.Emit(OpCodes.Stfld, typeof (PXView).GetField("Attributes"));
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, local2);
                il.Emit(OpCodes.Br, label2);
                il.MarkLabel(label1);
                il.Emit(OpCodes.Ldloc, local1);
                il.Emit(OpCodes.Ldloc, local2);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Castclass, typeof (PXViewExtensionAttribute));
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldstr, str + field.Name);
                il.Emit(OpCodes.Callvirt, typeof (PXViewExtensionAttribute).GetMethod("ViewCreated", new System.Type[2]
                {
                  typeof (PXGraph),
                  typeof (string)
                }));
                il.Emit(OpCodes.Ldloc, local3);
                il.Emit(OpCodes.Ldloc, local2);
                il.Emit(OpCodes.Ldloc, local1);
                il.Emit(OpCodes.Ldloc, local2);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Stelem_Ref);
                il.Emit(OpCodes.Ldloc, local2);
                il.Emit(OpCodes.Ldc_I4_1);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Stloc, local2);
                il.MarkLabel(label2);
                il.Emit(OpCodes.Ldloc, local2);
                il.Emit(OpCodes.Ldloc, local1);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Conv_I4);
                il.Emit(OpCodes.Clt);
                il.Emit(OpCodes.Brtrue, label1);
              }
            }
          }
        }
        else if (field.FieldType.IsSubclassOf(typeof (PXAction)) && (!(field.DeclaringType != type) || (object) field == (object) type.GetField(field.Name)))
        {
          (System.Type cls4, (MethodInfo meth, System.Type cls3)) = GetExtensionsReverse().TakeWhile<System.Type>((Func<System.Type, bool>) (methodDeclaringType => methodDeclaringType != type)).Where<System.Type>((Func<System.Type, bool>) (methodDeclaringType => !((IEnumerable<System.Reflection.FieldInfo>) methodDeclaringType.GetFields(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Any<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (methodTypeField => methodTypeField.Name.Equals(field.Name, StringComparison.OrdinalIgnoreCase) && methodTypeField.FieldType.IsSubclassOf(typeof (PXAction)))))).Concat<System.Type>((IEnumerable<System.Type>) new System.Type[1]
          {
            type
          }).Select<System.Type, (System.Type, (MethodInfo, System.Type))>((Func<System.Type, (System.Type, (MethodInfo, System.Type))>) (methodDeclaringType => (methodDeclaringType, PXGraph.GetHandler(methodDeclaringType, type, field)))).Where<(System.Type, (MethodInfo, System.Type))>((Func<(System.Type, (MethodInfo, System.Type)), bool>) (methodAndType => methodAndType.handler.delegateType != (System.Type) null)).FirstOrDefault<(System.Type, (MethodInfo, System.Type))>();
          il.Emit(OpCodes.Ldarg_0);
          if (type != tgraph)
          {
            il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
            il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
            il.Emit(OpCodes.Ldelem_Ref);
          }
          il.Emit(OpCodes.Castclass, type);
          ConstructorInfo con = (ConstructorInfo) null;
          if (!(cls3 == (System.Type) null))
          {
            System.Type fieldType1 = field.FieldType;
            System.Type[] types1 = new System.Type[3]
            {
              typeof (PXGraph),
              cls3,
              typeof (string)
            };
            ConstructorInfo constructor;
            if ((constructor = fieldType1.GetConstructor(types1)) == (ConstructorInfo) null)
            {
              System.Type fieldType2 = field.FieldType;
              System.Type[] types2 = new System.Type[2]
              {
                typeof (PXGraph),
                cls3
              };
              if ((con = fieldType2.GetConstructor(types2)) == (ConstructorInfo) null)
                goto label_90;
            }
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldtoken, cls3);
            il.Emit(OpCodes.Call, typeof (System.Type).GetMethod("GetTypeFromHandle", new System.Type[1]
            {
              typeof (RuntimeTypeHandle)
            }));
            if (!meth.IsStatic)
            {
              il.Emit(OpCodes.Ldarg_0);
              if (cls4 != tgraph)
              {
                il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
                il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(cls4));
                il.Emit(OpCodes.Ldelem_Ref);
              }
              il.Emit(OpCodes.Castclass, cls4);
            }
            il.Emit(OpCodes.Ldtoken, meth);
            il.Emit(OpCodes.Ldtoken, cls4);
            il.Emit(OpCodes.Call, typeof (MethodBase).GetMethod("GetMethodFromHandle", new System.Type[2]
            {
              typeof (RuntimeMethodHandle),
              typeof (RuntimeTypeHandle)
            }));
            il.Emit(OpCodes.Castclass, typeof (MethodInfo));
            if (meth.IsStatic)
              il.Emit(OpCodes.Call, typeof (Delegate).GetMethod("CreateDelegate", new System.Type[2]
              {
                typeof (System.Type),
                typeof (MethodInfo)
              }));
            else
              il.Emit(OpCodes.Call, typeof (Delegate).GetMethod("CreateDelegate", new System.Type[3]
              {
                typeof (System.Type),
                typeof (object),
                typeof (MethodInfo)
              }));
            il.Emit(OpCodes.Castclass, cls3);
            if (constructor != (ConstructorInfo) null)
            {
              il.Emit(OpCodes.Ldstr, str + meth.Name);
              il.Emit(OpCodes.Newobj, constructor);
              goto label_101;
            }
            il.Emit(OpCodes.Newobj, con);
            goto label_101;
          }
label_90:
          il.Emit(OpCodes.Ldarg_0);
          il.Emit(OpCodes.Ldstr, str + field.Name);
          il.Emit(OpCodes.Newobj, field.FieldType.GetConstructor(new System.Type[2]
          {
            typeof (PXGraph),
            typeof (string)
          }));
label_101:
          il.Emit(OpCodes.Stfld, field);
          il.Emit(OpCodes.Ldarg_0);
          il.Emit(OpCodes.Ldfld, tgraph.GetField("Actions"));
          il.Emit(OpCodes.Ldstr, str + field.Name);
          il.Emit(OpCodes.Ldarg_0);
          if (type != tgraph)
          {
            il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
            il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
            il.Emit(OpCodes.Ldelem_Ref);
          }
          il.Emit(OpCodes.Castclass, type);
          il.Emit(OpCodes.Ldfld, field);
          il.Emit(OpCodes.Callvirt, typeof (PXActionCollection).GetMethod("Add", new System.Type[2]
          {
            typeof (object),
            typeof (object)
          }));
        }
      }
      foreach (System.Reflection.FieldInfo field in fieldInfoList)
      {
        System.Type genericArgument = field.FieldType.GetGenericArguments()[0];
        if (!typeof (PXGraphExtension).IsAssignableFrom(type))
          throw new PXException("PXSelectExtension<{0}> is used outside of graph extension in {1}", new object[2]
          {
            (object) genericArgument.FullName,
            (object) type.FullName
          });
        System.Type table = PXCache._mapping.GetMap(tgraph, genericArgument).Table;
        (System.Type, System.Reflection.FieldInfo) valueTuple;
        if (dictionary.TryGetValue(table, out valueTuple))
        {
          System.Type cls = valueTuple.Item1;
          il.Emit(OpCodes.Ldarg_0);
          il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
          il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(type));
          il.Emit(OpCodes.Ldelem_Ref);
          il.Emit(OpCodes.Castclass, type);
          il.Emit(OpCodes.Ldarg_0);
          if (cls != tgraph)
          {
            il.Emit(OpCodes.Ldfld, tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic));
            il.Emit(OpCodes.Ldc_I4, graphextensions.IndexOf(cls));
            il.Emit(OpCodes.Ldelem_Ref);
          }
          il.Emit(OpCodes.Castclass, cls);
          il.Emit(OpCodes.Ldfld, valueTuple.Item2);
          il.Emit(OpCodes.Newobj, field.FieldType.GetConstructor(new System.Type[1]
          {
            typeof (PXSelectBase)
          }));
          il.Emit(OpCodes.Stfld, field);
        }
      }
    }

    IEnumerable<System.Type> GetExtensionsReverse()
    {
      for (int i = graphextensions.Count - 1; i >= 0; --i)
        yield return graphextensions[i];
    }
  }

  /// <summary>
  /// Similar to the Type.GetMethod lookup, iterates over the classes starting from the one given by the base classes up to the "Object" class.
  /// Looks for methods in the current class with the name from the variable "field" and matching a valid PXAction signature.
  /// If one matching method is found in the current class, returns the MethodInfo of that method and its signature as a delegate.
  /// If more than one matching method is found in the current class, throws a PXException.
  /// If all classes have been enumerated and no suitable method has been found, returns default.
  /// </summary>
  /// <param name="methodDeclaringType">Type of the method declaring.</param>
  /// <param name="actionField">The action field.</param>
  /// <param name="methods">Delegate candidate methods grouped by declaring type. Optional parameter allows to avoid regetting the methods.</param>
  internal static (MethodInfo method, System.Type delegateType) GetHandler(
    System.Type methodDeclaringType,
    System.Type fieldDeclaringType,
    System.Reflection.FieldInfo actionField,
    ILookup<System.Type, MethodInfo> methods = null)
  {
    if (methods == null)
      methods = ((IEnumerable<MethodInfo>) methodDeclaringType.GetMethods(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (method => method.Name.Equals(actionField.Name, StringComparison.OrdinalIgnoreCase) && !Attribute.IsDefined((MemberInfo) method, typeof (PXOverrideAttribute)))).ToLookup<MethodInfo, System.Type>((Func<MethodInfo, System.Type>) (method => method.DeclaringType));
    System.Type key = methodDeclaringType;
    ICollection<(MethodInfo, System.Type)> list;
    do
    {
      list = (ICollection<(MethodInfo, System.Type)>) methods[key].Select<MethodInfo, (MethodInfo, System.Type)>((Func<MethodInfo, (MethodInfo, System.Type)>) (method => (method, PXGraph.GetActionHandlerDelegateType(method)))).Where<(MethodInfo, System.Type)>((Func<(MethodInfo, System.Type), bool>) (handler => handler.delegateType != (System.Type) null)).ToList<(MethodInfo, System.Type)>();
    }
    while (!list.Any<(MethodInfo, System.Type)>() && (key = key.BaseType) != (System.Type) null);
    switch (list.Count)
    {
      case 0:
        return ();
      case 1:
        return list.First<(MethodInfo, System.Type)>();
      default:
        throw new PXException($"Failed to determine a delegate for the {actionField} action of the {fieldDeclaringType} class (declared in the {actionField.DeclaringType} class) in the {methodDeclaringType} class because the following methods with the same name exist in the {key} class: {string.Join<MethodInfo>(", ", list.Select<(MethodInfo, System.Type), MethodInfo>((Func<(MethodInfo, System.Type), MethodInfo>) (handler => handler.method)))}.");
    }
  }

  internal static System.Type GetActionHandlerDelegateType(MethodInfo method)
  {
    ParameterInfo[] parameters = method.GetParameters();
    if (!(method.ReturnType == typeof (void)))
    {
      if (method.ReturnType == typeof (IEnumerable) && parameters.Length >= 1)
      {
        int index = parameters.Length - 2;
        if (index < PXGraph.PXButtonDelegateGenerics.Length && !((IEnumerable<ParameterInfo>) parameters).Any<ParameterInfo>((Func<ParameterInfo, bool>) (parameter => parameter.ParameterType.IsByRef)) && parameters[0].ParameterType == typeof (PXAdapter))
          return parameters.Length != 1 ? PXGraph.PXButtonDelegateGenerics[index].MakeGenericType(((IEnumerable<ParameterInfo>) parameters).Skip<ParameterInfo>(1).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (parameter => parameter.ParameterType)).ToArray<System.Type>()) : typeof (PXButtonDelegate);
      }
      return (System.Type) null;
    }
    return parameters.Length != 0 ? (System.Type) null : typeof (System.Action);
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that the request has come from the Acumatica mobile application.</summary>
  public bool IsMobile
  {
    get
    {
      if (PXGraph.IsMobileContext)
        this._isMobile = true;
      return this._isMobile;
    }
    set => this._isMobile = value;
  }

  internal static bool IsMobileContext
  {
    get => PXContext.GetSlot<bool>(nameof (IsMobileContext));
    set => PXContext.SetSlot<bool>(nameof (IsMobileContext), value);
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that a user uploads records to a grid from an Excel file.</summary>
  public bool IsImportFromExcel { get; internal set; }

  /// <summary>Indicates (if set to <tt>true</tt>) that the graph is working with an archived record and may access other archived records.</summary>
  public bool? IsArchiveContext { get; internal set; }

  /// <exclude />
  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
  }

  /// <summary>
  /// Permits to reorder or adjust containers and fields of the form metadata for the Contract-Based API.
  /// </summary>
  /// <param name="fieldsByView">List returned by the form metadata collector. It can be modified inside the method.</param>
  /// <returns>Resulting list that will be used by the Contract-Based API.</returns>
  /// <exclude />
  protected internal virtual List<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>> AdjustApiScript(
    List<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>> fieldsByView)
  {
    return fieldsByView;
  }

  /// <exclude />
  public int CopyPasteCommitChanges(
    string viewName,
    OrderedDictionary keys,
    OrderedDictionary vals)
  {
    return this.ExecuteUpdate(viewName, (IDictionary) keys, (IDictionary) vals);
  }

  /// <summary>Gets an instance of the <tt>AccessInfo</tt> DAC, which
  /// contains some application settings of the current user, such as the
  /// branch ID, user ID and name, webpage ID, and other settings. The
  /// fields of this DAC can be referenced in BQL statements through the
  /// <tt>Current</tt> parameter. For example,
  /// <tt>Current&lt;AccessInfo.branchID&gt;</tt>.</summary>
  public virtual AccessInfo Accessinfo
  {
    get
    {
      if (this.accessinfo == null)
      {
        AccessInfo accessInfo = new AccessInfo();
        accessInfo.UserName = PXAccess.GetUserName();
        accessInfo.DisplayName = PXAccess.GetUserDisplayName();
        accessInfo.UserID = PXAccess.GetUserID();
        accessInfo.CompanyName = PXDatabase.Provider.GetCompanyDisplayName();
        System.DateTime? businessDate = PXContext.GetBusinessDate();
        ref System.DateTime? local = ref businessDate;
        accessInfo.BusinessDate = new System.DateTime?(local.HasValue ? local.GetValueOrDefault().Date : PXTimeZoneInfo.Today);
        accessInfo.ContactID = PXAccess.GetContactID();
        accessInfo.BAccountID = PXAccess.GetBAccountID();
        this.accessinfo = accessInfo;
        this.InitScopedAccessInfoProperties();
      }
      return this.accessinfo;
    }
    [PXInternalUseOnly] internal set => this.accessinfo = value;
  }

  internal void InitScopedAccessInfoProperties()
  {
    if (this.accessinfo == null)
      return;
    this.accessinfo.ScreenID = PXContext.GetScreenID();
    int? branchId = PXAccess.GetBranchID();
    this.accessinfo.BranchID = branchId;
    if (branchId.HasValue)
      this.accessinfo.BaseCuryID = PXAccess.MasterBranches.GetBranch(branchId)?.BaseCuryID;
    this.accessinfo.PortalID = PortalHelper.GetPortalID();
  }

  /// <summary>Gets or sets the uniquer identifier that is used for setting
  /// up the processing operations.</summary>
  public object UID
  {
    get
    {
      if (this._UID == null)
      {
        try
        {
          if (!this.UnattendedMode)
          {
            if (HttpContext.Current != null)
            {
              IPXSessionState inner = PXContext.Session.Inner;
              if (inner != null)
              {
                object[] graphInfo = inner.GetGraphInfo(GraphSessionStatePrefix.For(this));
                if (graphInfo != null)
                  this._UID = graphInfo[2];
              }
            }
          }
        }
        catch
        {
        }
        finally
        {
          if (this._UID == null)
            this._UID = (object) Guid.NewGuid();
        }
      }
      return this._UID;
    }
    set => this._UID = value;
  }

  /// <summary>Gets or sets the culture information.</summary>
  public CultureInfo Culture
  {
    get => this.culture;
    set => this.culture = value;
  }

  /// <exclude />
  public virtual void SetOffline()
  {
    foreach (System.Type key in this.Views.Caches.ToArray())
      this.Caches[key].Interceptor = (PXDBInterceptorAttribute) new PXOfflineAttribute();
  }

  internal void LoadQueryCache()
  {
    if (this._QueriesLoaded)
      return;
    this._QueriesLoaded = true;
    if (this.UnattendedMode || HttpContext.Current == null)
      return;
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner == null)
      return;
    GraphSessionStatePrefix sessionStatePrefix = GraphSessionStatePrefix.WithoutStatePrefixFor(this);
    object[] graphInfo = inner.GetGraphInfo(sessionStatePrefix);
    if (graphInfo != null && graphInfo.Length > 3)
      this._selectedViews = graphInfo[3] as HashSet<string>;
    PXGraphQueryCacheCollection graphQueryCache = inner.GetGraphQueryCache(sessionStatePrefix);
    if (graphQueryCache == null)
      return;
    foreach (KeyValuePair<ViewKey, PXViewQueryCollection> keyValuePair in (Dictionary<ViewKey, PXViewQueryCollection>) graphQueryCache)
    {
      keyValuePair.Value.RemoveExpired();
      this.QueryCache.Add(keyValuePair.Key, keyValuePair.Value);
      foreach (PXQueryResult pxQueryResult in keyValuePair.Value.Values)
        pxQueryResult.HasPlacedNotChanged = false;
    }
  }

  internal static void ClearSessionQueryCache()
  {
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner == null)
      return;
    inner.ClearGraphQueryCache();
  }

  internal void UnloadQueryCache(IPXSessionState session)
  {
    this.UnloadQueryCache(GraphSessionStatePrefix.For(this), session);
  }

  private void UnloadQueryCache(GraphSessionStatePrefix sessionStatePrefix, IPXSessionState session)
  {
    if (PXSqlCache.IsGraphLevelCacheEnabled)
      session.SetGraphQueryCache(sessionStatePrefix, this.QueryCache.Unload());
    this.QueryCache.Clear();
    this._QueriesLoaded = false;
  }

  /// <summary>Sets the value of the specified field in the data record. The
  /// method relies on the <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)">SetValueExt(object,
  /// string, object)</see> method of the cache related to the data
  /// view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="data">The data record to update as an instance of the DAC
  /// or <tt>IDictionary</tt> of field names and field values.</param>
  /// <param name="fieldName">The name of the field to update.</param>
  /// <param name="value">The new value for the field.</param>
  public virtual void SetValueExt(string viewName, object data, string fieldName, object value)
  {
    if (!(data is PXResult))
    {
      this.Caches[this.GetItemType(viewName)].SetValueExt(data, fieldName, value);
    }
    else
    {
      int length = fieldName.IndexOf("__");
      if (length > 0 && length < fieldName.Length - 2)
      {
        System.Type itemType = ((PXResult) data).GetItemType(fieldName.Substring(0, length));
        if (itemType != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(itemType))
        {
          this.Caches[itemType].SetValueExt(((PXResult) data)[itemType], fieldName.Substring(length + 2), value);
          return;
        }
      }
      this.Caches[this.GetItemType(viewName)].SetValueExt(((PXResult) data)[0], fieldName, value);
    }
  }

  /// <summary>Sets the value of the field by field name in the data record
  /// without raising any events. The method relies on the <see cref="M:PX.Data.PXCache`1.SetValue(System.Object,System.String,System.Object)">SetValue(object,
  /// string, object)</see> method of the cache related to the data
  /// view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="data">The data record to update.</param>
  /// <param name="fieldName">The name of the field to update.</param>
  /// <param name="value">The new value for the field.</param>
  public virtual void SetValue(string viewName, object data, string fieldName, object value)
  {
    if (!(data is PXResult))
    {
      this.Caches[this.GetItemType(viewName)].SetValue(data, fieldName, value);
    }
    else
    {
      int length = fieldName.IndexOf("__");
      if (length > 0 && length < fieldName.Length - 2)
      {
        System.Type itemType = ((PXResult) data).GetItemType(fieldName.Substring(0, length));
        if (itemType != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(itemType))
        {
          this.Caches[itemType].SetValue(((PXResult) data)[itemType], fieldName.Substring(length + 2), value);
          return;
        }
      }
      this.Caches[this.GetItemType(viewName)].SetValue(((PXResult) data)[0], fieldName, value);
    }
  }

  private object enableEmptyPossible(object val, bool force)
  {
    switch (val)
    {
      case PXStringState pxStringState when pxStringState.AllowedValues != null && !pxStringState.EmptyPossible:
        if (!force)
        {
          if (!pxStringState.ExclusiveValues)
          {
            force = true;
          }
          else
          {
            string name1 = pxStringState.Name;
            if ((name1 != null ? (name1.StartsWith("Attribute") ? 1 : 0) : 0) != 0)
            {
              force = true;
            }
            else
            {
              string name2 = pxStringState.Name;
              if ((name2 != null ? (name2.EndsWith("_Attributes") ? 1 : 0) : 0) != 0)
                force = true;
              else if (((IEnumerable<string>) pxStringState.AllowedValues).Any<string>((Func<string, bool>) (_ => string.IsNullOrWhiteSpace(_))))
                force = true;
            }
          }
        }
        if (force)
          pxStringState.EmptyPossible = true;
        return val;
      case PXIntState pxIntState when pxIntState.AllowedValues != null && !pxIntState.EmptyPossible:
        if (!force)
        {
          if (!pxIntState.ExclusiveValues)
          {
            force = true;
          }
          else
          {
            string name3 = pxIntState.Name;
            if ((name3 != null ? (name3.StartsWith("Attribute") ? 1 : 0) : 0) != 0)
            {
              force = true;
            }
            else
            {
              string name4 = pxIntState.Name;
              if ((name4 != null ? (name4.EndsWith("_Attributes") ? 1 : 0) : 0) != 0)
                force = true;
            }
          }
        }
        if (force)
        {
          pxIntState.EmptyPossible = true;
          break;
        }
        break;
    }
    return val;
  }

  private object disableJoined(object val)
  {
    if (val is PXFieldState pxFieldState)
      pxFieldState.Enabled = false;
    return val;
  }

  /// <summary>Gets the value or the <tt>PXFieldState</tt> object of the
  /// specified field in the data record. The method relies on the <see cref="M:PX.Data.PXCache`1.GetValueExt(System.Object,System.String)">GetValueExt(object,
  /// string)</see> method of the cache related to the data view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="data">The data record from the cache related to the data
  /// view.</param>
  /// <param name="fieldName">The name of the field whose value or state is
  /// returned.</param>
  public virtual object GetValueExt(string viewName, object data, string fieldName)
  {
    if (!(data is PXResult))
      return this.Caches[this.GetItemType(viewName)].GetValueExt(data, fieldName);
    int length = fieldName.IndexOf("__");
    if (length <= 0 || length >= fieldName.Length - 2)
      return this.enableEmptyPossible(this.Caches[this.GetItemType(viewName)].GetValueExt(((PXResult) data)[0], fieldName), false);
    System.Type itemType = ((PXResult) data).GetItemType(fieldName.Substring(0, length));
    if (!(itemType != (System.Type) null))
      return this.enableEmptyPossible(this.disableJoined(this.Caches[this.GetItemType(viewName)].GetValueExt(((PXResult) data)[0], fieldName)), true);
    return typeof (IBqlTable).IsAssignableFrom(itemType) ? this.enableEmptyPossible(this.disableJoined(this.Caches[itemType].GetValueExt(((PXResult) data)[itemType], fieldName.Substring(length + 2))), true) : ((PXResult) data)[itemType];
  }

  /// <summary>Gets the value as the <tt>PXFieldState</tt> object of the
  /// specified field in the data record. The method relies on the <see cref="M:PX.Data.PXCache`1.GetStateExt(System.Object,System.String)">GetStateExt(object,
  /// string)</see> method of the cache.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="data">The data record from the cache related to the data
  /// view.</param>
  /// <param name="fieldName">The name of the field whose state is
  /// returned.</param>
  public virtual object GetStateExt(string viewName, object data, string fieldName)
  {
    if (data == null)
    {
      int length = fieldName.IndexOf("__");
      if (length <= 0 || length >= fieldName.Length - 2)
        return this.enableEmptyPossible(this.Caches[this.GetItemType(viewName)].GetStateExt((object) null, fieldName), false);
      string key = fieldName.Substring(0, length);
      PXCache pxCache = (PXCache) null;
      foreach (System.Type itemType in this.Views[viewName].GetItemTypes())
      {
        if (itemType.Name == key)
          pxCache = this.Caches[itemType];
      }
      if (pxCache == null)
        pxCache = this.Caches[key];
      return pxCache != null ? this.enableEmptyPossible(this.disableJoined(pxCache.GetStateExt((object) null, fieldName.Substring(length + 2))), true) : (object) null;
    }
    if (!(data is PXResult))
      return this.Caches[this.GetItemType(viewName)].GetStateExt(data, fieldName);
    int length1 = fieldName.IndexOf("__");
    if (length1 <= 0 || length1 >= fieldName.Length - 2)
      return this.enableEmptyPossible(this.Caches[this.GetItemType(viewName)].GetStateExt(((PXResult) data)[0], fieldName), false);
    System.Type itemType1 = ((PXResult) data).GetItemType(fieldName.Substring(0, length1));
    if (!(itemType1 != (System.Type) null))
      return this.enableEmptyPossible(this.disableJoined(this.Caches[this.GetItemType(viewName)].GetStateExt(((PXResult) data)[0], fieldName)), true);
    return typeof (IBqlTable).IsAssignableFrom(itemType1) ? this.disableJoined(this.Caches[itemType1].GetStateExt(((PXResult) data)[itemType1], fieldName.Substring(length1 + 2))) : ((PXResult) data)[itemType1];
  }

  /// <summary>Gets the value of the specified field in the data record
  /// without raising any events. The method relies on the <see cref="M:PX.Data.PXCache`1.GetValue(System.Object,System.String)">GetValue(
  /// object, string)</see> method of the cache related to the data
  /// view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="data">The data record from the cache related to the data
  /// view.</param>
  /// <param name="fieldName">The name of the field whose value is
  /// returned.</param>
  public virtual object GetValue(string viewName, object data, string fieldName)
  {
    if (!(data is PXResult))
      return this.Caches[this.GetItemType(viewName)].GetValue(data, fieldName);
    int length = fieldName.IndexOf("__");
    if (length <= 0 || length >= fieldName.Length - 2)
      return this.enableEmptyPossible(this.Caches[this.GetItemType(viewName)].GetValue(((PXResult) data)[0], fieldName), false);
    System.Type itemType = ((PXResult) data).GetItemType(fieldName.Substring(0, length));
    if (!(itemType != (System.Type) null))
      return this.enableEmptyPossible(this.disableJoined(this.Caches[this.GetItemType(viewName)].GetValue(((PXResult) data)[0], fieldName)), true);
    return typeof (IBqlTable).IsAssignableFrom(itemType) ? this.enableEmptyPossible(this.disableJoined(this.Caches[itemType].GetValue(((PXResult) data)[itemType], fieldName.Substring(length + 2))), true) : ((PXResult) data)[itemType];
  }

  /// <summary>Returns the names of all fields from all DACs referenced by
  /// the BQL command of the data view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public string[] GetFieldNames(string viewName)
  {
    List<string> stringList = new List<string>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (System.Type itemType in this.Views[viewName].GetItemTypes())
    {
      if (stringList.Count == 0)
      {
        stringList.AddRange((IEnumerable<string>) this.Caches[itemType].Fields);
        EnumerableExtensions.AddRange<string>((ISet<string>) stringSet, (IEnumerable<string>) stringList);
      }
      else
      {
        foreach (string field in (List<string>) this.Caches[itemType].Fields)
        {
          string str = $"{itemType.Name}__{field}";
          if (stringSet.Add(str))
            stringList.Add(str);
        }
      }
    }
    return stringList.ToArray();
  }

  /// <summary>Gets all instances of attributes placed on the specified
  /// field from the cache related to the data view. The method relies on
  /// the <see cref="M:PX.Data.PXCache`1.GetAttributes(System.String)">GetAttributes(string)</see>
  /// method of the cache.</summary>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="name">The name of the field whose attributes are
  /// returned. If <tt>null</tt>, the attributes from all fields are
  /// returned.</param>
  public PXEventSubscriberAttribute[] GetAttributes(string viewName, string name)
  {
    return this.Caches[this.GetItemType(viewName)].GetAttributes(name).ToArray();
  }

  /// <summary>Returns the status of the <tt>Current</tt> data record of the
  /// cache related to the data view. If the <tt>Current</tt> property of
  /// the cache is <tt>null</tt>, the method returns the <tt>Notchanged</tt>
  /// status.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public PXEntryStatus GetStatus(string viewName)
  {
    PXCache cach = this.Caches[this.GetItemType(viewName)];
    return cach.Current != null ? cach.GetStatus(cach.Current) : PXEntryStatus.Notchanged;
  }

  /// <summary>Returns the type of the first DAC referenced by the data
  /// view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual System.Type GetItemType(string viewName) => this.Views[viewName].GetItemType();

  /// <summary>Gets default current item</summary>
  /// <typeparam name="TNode">Type of the data item</typeparam>
  /// <returns>Data item</returns>
  internal TNode GetDefault<TNode>() where TNode : class
  {
    PXGraph.GetDefaultDelegate getDefaultDelegate = (PXGraph.GetDefaultDelegate) null;
    if (this.Defaults != null)
      this.Defaults.TryGetValue(typeof (TNode), out getDefaultDelegate);
    return getDefaultDelegate != null ? getDefaultDelegate() as TNode : default (TNode);
  }

  /// <summary>Gets or sets the value of the global timestamp.</summary>
  public byte[] TimeStamp
  {
    get => this._TimeStamp;
    set => this._TimeStamp = value;
  }

  /// <summary>
  /// Indicates (if set to <tt>true</tt>) that the <b>Processing</b> dialog box is displayed on the processing page.
  /// You override this property in a processing graph to not display the <b>Processing</b> dialog box on the processing page.
  /// </summary>
  /// <value>If the value is <tt>false</tt>, the progress and the result of the processing are displayed on the page toolbar.
  /// By default, the value is <tt>true</tt> (the <b>Processing</b> dialog box is displayed).</value>
  /// <example>The following example shows the property overriden in a custom <tt>SalesOrderProcess</tt> graph.
  /// <code>
  /// public class SalesOrderProcess : PXGraph&lt;SalesOrderProcess&gt;
  /// {
  ///     ...
  ///     public override bool IsProcessing
  ///     {
  ///         get { return false; }
  ///         set { }
  ///     }
  /// }
  /// </code>
  /// </example>
  public virtual bool IsProcessing { get; set; }

  protected PXGraphExtension[] GraphExtensions => this.Extensions;

  public PXGraphExtension GetExtension(int at) => this.Extensions[at];

  /// <summary>Returns the instance of the graph extension of the specified
  /// type. The type of the extension is specified in the type
  /// parameter.</summary>
  /// <example>
  /// An extension of a graph is a class that derives from the
  /// <tt>PXGraphExtension&lt;&gt;</tt> type. The example below shows the
  /// definition of an extension on the <tt>InventoryItemMaint</tt> graph.
  /// <code>
  /// public class InventoryItemMaintExtension :
  ///     PXGraphExtension&lt;InventoryItemMaint&gt;
  /// {
  ///     public void SomeMethod()
  ///     {
  ///         // The Base variable references the instance of InventoryItemMaint
  ///         InventoryItemMaintExtension ext =
  ///             Base.GetExtension&lt;InventoryItemMaintExtension&gt;();
  ///         ...
  ///     }
  /// }</code>
  /// </example>
  public virtual Extension GetExtension<Extension>() where Extension : PXGraphExtension
  {
    return (Extension) ((IEnumerable<PXGraphExtension>) this.Extensions).FirstOrDefault<PXGraphExtension>((Func<PXGraphExtension, bool>) (item => CustomizedTypeManager.GetTypeNotCustomized(item.GetType()) == typeof (Extension)));
  }

  /// <summary>Initializes the cache mapping.</summary>
  /// <param name="map">A collection of cache mappings.</param>
  /// <remarks>
  ///   <para>In the override method, you need to call
  /// the base <see cref="M:PX.Data.PXGraph.InitCacheMapping(System.Collections.Generic.Dictionary{System.Type,System.Type})" /> method
  /// of the graph before specifying a custom cache mapping.</para>
  ///   <para>For details about cache mapping,
  ///   see <a href="https://help.acumatica.com/Help?ScreenId=ShowWiki&amp;pageid=a5fb7e46-c6e8-438a-b640-1fa9f56b9525" target="_blank">Cache Mapping</a>.</para>
  /// </remarks>
  /// <example>
  /// You can use the <paramref name="map" /> parameter to edit the collection of cache mappings.
  /// The following code from ARInvoiceEntry demonstrates this approach.
  /// <code>public override void InitCacheMapping(Dictionary&lt;Type, Type&gt; map)
  /// {
  ///     base.InitCacheMapping(map);
  ///     map.Add(typeof(CT.Contract), typeof(CT.Contract));
  /// }</code></example>
  /// <inheritdoc cref="M:PX.Data.PXCacheCollection.AddCacheMappingsWithInheritance(PX.Data.PXGraph,System.Type)" path="/example" />
  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    if (this.GetType() == typeof (PXGraph) || this.GetType() == typeof (PXGenericInqGrph) || this.GetType() == typeof (GenericInquiryDesigner))
      return;
    string primaryView = this.PrimaryView;
    if (string.IsNullOrEmpty(primaryView) || !this.Views.ContainsKey(primaryView))
      return;
    System.Type t = this.Views[primaryView].GetItemType();
    EnumerableExtensions.ForEach<System.Type>(t.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)).TakeWhile<System.Type>((Func<System.Type, bool>) (_ => _ != typeof (object))), (System.Action<System.Type>) (_ => map[_] = t));
  }

  public virtual IEnumerable<Extension> FindAllImplementations<Extension>() where Extension : class
  {
    return this.Extensions.OfType<Extension>();
  }

  public virtual Extension FindImplementation<Extension>() where Extension : class
  {
    PXGraphExtension[] extensions = this.Extensions;
    return (extensions != null ? ((IEnumerable<PXGraphExtension>) extensions).FirstOrDefault<PXGraphExtension>((Func<PXGraphExtension, bool>) (item => typeof (Extension).IsAssignableFrom(item.GetType()))) : (PXGraphExtension) null) as Extension;
  }

  /// <summary>Retrieves the names of all data views defined in the
  /// graph.</summary>
  public virtual IEnumerable<string> GetViewNames()
  {
    if (this._viewNames == null)
    {
      List<string> stringList = new List<string>((IEnumerable<string>) this.Views.Keys);
      int[] array = new int[stringList.Count];
      int[] indexbytype = new int[stringList.Count];
      Dictionary<System.Type, int> dictionary = new Dictionary<System.Type, int>(this.Views.Caches.Count);
      for (int index = this.Views.Caches.Count - 1; index >= 0; --index)
        dictionary[this.Views.Caches[index]] = index;
      for (int index = 0; index < stringList.Count; ++index)
      {
        int num = -1;
        indexbytype[index] = !dictionary.TryGetValue(this.Views[stringList[index]].GetItemType(), out num) ? -1 : num;
        array[index] = index;
      }
      Array.Sort<int>(array, (Comparison<int>) ((v1, v2) =>
      {
        int num1 = indexbytype[v1];
        int num2 = indexbytype[v2];
        if (num1 >= 0 && num2 == -1)
          return -1;
        if (num1 == -1 && num2 >= 0)
          return 1;
        if (num1 < num2)
          return -1;
        if (num1 > num2)
          return 1;
        int num3 = v1;
        int num4 = v2;
        if (num3 < num4)
          return -1;
        return num3 > num4 ? 1 : 0;
      }));
      string[] collection = new string[stringList.Count];
      for (int index = 0; index < array.Length; ++index)
        collection[index] = stringList[array[index]];
      this._viewNames = new List<string>((IEnumerable<string>) collection);
    }
    return (IEnumerable<string>) this._viewNames;
  }

  /// <summary>Returns pairs of the names of the fields by which the data
  /// view result will be sorted and values indicating if the sort by the
  /// field is descending.</summary>
  /// <param name="viewName">The name of the data view.</param>
  internal KeyValuePair<string, bool>[] GetSortColumnsWithUpdateSelect(string viewName)
  {
    return this.Views[viewName].GetSortColumnsWithUpdateSelect();
  }

  internal KeyValuePair<string, bool>[] GetSortColumnsWithUpdateSelectWithoutExtSort(string viewName)
  {
    return this.Views[viewName].GetSortColumnsWithUpdateSelectWithoutExtSort();
  }

  /// <summary>Returns pairs of the names of the fields by which the data
  /// view result will be sorted and values indicating if the sort by the
  /// field is descending.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual KeyValuePair<string, bool>[] GetSortColumns(string viewName)
  {
    return this.Views[viewName].GetSortColumns();
  }

  /// <summary>Returns pairs of the names of the fields by which the data
  /// view result will be sorted and values indicating if the sort by the
  /// field is descending.</summary>
  /// <param name="viewName">The name of the data view.</param>
  internal virtual KeyValuePair<string, bool>[] GetDefaultSortColumns(string viewName)
  {
    return this.Views[viewName].GetSortColumns(false);
  }

  private static PXGraph.GraphStaticInfo _Initialize(System.Type graphType)
  {
    PXGraph.GraphStaticInfo other = (PXGraph.GraphStaticInfo) null;
    PXExtensionManager.GraphKind key1 = new PXExtensionManager.GraphKind(graphType);
    Dictionary<PXExtensionManager.GraphKind, PXGraph.GraphStaticInfo> dictionary1 = (Dictionary<PXExtensionManager.GraphKind, PXGraph.GraphStaticInfo>) PXContext.GetSlot<PXGraph.GraphStaticInfoDictionary>();
    if (dictionary1 == null)
    {
      try
      {
        dictionary1 = (Dictionary<PXExtensionManager.GraphKind, PXGraph.GraphStaticInfo>) PXContext.SetSlot<PXGraph.GraphStaticInfoDictionary>(PXDatabase.GetSlot<PXGraph.GraphStaticInfoDictionary>("GraphStaticInfo", typeof (PXGraph.FeaturesSet)));
      }
      catch
      {
      }
    }
    if (dictionary1 != null)
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXExtensionManager._StaticInfoLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        dictionary1.TryGetValue(key1, out other);
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }
    if (other == null)
    {
      Dictionary<string, System.Type> inactiveViews;
      Dictionary<string, string> inactiveActions;
      List<System.Type> extensions = PXExtensionManager.GetExtensions(graphType, dictionary1 != null, out inactiveViews, out inactiveActions);
      PXExtensionManager.ListOfTypes key2 = new PXExtensionManager.ListOfTypes(extensions);
      PXReaderWriterScope readerWriterScope1;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope1).\u002Ector(PXExtensionManager._StaticInfoLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope1).AcquireReaderLock();
        Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo> dictionary2;
        if (PXExtensionManager._GraphStaticInfo.TryGetValue(graphType, out dictionary2))
        {
          if (dictionary2.TryGetValue(key2, out other))
          {
            if (dictionary1 != null)
            {
              ((PXReaderWriterScope) ref readerWriterScope1).UpgradeToWriterLock();
              dictionary1[key1] = other = new PXGraph.GraphStaticInfo(other);
            }
          }
        }
      }
      finally
      {
        readerWriterScope1.Dispose();
      }
      if (other == null)
      {
        other = PXGraph.InitGraphStaticInfo(graphType, extensions, inactiveViews, inactiveActions);
        PXReaderWriterScope readerWriterScope2;
        // ISSUE: explicit constructor call
        ((PXReaderWriterScope) ref readerWriterScope2).\u002Ector(PXExtensionManager._StaticInfoLock);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope2).AcquireWriterLock();
          Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo> dictionary3;
          if (!PXExtensionManager._GraphStaticInfo.TryGetValue(graphType, out dictionary3))
            PXExtensionManager._GraphStaticInfo[graphType] = dictionary3 = new Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo>();
          dictionary3[key2] = other;
          if (dictionary1 != null)
            dictionary1[key1] = other = new PXGraph.GraphStaticInfo(other);
        }
        finally
        {
          readerWriterScope2.Dispose();
        }
      }
    }
    if (other.AlteredSource != null && other.AlteredDescriptor == null)
    {
      List<System.Type> extensions = PXExtensionManager.GetExtensions(graphType, dictionary1 != null);
      Dictionary<System.Type, PXCache.AlteredDescriptor> dictionary4 = new Dictionary<System.Type, PXCache.AlteredDescriptor>();
      Dictionary<System.Type, List<System.Type>> dictionary5 = new Dictionary<System.Type, List<System.Type>>(other.AlteredSource.Count);
      foreach (KeyValuePair<System.Type, PXCache.AlteredSource> keyValuePair in other.AlteredSource)
        dictionary5.Add(keyValuePair.Key, PXCache._GetExtensions(keyValuePair.Key, dictionary1 != null));
      PXReaderWriterScope readerWriterScope3;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope3).\u002Ector(PXExtensionManager._StaticInfoLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope3).AcquireReaderLock();
        Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>> dictionary6;
        if (PXExtensionManager._AttributesStaticInfo.TryGetValue(graphType, out dictionary6))
        {
          foreach (KeyValuePair<System.Type, PXCache.AlteredSource> keyValuePair in other.AlteredSource)
          {
            List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) extensions);
            types.AddRange((IEnumerable<System.Type>) dictionary5[keyValuePair.Key]);
            Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor> dictionary7;
            PXCache.AlteredDescriptor alteredDescriptor;
            if (dictionary6.TryGetValue(keyValuePair.Key, out dictionary7) && dictionary7.TryGetValue(new PXExtensionManager.ListOfTypes(types), out alteredDescriptor))
              dictionary4[keyValuePair.Key] = alteredDescriptor;
          }
        }
      }
      finally
      {
        readerWriterScope3.Dispose();
      }
      if (dictionary4.Count == other.AlteredSource.Count)
      {
        other.AlteredDescriptor = dictionary4;
      }
      else
      {
        Dictionary<System.Type, PXCache.AlteredDescriptor> dictionary8 = new Dictionary<System.Type, PXCache.AlteredDescriptor>();
        foreach (KeyValuePair<System.Type, PXCache.AlteredSource> keyValuePair in other.AlteredSource)
        {
          if (!dictionary4.ContainsKey(keyValuePair.Key))
          {
            PXCache.AlteredDescriptor alteredDescriptor = new PXCache.AlteredDescriptor(keyValuePair.Value.Attributes, keyValuePair.Value.Fields, keyValuePair.Value.Method, keyValuePair.Value.CacheType);
            dictionary4[keyValuePair.Key] = alteredDescriptor;
            dictionary8[keyValuePair.Key] = alteredDescriptor;
          }
        }
        PXReaderWriterScope readerWriterScope4;
        // ISSUE: explicit constructor call
        ((PXReaderWriterScope) ref readerWriterScope4).\u002Ector(PXExtensionManager._StaticInfoLock);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope4).AcquireWriterLock();
          Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>> dictionary9;
          if (!PXExtensionManager._AttributesStaticInfo.TryGetValue(graphType, out dictionary9))
            PXExtensionManager._AttributesStaticInfo[graphType] = dictionary9 = new Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>>();
          foreach (KeyValuePair<System.Type, PXCache.AlteredDescriptor> keyValuePair in dictionary8)
          {
            List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) extensions);
            if (keyValuePair.Value._ExtensionTypes != null)
              types.AddRange((IEnumerable<System.Type>) keyValuePair.Value._ExtensionTypes);
            Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor> dictionary10;
            if (!dictionary9.TryGetValue(keyValuePair.Key, out dictionary10))
              dictionary9[keyValuePair.Key] = dictionary10 = new Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>();
            dictionary10[new PXExtensionManager.ListOfTypes(types)] = keyValuePair.Value;
          }
        }
        finally
        {
          readerWriterScope4.Dispose();
        }
      }
      other.AlteredDescriptor = dictionary4;
    }
    return other;
  }

  /// <summary>The <tt>PXGraph</tt> constructor is not called directly. To initialize a new instance of the <tt>PXGraph</tt> or <tt>PXGraph&lt;&gt;</tt> class, use the
  /// <tt>CreateInstance&lt;&gt;()</tt> method. Classes that derive from <tt>PXGraph&lt;&gt;</tt> (graphs) can define their own constructors without parameters to
  /// perform layout configuration or configure background processing operations.</summary>
  public PXGraph()
  {
    this.StatePrefix = PXGraph.GraphStatePrefix ?? "";
    if (!PXGraph.IsCreateInstance && this.GetType().IsSubclassOf(typeof (PXGraph)))
    {
      HttpContext current = HttpContext.Current;
    }
    this.Caches = this.CreateCacheCollection();
    this.Views = this.CreateViewCollection();
    this.ViewNames = new Dictionary<PXView, string>();
    this.TypedViews = new PXTypedViewCollection(this);
    this.Actions = new PXActionCollection(this);
    this.Defaults = new Dictionary<System.Type, PXGraph.GetDefaultDelegate>();
    this._LazyDialect = new Lazy<ISqlDialect>((Func<ISqlDialect>) (() => PXDatabase.Provider.SqlDialect));
    this.RaisePrepare();
    this.IsImport = SyScope.IsScoped(out this.IsExport, out this.IsContractBasedAPI);
    this.ExceptionRollback = new SessionRollback(this);
    this.VersionedState = new VersionedState(this);
    if (this.GetType() != typeof (PXGraph))
    {
      this.UnattendedMode = !PXPreserveScope.IsScoped();
      this.IsPageGeneratorRequest = PXPreserveScope.IsPageGeneratorRequest();
      this.FullTrust = PXPreserveScope.IsFullTrust();
      this.LoadQueryCache();
      PXGraph.GraphStaticInfo graphStaticInfo = PXGraph._Initialize(this.GetType());
      this.Prototype = graphStaticInfo.Prototype;
      this._AlteredDescriptors = graphStaticInfo.AlteredDescriptor;
      this.IsInitializing = true;
      graphStaticInfo.InitializeDelegate(this);
      this.IsInitializing = false;
      this._InactiveViews = graphStaticInfo.InactiveViews.Where<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (_ => !this.Views.ContainsKey(_.Key))).ToDictionary<KeyValuePair<string, System.Type>, string, System.Type>((Func<KeyValuePair<string, System.Type>, string>) (_ => _.Key), (Func<KeyValuePair<string, System.Type>, System.Type>) (_ => _.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._InactiveActions = graphStaticInfo.InactiveActions.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (_ => !this.Actions.Contains((object) _.Key))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (_ => _.Key), (Func<KeyValuePair<string, string>, string>) (_ => _.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (PXGraph.ProxyIsActive || PXGraph.GeneratorIsActive)
      {
        foreach (KeyValuePair<string, System.Type> inactiveView in this._InactiveViews)
        {
          if (!inactiveView.Value.IsGenericType || !(inactiveView.Value.GetGenericTypeDefinition() == typeof (PXSelectExtension<>)))
            this.Views[inactiveView.Key] = ((PXSelectBase) Activator.CreateInstance(inactiveView.Value, (object) this)).View;
        }
      }
      if (PXGraph.GraphInstanceCreating != this.GetType() && this.Extensions != null && this.Extensions.Length != 0)
      {
        foreach (PXGraphExtension extension in this.Extensions)
          extension.Initialize();
      }
      PXGraph.GraphInstanceCreating = (System.Type) null;
      this.RaiseInitialized();
      PXGraph.InstanceCreated.OnInstanceCreated(this);
      if (string.IsNullOrEmpty(this.PrimaryView))
        return;
      this.RowSelected.AddHandler(this.PrimaryView, new PXRowSelected(this.ShowRecordIsArchivedWarning));
    }
    else
    {
      this._InactiveViews = new Dictionary<string, System.Type>();
      this._InactiveActions = new Dictionary<string, string>();
      this.Prototype = new PXGraph.PXGraphPrototype();
    }
  }

  public virtual void Configure(PXScreenConfiguration graph)
  {
  }

  [InjectDependency]
  public IGraphLongOperationManager LongOperationManager
  {
    get
    {
      return this._longOperationManager ?? throw new InvalidOperationException("LongOperationManager wasn't set up");
    }
    internal set => this._longOperationManager = value;
  }

  [InjectDependency]
  internal PXWorkflowService WorkflowService { get; set; }

  [InjectDependency]
  internal IBusinessProcessEventProcessor BusinessProcessEventProcessor { get; set; }

  [InjectDependency]
  private IDacDescriptorProvider DacDescriptorProvider { get; set; }

  public bool IsInitializing { get; private set; }

  /// <summary>
  /// Returns new cache collection instance.
  /// IMPORTANT NOTE: THIS METHOD HAS TO BE PURE BECAUSE IT IS CALLED FROM THE CONSTRUCTOR.
  /// </summary>
  protected virtual PXCacheCollection CreateCacheCollection() => new PXCacheCollection(this);

  /// <summary>
  /// Returns new view collection instance.
  /// IMPORTANT NOTE: THIS METHOD HAS TO BE PURE BECAUSE IT IS CALLED FROM THE CONSTRUCTOR.
  /// </summary>
  protected virtual PXViewCollection CreateViewCollection() => new PXViewCollection(this);

  /// <summary>Retrieves the timestamp value from the database and stores
  /// this value in the <tt>TimeStamp</tt> property of the graph.</summary>
  public virtual void SelectTimeStamp()
  {
    this._TimeStamp = PXDatabase.SelectTimeStamp();
    this._primaryRecordTimeStamp = (byte[]) null;
  }

  /// <summary>Executes the specified data view and returns the data records
  /// the data view selects.</summary>
  /// <remarks>
  /// <para>The method raises the <tt>RowSelected</tt> event for each
  /// retrieved data record and sets the <tt>Current</tt> property of the
  /// cache to the last data record retrieved.</para>
  /// <para>The method is used by the user interface. The application code
  /// does not typically need to use this method and selects the data
  /// directly through the data views.</para>
  /// </remarks>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="parameters">Parameters for the BQL command.</param>
  /// <param name="searches">The values by which the data is
  /// filtered.</param>
  /// <param name="sortcolumns">The fields by which the if sorted and
  /// filtered (the filtering values are provided in the <tt>searches</tt>
  /// parameter)</param>
  /// <param name="(ref) startRow(ref)startRow">The index of the data record to start
  /// retreiving with (after filtering by the <tt>searches</tt>
  /// parameter).</param>
  /// <param name="maximumRows">The maximum number of data records to
  /// retrieve.</param>
  /// <param name="(ref) totalRows(ref)totalRows">The total amount of data records in the
  /// resultset.</param>
  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return this.ExecuteSelect(viewName, (object[]) null, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public IEnumerable ExecuteSelect(
    string viewName,
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
    return this.ExecuteSelect(viewName, currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows, false);
  }

  /// <exclude />
  internal IEnumerable ExecuteSelect(
    string viewName,
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows,
    bool skipHints)
  {
    if ((maximumRows == 0 || maximumRows > 10) && (PXGraph.ProxyIsActive || PXGraph.GeneratorIsActive) && !PXGraph.ExportIsActive)
      maximumRows = 3;
    this._selectedViews.Add(viewName);
    if (this._TimeStamp == null)
      this.SelectTimeStamp();
    PXView view = this.Views[viewName];
    PXCache cache = view.Cache;
    if (!cache.AllowSelect)
      return (IEnumerable) new List<object>();
    bool skipQueryHints = view.SkipQueryHints;
    List<object> objectList = (List<object>) null;
    try
    {
      this._ForceUnattended = true;
      view.SkipQueryHints = skipHints;
      objectList = view.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      throw;
    }
    finally
    {
      this._ForceUnattended = false;
      view.SkipQueryHints = skipQueryHints;
    }
    if (!this.Views[viewName].IsReadOnly)
    {
      for (int index = objectList.Count - 1; index >= 0; --index)
      {
        object obj = objectList[index];
        cache.Current = obj is PXResult ? ((PXResult) obj)[0] : obj;
      }
      this.EnsureIfArchived(viewName);
    }
    if (objectList.Count > 0 && objectList[0] is PXResult)
    {
      PXResult pxResult = (PXResult) objectList[0];
      for (int i = 1; i < pxResult.TableCount; ++i)
      {
        System.Type itemType = pxResult.GetItemType(i);
        PXCache pxCache;
        if (typeof (IBqlTable).IsAssignableFrom(itemType) && this.Caches.TryGetValue(itemType, out pxCache) && !pxCache.AllowSelect)
        {
          object instance = pxCache.CreateInstance();
          for (int index = 0; index < objectList.Count; ++index)
          {
            if (objectList[index] is PXResult && ((PXResult) objectList[index]).Items.Length > i)
              ((PXResult) objectList[index]).Items[i] = instance;
          }
        }
      }
    }
    return (IEnumerable) objectList;
  }

  /// <summary>Gets the name of the primary data view.</summary>
  public virtual string PrimaryView
  {
    get
    {
      if (this._PrimaryView == null)
      {
        string[] dataMembers = PXPageIndexingService.GetDataMembers(CustomizedTypeManager.GetTypeNotCustomized(this).FullName);
        this._PrimaryView = dataMembers == null || dataMembers.Length == 0 || dataMembers[0] == null ? PXContext.GetSlot<string>(CustomizedTypeManager.GetTypeNotCustomized(this).FullName + "$PrimaryView") ?? "" : dataMembers[0];
      }
      return this._PrimaryView;
    }
  }

  /// <summary>Gets the type of the primary view DAC.</summary>
  public virtual System.Type PrimaryItemType
  {
    get
    {
      if (this._PrimaryItemType == (System.Type) null && !string.IsNullOrWhiteSpace(this.PrimaryView) && this.Views.ContainsKey(this.PrimaryView))
        this._PrimaryItemType = this.Views[this.PrimaryView].GetItemType();
      return this._PrimaryItemType;
    }
    internal set => this._PrimaryItemType = value;
  }

  protected internal void _RecordCachedSlot(System.Type entity, object slot, Func<object> getter)
  {
    this._CachedSlots[entity] = new Tuple<object, Func<object>>(slot, getter);
  }

  protected internal bool _HasChangedSlots()
  {
    foreach (Tuple<object, Func<object>> tuple in this._CachedSlots.Values)
    {
      if (tuple.Item1 != tuple.Item2())
        return true;
    }
    return false;
  }

  /// <summary>Updates a data record in the cache related to the data view
  /// by invoking the <see cref="M:PX.Data.PXCache`1.Update(System.Collections.IDictionary,System.Collections.IDictionary)">Update(IDictionary,IDictionary)</see>
  /// method on the cache. Returns 1 in case of successful update and 0
  /// otherwise.</summary>
  /// <remarks>The method is used by the user interface.</remarks>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="keys">The keys that identify the data record.</param>
  /// <param name="values">The new values of the data record fields.</param>
  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (this._TimeStamp == null)
      this.SelectTimeStamp();
    PXCache cache = this.Views[viewName].Cache;
    int num = 0;
    try
    {
      num = cache.Update(keys, values);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      if (ex is PXBaseRedirectException)
        throw;
      this.ExceptionRollback.ProcessUpdateException<Exception>(ex, cache, keys, values);
    }
    if (num > 0)
      this._ProcessTail(viewName, values, parameters);
    return num;
  }

  protected void _ProcessTail(string viewName, IDictionary values, object[] parameters)
  {
    PXView view = this.Views[viewName];
    PXCache cache = view.Cache;
    List<object> list = new List<object>();
    view.AppendTail(cache.Current, list, view.PrepareParameters((object[]) null, parameters));
    if (list.Count > 0 && list[0] is PXResult pxResult)
    {
      foreach (string key in values.Keys.ToArray<string>())
      {
        int length = key.IndexOf("__");
        if (length > 0 && length < key.Length - 2)
        {
          System.Type itemType = pxResult.GetItemType(key.Substring(0, length));
          if (itemType != (System.Type) null)
            values[(object) key] = this.enableEmptyPossible(this.disableJoined(this.Caches[itemType].GetValueExt(pxResult[itemType], key.Substring(length + 2))), true);
        }
        else
          values[(object) key] = this.enableEmptyPossible(values[(object) key], false);
      }
    }
    this._UpdatePrimaryView(viewName);
  }

  protected internal void _UpdatePrimaryView(string viewName)
  {
    if (string.IsNullOrEmpty(this.PrimaryView) || string.Equals(this.PrimaryView, viewName) || viewName.EndsWith("$FilterHeader", StringComparison.OrdinalIgnoreCase) || viewName.EndsWith("$FilterRow", StringComparison.OrdinalIgnoreCase))
      return;
    PXCache cache = this.Views[this.PrimaryView].Cache;
    PXEntryStatus status;
    if (cache.Current == null || (status = cache.GetStatus(cache.Current)) != PXEntryStatus.Notchanged && status != PXEntryStatus.Held)
      return;
    bool isDirty = cache.IsDirty;
    try
    {
      cache.Update(cache.Current);
    }
    finally
    {
      cache.IsDirty = isDirty;
    }
  }

  /// <summary>Deletes the data record from the cache related to the data
  /// view by invoking the <see cref="M:PX.Data.PXCache`1.Delete(System.Collections.IDictionary,System.Collections.IDictionary)">Delete(IDictionary,IDictionary)</see>
  /// method on the cache. Returns 1 in case of successful deletion and 0
  /// otherwise.</summary>
  /// <remarks>The method is used by the user interface.</remarks>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="keys">The keys that identify the data record.</param>
  /// <param name="values">The values of the data record fields.</param>
  public virtual int ExecuteDelete(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (this._TimeStamp == null)
      this.SelectTimeStamp();
    PXCache cache = this.Views[viewName].Cache;
    try
    {
      int num = cache.Delete(keys, values);
      if (num > 0)
        this._UpdatePrimaryView(viewName);
      return num;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      if (!(ex is PXBaseRedirectException))
        this.ExceptionRollback.ProcessDeleteException<Exception>(ex);
      throw;
    }
  }

  /// <summary>Inserts a new data record into the cache related to the data
  /// view by invoking the <see cref="M:PX.Data.PXCache`1.Insert(System.Collections.IDictionary)">Insert(IDictionary)</see>
  /// method on the cache. Returns 1 in case of successful insertion and 0
  /// otherwise.</summary>
  /// <remarks>The method is used by the user interface.</remarks>
  /// <param name="viewName">The name of the data view.</param>
  /// <param name="values">The values to populates the data record fields
  /// .</param>
  public virtual int ExecuteInsert(string viewName, IDictionary values, params object[] parameters)
  {
    PXCache cache = this.Views[viewName].Cache;
    int num;
    try
    {
      num = cache.Insert(values);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      if (!(ex is PXBaseRedirectException))
        this.ExceptionRollback.ProcessInsertException<Exception>(ex);
      throw;
    }
    if (num > 0)
      this._ProcessTail(viewName, values, parameters);
    return num;
  }

  /// <summary>Stores the graph state and the modified data records from all
  /// caches to the user session.</summary>
  /// <remarks>The instance of the graph is destroyed at the end of the each
  /// callback. To preserve user data not saved in the database between
  /// callbacks, the caches of modified data record are serialized to the
  /// session using this method.</remarks>
  public virtual void Unload()
  {
    this.ValidateDataConsistency();
    this.ExceptionRollback.OnUnload();
    this.Caches._cacheLogger.Report(this);
    if (this.IsSessionReadOnly)
      return;
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner != null)
    {
      if (this.IsReusableGraph && !this.ReuseRestricted && !this.NeedFullUnload)
      {
        this.RaiseRequestCompleted();
        PXReusableGraphFactory.Unload(this, inner);
        this.ThrowIfDataConsistencyIssueSet();
        return;
      }
      this.EnsureIfArchived();
      if (this._TimeStamp == null && this._VeryFirstTimeStamp != null && !string.IsNullOrEmpty(this.PrimaryView))
      {
        PXCache cache = this.Views[this.PrimaryView].Cache;
        if (cache.Current != null && cache.GetStatus(cache.Current) != PXEntryStatus.Inserted)
          this._TimeStamp = this._VeryFirstTimeStamp;
      }
      this.RaiseBeforeUnloadEvent();
      GraphSessionStatePrefix sessionStatePrefix = GraphSessionStatePrefix.For(this);
      bool flag;
      if (flag = this._ReWriteUnloadData || inner.GetGraphInfo(sessionStatePrefix) == null)
        inner.SetGraphInfo(sessionStatePrefix, new object[12]
        {
          (object) this._TimeStamp,
          (object) this.accessinfo,
          this._UID,
          (object) this._selectedViews,
          (object) this.WorkflowID,
          (object) this.WorkflowStepID,
          (object) this.NonUIErrors,
          (object) this._primaryRecordTimeStamp,
          (object) this.ShouldSaveVersionModified,
          (object) this.IsInVersionModifiedState,
          (object) this.IsArchiveContext,
          (object) this.NavigationParams
        });
      if (flag)
      {
        if (this.UnattendedMode)
        {
          foreach (System.Type key in this.Views.RestorableCaches.ToArray())
          {
            PXCache cach = this.Caches[key];
          }
        }
        PXCache[] array = new PXCache[this.Caches.Count];
        try
        {
          this.Caches.Values.CopyTo(array, 0);
        }
        catch (ArgumentException ex)
        {
          array = new PXCache[this.Caches.Count + 5];
          this.Caches.Values.CopyTo(array, 0);
        }
        foreach (PXCache pxCache in array)
          pxCache?.Unload();
        this.UnloadQueryCache(sessionStatePrefix, inner);
      }
    }
    this.QueryCache.Clear();
    PXView[] array1 = new PXView[this.Views.Count];
    try
    {
      this.Views.Values.CopyTo(array1, 0);
    }
    catch (ArgumentException ex)
    {
      array1 = new PXView[this.Views.Count + 5];
      this.Views.Values.CopyTo(array1, 0);
    }
    foreach (PXView pxView in array1)
      pxView?.DetachCache();
    this.NeedFullUnload = false;
    this.ThrowIfDataConsistencyIssueSet();
  }

  protected internal void EnsureIfArchived(PXView view)
  {
    string str;
    if (string.IsNullOrEmpty(this.PrimaryView) || !this.ViewNames.TryGetValue(view, out str) || !(str == this.PrimaryView))
      return;
    this.EnsureIfArchived();
  }

  protected internal void EnsureIfArchived(string viewName)
  {
    if (string.IsNullOrEmpty(this.PrimaryView) || !(viewName == this.PrimaryView))
      return;
    this.EnsureIfArchived();
  }

  protected internal void EnsureIfArchived()
  {
    PXCache cache = string.IsNullOrEmpty(this.PrimaryView) ? (PXCache) null : this.Views[this.PrimaryView].Cache;
    this.IsArchiveContext = cache == null || !cache.HasRecordStatusSupport() ? new bool?() : new bool?(cache.IsArchived(cache.Current));
  }

  private void ShowRecordIsArchivedWarning(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!this.IsArchiveContext.HasValue || e.Row == null || cache.Fields.Count <= 0)
      return;
    cache.RaiseExceptionHandling(cache.Keys.FirstOrDefault<string>() ?? cache.Fields.FirstOrDefault<string>(), e.Row, (object) null, this.IsArchiveContext.Value ? (Exception) new PXSetPropertyException("The current record is archived.", PXErrorLevel.RowWarning) : (Exception) null);
  }

  private bool IsDefaultArchiveContextNeeded()
  {
    PXCache cache = string.IsNullOrEmpty(this.PrimaryView) ? (PXCache) null : this.Views[this.PrimaryView].Cache;
    return cache != null && cache.Current != null && cache.HasRecordStatusSupport() && PXGraph.ContainsOnlyThis(cache.Inserted, cache.Current);
  }

  private void SetDefaultArchiveContext()
  {
    PXCache cache = string.IsNullOrEmpty(this.PrimaryView) ? (PXCache) null : this.Views[this.PrimaryView].Cache;
    if (cache == null || cache.Current == null || !cache.HasRecordStatusSupport())
      return;
    cache.SetArchived(cache.Current, false);
  }

  private static bool ContainsOnlyThis(IEnumerable source, object item)
  {
    IEnumerator enumerator = source.GetEnumerator();
    try
    {
      return enumerator.MoveNext() && enumerator.Current == item && !enumerator.MoveNext();
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
  }

  internal PXCache _GetReadonlyCache(System.Type key)
  {
    if (this._ReadonlyCacheCreation)
      return this.Caches[key];
    this._ReadonlyCacheCreation = true;
    try
    {
      return this.Caches[key];
    }
    finally
    {
      this._ReadonlyCacheCreation = false;
    }
  }

  /// <summary>Loads the state of the graph and caches from the
  /// session.</summary>
  /// <remarks>The state is stored in the session through the <see cref="M:PX.Data.PXGraph.Unload">Unload()</see>
  /// method.</remarks>
  public virtual void Load()
  {
    if (!this.UnattendedMode)
      LicensingManager.Instance.TrackRequest();
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner != null)
    {
      if (this.IsReusableGraph && this.ReuseCount != 0 && !this.ReuseRestricted)
      {
        PXReusableGraphFactory.Load(this, inner);
        this.ThrowIfDataConsistencyIssueSet();
        return;
      }
      this._ReWriteUnloadData = true;
      this.stateLoading = true;
      GraphSessionStatePrefix sessionPrefix = GraphSessionStatePrefix.For(this);
      object[] graphInfo = inner.GetGraphInfo(sessionPrefix);
      if (graphInfo != null && !this.UnattendedMode)
      {
        this._TimeStamp = (byte[]) graphInfo[0];
        this.accessinfo = (AccessInfo) graphInfo[1];
        this.InitScopedAccessInfoProperties();
        this._UID = graphInfo[2];
        this.WorkflowID = (string) graphInfo[4];
        this.WorkflowStepID = (string) graphInfo[5];
        this.NonUIErrors = graphInfo[6] as PXErrorInfo[];
        this._primaryRecordTimeStamp = (byte[]) graphInfo[7];
        this.ShouldSaveVersionModified = (bool) graphInfo[8];
        this.IsInVersionModifiedState = (bool) graphInfo[9];
        this.IsArchiveContext = (bool?) graphInfo[10];
        if (graphInfo.Length >= 12)
          this.NavigationParams = graphInfo[11] as IEnumerable<string>;
      }
      else
      {
        this._TimeStamp = (byte[]) null;
        this._primaryRecordTimeStamp = (byte[]) null;
        this.accessinfo = (AccessInfo) null;
        this._UID = (object) Guid.NewGuid();
        this.WorkflowID = (string) null;
        this.WorkflowStepID = (string) null;
      }
      List<System.Type> list = this.Caches.Keys.ToList<System.Type>();
      foreach (System.Type key in inner.GetDacTypesFromCacheInfo(sessionPrefix))
      {
        if (!list.Contains(key))
        {
          PXCache cach = this.Caches[key];
        }
      }
      foreach (PXCache pxCache in new List<PXCache>((IEnumerable<PXCache>) this.Caches.Values))
        pxCache.Load();
      PXWorkflowService workflowService = this.WorkflowService;
      if ((workflowService != null ? (workflowService.IsEnabled(this) ? 1 : 0) : 0) != 0 || !string.IsNullOrEmpty(this.WorkflowStepID))
      {
        object current = this.Views[this.PrimaryView].Cache.Current;
        if (current != null)
          this.ApplyWorkflowState(current);
      }
      this.stateLoading = false;
    }
    this.ThrowIfDataConsistencyIssueSet();
  }

  /// <summary>Clears the graph state stored in the session by clearing the
  /// data from each cache.</summary>
  public virtual void Clear()
  {
    if (this.IsSessionReadOnly)
      return;
    this.Clear(PXClearOption.PreserveQueries);
  }

  /// <exclude />
  [Obsolete("This method is deprecated, use Clear(PXClearOption option) instead", true)]
  public void Clear(PXClearOption option, string str) => this.Clear(option);

  /// <summary>Clears graph state inside the session</summary>
  public virtual void Clear(PXClearOption option)
  {
    if (this.IsReusableGraph)
      this.NeedFullUnload = true;
    if (!this.UnattendedMode)
      LicensingManager.Instance.TrackRequest();
    this.RaiseClear(option);
    if (option != PXClearOption.PreserveTimeStamp && option != PXClearOption.ClearQueriesOnly)
    {
      this._TimeStamp = PXTimeStampScope.GetValue();
      this._VeryFirstTimeStamp = ((PXDatabaseProviderBase) PXDatabase.Provider).selectTimestamp()?.Item1;
      this._primaryRecordTimeStamp = (byte[]) null;
    }
    if (option != PXClearOption.PreserveData)
    {
      HashSet<System.Type> updatable = new HashSet<System.Type>();
      if (option != PXClearOption.ClearQueriesOnly)
      {
        string screenId = this.accessinfo != null ? this.accessinfo.ScreenID : (string) null;
        this.accessinfo = (AccessInfo) null;
        if (screenId != null)
        {
          this.Accessinfo.ScreenID = screenId;
          if (!this.Caches.ContainsKey(typeof (AccessInfo)))
          {
            this.Caches[typeof (AccessInfo)].Clear();
            if (!this.UnattendedMode)
              this.Caches[typeof (AccessInfo)].ClearSession();
          }
        }
        this.WorkflowID = (string) null;
        this.WorkflowStepID = (string) null;
        GraphSessionStatePrefix sessionPrefix = GraphSessionStatePrefix.For(this);
        System.DateTime? nullable = new System.DateTime?();
        IPXSessionState inner = PXContext.Session.Inner;
        if (inner != null)
        {
          inner.GetGraphInfo(sessionPrefix);
          inner.RemoveGraphInfo(sessionPrefix);
          nullable = PXContext.GetBusinessDate();
        }
        if (nullable.HasValue)
        {
          this.Accessinfo.BusinessDate = nullable;
          if (screenId != null && !this.Caches.ContainsKey(typeof (AccessInfo)))
          {
            this.Caches[typeof (AccessInfo)].Clear();
            if (!this.UnattendedMode)
              this.Caches[typeof (AccessInfo)].ClearSession();
          }
        }
        foreach (PXCache pxCache in this.Caches.Values)
        {
          if (pxCache.Version > 0)
          {
            for (System.Type type = pxCache.GetItemType(); type != typeof (object); type = type.BaseType)
              updatable.Add(type);
          }
          if (this == pxCache.Graph && (!this.IsImport ? 0 : (pxCache.GetItemType() == typeof (DialogAnswer) ? 1 : 0)) == 0)
            pxCache.Clear();
          if (!this.UnattendedMode)
            pxCache.ClearSession();
        }
        if (PXContext.Session.IsSessionEnabled && !this.UnattendedMode)
          PXContext.Session.RemoveAll(sessionPrefix);
        if (PXLongOperation.GetStatus(this.UID) != PXLongRunStatus.InProcess)
        {
          if (HttpContext.Current != null)
            PXLongOperation.ClearStatus(this.UID);
          if (!this._DoNotClearCustomInfo)
          {
            if (PXContext.Session.IsSessionEnabled)
              PXLongOperation.RemoveCustomInfoPersistent(this.UID);
            this.UID = (object) Guid.NewGuid();
          }
        }
      }
      foreach (PXViewQueryCollection nonstandardView in this.TypedViews._NonstandardViews)
      {
        if (option != PXClearOption.ClearAll && option != PXClearOption.ClearQueriesOnly && !updatable.Contains(nonstandardView.CacheType))
        {
          if (!this.IsImport)
          {
            System.Type[] cacheTypes = nonstandardView.CacheTypes;
            if ((cacheTypes != null ? (((IEnumerable<System.Type>) cacheTypes).Any<System.Type>((Func<System.Type, bool>) (_ => updatable.Contains(_))) ? 1 : 0) : 0) == 0)
              continue;
          }
          else
            continue;
        }
        nonstandardView.Clear();
      }
      if (this.QueryCache != null)
      {
        foreach (PXViewQueryCollection viewQueryCollection in this.QueryCache.Values)
        {
          viewQueryCollection.ClearPreparedResults();
          viewQueryCollection.RemoveExpired();
          if (option != PXClearOption.ClearQueriesOnly && !updatable.Contains(viewQueryCollection.CacheType))
          {
            if (!this.IsImport)
            {
              System.Type[] cacheTypes = viewQueryCollection.CacheTypes;
              if ((cacheTypes != null ? (((IEnumerable<System.Type>) cacheTypes).Any<System.Type>((Func<System.Type, bool>) (_ => updatable.Contains(_))) ? 1 : 0) : 0) != 0)
                goto label_53;
            }
            viewQueryCollection.RemoveAll((Func<PXQueryResult, bool>) (result => result.Items is PXView.VersionedList items1 && items1.AnyMerged));
            using (IEnumerator<PXQueryResult> enumerator = viewQueryCollection.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                if (enumerator.Current.Items is PXView.VersionedList items2)
                  items2.MergedList = (PXView.VersionedList) null;
              }
              continue;
            }
          }
label_53:
          viewQueryCollection.Clear();
        }
      }
      this._selectedViews.Clear();
    }
    PXGraphPrepareDelegate afterConstructor = PXGraph.OnAfterConstructor;
    if (afterConstructor == null)
      return;
    afterConstructor(this);
  }

  /// <summary>Event raised from Persist method</summary>
  /// <exclude />
  public event System.Action<PXGraph> OnBeforePersist;

  protected internal void RaiseBeforePersist()
  {
    System.Action<PXGraph> onBeforePersist = this.OnBeforePersist;
    if (onBeforePersist == null)
      return;
    onBeforePersist(this);
  }

  /// <summary>
  /// Event raised from Persist method right before completing transaction
  /// </summary>
  /// <exclude />
  public event System.Action<PXGraph> OnBeforeCommit;

  protected internal void RaiseBeforeCommit()
  {
    System.Action<PXGraph> onBeforeCommit = this.OnBeforeCommit;
    if (onBeforeCommit == null)
      return;
    onBeforeCommit(this);
  }

  /// <summary>
  /// Event raised from Persist method after it's completion.
  /// </summary>
  /// <exclude />
  public event System.Action<PXGraph> OnAfterPersist;

  protected internal void RaiseAfterPersist()
  {
    System.Action<PXGraph> onAfterPersist = this.OnAfterPersist;
    if (onAfterPersist == null)
      return;
    onAfterPersist(this);
  }

  /// <summary>Event raised from Clear method</summary>
  /// <exclude />
  public event PXGraphClearDelegate OnClear;

  internal void RaiseClear(PXClearOption option)
  {
    PXGraphClearDelegate onClear = this.OnClear;
    if (onClear == null)
      return;
    onClear(this, option);
  }

  internal event System.Action OnReuseInitialize;

  internal void RaiseReuseInitialize()
  {
    System.Action onReuseInitialize = this.OnReuseInitialize;
    if (onReuseInitialize == null)
      return;
    onReuseInitialize();
  }

  /// <exclude />
  [PXInternalUseOnly]
  public event System.Action<PXGraph> OnRequestCompleted;

  protected void RaiseRequestCompleted()
  {
    System.Action<PXGraph> requestCompleted = this.OnRequestCompleted;
    if (requestCompleted == null)
      return;
    requestCompleted(this);
  }

  /// <summary>Saves the modified data records kept in the caches to the
  /// database.</summary>
  /// <remarks>
  /// <para>All data records are saved within a single transaction context.
  /// The method takes into account only the caches from the <tt>Views.Caches</tt> collection.</para>
  /// <para>The method divides the caches into two groups:</para>
  /// <list type="number">
  ///  <item><description>Caches that store records of DACs that either are not marked with the <see cref="T:PX.Data.PXDBInterceptorAttribute" />
  ///  attribute (or its descendant), or whose <see cref="T:PX.Data.PXDBInterceptorAttribute" /> attribute
  ///  has the <tt>PersistOrder</tt> property not equal to <tt>PersistOrder.AtTheEndOfTransaction</tt>.</description></item>
  ///  <item><description>Caches that store records of DACs whose <see cref="T:PX.Data.PXDBInterceptorAttribute" /> attribute
  ///  has the <tt>PersistOrder</tt> property equal to <tt>PersistOrder.AtTheEndOfTransaction</tt>.</description></item>
  /// </list>
  /// <para>Then the method saves the data records (first from the first group,
  /// then from the second) in the following order:</para>
  ///   <list type="number">
  ///     <item><description>Data records with the <tt>Inserted</tt> status from all caches.</description></item>
  ///     <item><description>Data records with the <tt>Updated</tt> status from all caches</description></item>
  ///     <item><description>Data records with the <tt>Deleted</tt> status from all caches</description></item>
  ///   </list>
  ///   <para>The application does not typically saves the changes through this method directly. The preferred way of saving the changes to the database is to execute
  /// <tt>Actions.PressSave()</tt> on the graph. The <tt>PressSave()</tt> method of the <tt>Actions</tt> collection invokes the <tt>Persist()</tt> method on the
  /// graph and performs additional procedures.</para>
  /// </remarks>
  public virtual void Persist()
  {
    PXWorkflowService.SaveAfterAction[this] = false;
    if (!this.PrePersist())
      return;
    this.ExceptionRollback.IsTransactionInsidePersist = PXTransactionScope.IsScoped;
    for (int index = 0; index < this.Views.Caches.Count; ++index)
    {
      PXCache cach = this.Caches[this.Views.Caches[index]];
    }
    if (PXTransactionScope.RetryAllowed)
      this.RetryTransaction(new System.Action(this.TransactionalPersist), 11);
    else
      this.TransactionalPersist();
    this.ActualizeAfterPersist();
    this.PostPersist();
  }

  /// <summary>
  /// Override this method to inject logic that should happen before the core of the persisting process, and before the inner transaction gets opened.
  /// </summary>
  protected virtual bool PrePersist()
  {
    this.RaiseBeforePersist();
    return true;
  }

  private void RetryTransaction(System.Action persistBody, int maxRetries)
  {
    int num = 0;
    while (true)
    {
      try
      {
        persistBody();
        break;
      }
      catch (PXLockViolationException ex)
      {
        if (!ex.Retry || num == maxRetries)
          throw;
        PXTransactionScope.Restart();
      }
      catch (PXDatabaseException ex)
      {
        if (!ex.Retry || num == maxRetries)
          throw;
        PXTransactionScope.Restart();
      }
      ++num;
    }
  }

  private void TransactionalPersist()
  {
    PXGraph.Persister persister = new PXGraph.Persister(this);
    try
    {
      bool flag = this.IsDefaultArchiveContextNeeded();
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.PerformPersist((PXGraph.IPersistPerformer) persister);
        if (persister.PersistedRecordsCount > 0)
        {
          this.SelectTimeStamp();
          this.PreCommit();
          this.ThrowIfDataConsistencyIssueSet();
          this.ValidateDataConsistency();
          this.ThrowIfDataConsistencyIssueSet();
        }
        transactionScope.Complete();
        transactionScope.LicensePolicy.RegisterErpTransaction(this, false);
      }
      if (flag)
        this.SetDefaultArchiveContext();
    }
    catch (Exception ex)
    {
      persister.AbortingException = ex;
      throw;
    }
    finally
    {
      this.PerformPersistCleanup(persister);
    }
    foreach (PXViewQueryCollection viewQueryCollection in this.QueryCache.Values)
      viewQueryCollection.RemoveExpired();
    foreach (PXViewQueryCollection nonstandardView in this.TypedViews._NonstandardViews)
      nonstandardView.Clear();
    this._selectedViews.Clear();
  }

  /// <summary>
  /// The core of the persisting process, which happens within an opened inner transaction.
  /// Override this method to manually manage the set of caches that should be persisted.
  /// </summary>
  protected virtual void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    IEnumerable<System.Type> source1 = this.Views.Caches.Where<System.Type>((Func<System.Type, bool>) (type => !persister.PersistedTypes.Contains(type)));
    while (source1.Any<System.Type>())
    {
      foreach (IGrouping<int, System.Type> source2 in (IEnumerable<IGrouping<int, System.Type>>) source1.Distinct<System.Type>().GroupBy<System.Type, int>((Func<System.Type, int>) (x => x.GetPersistOrder())).OrderBy<IGrouping<int, System.Type>, int>((Func<IGrouping<int, System.Type>, int>) (x => x.Key)))
      {
        foreach (System.Type cacheType in (IEnumerable<System.Type>) source2)
          persister.Insert(cacheType);
        foreach (System.Type cacheType in (IEnumerable<System.Type>) source2)
          persister.Update(cacheType);
        foreach (System.Type cacheType in source2.Reverse<System.Type>())
          persister.Delete(cacheType);
      }
    }
  }

  /// <summary>
  /// Override this method to inject logic should happen just after the core of the persisting process, but before the inner transaction gets closed.
  /// </summary>
  protected virtual void PreCommit() => this.RaiseBeforeCommit();

  private void PerformPersistCleanup(PXGraph.Persister persister)
  {
    for (int index = 0; index < this.Views.Caches.Count; ++index)
      persister.MarkPersisted(this.Views.Caches[index]);
  }

  private void ActualizeAfterPersist()
  {
    if (string.IsNullOrEmpty(this.PrimaryView))
      return;
    if (this.IsImport.Implies(this.IsMobile))
    {
      PXCache cache = this.Views[this.PrimaryView].Cache;
      if (cache != null && cache.Current != null && !PXTransactionScope.IsScoped)
        cache.RaiseRowSelected(cache.Current);
    }
    this.EnsureIfArchived();
  }

  /// <summary>
  /// Override this method to inject logic that should happen after the core of the persisting process, and after the inner transaction gets closed.
  /// </summary>
  protected virtual void PostPersist() => this.RaiseAfterPersist();

  /// <summary>Saves the modifications of a particular type from the specified cache to the database. The method relies on the
  /// <see cref="M:PX.Data.PXCache`1.Persist(PX.Data.PXDBOperation)">Persist(PXDBOperation)</see> method of the cache.</summary>
  /// <param name="cacheType">The DAC type of the cache whose changes are
  /// saved.</param>
  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    return this.Caches[cacheType].Persist(operation);
  }

  /// <summary>Returns the value indicating if the cache related to the data
  /// view allows selecting data records through the user interface. This
  /// flag does not affect the ability to select data records through
  /// code.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool AllowSelect(string viewName) => this.Views[viewName].AllowSelect;

  /// <summary>Returns the value indicating if the cache related to the data
  /// view allows updating data records through the user interface. This
  /// flag does not affect the ability to update a data record through
  /// code.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool AllowUpdate(string viewName) => this.Views[viewName].AllowUpdate;

  /// <summary>Returns a value that indicates if updating of the cache
  /// related to the data view is allowed.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool UpdateRights(string viewName)
  {
    return !this.Views[viewName].IsReadOnly && this.Views[viewName].Cache.UpdateRights;
  }

  /// <summary>Returns the value indicating if the cache related to the data
  /// view allows inserting data records through the user interface. This
  /// flag does not affect the ability to insert a data record through
  /// code.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool AllowInsert(string viewName) => this.Views[viewName].AllowInsert;

  /// <summary>Returns the value indicating if the cache related to the data
  /// view allows deleting data records through the user interface. This
  /// flag does not affect the ability to delete a data record through
  /// code.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool AllowDelete(string viewName) => this.Views[viewName].AllowDelete;

  /// <summary>Returns the value indicating if the data view is read-only.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual bool GetUpdatable(string viewName)
  {
    return !string.IsNullOrEmpty(viewName) && !this.Views[viewName].IsReadOnly;
  }

  /// <summary>Returns the names of the keys fields of the cache related to
  /// the data view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public virtual string[] GetKeyNames(string viewName)
  {
    return PXCache.GetKeyNames(this, this.Views[viewName].GetItemType());
  }

  /// <summary>Returns the value indicating if any updatable cache has an
  /// exception.</summary>
  public bool HasException()
  {
    foreach (System.Type key in this.Views.Caches.ToArray())
    {
      if (this.Caches.ContainsKey(key))
      {
        foreach (PXEventSubscriberAttribute attribute in this.Caches[key].GetAttributes((string) null))
        {
          if (attribute is IPXInterfaceField && !string.IsNullOrEmpty(((IPXInterfaceField) attribute).ErrorText))
            return true;
        }
      }
    }
    return false;
  }

  internal bool HasGraphSpecificFields(System.Type itemType)
  {
    PXCache.AlteredDescriptor alteredDescriptor;
    return this._AlteredDescriptors != null && this._AlteredDescriptors.TryGetValue(itemType, out alteredDescriptor) && alteredDescriptor.Fields != null && alteredDescriptor.Fields.Count > 0;
  }

  internal virtual bool HasDirtyItems
  {
    get
    {
      return this.Caches.Any<KeyValuePair<System.Type, PXCache>>((Func<KeyValuePair<System.Type, PXCache>, bool>) (kv => kv.Value.Dirty.Count() > 0L));
    }
  }

  /// <summary>Returns the names of parameters of the data view by invoking
  /// the <see cref="M:PX.Data.PXView.GetParameterNames">GetParameterNames()</see>
  /// method on the data view.</summary>
  /// <param name="viewName">The name of the data view.</param>
  public string[] GetParameterNames(string viewName) => this.Views[viewName].GetParameterNames();

  /// <summary>Gets the value that indicates whether there are modified data
  /// records not saved to the database in the caches related to the graph
  /// data views. If the <tt>IsDirty</tt> property of at least one cache
  /// object is <tt>true</tt>, the <tt>IsDirty</tt> property of the graph is
  /// also <tt>true</tt>.</summary>
  public virtual bool IsDirty
  {
    get
    {
      if (this.stateLoading)
        return false;
      switch (PXLongOperation.GetStatus(this.UID, out TimeSpan _, out Exception _))
      {
        case PXLongRunStatus.Completed:
        case PXLongRunStatus.Aborted:
          return false;
        default:
          foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.Caches)
          {
            if (this.Views.Caches.Contains(cach.Key) && cach.Value.IsDirty)
              return true;
          }
          return false;
      }
    }
  }

  /// <summary>
  /// This method is entry point for data consistency validation
  /// Override the method in graph extensions to add validation logic
  /// </summary>
  protected virtual void ValidateDataConsistency()
  {
  }

  private void ThrowIfDataConsistencyIssueSet()
  {
    RecordDataConsistencyIssue info;
    if (!this.IsDataConsistencyIssueSet(out info))
      return;
    PXGraph.ThrowWithoutRollback((Exception) new PXException("A data corruption state has been detected. You cannot save the changes. Copy the data you have entered and reload the page. Date and Time: {0}; IncidentID: {1}; Name: {2}. You can view detailed information about the issue on the System Events tab of the System Monitor (SM201530) form.", new object[3]
    {
      (object) info.Tm.ToString("s"),
      (object) info.ID,
      (object) info.Name
    }));
  }

  private bool IsDataConsistencyIssueSet(out RecordDataConsistencyIssue info)
  {
    info = (RecordDataConsistencyIssue) null;
    PXCache pxCache;
    if (!this.Caches.TryGetValue(typeof (RecordDataConsistencyIssue), out pxCache))
      return false;
    info = pxCache.Inserted.OfType<RecordDataConsistencyIssue>().FirstOrDefault<RecordDataConsistencyIssue>();
    return info != null;
  }

  private string DumpCaches(int maxSize = 90000)
  {
    StringBuilder b = new StringBuilder();
    try
    {
      foreach (KeyValuePair<System.Type, PXCache> keyValuePair in this.Caches.ToList<KeyValuePair<System.Type, PXCache>>())
      {
        ((IPXDumpObjectState) keyValuePair.Value).DumpObjectState(b, maxSize);
        if (b.Length >= maxSize)
          break;
      }
    }
    catch (Exception ex)
    {
      b.Append(ex.ToString());
    }
    return b.ToString();
  }

  /// <summary>
  /// Invoke the method to specify that data corruption was detected
  /// ui user will be informed, diagnostic will be written to the system events and telemetry
  /// it will be impossible to persist the data to the DB
  /// it is recommended to invoke this method from ValidateDataConsistency overrides
  /// </summary>
  /// <param name="name"></param>
  /// <param name="diagnosticDetails"></param>
  /// <param name="dumpAllCaches"></param>
  public void SetDataConsistencyIssue(string name, string diagnosticDetails = null, bool dumpAllCaches = false)
  {
    if (this.IsDataConsistencyIssueSet(out RecordDataConsistencyIssue _))
      return;
    RecordDataConsistencyIssue dataConsistencyIssue = new RecordDataConsistencyIssue()
    {
      Name = name,
      Tm = System.DateTime.UtcNow,
      ID = Guid.NewGuid().ToString()
    };
    dataConsistencyIssue.Message = $"A data corruption state has been detected. You cannot save the changes. Copy the data you have entered and reload the page. Date and Time: {dataConsistencyIssue.Tm.ToString("s")}; IncidentID: {dataConsistencyIssue.ID}; Name: {dataConsistencyIssue.Name}. You can view detailed information about the issue on the System Events tab of the System Monitor (SM201530) form.";
    this.SetDataConsistencyIssue(dataConsistencyIssue, diagnosticDetails, dumpAllCaches);
  }

  /// <summary>
  /// Invoke the method to specify that data corruption was detected
  /// ui user will be informed, diagnostic will be written to the system events and telemetry
  /// it will be impossible to persist the data to the DB
  /// it is recommended to invoke this method from ValidateDataConsistency overrides
  /// </summary>
  /// <param name="name"></param>
  /// <param name="diagnosticDetails"></param>
  /// <param name="dumpAllCaches"></param>
  internal void SetDataConsistencyIssue(
    string name,
    string text,
    string diagnosticDetails = null,
    bool dumpAllCaches = false)
  {
    if (this.IsDataConsistencyIssueSet(out RecordDataConsistencyIssue _))
      return;
    this.SetDataConsistencyIssue(new RecordDataConsistencyIssue()
    {
      Name = name,
      Tm = System.DateTime.UtcNow,
      ID = Guid.NewGuid().ToString(),
      Message = text
    }, diagnosticDetails, dumpAllCaches);
  }

  private void SetDataConsistencyIssue(
    RecordDataConsistencyIssue dataConsistencyIssue,
    string diagnosticDetails = null,
    bool dumpAllCaches = false)
  {
    this.Caches<RecordDataConsistencyIssue>().SetStatus((object) dataConsistencyIssue, PXEntryStatus.Inserted);
    ILogger ilogger = PXTrace.Logger.ForContext("SystemEvent", (object) true, false).WithEventID("DataConsistency", "DataConsistency_IssueDetectedEventID");
    if (dumpAllCaches)
      ilogger = ilogger.ForContext("DumpCaches", (object) this.DumpCaches(), false);
    ilogger.ForContext("CustomData", (object) diagnosticDetails, false).ForContext("GUID", (object) dataConsistencyIssue.ID, false).ForContext("ErrorType", (object) dataConsistencyIssue.Name, false).Error("A data corruption state has been detected");
    PXTrace.Logger.ForTelemetry("DataConsistency", dataConsistencyIssue.Name).Error<string>("{ID}", dataConsistencyIssue.ID);
  }

  /// <summary>Get the DAC descriptor.</summary>
  /// <exception cref="T:System.InvalidOperationException">Thrown when the DAC descriptor provider is not initialized.</exception>
  /// <param name="dac">The DAC.</param>
  /// <param name="customDescriptorCreationOptions">
  /// (Optional) Custom options for the DAC descriptor creation. If <see langword="null" /> then default options <see cref="P:PX.Data.DacDescriptorGeneration.DacDescriptorCreationOptions.Default" /> will be used.
  /// </param>
  /// <returns>The DAC descriptor.</returns>
  public DacDescriptor GetDacDescriptor(
    IBqlTable dac,
    DacDescriptorCreationOptions customDescriptorCreationOptions = null)
  {
    IDacDescriptorProvider descriptorProvider = this.DacDescriptorProvider;
    if (descriptorProvider == null && ServiceLocator.IsLocationProviderSet)
      ServiceLocatorExtensions.TryGetInstance<IDacDescriptorProvider>(ServiceLocator.Current, ref descriptorProvider);
    if (descriptorProvider == null)
    {
      PXTrace.WriteError("The DAC descriptor provider service is not initialized for Graph {GraphType}", (object) this.GetType().FullName);
      return new DacDescriptor();
    }
    if (descriptorProvider == null)
      throw new InvalidOperationException("The DAC descriptor provider is not initialized");
    return descriptorProvider.CreateDacDescriptor(this, dac, customDescriptorCreationOptions);
  }

  /// <summary>Selects the specified amount of top records from the database
  /// table.</summary>
  /// <param name="command">The BQL command defining the select query to
  /// execute.</param>
  /// <param name="topCount">The number of the data record to retreive from
  /// the top of the data set.</param>
  /// <param name="pars">The parameters.</param>
  public virtual IEnumerable<PXDataRecord> ProviderSelect(
    BqlCommand command,
    int topCount,
    params PXDataValue[] pars)
  {
    return this.ProviderSelect(command, topCount, (PXView) null, pars);
  }

  internal virtual IEnumerable<PXDataRecord> ProviderSelect(
    BqlCommand command,
    int topCount,
    PXView view,
    params PXDataValue[] pars)
  {
    return PXDatabase.Provider.Select(command.GetQuery(this, view, (long) topCount), (IEnumerable<PXDataValue>) pars);
  }

  /// <summary>Selects a single record from the database table. The table is
  /// specified as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual PXDataRecord ProviderSelectSingle<Table>(params PXDataField[] pars) where Table : IBqlTable
  {
    return PXDatabase.SelectSingle<Table>(pars);
  }

  /// <summary>Selects multiple records from the database table. The table
  /// is specified as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual IEnumerable<PXDataRecord> ProviderSelectMulti<Table>(params PXDataField[] pars) where Table : IBqlTable
  {
    return PXDatabase.SelectMulti<Table>(pars);
  }

  /// <summary>Performs a database insert operation. The table is specified
  /// as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderInsert<Table>(params PXDataFieldAssign[] pars) where Table : IBqlTable
  {
    return PXDatabase.Insert<Table>(pars);
  }

  /// <summary>Performs a database update operation. The table is specified
  /// as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderUpdate<Table>(params PXDataFieldParam[] pars) where Table : IBqlTable
  {
    return PXDatabase.Update<Table>(pars);
  }

  /// <summary>Performs a database delete operation. The table is specified
  /// as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderDelete<Table>(params PXDataFieldRestrict[] pars) where Table : IBqlTable
  {
    return PXDatabase.Delete<Table>(pars);
  }

  /// <summary>Performs a database archive operation. The table is specified
  /// as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderArchive(System.Type table, params PXDataFieldRestrict[] pars)
  {
    int num = PXDatabase.Archive(table, pars) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.Clear(PXClearOption.ClearQueriesOnly);
    return num != 0;
  }

  /// <summary>Performs a database extract from archive operation. The table is specified
  /// as the DAC through the type parameter.</summary>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderExtract(System.Type table, params PXDataFieldRestrict[] pars)
  {
    int num = PXDatabase.Extract(table, pars) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.Clear(PXClearOption.ClearQueriesOnly);
    return num != 0;
  }

  /// <summary>Selects a single record from the database table.</summary>
  /// <param name="table">The DAC representing the table from which the data
  /// record is selected.</param>
  /// <param name="pars">The parameters.</param>
  public virtual PXDataRecord ProviderSelectSingle(System.Type table, params PXDataField[] pars)
  {
    return PXDatabase.SelectSingle(table, pars);
  }

  /// <summary>Selects multiple records from the database table.</summary>
  /// <param name="table">The DAC representing the table from which the data
  /// records are selected.</param>
  /// <param name="pars">The parameters.</param>
  public virtual IEnumerable<PXDataRecord> ProviderSelectMulti(
    System.Type table,
    params PXDataField[] pars)
  {
    return PXDatabase.SelectMulti(table, pars);
  }

  /// <summary>Performs a database insert operation.</summary>
  /// <param name="table">The DAC representing the table to which the data
  /// records are inserted.</param>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderInsert(System.Type table, params PXDataFieldAssign[] pars)
  {
    return PXDatabase.Insert(table, pars);
  }

  /// <summary>Performs a database update operation.</summary>
  /// <param name="table">The DAC representing the table from where the data
  /// records are updated.</param>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderUpdate(System.Type table, params PXDataFieldParam[] pars)
  {
    return PXDatabase.Update(table, pars);
  }

  /// <summary>Performs a database delete operation.</summary>
  /// <param name="table">The DAC representing the table whose records are
  /// deleted.</param>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderDelete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    return PXDatabase.Delete(table, pars);
  }

  /// <param name="table">The DAC representing the table.</param>
  /// <param name="values">The values.</param>
  /// <param name="pars">The parameters.</param>
  public virtual bool ProviderEnsure(System.Type table, PXDataFieldAssign[] values, PXDataField[] pars)
  {
    return PXDatabase.Ensure(table, values, pars);
  }

  /// <summary>
  /// Depricated.
  /// Occurs when the view content has been changed.
  /// </summary>
  /// <exclude />
  public event PXGraphViewChanged ViewChanged;

  private void OnViewChanged(PXGraphViewChangedEventArgs args)
  {
    if (this.ViewChanged == null)
      return;
    this.ViewChanged((object) this, args);
  }

  /// <summary>Deprecated</summary>
  /// <exclude />
  public void RaisePXGraphViewChanged(string viewName)
  {
    this.OnViewChanged(new PXGraphViewChangedEventArgs(viewName));
  }

  /// <exclude />
  public event PXGraphBeforeUnloadDelegate BeforeUnload;

  protected void RaiseBeforeUnloadEvent()
  {
    if (this.BeforeUnload == null)
      return;
    this.BeforeUnload(this);
  }

  /// <summary>Occurs at the end of graph constructor.</summary>
  /// <exclude />
  public event PXGraphInitializedDelegate Initialized;

  protected void RaiseInitialized()
  {
    if (this.Initialized == null)
      return;
    this.Initialized(this);
  }

  /// <summary>Occurs when new instance of graph is created.</summary>
  /// <exclude />
  public static event PXGraphPrepareDelegate OnPrepare;

  protected void RaisePrepare()
  {
    if (PXGraph.OnPrepare != null)
    {
      try
      {
        PXGraph.OnPrepare(this);
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch
      {
      }
    }
    PXGraphPrepareDelegate afterConstructor = PXGraph.OnAfterConstructor;
    if (afterConstructor == null)
      return;
    afterConstructor(this);
  }

  /// <summary>
  /// Gets the instance of <tt>RowSelectingEvents</tt> type that represents the collection of <tt>RowSelecting</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowSelectingEvents RowSelecting
  {
    get
    {
      return this._RowSelectingEvents ?? (this._RowSelectingEvents = new PXGraph.RowSelectingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowSelectedEvents</tt> type that represents the collection of <tt>RowSelected</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowSelectedEvents RowSelected
  {
    get
    {
      return this._RowSelectedEvents ?? (this._RowSelectedEvents = new PXGraph.RowSelectedEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowInsertingEvents</tt> type that represents the collection of <tt>RowInserting</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowInsertingEvents RowInserting
  {
    get
    {
      return this._RowInsertingEvents ?? (this._RowInsertingEvents = new PXGraph.RowInsertingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowInsertedEvents</tt> type that represents the collection of <tt>RowInserted</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowInsertedEvents RowInserted
  {
    get
    {
      return this._RowInsertedEvents ?? (this._RowInsertedEvents = new PXGraph.RowInsertedEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowUpdatingEvents</tt> type that represents the collection of <tt>RowUpdating</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowUpdatingEvents RowUpdating
  {
    get
    {
      return this._RowUpdatingEvents ?? (this._RowUpdatingEvents = new PXGraph.RowUpdatingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowUpdatedEvents</tt> type that represents the collection of <tt>RowUpdated</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowUpdatedEvents RowUpdated
  {
    get => this._RowUpdatedEvents ?? (this._RowUpdatedEvents = new PXGraph.RowUpdatedEvents(this));
  }

  /// <summary>
  /// Gets the instance of <tt>RowDeletingEvents</tt> type that represents the collection of <tt>RowDeleting</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowDeletingEvents RowDeleting
  {
    get
    {
      return this._RowDeletingEvents ?? (this._RowDeletingEvents = new PXGraph.RowDeletingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowDeletedEvents</tt> type that represents the collection of <tt>RowDeleted</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowDeletedEvents RowDeleted
  {
    get => this._RowDeletedEvents ?? (this._RowDeletedEvents = new PXGraph.RowDeletedEvents(this));
  }

  /// <summary>
  /// Gets the instance of <tt>RowPersistingEvents</tt> type that represents the collection of <tt>RowPersisting</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowPersistingEvents RowPersisting
  {
    get
    {
      return this._RowPersistingEvents ?? (this._RowPersistingEvents = new PXGraph.RowPersistingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>RowPersistedEvents</tt> type that represents the collection of <tt>RowPersisted</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.RowPersistedEvents RowPersisted
  {
    get
    {
      return this._RowPersistedEvents ?? (this._RowPersistedEvents = new PXGraph.RowPersistedEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>CommandPreparingEvents</tt> type that represents the collection of <tt>CommandPreparing</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.CommandPreparingEvents CommandPreparing
  {
    get
    {
      return this._CommandPreparingEvents ?? (this._CommandPreparingEvents = new PXGraph.CommandPreparingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>FieldDefaultingEvents</tt> type that represents the collection of <tt>FieldDefaulting</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.FieldDefaultingEvents FieldDefaulting
  {
    get
    {
      return this._FieldDefaultingEvents ?? (this._FieldDefaultingEvents = new PXGraph.FieldDefaultingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>FieldUpdatingEvents</tt> type that represents the collection of <tt>FieldUpdating</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.FieldUpdatingEvents FieldUpdating
  {
    get
    {
      return this._FieldUpdatingEvents ?? (this._FieldUpdatingEvents = new PXGraph.FieldUpdatingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>FieldVerifyingEvents</tt> type that represents the collection of <tt>FieldVerifying</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.FieldVerifyingEvents FieldVerifying
  {
    get
    {
      return this._FieldVerifyingEvents ?? (this._FieldVerifyingEvents = new PXGraph.FieldVerifyingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of <tt>FieldUpdatedEvents</tt> type that represents the collection of <tt>FieldUpdated</tt> event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.FieldUpdatedEvents FieldUpdated
  {
    get
    {
      return this._FieldUpdatedEvents ?? (this._FieldUpdatedEvents = new PXGraph.FieldUpdatedEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of FieldSelectingEvents type that represents the collection of FieldSelecting event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.FieldSelectingEvents FieldSelecting
  {
    get
    {
      return this._FieldSelectingEvents ?? (this._FieldSelectingEvents = new PXGraph.FieldSelectingEvents(this));
    }
  }

  /// <summary>
  /// Gets the instance of ExceptionHandlingEvents type that represents the collection of ExceptionHandling event handlers related to the graph. The collection initially contains the event handlers defined in the graph, but it can be modified at run time.
  /// </summary>
  public PXGraph.ExceptionHandlingEvents ExceptionHandling
  {
    get
    {
      return this._ExceptionHandlingEvents ?? (this._ExceptionHandlingEvents = new PXGraph.ExceptionHandlingEvents(this));
    }
  }

  public ISqlDialect SqlDialect => this._LazyDialect.Value;

  /// <exclude />
  public virtual bool CanClipboardCopyPaste()
  {
    if (!this.canCopyPaste.HasValue)
      this.canCopyPaste = new bool?(((IEnumerable<System.Reflection.FieldInfo>) this.GetType().GetFields()).Concat<System.Reflection.FieldInfo>(PXGraph._GetExtensions(this.GetType()).SelectMany<System.Type, System.Reflection.FieldInfo>((Func<System.Type, IEnumerable<System.Reflection.FieldInfo>>) (c => (IEnumerable<System.Reflection.FieldInfo>) c.GetFields()))).Any<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof (PXCopyPasteAction<>))));
    return this.canCopyPaste.Value;
  }

  public void CloneEntitiesFromParent(ExportTemplate template)
  {
    PXView view = this.Views[this.PrimaryView];
    PXCache c1 = view.Cache;
    object currentObj = c1.Current;
    Dictionary<string, object> dictionary = view.Cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (a => a), (Func<string, object>) (k => c1.GetValue(currentObj, k)));
    companySetting settings;
    int? nullable = new int?(PXDatabase.Provider.getCompanyID(this.PrimaryItemType.Name, out settings));
    if (settings.Flag == companySetting.companyFlag.Global)
      return;
    YaqlCondition yaqlCondition = Yaql.and(dictionary.Select<KeyValuePair<string, object>, YaqlCondition>((Func<KeyValuePair<string, object>, YaqlCondition>) (kv => Yaql.eq<object>((YaqlScalar) Yaql.column(kv.Key, (string) null), kv.Value))));
    new DataDownloader(PXDatabase.Provider.CreateDbServicesPoint(), template).CloneFromParent(nullable ?? 1, yaqlCondition);
  }

  public PxDataSet ResetEntitiesToParent(ExportTemplate template)
  {
    PXView view = this.Views[this.PrimaryView];
    PXCache c1 = view.Cache;
    object currentObj = c1.Current;
    Dictionary<string, object> dictionary = view.Cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (a => a), (Func<string, object>) (k => c1.GetValue(currentObj, k)));
    companySetting settings;
    int? nullable = new int?(PXDatabase.Provider.getCompanyID(this.PrimaryItemType.Name, out settings));
    if (settings.Flag == companySetting.companyFlag.Global)
      return (PxDataSet) null;
    YaqlCondition yaqlCondition = Yaql.and(dictionary.Select<KeyValuePair<string, object>, YaqlCondition>((Func<KeyValuePair<string, object>, YaqlCondition>) (kv => Yaql.eq<object>((YaqlScalar) Yaql.column(kv.Key, (string) null), kv.Value))));
    return new DataDownloader(PXDatabase.Provider.CreateDbServicesPoint(), template).ResetToDefault(nullable ?? 1, yaqlCondition);
  }

  /// <exclude />
  public FileInfo GetCurrentEntityAsXml(ExportTemplate template)
  {
    PXView view = this.Views[this.PrimaryView];
    PXCache c1 = view.Cache;
    object currentObj = c1.Current;
    Dictionary<string, object> dictionary = view.Cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (a => a), (Func<string, object>) (k => c1.GetValue(currentObj, k)));
    if (currentObj == null || dictionary.Values.Any<object>((Func<object, bool>) (k => k == null)))
      return (FileInfo) null;
    companySetting settings;
    int? nullable = new int?(PXDatabase.Provider.getCompanyID(this.PrimaryItemType.Name, out settings));
    if (settings.Flag == companySetting.companyFlag.Global)
      nullable = new int?();
    YaqlCondition yaqlCondition = Yaql.and(dictionary.Select<KeyValuePair<string, object>, YaqlCondition>((Func<KeyValuePair<string, object>, YaqlCondition>) (kv => Yaql.eq<object>((YaqlScalar) Yaql.column(kv.Key, (string) null), kv.Value))));
    YaqlCondition recordsForExport = this.GetFilterConditionForAdditionalRecordsForExport(dictionary);
    string str = (string) null;
    if (template.FileNameTemplate != null)
    {
      str = template.FileNameTemplate;
      foreach (string field in (List<string>) view.Cache.Fields)
      {
        string oldValue = $"({field})";
        if (template.FileNameTemplate.Contains(oldValue))
        {
          object obj = view.Cache.GetValue(currentObj, field);
          str = str.Replace(oldValue, obj != null ? obj.ToString() : string.Empty);
        }
      }
    }
    if (string.IsNullOrEmpty(str))
      str = string.Join("-", dictionary.Values.Select<object, string>((Func<object, string>) (v => v != null ? v.ToString() : "")));
    DataDownloader dataDownloader = new DataDownloader(PXDatabase.Provider.CreateDbServicesPoint(), template);
    PxDataSet dataSet = dataDownloader.Extract(nullable, yaqlCondition);
    if (recordsForExport != null)
      EnumerableExtensions.ForEach<PxDataTable>((IEnumerable<PxDataTable>) dataDownloader.Extract(nullable, recordsForExport), (System.Action<PxDataTable>) (x => dataSet.Merge(x)));
    byte[] memory = new DataSetXmlWriter(dataSet, template, (SchemaXmlLayout) null).WriteToMemory();
    return new FileInfo(str + ".xml", (string) null, memory);
  }

  internal virtual YaqlCondition GetFilterConditionForAdditionalRecordsForExport(
    Dictionary<string, object> keys)
  {
    return (YaqlCondition) null;
  }

  /// <exclude />
  public Dictionary<string, PerTableReport> ImportEntitiesFromXml(
    byte[] fileContent,
    RecordImportMode overwriteMode,
    out DataUploader dl)
  {
    PXView view = this.Views[this.PrimaryView];
    WebDialogResult answer1 = DialogManager.GetAnswer(view, "xmlImportDuplicatesExist");
    WebDialogResult answer2 = DialogManager.GetAnswer(view, "xmlImportRootRowExist");
    if (answer1 == WebDialogResult.No || answer2 == WebDialogResult.No)
      throw new PXException();
    int num1;
    System.Action<Dictionary<string, HashSet<string>>> action = answer1 == WebDialogResult.None ? (System.Action<Dictionary<string, HashSet<string>>>) (rows => num1 = (int) DialogManager.Ask(view, "xmlImportDuplicatesExist", (object) null, PXLocalizer.Localize("Import"), GetRowsStr(rows), MessageButtons.YesNo, MessageIcon.None, false)) : (System.Action<Dictionary<string, HashSet<string>>>) null;
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    dl = DataUploader.fromBufferAndPoint(fileContent, dbServicesPoint, action);
    if (!((DataLoaderBase) dl).Relations.RootTable.Equals(this.PrimaryItemType.Name))
      throw new InvalidOperationException($"The uploaded file contains entities of the wrong type: {((DataLoaderBase) dl).Relations.RootTable}, while {this.PrimaryItemType.Name} is expected.");
    companySetting settings;
    int companyId = PXDatabase.Provider.getCompanyID(this.PrimaryItemType.Name, out settings);
    if (settings.TableNotFound)
      throw new PXException("The table schema of the {0} table was not found in the cache. The table is locked by another process. Please try again later.", new object[1]
      {
        (object) this.PrimaryItemType.Name
      });
    if (answer2 == WebDialogResult.None)
    {
      PxDataProjection existingRootRowProjection = dl.CollectExistingRecords(companyId, true).Single<KeyValuePair<string, PxDataProjection>>().Value;
      object[] existingRootRow = ((PxDataRows) existingRootRowProjection).Rows.FirstOrDefault<object[]>();
      if (existingRootRow != null)
      {
        IEnumerable<string> values = dl.GetColumnsForRowExistingDialog(((PxDataRows) existingRootRowProjection).Name).Select<string, string>((Func<string, string>) (column => $"{column}: {existingRootRow[((PxDataRows) existingRootRowProjection).IndexOfColumn(column)].ToString()}"));
        string message = string.Format(PXMessages.LocalizeNoPrefix("The following record will be overwritten: {0}. Do you want to import the file?"), (object) string.Join(",\r\n", values));
        int num2 = (int) DialogManager.Ask(view, "xmlImportRootRowExist", (object) null, PXLocalizer.Localize("Import"), message, MessageButtons.YesNo, MessageIcon.None, false);
      }
    }
    Dictionary<string, PxDataProjection> dictionary1 = dl.CollectExistingRecords(companyId, false);
    List<PlannedActionsForTable> plannedActionsForTableList = dl.ComposeImportScrenario(dictionary1, overwriteMode);
    Dictionary<string, PerTableReport> dictionary2 = dl.ImportDataSet(companyId, plannedActionsForTableList, false);
    PXDBLocalizableStringAttribute.EnsureTranslations(PerTableReport.GetAffectedMainTables(dictionary2));
    return dictionary2;

    static string GetRowsStr(Dictionary<string, HashSet<string>> Rows)
    {
      return PXLocalizer.LocalizeFormat("The following duplicate records have been found by the key fields in the following tables.\r\n{0}\r\nDo you want to continue importing the file?", (object) string.Join("\r\n", Rows.Select<KeyValuePair<string, HashSet<string>>, string>((Func<KeyValuePair<string, HashSet<string>>, string>) (rpt => $"{PXLocalizer.LocalizeFormat("The {0} table:", (object) rpt.Key)}\r\n{string.Join("\r\n", (IEnumerable<string>) rpt.Value)}"))));
    }
  }

  /// <summary>
  /// Defines and stores the actions that are displayed in the side panel of an Acumatica ERP form.
  /// These actions allow a user to navigate to different parts of the application directly from the side panel.
  /// </summary>
  public List<PXGraph.SidePanelAction> SidePanelActions { get; } = new List<PXGraph.SidePanelAction>();

  internal bool ReuseRestricted
  {
    get => this._reuseRestricted || this.IsImport;
    set => this._reuseRestricted = value;
  }

  public static void ThrowWithoutRollback(Exception ex)
  {
    throw new SessionRollback.IgnoreRollbackException(ex);
  }

  /// <exclude />
  internal class StatePrefixScope : IDisposable
  {
    private readonly PXGraph _graph;
    private readonly string _oldPrefix;

    public StatePrefixScope(PXGraph graph, string newPrefix)
    {
      this._graph = graph;
      this._oldPrefix = graph.StatePrefix;
      graph.StatePrefix = newPrefix;
    }

    public void Dispose() => this._graph.StatePrefix = this._oldPrefix;
  }

  /// <exclude />
  protected internal delegate void _InitializeDelegate(PXGraph graph);

  /// <exclude />
  internal sealed class AlteredState
  {
    public readonly Dictionary<string, List<PXEventSubscriberAttribute>> Fields;
    public readonly HashSet<Tuple<string, MethodInfo>> ProcessedMethods;
    public readonly DynamicMethod Method;
    public readonly ILGenerator Generator;

    public AlteredState()
    {
      if (!PXGraph.IsRestricted)
        this.Method = new DynamicMethod("_CacheAttached", (System.Type) null, new System.Type[2]
        {
          typeof (PXGraph),
          typeof (PXCache)
        }, typeof (PXGraph), true);
      else
        this.Method = new DynamicMethod("_CacheAttached", (System.Type) null, new System.Type[2]
        {
          typeof (PXGraph),
          typeof (PXCache)
        }, true);
      this.Generator = this.Method.GetILGenerator();
      this.Fields = new Dictionary<string, List<PXEventSubscriberAttribute>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.ProcessedMethods = new HashSet<Tuple<string, MethodInfo>>();
    }
  }

  /// <exclude />
  internal sealed class FieldInfoComparer : IComparer<MemberInfo>
  {
    int IComparer<MemberInfo>.Compare(MemberInfo f1, MemberInfo f2)
    {
      if (f1.DeclaringType.IsSubclassOf(f2.DeclaringType))
        return -1;
      if (f2.DeclaringType.IsSubclassOf(f1.DeclaringType))
        return 1;
      if (f1.MetadataToken < f2.MetadataToken)
        return -1;
      return f2.MetadataToken < f1.MetadataToken ? 1 : 0;
    }
  }

  /// <summary>Delegate to get default current item</summary>
  /// <returns>Data item</returns>
  public delegate object GetDefaultDelegate();

  public class PXGraphPrototype
  {
    private readonly ConcurrentDictionary<PXGraph.PXGraphPrototype.MethodKey, object> StaticProperties = new ConcurrentDictionary<PXGraph.PXGraphPrototype.MethodKey, object>();

    public T Memoize<T>(Func<T> factory, params object[] depends)
    {
      if (!WebConfig.LazyCachesEnabled)
        return factory();
      return (T) this.StaticProperties.GetOrAdd(new PXGraph.PXGraphPrototype.MethodKey()
      {
        M = (object) factory.Method,
        Keys = depends
      }, (Func<PXGraph.PXGraphPrototype.MethodKey, object>) (k => (object) factory()));
    }

    public bool TryGetValue<T>(out T V, object key, params object[] depends)
    {
      object obj;
      int num = this.StaticProperties.TryGetValue(new PXGraph.PXGraphPrototype.MethodKey()
      {
        M = key,
        Keys = depends
      }, out obj) ? 1 : 0;
      V = (T) obj;
      return num != 0;
    }

    public void SetValue<T>(T V, object key, params object[] depends)
    {
      this.StaticProperties.TryAdd(new PXGraph.PXGraphPrototype.MethodKey()
      {
        M = key,
        Keys = depends
      }, (object) V);
    }

    private class MethodKey
    {
      public object M;
      public object[] Keys;

      public override bool Equals(object obj)
      {
        PXGraph.PXGraphPrototype.MethodKey methodKey1 = (PXGraph.PXGraphPrototype.MethodKey) obj;
        PXGraph.PXGraphPrototype.MethodKey methodKey2 = this;
        if (methodKey2.M != methodKey1.M || methodKey2.Keys.Length != methodKey1.Keys.Length)
          return false;
        for (int index = 0; index < methodKey2.Keys.Length; ++index)
        {
          if (!object.Equals(methodKey2.Keys[index], methodKey1.Keys[index]))
            return false;
        }
        return true;
      }

      public override int GetHashCode()
      {
        int hashCode = this.M.GetHashCode();
        foreach (object key in this.Keys)
        {
          if (key != null)
            hashCode ^= key.GetHashCode();
        }
        return hashCode;
      }
    }
  }

  /// <exclude />
  internal sealed class GraphStaticInfo
  {
    public PXGraph._InitializeDelegate InitializeDelegate;
    public Dictionary<System.Type, PXCache.AlteredSource> AlteredSource;
    public Dictionary<System.Type, PXCache.AlteredDescriptor> AlteredDescriptor;
    public System.Type Wrapper;
    public Dictionary<string, System.Type> InactiveViews;
    public Dictionary<string, string> InactiveActions;
    public List<System.Type> Extensions;
    public PXGraph.PXGraphPrototype Prototype = new PXGraph.PXGraphPrototype();

    public GraphStaticInfo()
    {
    }

    public GraphStaticInfo(PXGraph.GraphStaticInfo other)
    {
      this.InitializeDelegate = other.InitializeDelegate;
      this.AlteredSource = other.AlteredSource;
      this.Wrapper = other.Wrapper;
      this.InactiveViews = other.InactiveViews;
      this.InactiveActions = other.InactiveActions;
      this.Extensions = other.Extensions;
    }
  }

  internal class FeaturesSet
  {
  }

  internal class GraphStaticInfoDictionary : 
    Dictionary<PXExtensionManager.GraphKind, PXGraph.GraphStaticInfo>,
    IPXCompanyDependent
  {
  }

  /// <summary>
  /// Allows you to manually manage the set of caches that should be persisted.
  /// </summary>
  public interface IPersistPerformer
  {
    /// <summary>
    /// Represents a set of caches whose data rows have been already persisted.
    /// </summary>
    ISet<System.Type> PersistedTypes { get; }

    /// <summary>Inserts new data rows to the database.</summary>
    void Insert(PXCache cache);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Insert(PX.Data.PXCache)" />
    void Insert(System.Type cacheType);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Insert(PX.Data.PXCache)" />
    void Insert<TTable>() where TTable : class, IBqlTable, new();

    /// <summary>Saves changed data rows to the database.</summary>
    void Update(PXCache cache);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Update(PX.Data.PXCache)" />
    void Update(System.Type cacheType);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Update(PX.Data.PXCache)" />
    void Update<TTable>() where TTable : class, IBqlTable, new();

    /// <summary>Removes deleted data rows from the database.</summary>
    void Delete(PXCache cache);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Delete(PX.Data.PXCache)" />
    void Delete(System.Type cacheType);

    /// <inheritdoc cref="M:PX.Data.PXGraph.IPersistPerformer.Delete(PX.Data.PXCache)" />
    void Delete<TTable>() where TTable : class, IBqlTable, new();
  }

  internal class Persister : PXGraph.IPersistPerformer
  {
    private readonly PXGraph _graph;

    public Persister(PXGraph graph) => this._graph = graph;

    public bool IsAborted => this.AbortingException != null;

    public int PersistedRecordsCount { get; private set; }

    public ISet<System.Type> PersistedTypes { get; } = (ISet<System.Type>) new OrderedHashSet<System.Type>();

    private System.Type FaultedType { get; set; }

    public Exception AbortingException { get; set; }

    public void Insert<TTable>() where TTable : class, IBqlTable, new()
    {
      this.Insert(typeof (TTable));
    }

    public void Update<TTable>() where TTable : class, IBqlTable, new()
    {
      this.Update(typeof (TTable));
    }

    public void Delete<TTable>() where TTable : class, IBqlTable, new()
    {
      this.Delete(typeof (TTable));
    }

    public void Insert(System.Type cacheType) => this.Persist(cacheType, PXDBOperation.Insert);

    public void Update(System.Type cacheType) => this.Persist(cacheType, PXDBOperation.Update);

    public void Delete(System.Type cacheType) => this.Persist(cacheType, PXDBOperation.Delete);

    private void Persist(System.Type cacheType, PXDBOperation operation)
    {
      this.PersistedTypes.Add(cacheType);
      this.FaultedType = cacheType;
      this.PersistedRecordsCount += this._graph.Persist(cacheType, operation);
    }

    public void Insert(PXCache cache) => this.Persist(cache, PXDBOperation.Insert);

    public void Update(PXCache cache) => this.Persist(cache, PXDBOperation.Update);

    public void Delete(PXCache cache) => this.Persist(cache, PXDBOperation.Delete);

    private void Persist(PXCache cache, PXDBOperation operation)
    {
      System.Type itemType = cache.GetItemType();
      this.PersistedTypes.Add(itemType);
      this.FaultedType = itemType;
      this.PersistedRecordsCount += cache.Persist(operation);
    }

    public void MarkPersisted(System.Type cacheType)
    {
      if (!this.PersistedTypes.Remove(cacheType))
        return;
      this._graph.Caches[cacheType].Persisted(this.IsAborted, !this.IsAborted || !(this.FaultedType == cacheType) ? (Exception) null : this.AbortingException);
    }
  }

  /// <exclude />
  internal delegate void InstanceCreatedDelegate(PXGraph graph);

  /// <exclude />
  public delegate void InstanceCreatedDelegate<TGraph>(TGraph graph) where TGraph : PXGraph;

  /// <exclude />
  protected internal abstract class InstanceCreatedSubscribers
  {
    internal PXGraph.InstanceCreatedDelegate GenericSubscribers;

    public abstract void OnInstanceCreated(PXGraph graph);
  }

  /// <exclude />
  protected sealed class InstanceCreatedSubscribers<TGraph> : PXGraph.InstanceCreatedSubscribers where TGraph : PXGraph
  {
    public PXGraph.InstanceCreatedDelegate<TGraph> Subscribers;

    public override void OnInstanceCreated(PXGraph graph)
    {
      if (this.Subscribers != null)
        this.Subscribers((TGraph) graph);
      if (this.GenericSubscribers == null)
        return;
      this.GenericSubscribers(graph);
    }
  }

  /// <summary>
  /// Represents the colection of <tt>InstanceCreated</tt> event handlers, which are
  /// invoked when a new instance of the graph is initialized.
  /// </summary>
  public sealed class InstanceCreatedEvents
  {
    /// <summary>
    /// Adds the provided handler to the collection for the specified graph type.
    /// </summary>
    /// <typeparam name="TGraph">The graph type.</typeparam>
    /// <param name="del">The handler to add to the collection.</param>
    public void AddHandler<TGraph>(PXGraph.InstanceCreatedDelegate<TGraph> del) where TGraph : PXGraph
    {
      Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> dictionary = PXContext.GetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>();
      if (dictionary == null)
        PXContext.SetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>(dictionary = new Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>());
      PXGraph.InstanceCreatedSubscribers createdSubscribers;
      if (!dictionary.TryGetValue(typeof (TGraph), out createdSubscribers))
      {
        dictionary[typeof (TGraph)] = createdSubscribers = (PXGraph.InstanceCreatedSubscribers) new PXGraph.InstanceCreatedSubscribers<TGraph>();
        ((PXGraph.InstanceCreatedSubscribers<TGraph>) createdSubscribers).Subscribers = del;
      }
      else
        ((PXGraph.InstanceCreatedSubscribers<TGraph>) createdSubscribers).Subscribers = ((PXGraph.InstanceCreatedSubscribers<TGraph>) createdSubscribers).Subscribers + del;
    }

    internal void AddHandler(System.Type tgraph, PXGraph.InstanceCreatedDelegate del)
    {
      Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> dictionary1 = PXContext.GetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>();
      if (dictionary1 == null)
        PXContext.SetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>(dictionary1 = new Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>());
      PXGraph.InstanceCreatedSubscribers createdSubscribers1;
      if (!dictionary1.TryGetValue(tgraph, out createdSubscribers1))
      {
        Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> dictionary2 = dictionary1;
        System.Type key = tgraph;
        System.Type type = typeof (PXGraph.InstanceCreatedSubscribers<>);
        System.Type[] typeArray = new System.Type[1]{ tgraph };
        PXGraph.InstanceCreatedSubscribers instance;
        PXGraph.InstanceCreatedSubscribers createdSubscribers2 = instance = (PXGraph.InstanceCreatedSubscribers) Activator.CreateInstance(type.MakeGenericType(typeArray));
        dictionary2[key] = instance;
        createdSubscribers2.GenericSubscribers = del;
      }
      else
        createdSubscribers1.GenericSubscribers += del;
    }

    /// <summary>
    /// Removes the provided handler from the collection for the specified graph type.
    /// </summary>
    /// <typeparam name="TGraph">The graph type.</typeparam>
    /// <param name="del">The handler to remove from the collection.</param>
    public void RemoveHandler<TGraph>(PXGraph.InstanceCreatedDelegate<TGraph> del) where TGraph : PXGraph
    {
      Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> slot = PXContext.GetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>();
      PXGraph.InstanceCreatedSubscribers createdSubscribers;
      if (slot == null || !slot.TryGetValue(typeof (TGraph), out createdSubscribers))
        return;
      ((PXGraph.InstanceCreatedSubscribers<TGraph>) createdSubscribers).Subscribers = ((PXGraph.InstanceCreatedSubscribers<TGraph>) createdSubscribers).Subscribers - del;
    }

    internal void RemoveHandler(PXGraph.InstanceCreatedDelegate del, System.Type graphType)
    {
      Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> slot = PXContext.GetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>();
      PXGraph.InstanceCreatedSubscribers createdSubscribers;
      if (slot == null || !slot.TryGetValue(graphType, out createdSubscribers))
        return;
      createdSubscribers.GenericSubscribers -= del;
    }

    internal void OnInstanceCreated(PXGraph graph)
    {
      Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers> slot = PXContext.GetSlot<Dictionary<System.Type, PXGraph.InstanceCreatedSubscribers>>();
      PXGraph.InstanceCreatedSubscribers createdSubscribers;
      if (slot == null || !slot.TryGetValue(CustomizedTypeManager.GetTypeNotCustomized(graph), out createdSubscribers))
        return;
      createdSubscribers.OnInstanceCreated(graph);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowSelectingEvents(PXGraph graph) : PX.Data.RowSelectingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowSelecting), typeof (PXRowSelectingEventArgs), typeof (PXGraph.RowSelectingEvents), typeof (EventsBase<PXRowSelectingEventArgs, PXRowSelecting, List<PXRowSelecting>>.Interceptor));

    public new void AddHandler(string view, PXRowSelecting handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowSelecting handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowSelecting handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowSelecting handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowSelectingEventArgs, Events.RowSelecting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowSelecting handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowSelecting handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowSelectedEvents(PXGraph graph) : PX.Data.RowSelectedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowSelected), typeof (PXRowSelectedEventArgs), typeof (PXGraph.RowSelectedEvents), typeof (EventsBase<PXRowSelectedEventArgs, PXRowSelected, List<PXRowSelected>>.Interceptor));

    public new void AddHandler(string view, PXRowSelected handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowSelected handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowSelected handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowSelected handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowSelectedEventArgs, Events.RowSelected<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowSelected handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowSelected handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowInsertingEvents(PXGraph graph) : PX.Data.RowInsertingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowInserting), typeof (PXRowInsertingEventArgs), typeof (PXGraph.RowInsertingEvents), typeof (EventsBase<PXRowInsertingEventArgs, PXRowInserting, List<PXRowInserting>>.Interceptor));

    public new void AddHandler(string view, PXRowInserting handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowInserting handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowInserting handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowInserting handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowInsertingEventArgs, Events.RowInserting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowInserting handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowInserting handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowInsertedEvents(PXGraph graph) : PX.Data.RowInsertedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowInserted), typeof (PXRowInsertedEventArgs), typeof (PXGraph.RowInsertedEvents), typeof (EventsBase<PXRowInsertedEventArgs, PXRowInserted, List<PXRowInserted>>.Interceptor));

    public new void AddHandler(string view, PXRowInserted handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowInserted handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowInserted handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowInserted handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowInsertedEventArgs, Events.RowInserted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowInserted handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowInserted handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowUpdatingEvents(PXGraph graph) : PX.Data.RowUpdatingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowUpdating), typeof (PXRowUpdatingEventArgs), typeof (PXGraph.RowUpdatingEvents), typeof (EventsBase<PXRowUpdatingEventArgs, PXRowUpdating, List<PXRowUpdating>>.Interceptor));

    public new void AddHandler(string view, PXRowUpdating handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowUpdating handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowUpdating handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowUpdating handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowUpdatingEventArgs, Events.RowUpdating<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowUpdating handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowUpdating handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowUpdatedEvents(PXGraph graph) : PX.Data.RowUpdatedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowUpdated), typeof (PXRowUpdatedEventArgs), typeof (PXGraph.RowUpdatedEvents), typeof (EventsBase<PXRowUpdatedEventArgs, PXRowUpdated, List<PXRowUpdated>>.Interceptor));

    public new void AddHandler(string view, PXRowUpdated handler) => base.AddHandler(view, handler);

    public new void RemoveHandler(string view, PXRowUpdated handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowUpdated handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowUpdated handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowUpdatedEventArgs, Events.RowUpdated<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowUpdated handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowUpdated handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowDeletingEvents(PXGraph graph) : PX.Data.RowDeletingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowDeleting), typeof (PXRowDeletingEventArgs), typeof (PXGraph.RowDeletingEvents), typeof (EventsBase<PXRowDeletingEventArgs, PXRowDeleting, List<PXRowDeleting>>.Interceptor));

    public new void AddHandler(string view, PXRowDeleting handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowDeleting handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowDeleting handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowDeleting handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowDeletingEventArgs, Events.RowDeleting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowDeleting handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowDeleting handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowDeletedEvents(PXGraph graph) : PX.Data.RowDeletedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowDeleted), typeof (PXRowDeletedEventArgs), typeof (PXGraph.RowDeletedEvents), typeof (EventsBase<PXRowDeletedEventArgs, PXRowDeleted, List<PXRowDeleted>>.Interceptor));

    public new void AddHandler(string view, PXRowDeleted handler) => base.AddHandler(view, handler);

    public new void RemoveHandler(string view, PXRowDeleted handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowDeleted handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowDeleted handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowDeletedEventArgs, Events.RowDeleted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowDeleted handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowDeleted handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowPersistingEvents(PXGraph graph) : PX.Data.RowPersistingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowPersisting), typeof (PXRowPersistingEventArgs), typeof (PXGraph.RowPersistingEvents), typeof (EventsBase<PXRowPersistingEventArgs, PXRowPersisting, List<PXRowPersisting>>.Interceptor));

    public new void AddHandler(string view, PXRowPersisting handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowPersisting handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowPersisting handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowPersisting handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowPersistingEventArgs, Events.RowPersisting<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowPersisting handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowPersisting handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class RowPersistedEvents(PXGraph graph) : PX.Data.RowPersistedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXRowPersisted), typeof (PXRowPersistedEventArgs), typeof (PXGraph.RowPersistedEvents), typeof (EventsBase<PXRowPersistedEventArgs, PXRowPersisted, List<PXRowPersisted>>.Interceptor));

    public new void AddHandler(string view, PXRowPersisted handler)
    {
      base.AddHandler(view, handler);
    }

    public new void RemoveHandler(string view, PXRowPersisted handler)
    {
      base.RemoveHandler(view, handler);
    }

    public new void AddHandler<TTable>(PXRowPersisted handler) => base.AddHandler<TTable>(handler);

    public new void RemoveHandler<TTable>(PXRowPersisted handler)
    {
      base.RemoveHandler<TTable>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.AddHandler<TTable>(handler);
    }

    public new void RemoveHandler<TTable>(
      Events.Event<
      #nullable enable
      PXRowPersistedEventArgs, Events.RowPersisted<TTable>>.EventDelegate handler)
      where TTable : 
      #nullable disable
      class, IBqlTable, new()
    {
      base.RemoveHandler<TTable>(handler);
    }

    public new void AddHandler(System.Type table, PXRowPersisted handler)
    {
      base.AddHandler(table, handler);
    }

    public new void RemoveHandler(System.Type table, PXRowPersisted handler)
    {
      base.RemoveHandler(table, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class CommandPreparingEvents(PXGraph graph) : PX.Data.CommandPreparingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXCommandPreparing), typeof (PXCommandPreparingEventArgs), typeof (PXGraph.CommandPreparingEvents), typeof (EventsBase<PXCommandPreparingEventArgs, PXCommandPreparing, PXCache.EventDictionary<PXCommandPreparing>>.Interceptor));

    public new void AddHandler(string view, string field, PXCommandPreparing handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXCommandPreparing handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXCommandPreparing handler)
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(PXCommandPreparing handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXCommandPreparingEventArgs, Events.CommandPreparing<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXCommandPreparing handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXCommandPreparing handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class FieldDefaultingEvents(PXGraph graph) : PX.Data.FieldDefaultingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXFieldDefaulting), typeof (PXFieldDefaultingEventArgs), typeof (PXGraph.FieldDefaultingEvents), typeof (EventsBase<PXFieldDefaultingEventArgs, PXFieldDefaulting, PXCache.EventDictionary<PXFieldDefaulting>>.Interceptor));

    public new void AddHandler(string view, string field, PXFieldDefaulting handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXFieldDefaulting handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXFieldDefaulting handler)
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(PXFieldDefaulting handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldDefaultingEventArgs, Events.FieldDefaulting<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXFieldDefaulting handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXFieldDefaulting handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class FieldUpdatingEvents(PXGraph graph) : PX.Data.FieldUpdatingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXFieldUpdating), typeof (PXFieldUpdatingEventArgs), typeof (PXGraph.FieldUpdatingEvents), typeof (EventsBase<PXFieldUpdatingEventArgs, PXFieldUpdating, PXCache.EventDictionary<PXFieldUpdating>>.Interceptor));

    public new void AddHandler(string view, string field, PXFieldUpdating handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXFieldUpdating handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXFieldUpdating handler) => base.AddHandler<TField>(handler);

    public new void RemoveHandler<TField>(PXFieldUpdating handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldUpdatingEventArgs, Events.FieldUpdating<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXFieldUpdating handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXFieldUpdating handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class FieldUpdatedEvents(PXGraph graph) : PX.Data.FieldUpdatedEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXFieldUpdated), typeof (PXFieldUpdatedEventArgs), typeof (PXGraph.FieldUpdatedEvents), typeof (EventsBase<PXFieldUpdatedEventArgs, PXFieldUpdated, PXCache.EventDictionary<PXFieldUpdated>>.Interceptor));

    public new void AddHandler(string view, string field, PXFieldUpdated handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXFieldUpdated handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXFieldUpdated handler) => base.AddHandler<TField>(handler);

    public new void RemoveHandler<TField>(PXFieldUpdated handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldUpdatedEventArgs, Events.FieldUpdated<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXFieldUpdated handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXFieldUpdated handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class FieldSelectingEvents(PXGraph graph) : PX.Data.FieldSelectingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXFieldSelecting), typeof (PXFieldSelectingEventArgs), typeof (PXGraph.FieldSelectingEvents), typeof (EventsBase<PXFieldSelectingEventArgs, PXFieldSelecting, PXCache.EventDictionary<PXFieldSelecting>>.Interceptor));

    public new void AddHandler(string view, string field, PXFieldSelecting handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXFieldSelecting handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXFieldSelecting handler)
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(PXFieldSelecting handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldSelectingEventArgs, Events.FieldSelecting<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXFieldSelecting handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXFieldSelecting handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class ExceptionHandlingEvents(PXGraph graph) : PX.Data.ExceptionHandlingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXExceptionHandling), typeof (PXExceptionHandlingEventArgs), typeof (PXGraph.ExceptionHandlingEvents), typeof (EventsBase<PXExceptionHandlingEventArgs, PXExceptionHandling, PXCache.EventDictionary<PXExceptionHandling>>.Interceptor));

    public new void AddHandler(string view, string field, PXExceptionHandling handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXExceptionHandling handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXExceptionHandling handler)
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(PXExceptionHandling handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXExceptionHandlingEventArgs, Events.ExceptionHandling<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXExceptionHandling handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXExceptionHandling handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  /// <exclude />
  public sealed class FieldVerifyingEvents(PXGraph graph) : PX.Data.FieldVerifyingEvents(graph)
  {
    internal static readonly EventDescription.Definition Definition = new EventDescription.Definition(typeof (PXFieldVerifying), typeof (PXFieldVerifyingEventArgs), typeof (PXGraph.FieldVerifyingEvents), typeof (EventsBase<PXFieldVerifyingEventArgs, PXFieldVerifying, PXCache.EventDictionary<PXFieldVerifying>>.Interceptor));

    public new void AddHandler(string view, string field, PXFieldVerifying handler)
    {
      base.AddHandler(view, field, handler);
    }

    public new void RemoveHandler(string view, string field, PXFieldVerifying handler)
    {
      base.RemoveHandler(view, field, handler);
    }

    public new void AddHandler<TField>(PXFieldVerifying handler)
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(PXFieldVerifying handler)
    {
      base.RemoveHandler<TField>(handler);
    }

    [Obsolete("Avoid manual subscription of auto subscribing event handlers. Instead use the AddAbstractHandler method and the interfaces from the AbstractEvents class.")]
    public new void AddHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.AddHandler<TField>(handler);
    }

    public new void RemoveHandler<TField>(
      Events.Event<
      #nullable enable
      PXFieldVerifyingEventArgs, Events.FieldVerifying<TField>>.EventDelegate handler)
      where TField : 
      #nullable disable
      class, IBqlField
    {
      base.RemoveHandler<TField>(handler);
    }

    public new void AddHandler(System.Type table, string field, PXFieldVerifying handler)
    {
      base.AddHandler(table, field, handler);
    }

    public new void RemoveHandler(System.Type table, string field, PXFieldVerifying handler)
    {
      base.RemoveHandler(table, field, handler);
    }
  }

  /// <exclude />
  internal class PXDebugView
  {
    private PXGraph G;

    public PXDebugView(PXGraph g) => this.G = g;

    public PXCache[] DirtyCaches
    {
      get
      {
        return ((IEnumerable<PXCache>) this.G.Caches.Caches).Where<PXCache>((Func<PXCache, bool>) (_ => _.IsDirty)).ToArray<PXCache>();
      }
    }

    public IBqlTable[] Currents => this.G.Caches.Currents;

    public PXCache[] Caches => this.G.Caches.Caches;

    public string[] CacheMap
    {
      get
      {
        return this.G.Caches.Select<KeyValuePair<System.Type, PXCache>, string>((Func<KeyValuePair<System.Type, PXCache>, string>) (_ => $"{_.Key.FullName}->{_.Value.GetItemType().FullName}")).ToArray<string>();
      }
    }

    public PXViewCollection Views => this.G.Views;
  }

  /// <summary>
  /// A class that is used for managing of side panel actions.
  /// These actions allow a user to navigate to different parts of the application directly from the side panel.
  /// </summary>
  public class SidePanelAction
  {
    public const string Prefix = "NavigateToLayer$";

    public SidePanelAction(
      string actionName,
      string icon,
      string toolTip,
      string after = null,
      Placement placement = Placement.After)
    {
      this.ActionName = actionName;
      this.Icon = icon;
      this.ToolTip = toolTip;
      this.Placement = placement;
      this.After = after;
    }

    public string ActionName { get; }

    public string Icon { get; }

    public string ToolTip { get; }

    public bool Visible { get; set; } = true;

    public Placement Placement { get; }

    public string After { get; }
  }
}

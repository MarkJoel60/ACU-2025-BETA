// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data.Api.Export;
using PX.Data.BQL;
using PX.Data.DacDescriptorGeneration;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.ReferentialIntegrity.Inspecting;
using PX.Data.SQLTree;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>Configures the lookup control for a DAC field that references a data record from a particular table by holding its key field.See</summary>
/// <remarks>
///   <para>The attribute configures the input control for a DAC field that references a data record from a particular table. Such field holds a key value that
/// identifies the data record in this table.</para>
///   <para>The input control will be of "lookup" type (may also be called a "selector"). A user can either input the value for the field manually or select from the
/// list of the data records. If a value is inserted manually, the attribute checks if it is included in the list. You can specify a complex BQL query to define
/// the set of data records that appear in the list.</para>
///   <para>The key field usually represents a database identity column that may not be user-friendly (surrogate key). It is possible to substitute its value with the
/// value of another field from the same data record (natural key). This field should be specified in the <tt>SubstituteKey</tt> property. In this case, the table,
/// and the DAC, have two fields that uniquely identify a data record from this table. For example, the <tt>Account</tt> table may have the numeric
/// <tt>AccountID</tt> field and the user-friendly string <tt>AccountCD</tt> field. On a field that references <tt>Account</tt> data records in another DAC, you
/// should place the <tt>PXSelector</tt> attribute as follows.</para>
///   <code>[PXSelector(typeof(Search&lt;Account.accountID&gt;), SubstituteKey =
/// typeof(Account.accountCD))]</code>
///   <para>The attribute will automatically convert the stored numeric value to the displayed string value and back. Note that only the <tt>AccountCD</tt> property
/// should be marked with <tt>IsKey</tt> property set to <tt>true</tt>.</para>
///   <para>It is also possible to define the list of columns to display. You can use an appropriated constructor and specify the types of the fields. By default, all
/// fields that have the <tt>PXUIField</tt> attribute's <tt>Visibility</tt> property set to <tt>PXUIVisibility.SelectorVisible</tt>.</para>
///   <para>Along with a key, some other field can be displayed as the description of the key. This field should be specified in the <tt>DescriptionField</tt> property.
/// The way the description is displayed in the lookup control is configured in the webpage layout through the <tt>DisplayMode</tt> property of the
/// <tt>PXSelector</tt> control. The default display format is <i>ValueField – DescriptionField</i>. It can be changed to display the description only.</para>
///   <para>To achieve better performance, the attribute can be configured to cache the displayed data records.</para>
/// </remarks>
/// <example>
/// <code title="Example" lang="CS">
/// //The example below shows the simplest PXSelector attribute declaration.
/// //All Category data records will be available for selection.
/// //Their CategoryCD field values will be inserted without conversion.
/// [PXSelector(typeof(Category.categoryCD))]
/// public virtual string CategoryCD { get; set; }</code>
/// <code title="Example2" lang="CS">
/// //The attribute below configures the lookup control to let the user select from the Customer data records
/// //retrieved by the Search BQL query.
/// //The displayed columns are specified explicitly: AccountCD and CompanyName.
/// [PXSelector(
///     typeof(Search&lt;Customer.accountCD,
///                Where&lt;Customer.companyType, Equal&lt;CompanyType.customer&gt;&gt;&gt;),
///     new Type[]
///     {
///         typeof(Customer.accountCD),
///         typeof(Customer.companyName)
///     })]
/// public virtual string AccountCD { get; set; }</code>
///   <code title="Example3" lang="CS">
/// //The Customer.accountCD field data will be inserted as a value without conversion.
/// //The attribute below let the user select from the Branch data records.
/// //The attribute displays the Branch.BranchCD field value in the user interface,
/// //but actually assigns the Branch.BranchID field value to the field.
/// [PXSelector(typeof(Branch.branchID),
///             SubstituteKey = typeof(Branch.branchCD))]
/// public virtual int? BranchID { get; set; }</code>
/// <code title="Example4" lang="CS">
/// //The example below shows the PXSelector attribute in combination with other attributes.
/// //Here, the PXSelector attribute configures a lookup field that will let a user select from the data set
/// //defined by the Search query. The lookup control will display descriptions the data records,
/// //taking them from CRLeadClass.description field. The attribute will cache records in memory
/// //to reduce the number of database calls.
/// [PXDBString(10, IsUnicode = true, InputMask = "&gt;aaaaaaaaaa")]
/// [PXUIField(DisplayName = "Class ID")]
/// [PXSelector(
///     typeof(Search&lt;CRLeadClass.cRLeadClassID,
///                Where&lt;CRLeadClass.isActive, Equal&lt;True&gt;&gt;&gt;),
///     DescriptionField = typeof(CRLeadClass.description),
///     CacheGlobal = true)]
/// public virtual string ClassID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXSelectorAttribute))]
public class PXSelectorAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXDependsOnFields
{
  protected System.Type _Type;
  protected System.Type _BqlType;
  protected System.Type _FilterEntity;
  protected System.Type _CacheType;
  protected BqlCommand _Select;
  protected int _ParsCount;
  protected int _ParsSimpleCount;
  protected BqlCommand _PrimarySelect;
  protected internal BqlCommand _PrimarySimpleSelect;
  protected BqlCommand _OriginalSelect;
  protected BqlCommand _NaturalSelect;
  protected BqlCommand _LookupSelect;
  protected BqlCommand _UnconditionalSelect;
  protected string[] _FieldList;
  protected string[] _HeaderList;
  protected string _ViewName;
  protected System.Type _DescriptionField;
  protected string _DescriptionDisplayName;
  protected System.Type _SubstituteKey;
  protected bool _DirtyRead;
  protected bool _Filterable;
  protected bool _CacheGlobal;
  protected bool _ViewCreated;
  protected bool _IsOwnView;
  protected UIFieldRef _UIFieldRef;
  private System.Type _originalBqlTable;
  protected Delegate _ViewHandler;
  /// <summary>Allows to control validation process.</summary>
  public bool ValidateValue = true;
  protected System.Type _ValueField;
  protected PXSelectorMode _SelectorMode;
  internal static bool _Initialized = false;
  internal static object _lockObj = new object();
  private static readonly ConcurrentDictionary<Tuple<System.Type, System.Type>, Func<BqlCommand>> WhereAndFactories = new ConcurrentDictionary<Tuple<System.Type, System.Type>, Func<BqlCommand>>();
  internal const char FieldNamesSeparator = '!';
  private bool? _isReadDeletedSupported;
  protected static ConcurrentDictionary<System.Type, PXSelectorAttribute.SubstituteKeyInfo> _substitutekeys = new ConcurrentDictionary<System.Type, PXSelectorAttribute.SubstituteKeyInfo>();
  protected internal PXEventSubscriberAttribute.ObjectRef<bool> _BypassFieldVerifying;
  protected static Dictionary<System.Type, List<KeyValuePair<string, System.Type>>> _SelectorFields = new Dictionary<System.Type, List<KeyValuePair<string, System.Type>>>();
  protected internal const string selectorBypassInit = "selectorBypassInit";
  private static readonly ConcurrentDictionary<System.Type, WeakSet<PXGraph>> SelfSelectingTables = new ConcurrentDictionary<System.Type, WeakSet<PXGraph>>();

  public bool IsPrimaryViewCompatible { get; set; }

  /// <summary>
  /// Returns Bql command used for selection of referenced records.
  /// </summary>
  /// <exclude />
  public virtual BqlCommand GetSelect() => this._Select;

  [InjectDependencyOnTypeLevel]
  internal ITableReferenceCollector TableReferenceCollector { get; set; }

  [InjectDependencyOnTypeLevel]
  internal SelectorToReferenceConverter ReferenceConverter { get; set; }

  [InjectDependencyOnTypeLevel]
  protected IMacroVariablesManager MacroVariablesManager { get; set; }

  [PXInternalUseOnly]
  public virtual bool ShowWarningForNotExistsOnSelect { get; set; }

  public virtual string CustomMessageElementDoesntExist { get; set; }

  public virtual string CustomMessageValueDoesntExist { get; set; }

  public virtual string CustomMessageElementDoesntExistOrNoRights { get; set; }

  public virtual string CustomMessageValueDoesntExistOrNoRights { get; set; }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// should cache the data records retrieved from the database to show in
  /// the lookup control. By default, the attribute does not cache the data
  /// records.</summary>
  public virtual bool CacheGlobal
  {
    get => this._CacheGlobal;
    set
    {
      if (this._NaturalSelect != null && this._CacheGlobal != value)
      {
        System.Type referencedType = ((IEnumerable<IBqlParameter>) this._NaturalSelect.GetParameters()).Last<IBqlParameter>().GetReferencedType();
        this._NaturalSelect = this.BuildNaturalSelect(value, referencedType);
      }
      this._CacheGlobal = value;
    }
  }

  /// <summary>Gets or sets the field from the referenced table that
  /// contains the description.</summary>
  /// <example>
  /// In the code below, the <apiname>PXSelector</apiname> attribute configures
  /// a lookup field that will let a user select from the data set defined
  /// by the <tt>Search</tt> query. The lookup control will display descriptions
  /// of the data records taken from <tt>CRLeadClass.description</tt> field.
  /// <code>
  /// [PXDBString(10, IsUnicode = true, InputMask = "&gt;aaaaaaaaaa")]
  /// [PXUIField(DisplayName = "Class ID")]
  /// [PXSelector(
  ///     typeof(Search&lt;CRLeadClass.cRLeadClassID,
  ///                Where&lt;CRLeadClass.isActive, Equal&lt;True&gt;&gt;&gt;),
  ///     DescriptionField = typeof(CRLeadClass.description))]
  /// public virtual string ClassID { get; set; }
  /// </code>
  /// </example>
  public virtual System.Type DescriptionField
  {
    get => this._DescriptionField;
    set
    {
      this._DescriptionField = value == (System.Type) null || typeof (IBqlField).IsAssignableFrom(value) && value.IsNested ? value : throw new PXException("The Description field cannot be set from type '{0}'.", new object[1]
      {
        (object) value
      });
    }
  }

  /// <summary>
  /// The name that is displayed in the user interface for the <see cref="P:PX.Data.PXSelectorAttribute.DescriptionField" /> field.
  /// </summary>
  public virtual string DescriptionDisplayName
  {
    get => this._DescriptionDisplayName;
    set => this._DescriptionDisplayName = value;
  }

  public virtual bool ShowPopupWarning { get; set; }

  public virtual bool ShowPopupMessage { get; set; }

  /// <summary>
  /// Gets or sets the type that is used as a key for saved filters.
  /// </summary>
  public virtual System.Type FilterEntity
  {
    get => this._FilterEntity;
    set
    {
      this._FilterEntity = value == (System.Type) null || typeof (IBqlTable).IsAssignableFrom(value) ? value : throw new PXException("The Filter Entity cannot be set from type '{0}'.", new object[1]
      {
        (object) value
      });
    }
  }

  /// <summary>Gets or sets the field from the referenced table that
  /// substitutes the key field used as internal value and is displayed as a
  /// value in the user interface (natural key).</summary>
  /// <example>
  /// The attribute below let the user select from the <tt>Branch</tt> data records.
  /// The attribute displays the <tt>Branch.BranchCD</tt> field value in the user
  /// interface, but actually assigns the <tt>Branch.BranchID</tt> field value to the
  /// field.
  /// <code>
  /// [PXSelector(typeof(Branch.branchID),
  ///             SubstituteKey = typeof(Branch.branchCD))]
  /// public virtual int? BranchID { get; set; }
  /// </code>
  /// </example>
  public virtual System.Type SubstituteKey
  {
    get => this._SubstituteKey;
    set
    {
      this._SubstituteKey = value != (System.Type) null && typeof (IBqlField).IsAssignableFrom(value) && value.IsNested ? value : throw new PXException("Cannot substitute key from type '{0}'", new object[1]
      {
        (object) value
      });
      this._NaturalSelect = this.BuildNaturalSelect(this._CacheGlobal, value);
    }
  }

  /// <summary>Gets the field that identifies a referenced data record
  /// (surrogate key) and is assigned to the field annotated with the
  /// <tt>PXSelector</tt> attribute. Typically, it is the first parameter of
  /// the BQL query passed to the attribute constructor.</summary>
  public virtual System.Type Field => this.ForeignField;

  protected System.Type ForeignField => ((IBqlSearch) this._Select).GetField();

  /// <summary>Gets or sets a value that indicates whether the attribute
  /// should take into account the unsaved modifications when displaying
  /// data records in control. If <tt>false</tt>, the data records are taken
  /// from the database and not merged with the cache object. If
  /// <tt>true</tt>, the data records are merged with the modification
  /// stored in the cache object.</summary>
  public virtual bool DirtyRead
  {
    get => this._DirtyRead;
    set => this._DirtyRead = value;
  }

  /// <summary>Gets or sets the value that indicates whether the filters
  /// defined by the user should be stored in the database.</summary>
  public virtual bool Filterable
  {
    get => this._Filterable;
    set => this._Filterable = value;
  }

  /// <summary>Gets or sets the list of labels for column headers that are
  /// displayed in the lookup control. By default, the attribute uses
  /// display names of the fields.</summary>
  public virtual string[] Headers
  {
    get => this._HeaderList;
    set
    {
      if (this._FieldList == null || value != null && value.Length != this._FieldList.Length)
        throw new PXArgumentException(nameof (Headers), "The headers don't match the column list.");
      this._HeaderList = value;
    }
  }

  /// <summary>
  ///  Gets the referenced data record field whose value is
  ///  assigned to the current field (marked with the <tt>PXSelector</tt>
  ///  attribute).
  /// </summary>
  public System.Type ValueField => this._ValueField;

  /// <summary>
  /// Gets or sets the value that determines the value displayed by
  /// the selector control in the UI and some aspects of
  /// attribute's behavior. You can assign a combination of
  /// <see cref="T:PX.Data.PXSelectorMode">PXSelectorMode</see> values joined
  /// by bitwise or ("|").
  /// </summary>
  /// <example>
  /// In the following example, the <tt>SelectorMode</tt> property
  /// is used to disable autocompletion in the selector control.
  /// <code>
  /// ...
  /// [PXSelector(
  ///     typeof(FinPeriod.finPeriodID),
  ///     DescriptionField = typeof(FinPeriod.descr),
  ///     SelectorMode = PXSelectorMode.NoAutocomplete)]
  /// public virtual String FinPeriodID { get; set; }
  /// </code>
  /// </example>
  public virtual PXSelectorMode SelectorMode
  {
    get => this._SelectorMode & ~PXSelectorMode.NoAutocomplete;
    set => this._SelectorMode = value;
  }

  /// <summary>
  /// Exclude <see cref="T:PX.Data.PXSelectorAttribute" /> from using it in <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> generating process.
  /// </summary>
  public virtual bool ExcludeFromReferenceGeneratingProcess { get; set; }

  /// <summary>Gets the BQL query that is used to retrieve data records to
  /// show to the user.</summary>
  /// <remarks>This select contains condition by ID to retrieve a specific record by key.</remarks>
  public BqlCommand PrimarySelect => this._PrimarySelect;

  /// <summary>Gets the BQL query that was passed to the attribute on it's creation.</summary>
  public BqlCommand OriginalSelect => this._OriginalSelect;

  /// <exclude />
  public int ParsCount => this._ParsCount;

  /// <exclude />
  internal System.Type Type => this._Type;

  protected bool IsSelfReferencing
  {
    get
    {
      if (!(this._CacheType != (System.Type) null) || !(this._Type != (System.Type) null))
        return false;
      return this._CacheType == this._Type || this._CacheType.IsSubclassOf(this._Type);
    }
  }

  /// <exclude />
  public virtual bool SuppressUnconditionalSelect { get; set; }

  /// <exclude />
  [PXInternalUseOnly]
  public string ViewName => this._ViewName;

  /// <summary>Initializes a new instance that will use the specified BQL
  /// query to retrieve the data records to select from. The list of
  /// displayed columns is created automatically and consists of all columns
  /// from the referenced table with the <tt>Visibility</tt> property of the
  /// <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see> attribute set to
  /// <tt>PXUIVisibility.SelectorVisible</tt>.</summary>
  /// <param name="type">A BQL query that defines the data set that is shown
  /// to the user along with the key field that is used as a value. Set to a
  /// field (type part of a DAC field) to select all data records from the
  /// referenced table. Set to a BQL command of <tt>Search</tt> type to
  /// specify a complex select statement.</param>
  public PXSelectorAttribute(System.Type type)
  {
    this.SubscribeToFeatureSet();
    if (type == (System.Type) null)
      throw new PXArgumentException(nameof (type), "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(type))
    {
      this._Select = BqlCommand.CreateInstance(type);
      this._Type = BqlCommand.GetItemType(this.ForeignField);
    }
    else
    {
      this._Select = type.IsNested && typeof (IBqlField).IsAssignableFrom(type) ? BqlCommand.CreateInstance(typeof (Search<>), type) : throw new PXArgumentException(nameof (type), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) type
      });
      this._Type = BqlCommand.GetItemType(type);
    }
    this._BqlType = PXSelectorAttribute.GetDBTableType(this._Type);
    this._ValueField = this.ForeignField;
    this._LookupSelect = BqlCommand.CreateInstance(this._Select.GetType());
    this._PrimarySelect = this._Select.WhereAnd(BqlCommand.Compose(typeof (Where<,>), this._ValueField, typeof (Equal<>), typeof (Required<>), this._ValueField));
    this._PrimarySimpleSelect = this._Select.WhereAnd(BqlCommand.Compose(typeof (Where<,>), this._ValueField, typeof (Equal<>), typeof (Required<>), this._ValueField));
    this._OriginalSelect = BqlCommand.CreateInstance(this._Select.GetSelectType());
    this._UnconditionalSelect = BqlCommand.CreateInstance(typeof (Search<,>), this._ValueField, typeof (Where<,>), this._ValueField, typeof (Equal<>), typeof (Required<>), this._ValueField);
    this._ViewName = this.GenerateViewName();
  }

  /// <summary>Initializes a new instance that will use the specified BQL
  /// query to retrieve the data records to select from, and display the
  /// provided set of columns.</summary>
  /// <param name="type">A BQL query that defines the data set that is shown
  /// to the user along with the key field that is used as a value. Set to a
  /// field (type part of a DAC field) to select all data records from the
  /// referenced table. Set to a BQL command of <tt>Search</tt> type to
  /// specify a complex select statement.</param>
  /// <param name="fieldList">Fields to display in the control.</param>
  /// <example>
  /// The attribute below configures the lookup control to let the user select from the
  /// <tt>Customer</tt> data records retrieved by the <tt>Search</tt> BQL
  /// query. The displayed columns are specified explicitly: <tt>AccountCD</tt> and
  /// <tt>CompanyName</tt>. The <tt>Customer.accountCD</tt> field data will be
  /// inserted as a value without conversion.
  /// <code>
  /// [PXSelector(
  ///     typeof(Search&lt;Customer.accountCD,
  ///                Where&lt;Customer.companyType, Equal&lt;CompanyType.customer&gt;&gt;&gt;),
  ///     new Type[]
  ///     {
  ///         typeof(Customer.accountCD),
  ///         typeof(Customer.companyName)
  ///     })]
  /// public virtual string AccountCD { get; set; }
  /// </code>
  /// </example>
  public PXSelectorAttribute(System.Type type, params System.Type[] fieldList)
    : this(type)
  {
    this.SetFieldList(fieldList);
  }

  public PXSelectorAttribute(System.Type type, System.Type lookupJoin, bool cacheGlobal, System.Type[] fieldList)
    : this(type)
  {
    if (lookupJoin == (System.Type) null || !typeof (IBqlJoin).IsAssignableFrom(lookupJoin))
      throw new PXArgumentException(nameof (lookupJoin), "Unsupported value {0}", new object[1]
      {
        (object) lookupJoin
      });
    this.CacheGlobal = cacheGlobal;
    this._LookupSelect = BqlCommand.CreateInstance(BqlCommand.AppendJoin(this._LookupSelect.GetType(), lookupJoin));
    this._PrimarySelect = BqlCommand.CreateInstance(BqlCommand.AppendJoin(this._PrimarySelect.GetType(), lookupJoin));
    this._ViewName = this.GenerateViewName();
    this.SetFieldList(fieldList);
  }

  internal void SubscribeToFeatureSet()
  {
    if (PXSelectorAttribute._Initialized)
      return;
    lock (PXSelectorAttribute._lockObj)
    {
      if (PXSelectorAttribute._Initialized)
        return;
      try
      {
        System.Type type = PXBuildManager.GetType("PX.Objects.CS.FeaturesSet", false);
        if (type != (System.Type) null)
          PXDatabase.Subscribe(type, (PXDatabaseTableChanged) (() =>
          {
            PXSelectorAttribute.FieldHeaderDictionaryIndependant slot = PXDatabase.GetSlot<PXSelectorAttribute.FieldHeaderDictionaryIndependant>("FieldHeaderDictionaryIndependant");
            slot._fields.Clear();
            slot._headers.Clear();
          }));
        PXSelectorAttribute._Initialized = true;
      }
      catch
      {
      }
    }
  }

  internal void SetFieldList(System.Type[] fieldList)
  {
    fieldList = this.ExpandFieldList(fieldList);
    this._FieldList = new string[fieldList.Length];
    System.Type[] tables = this._LookupSelect.GetTables();
    for (int index = 0; index < fieldList.Length; ++index)
    {
      if (!fieldList[index].IsNested || !typeof (IBqlField).IsAssignableFrom(fieldList[index]))
        throw new PXArgumentException(nameof (fieldList), "An invalid selector column has been provided.");
      this._FieldList[index] = tables.Length <= 1 || BqlCommand.GetItemType(fieldList[index]).IsAssignableFrom(tables[0]) ? fieldList[index].Name : $"{BqlCommand.GetItemType(fieldList[index]).Name}__{fieldList[index].Name}";
    }
  }

  private System.Type[] ExpandFieldList(System.Type[] fieldList)
  {
    List<System.Type> typeList = new List<System.Type>();
    foreach (System.Type field in fieldList)
    {
      if (TypeArrayOf<IBqlField>.IsTypeArray(field))
        typeList.AddRange((IEnumerable<System.Type>) TypeArrayOf<IBqlField>.CheckAndExtract(field, (string) null));
      else
        typeList.Add(field);
    }
    return typeList.ToArray();
  }

  private static System.Type GetDBTableType(System.Type tableType)
  {
    System.Type dbTableType = tableType;
    while (typeof (IBqlTable).IsAssignableFrom(dbTableType.BaseType) && !dbTableType.IsDefined(typeof (PXTableAttribute), false) && (!dbTableType.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) dbTableType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive))
      dbTableType = dbTableType.BaseType;
    return dbTableType;
  }

  private BqlCommand WhereAnd(BqlCommand select, System.Type Where)
  {
    if (!WebConfig.EnablePageOpenOptimizations)
      return select.WhereAnd(Where);
    Tuple<System.Type, System.Type> key = Tuple.Create<System.Type, System.Type>(select.GetType(), Where);
    Func<BqlCommand> func;
    if (PXSelectorAttribute.WhereAndFactories.TryGetValue(key, out func))
      return func();
    BqlCommand bqlCommand = select.WhereAnd(Where);
    func = ((Expression<Func<BqlCommand>>) (() => Expression.New(bqlCommand.GetType()))).Compile();
    PXSelectorAttribute.WhereAndFactories.TryAdd(key, func);
    return bqlCommand;
  }

  /// <exclude />
  public virtual BqlCommand WhereAnd(PXCache sender, System.Type whr)
  {
    if (!typeof (IBqlWhere).IsAssignableFrom(whr))
      return this._PrimarySelect;
    this._Select = this.WhereAnd(this._Select, whr);
    this._LookupSelect = this.WhereAnd(this._LookupSelect, whr);
    if ((object) this._ViewHandler == null)
      this._ViewHandler = (Delegate) (() =>
      {
        if (PXView.MaximumRows != 1)
          return (IEnumerable) null;
        IBqlParameter[] parameters1 = this._Select.GetParameters();
        object[] source = PXView.Parameters;
        if (this._LookupSelect.GetType() != this._Select.GetType())
        {
          IBqlParameter[] parameters2 = this._LookupSelect.GetParameters();
          if (parameters2 != null && parameters2.Length > parameters1.Length)
            source = ((IEnumerable<object>) source).Skip<object>(parameters2.Length - parameters1.Length).ToArray<object>();
        }
        List<object> objectList = new List<object>();
        for (int index = 0; index < parameters1.Length && index < source.Length && !(parameters1[index].MaskedType != (System.Type) null); ++index)
        {
          if (parameters1[index].IsVisible)
            objectList.Add(source[index]);
        }
        int startRow1 = PXView.StartRow;
        int totalRows = 0;
        int startRow2 = startRow1;
        return (IEnumerable) new PXView(sender.Graph, !this._DirtyRead, this._OriginalSelect).Select(PXView.Currents, objectList.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow2, PXView.MaximumRows, ref totalRows);
      });
    if (this._ViewCreated)
      this.CreateView(sender);
    return this.WhereAnd(this._PrimarySelect, whr);
  }

  /// <summary>
  /// Generates default view name. View name is used by UI controls when selecting list of records available for selection.
  /// </summary>
  /// <returns>A string that references a PXView instance which will be used to retrive a list of records.</returns>
  protected virtual string GenerateViewName()
  {
    if (!(this._Select is IBqlSearch))
      return (string) null;
    IBqlParameter[] parameters1 = this._Select.GetParameters();
    if (parameters1 != null)
      this._ParsSimpleCount = ((IEnumerable<IBqlParameter>) parameters1).Count<IBqlParameter>((Func<IBqlParameter, bool>) (p => p.IsVisible));
    IBqlParameter[] parameters2 = this._LookupSelect.GetParameters();
    if (parameters2 != null)
      this._ParsCount = ((IEnumerable<IBqlParameter>) parameters2).Count<IBqlParameter>((Func<IBqlParameter, bool>) (p => p.IsVisible));
    return $"_{this.ForeignField.FullName}_";
  }

  protected virtual BqlCommand BuildNaturalSelect(bool cacheGlobal, System.Type substituteKey)
  {
    System.Type where = BqlCommand.Compose(typeof (Where<,>), substituteKey, typeof (Equal<>), typeof (Required<>), substituteKey);
    if (!cacheGlobal)
      return this._Select.WhereAnd(where);
    return BqlCommand.CreateInstance(typeof (Search<,>), this.ForeignField, where);
  }

  /// <summary>
  /// A wrapper to PXView.SelectMultiBound() method, extracts the first table in a row if a result of a join is returned.<br />
  /// While we are looking for a single record here, we still call SelectMulti() for performance reason, to hit cache and get the result of previously executed queries if any.<br />
  /// 'Bound' means we will take omitted parameters from explicitly defined array of rows, not from current records set in the graph.
  /// </summary>
  /// <param name="view">PXView instance to be called for a selection result</param>
  /// <param name="currents">List of rows used as a source for omitted parameter values</param>
  /// <param name="pars">List of parameters to be passed to the query</param>
  /// <returns>Foreign record retrieved</returns>
  internal static object SelectSingleBound(PXView view, object[] currents, params object[] pars)
  {
    List<object> objectList = view.SelectMultiBound(currents, pars);
    return objectList.Count <= 0 ? (object) null : (object) PXResult.UnwrapMain(objectList[0]);
  }

  /// <summary>
  /// A wrapper to PXView.SelectSingleBound() method, extracts the first table in a row if a result of a join is returned.<br />
  /// </summary>
  /// <param name="view">PXView instance to be called for a selection result</param>
  /// <param name="pars">List of parameters to be passed to the query</param>
  /// <returns>Foreign record retrieved</returns>
  internal static object SelectSingle(PXView view, params object[] pars)
  {
    List<object> objectList = view.SelectMulti(pars);
    return objectList.Count <= 0 ? (object) null : (object) PXResult.UnwrapMain(objectList[0]);
  }

  internal static object SelectSingle(PXCache cache, object data, string field, object value)
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly(field).OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
        return enumerator.Current.GetViewWithParameters(cache, value, true).SelectMultiBound(data).FirstOrDefault<object>();
    }
    return (object) null;
  }

  internal static object SelectSingle(PXCache cache, object data, string field)
  {
    object obj = cache.GetValue(data, field);
    return PXSelectorAttribute.SelectSingle(cache, data, field, obj);
  }

  /// <summary>
  /// Returns cached typed view, can be ovirriden to substitute a view with a delegate instead.
  /// </summary>
  /// <param name="cache">PXCache instance, used to retrive a graph object</param>
  /// <param name="select">Bql command to be searched</param>
  /// <param name="dirtyRead">Flag to separate result sets either merged with not saved changes or not</param>
  /// <returns></returns>
  protected virtual PXView GetView(PXCache cache, BqlCommand select, bool isReadOnly)
  {
    return cache.Graph.TypedViews.GetView(select, isReadOnly);
  }

  protected virtual PXView GetUnconditionalView(PXCache cache)
  {
    return cache.Graph.TypedViews.GetView(this._UnconditionalSelect, !this.DirtyRead);
  }

  protected object[] MakeParameters(object lastParameter, bool includeLookupJoins = false)
  {
    object[] objArray = new object[(includeLookupJoins ? this._ParsCount : this._ParsSimpleCount) + 1];
    objArray[objArray.Length - 1] = lastParameter;
    return objArray;
  }

  protected PXSelectorAttribute.ViewWithParameters GetViewWithParameters(
    PXCache cache,
    object lastParameter,
    bool includeLookupJoins = false)
  {
    return new PXSelectorAttribute.ViewWithParameters(this.GetView(cache, includeLookupJoins ? this._PrimarySelect : this._PrimarySimpleSelect, !this.DirtyRead), this.MakeParameters(lastParameter, includeLookupJoins));
  }

  /// <summary>Returns the data record referenced by the attribute instance
  /// that marks the field with the specified name in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="field">The name of the field that is be marked with the
  /// attribute.</param>
  public static object Select(PXCache cache, object data, string field)
  {
    object obj = cache.GetValue(data, field);
    return PXSelectorAttribute.Select(cache, data, field, obj);
  }

  /// <summary>Returns the first data record retrieved by the attribute
  /// instance that marks the specified field in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static object SelectFirst<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXSelectorAttribute.SelectFirst(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the first data record retrieved by the attribute
  /// instance that marks the field with the specified name in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="field">The name of the field that is be marked with the
  /// attribute.</param>
  public static object SelectFirst(PXCache cache, object data, string field)
  {
    return PXSelectorAttribute.SelectSpecific(cache, data, field, 0);
  }

  /// <summary>Returns the last data record retrieved by the attribute
  /// instance that marks the specified field in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static object SelectLast<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXSelectorAttribute.SelectLast(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the last data record retrieved by the attribute
  /// instance that marks the field with the specified name in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="field">The name of the field that is be marked with the
  /// attribute.</param>
  public static object SelectLast(PXCache cache, object data, string field)
  {
    return PXSelectorAttribute.SelectSpecific(cache, data, field, -1);
  }

  private static object SelectSpecific(PXCache cache, object data, string field, int rowPosition)
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly(field).OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXSelectorAttribute current = enumerator.Current;
        PXView view = cache.Graph.TypedViews.GetView(current._Select, !current._DirtyRead);
        int num1 = rowPosition;
        int num2 = 0;
        object[] currents = new object[1]{ data };
        ref int local1 = ref num1;
        ref int local2 = ref num2;
        List<object> objectList = view.Select(currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2);
        return objectList != null && objectList.Count > 0 ? (object) PXResult.UnwrapMain(objectList[objectList.Count - 1]) : (object) null;
      }
    }
    return (object) null;
  }

  /// <summary>Returns the referenced data record that holds the specified
  /// value. The data record should be referenced by the attribute instance
  /// that marks the field with the specified in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="field">The name of the field that is be marked with the
  /// attribute.</param>
  /// <param name="value">The value to search the referenced table
  /// for.</param>
  /// <returns>Foreign record.</returns>
  public static object Select(PXCache cache, object data, string field, object value)
  {
    return cache.GetAttributesReadonly(field).OfType<PXSelectorAttribute>().Select<PXSelectorAttribute, object>((Func<PXSelectorAttribute, object>) (attr => PXSelectorAttribute.GetItem(cache, attr, data, value))).FirstOrDefault<object>();
  }

  internal PXSelectorAttribute.GlobalDictionary GetGlobalCache()
  {
    return PXSelectorAttribute.GlobalDictionary.GetOrCreate(this._Type, this._BqlType, this.KnownForeignKeysCount);
  }

  protected virtual void AppendOtherValues(
    Dictionary<string, object> values,
    PXCache cache,
    object row)
  {
  }

  protected virtual object CreateGlobalCacheKey(PXCache cache, object row, object keyValue)
  {
    return PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(keyValue);
  }

  protected virtual byte KnownForeignKeysCount => 1;

  protected bool CanCacheGlobal(PXCache foreignCache)
  {
    return foreignCache.Keys.Count <= (int) this.KnownForeignKeysCount && !foreignCache._AggregateSelecting && !foreignCache._SingleTableSelecting;
  }

  /// <summary>Returns the foreign data record by the specified
  /// key.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="attr">The instance of the <tt>PXSelector</tt> attribute
  /// to query for a data record.</param>
  /// <param name="data">The data record that contains a reference to the
  /// foreign data record.</param>
  /// <param name="key">The key value of the referenced data record.</param>
  public static object GetItem(PXCache cache, PXSelectorAttribute attr, object data, object key)
  {
    return PXSelectorAttribute.GetItem(cache, attr, data, key, false);
  }

  internal static object GetItem(
    PXCache cache,
    PXSelectorAttribute attr,
    object data,
    object key,
    bool unconditionally)
  {
    PXSelectorAttribute.GlobalDictionary globalDictionary;
    object foreignItem = PXSelectorAttribute.LookupGlobalDictionary(cache, attr, data, key, out globalDictionary);
    if (foreignItem == null && (key == null || key.GetType() == cache.GetFieldType(attr._FieldName)))
    {
      PXSelectorAttribute.ViewWithParameters viewWithParameters = attr.GetViewWithParameters(cache, key);
      foreignItem = viewWithParameters.SelectSingleBound(data);
      if (foreignItem == null)
      {
        if (!unconditionally || attr.SuppressUnconditionalSelect)
          return (object) null;
        PXView unconditionalView = attr.GetUnconditionalView(cache);
        object obj = cache._InvokeSelectorGetter(data, attr.FieldName, unconditionalView, new object[1]
        {
          key
        }, true);
        if (obj == null)
          obj = PXSelectorAttribute.SelectSingleBound(unconditionalView, new object[1]
          {
            data
          }, key);
        return obj;
      }
      attr.cacheOnReadItem(globalDictionary, viewWithParameters.Cache, attr.ForeignField.Name, foreignItem, key, false);
    }
    return foreignItem;
  }

  /// <summary>
  /// Gets the DAC referenced by this selector attribute and <paramref name="foreignKeyValue" /> from the database without the lookup into selector's cache.
  /// </summary>
  /// <param name="cache">The cache</param>
  /// <param name="row">The DAC instance which has this <see cref="T:PX.Data.PXSelectorAttribute" /> and references the required foreign DAC.</param>
  /// <param name="foreignKeyValue">The value of a foreign key of the referenced DAC.</param>
  /// <returns />
  internal IBqlTable GetReferencedDacWithoutSelectorCacheUsage(
    PXCache cache,
    object row,
    object foreignKeyValue)
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    return this.GetViewWithParameters(cache, foreignKeyValue).SelectSingleBound(row) as IBqlTable;
  }

  internal static object GetItemUnconditionally(
    PXCache cache,
    PXSelectorAttribute attr,
    object key)
  {
    if (key == null)
      return (object) null;
    if (attr.SuppressUnconditionalSelect)
      return (object) null;
    object itemUnconditionally1 = PXSelectorAttribute.LookupGlobalDictionary(cache, attr, (object) null, key, out PXSelectorAttribute.GlobalDictionary _);
    System.Type fieldType = cache.GetFieldType(attr._FieldName);
    if ((itemUnconditionally1 != null || !(fieldType == (System.Type) null)) && !(key.GetType() == fieldType))
      return itemUnconditionally1;
    PXView view = attr.GetView(cache, attr._UnconditionalSelect, !attr._DirtyRead);
    object itemUnconditionally2 = cache._InvokeSelectorGetter((object) null, attr.FieldName, view, new object[1]
    {
      key
    }, true);
    if (itemUnconditionally2 == null)
      itemUnconditionally2 = PXSelectorAttribute.SelectSingleBound(view, new object[0], key);
    return itemUnconditionally2;
  }

  private static object LookupGlobalDictionary(
    PXCache cache,
    PXSelectorAttribute attr,
    object data,
    object key,
    out PXSelectorAttribute.GlobalDictionary globalDictionary)
  {
    object obj = (object) null;
    globalDictionary = (PXSelectorAttribute.GlobalDictionary) null;
    if (attr._CacheGlobal && key != null)
    {
      globalDictionary = attr.GetGlobalCache();
      lock (globalDictionary.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (globalDictionary.TryGetValue(attr.CreateGlobalCacheKey(cache, data, key), out cacheValue))
        {
          if (!cacheValue.IsDeleted)
          {
            if (!(cacheValue.Item is IDictionary))
              obj = cacheValue.Item;
          }
        }
      }
    }
    return obj;
  }

  /// <summary>Clears the internal cache of the <tt>PXSelector</tt>
  /// attribute, removing the data records retrieved from the specified
  /// table. Typically, you don't need to call this method, because the
  /// attribute subscribes on the change notifications related to the table
  /// and drops the cache automatically.</summary>
  public static void ClearGlobalCache<Table>() where Table : IBqlTable
  {
    PXSelectorAttribute.ClearGlobalCache<Table>((byte) 1);
  }

  public static void ClearGlobalCache<Table>(byte keysCount) where Table : IBqlTable
  {
    PXSelectorAttribute.GlobalDictionary.ClearFor(typeof (Table), keysCount);
  }

  /// <summary>Clears the internal cache of the <tt>PXSelector</tt>
  /// attribute, removing the data records retrieved from the specified
  /// table. Typically, you don't need to call this method, because the
  /// attribute subscribes on the change notifications related to the table
  /// and drops the cache automatically.</summary>
  /// <param name="table">The DAC to drop from the attribute's
  /// cache.</param>
  public static void ClearGlobalCache(System.Type table)
  {
    PXSelectorAttribute.ClearGlobalCache(table, (byte) 1);
  }

  public static void ClearGlobalCache(System.Type table, byte keysCount)
  {
    if (table == (System.Type) null)
      throw new PXArgumentException(nameof (table), "The argument cannot be null.");
    PXSelectorAttribute.GlobalDictionary.ClearFor(table, keysCount);
  }

  /// <summary>Returns a value of the field from a foreign data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record that contains a reference to the
  /// foreign data record.</param>
  /// <param name="field">The name of the field holding the referenced data
  /// record key value.</param>
  /// <param name="value">The key value of the referenced data
  /// record.</param>
  /// <param name="foreignField">The name of the referenced data record
  /// field whose value is returned by the method.</param>
  public static object GetField(
    PXCache cache,
    object data,
    string field,
    object value,
    string foreignField)
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly(data, field).OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXSelectorAttribute current = enumerator.Current;
        object obj = (object) null;
        PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
        if (current._CacheGlobal && value != null)
        {
          dict = PXSelectorAttribute.GlobalDictionary.GetOrCreate(current._Type, cache.Graph.Caches[current._Type].BqlTable, current.KnownForeignKeysCount);
          lock (dict.SyncRoot)
          {
            PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
            if (dict.TryGetValue(current.CreateGlobalCacheKey(cache, data, value), out cacheValue))
            {
              if (!cacheValue.IsDeleted)
                obj = cacheValue.Item;
            }
          }
        }
        if (obj == null || obj is IDictionary dictionary && !dictionary.Contains((object) foreignField))
        {
          PXSelectorAttribute.ViewWithParameters viewWithParameters = current.GetViewWithParameters(cache, value);
          obj = viewWithParameters.SelectSingleBound(data);
          if (obj == null)
            return (object) null;
          current.cacheOnReadItem(dict, viewWithParameters.Cache, current.ForeignField.Name, obj, value, false);
        }
        return cache.Graph.Caches[current._Type].GetValue(obj, foreignField) ?? (object) new byte[0];
      }
    }
    return (object) null;
  }

  internal static void CheckIntegrityAndPutGlobal(
    PXSelectorAttribute.GlobalDictionary globalDictionary,
    PXCache foreignCache,
    string foreignField,
    object foreignRow,
    object ownKey,
    bool isRowDeleted)
  {
    Composite source = ownKey as Composite;
    if (Composite.op_Inequality(source, (Composite) null))
      ownKey = ((IEnumerable<object>) source).Last<object>();
    object key = foreignCache.GetValue(foreignRow, foreignField);
    if (!object.Equals(ownKey, PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(key)) || foreignCache.Keys.Count != 0 && !foreignCache.Keys.Contains(foreignField) && foreignCache.Identity != null && !string.Equals(foreignCache.Identity, foreignField, StringComparison.OrdinalIgnoreCase))
      return;
    lock (globalDictionary.SyncRoot)
      globalDictionary.Set((object) source ?? ownKey, foreignRow, isRowDeleted);
  }

  /// <summary>Returns the data access class referenced by the attribute
  /// instance that marks the field with specified name.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="field">The name of the field that marked with the
  /// attribute.</param>
  public static System.Type GetItemType(PXCache cache, string field)
  {
    return cache.GetAttributesReadonly(field).OfType<PXSelectorAttribute>().Select<PXSelectorAttribute, System.Type>((Func<PXSelectorAttribute, System.Type>) (a => a._Type)).FirstOrDefault<System.Type>();
  }

  /// <summary>Returns all data records kept by the attribute instance the
  /// marks the specified field in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static List<object> SelectAll<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXSelectorAttribute.SelectAll(cache, typeof (Field).Name, data);
  }

  /// <summary>Returns all data records kept by the attribute instance the
  /// marks the field with the specified name in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="fieldname">The name of the field that should be marked
  /// with the attribute.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static List<object> SelectAll(PXCache cache, string fieldname, object data)
  {
    return cache.GetAttributesReadonly(fieldname).OfType<PXSelectorAttribute>().Select<PXSelectorAttribute, PXView>((Func<PXSelectorAttribute, PXView>) (attr => attr.GetView(cache, attr._LookupSelect, !attr._DirtyRead))).Select<PXView, List<object>>((Func<PXView, List<object>>) (select => select.SelectMultiBound(new object[1]
    {
      data
    }))).FirstOrDefault<List<object>>();
  }

  /// <summary>Returns the data record referenced by the attribute instance
  /// that marks the specified field in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  public static object Select<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return PXSelectorAttribute.Select(cache, data, typeof (Field).Name);
  }

  /// <summary>Returns the referenced data record that holds the specified
  /// value. The data record is searched among the ones referenced by the
  /// attribute instance that marks the specified field in a particular data
  /// record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="value">The value to search the referenced table
  /// for.</param>
  public static object Select<Field>(PXCache cache, object data, object value) where Field : IBqlField
  {
    return PXSelectorAttribute.Select(cache, data, typeof (Field).Name, value);
  }

  /// <summary>Sets the list of columns and column headers to display for
  /// the attribute instance that marks the field with the specified name in
  /// a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="field">The name of the field marked with the
  /// attribute.</param>
  /// <param name="fieldList">The new list of field names.</param>
  /// <param name="headerList">The new list of column headers.</param>
  public static void SetColumns(
    PXCache cache,
    object data,
    string field,
    string[] fieldList,
    string[] headerList)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXSelectorAttribute selectorAttribute in cache.GetAttributes(data, field).OfType<PXSelectorAttribute>())
    {
      selectorAttribute._FieldList = fieldList;
      selectorAttribute._HeaderList = headerList;
    }
  }

  /// <summary>Sets the list of columns and column headers for all attribute
  /// instances that mark the field with the specified name in all data
  /// records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="field">The name of the field marked with the
  /// attribute.</param>
  /// <param name="fieldList">The new list of field names.</param>
  /// <param name="headerList">The new list of column headers.</param>
  public static void SetColumns(
    PXCache cache,
    string field,
    string[] fieldList,
    string[] headerList)
  {
    cache.SetAltered(field, true);
    foreach (PXSelectorAttribute selectorAttribute in cache.GetAttributes(field).OfType<PXSelectorAttribute>())
    {
      selectorAttribute._FieldList = fieldList;
      selectorAttribute._HeaderList = headerList;
    }
  }

  /// <summary>Sets the list of columns and column headers for an attribute
  /// instance.</summary>
  /// <param name="fieldList">The new list of field names.</param>
  /// <param name="headerList">The new list of column headers.</param>
  public virtual void SetColumns(string[] fieldList, string[] headerList)
  {
    this._FieldList = fieldList;
    this._HeaderList = headerList;
  }

  /// <summary>Sets the list of columns and column headers to display for
  /// the attribute instance that marks the specified field in a particular
  /// data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="fieldList">The new list of field names.</param>
  /// <param name="headerList">The new list of column headers.</param>
  public static void SetColumns<Field>(
    PXCache cache,
    object data,
    System.Type[] fieldList,
    string[] headerList)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXSelectorAttribute selectorAttribute in cache.GetAttributes<Field>(data).OfType<PXSelectorAttribute>())
      selectorAttribute.SetColumns(fieldList, headerList);
  }

  /// <summary>Sets the list of columns and column headers for all attribute
  /// instances that mark the specified field in all data records in the
  /// cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXSelector</tt> type.</param>
  /// <param name="fieldList">The new list of field names.</param>
  /// <param name="headerList">The new list of column headers.</param>
  public static void SetColumns<Field>(PXCache cache, System.Type[] fieldList, string[] headerList) where Field : IBqlField
  {
    PXSelectorAttribute.SetColumns<Field>(cache, (object) null, fieldList, headerList);
  }

  private void SetColumns(System.Type[] fieldList, string[] headerList)
  {
    this.SetFieldList(fieldList);
    this._HeaderList = headerList;
  }

  /// <exclude />
  public static void StoreCached<Field>(PXCache cache, object data, object item) where Field : IBqlField
  {
    PXSelectorAttribute.StoreCached<Field>(cache, data, item, false);
  }

  /// <exclude />
  public static void StoreCached<Field>(PXCache cache, object data, object item, bool clearCache) where Field : IBqlField
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly<Field>().OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXSelectorAttribute current = enumerator.Current;
      PXSelectorAttribute.ViewWithParameters viewWithParameters = current.GetViewWithParameters(cache, cache.GetValue(data, current._FieldOrdinal), true);
      object[] parametes = viewWithParameters.PrepareParameters(data);
      if (clearCache)
        viewWithParameters.View.Clear();
      viewWithParameters.View.StoreCached(new PXCommandKey(parametes), new List<object>()
      {
        item
      });
    }
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public static void StoreResult<Field>(PXCache cache, object data, IBqlTable selectResult) where Field : IBqlField
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly<Field>().OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXSelectorAttribute current = enumerator.Current;
      PXSelectorAttribute.ViewWithParameters viewWithParameters = current.GetViewWithParameters(cache, cache.GetValue(data, current._FieldOrdinal), true);
      object[] objArray = viewWithParameters.PrepareParameters(data);
      viewWithParameters.View.StoreResult(selectResult, PXQueryParameters.ExplicitParameters(objArray));
    }
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  public static void StoreResult<Field>(PXCache cache, IBqlTable selectResult) where Field : IBqlField
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly<Field>().OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXSelectorAttribute current = enumerator.Current;
      current.GetView(cache, current._PrimarySelect, !current.DirtyRead).StoreResult(selectResult);
    }
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  public static void StoreResult<Field>(PXCache cache, object data, List<object> selectResult) where Field : IBqlField
  {
    using (IEnumerator<PXSelectorAttribute> enumerator = cache.GetAttributesReadonly<Field>().OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXSelectorAttribute current = enumerator.Current;
      PXSelectorAttribute.ViewWithParameters viewWithParameters = current.GetViewWithParameters(cache, cache.GetValue(data, current._FieldOrdinal), true);
      object[] objArray = viewWithParameters.PrepareParameters(data);
      viewWithParameters.View.StoreResult(selectResult, PXQueryParameters.ExplicitParameters(objArray));
    }
  }

  /// <summary>
  /// Checks foreign keys and raises exception on violation. Works only if foreing key feild has PXSelectorAttribute
  /// </summary>
  /// <param name="Row">Current record</param>
  /// <param name="fieldType">BQL type of foreing key</param>
  /// <param name="searchType">Optional additional BQL statement to be checked</param>
  /// <param name="customMessage">Optional custom message to be displayed to user. Must either have {0} placeholder for name of current table
  /// and {1} placeholder for foreign key table name, or no format placeholders at all</param>
  public static void CheckAndRaiseForeignKeyException(
    PXCache sender,
    object Row,
    System.Type fieldType,
    System.Type searchType = null,
    string customMessage = null)
  {
    ForeignKeyChecker foreignKeyChecker = new ForeignKeyChecker(sender, Row, fieldType, searchType);
    if (!string.IsNullOrEmpty(customMessage))
      foreignKeyChecker.CustomMessage = customMessage;
    foreignKeyChecker.DoCheck();
  }

  /// <exclude />
  public virtual ISet<System.Type> GetDependencies(PXCache sender)
  {
    HashSet<System.Type> dependencies = new HashSet<System.Type>();
    System.Type itemType = sender.GetItemType();
    if ((itemType == this._Type || itemType.IsSubclassOf(this._Type)) && this._DescriptionField != (System.Type) null)
      dependencies.Add(this._DescriptionField);
    return (ISet<System.Type>) dependencies;
  }

  internal static bool SplitFieldNames(
    string fieldName,
    out string outerField,
    out string innerField)
  {
    string[] strArray = fieldName.Split('!');
    if (strArray.Length > 1)
    {
      outerField = strArray[0];
      innerField = strArray[1];
      return true;
    }
    outerField = fieldName;
    innerField = (string) null;
    return false;
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || !this.ValidateValue || this._BypassFieldVerifying.Value || sender.Keys.Count != 0 && !(this._FieldName != sender.Keys[sender.Keys.Count - 1]))
      return;
    object obj = (object) null;
    this.Verify(sender, e, ref obj);
  }

  protected virtual void Verify(PXCache sender, PXFieldVerifyingEventArgs e, ref object item)
  {
    if (item == null)
    {
      PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, e.NewValue);
      try
      {
        item = sender._InvokeSelectorGetter(e.Row, this._FieldName, viewWithParameters.View, viewWithParameters.Parameters, true) ?? viewWithParameters.SelectSingleBound(e.Row);
      }
      catch (FormatException ex)
      {
      }
      catch (InvalidCastException ex)
      {
      }
    }
    if (item == null)
    {
      if (this._SubstituteKey != (System.Type) null)
      {
        if (e.ExternalCall)
        {
          object valuePending = sender.GetValuePending(e.Row, this._FieldName);
          if (valuePending != null)
            e.NewValue = valuePending;
        }
        else if (object.Equals(e.NewValue, sender.GetValue(e.Row, this._FieldOrdinal)))
        {
          try
          {
            object valueExt = sender.GetValueExt(e.Row, this._FieldName);
            if (valueExt is PXFieldState)
              e.NewValue = ((PXFieldState) valueExt).Value;
            else if (valueExt != null)
              e.NewValue = valueExt;
          }
          catch
          {
          }
        }
        else
        {
          try
          {
            object newValue = e.NewValue;
            sender.RaiseFieldSelecting(this._FieldName, e.Row, ref newValue, false);
            if (newValue is PXFieldState)
              e.NewValue = ((PXFieldState) newValue).Value;
            else if (newValue != null)
              e.NewValue = newValue;
          }
          catch
          {
          }
        }
      }
      this.throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, this._PrimarySimpleSelect, e.Row), e.ExternalCall && !sender.Graph.IsContractBasedAPI, e.NewValue, (IBqlTable) e.Row);
    }
    else
    {
      if (!this.ShowPopupMessage)
        return;
      string popupNote = PXNoteAttribute.GetPopupNote(sender.Graph.Caches[this._Type], item);
      if (string.IsNullOrEmpty(popupNote))
        return;
      PopupNoteManager.RegisterText(sender, e.Row, this._FieldName, popupNote);
    }
  }

  protected internal static string[] hasRestrictedAccess(
    PXCache sender,
    BqlCommand command,
    object row)
  {
    List<string> stringList = new List<string>();
    foreach (IBqlParameter bqlParameter in ((IEnumerable<IBqlParameter>) command.GetParameters()).Where<IBqlParameter>((Func<IBqlParameter, bool>) (p => p.MaskedType != (System.Type) null)))
    {
      System.Type referencedType = bqlParameter.GetReferencedType();
      if (referencedType.IsNested)
      {
        System.Type declaringType = referencedType.DeclaringType;
        PXCache cach = sender.Graph.Caches[declaringType];
        object newValue = (object) null;
        bool flag = false;
        if (row != null && (row.GetType() == declaringType || row.GetType().IsSubclassOf(declaringType)))
        {
          newValue = cach.GetValue(row, referencedType.Name);
          flag = true;
        }
        if (!flag && newValue == null && cach.Current != null)
          newValue = cach.GetValue(cach.Current, referencedType.Name);
        if (newValue == null && bqlParameter.TryDefault && cach.RaiseFieldDefaulting(referencedType.Name, (object) null, out newValue))
          cach.RaiseFieldUpdating(referencedType.Name, (object) null, ref newValue);
        if (newValue != null)
          stringList.Add($"{referencedType.Name.ToCapitalized()}={newValue}");
      }
    }
    return stringList.Count <= 0 ? (string[]) null : stringList.ToArray();
  }

  protected void throwNoItem(string[] restricted, bool external, object value)
  {
    this.throwNoItem(restricted, external, value, (IBqlTable) null);
  }

  protected void throwNoItem(string[] restricted, bool external, object value, IBqlTable row)
  {
    PXTrace.WriteInformation("The item {0} is not found (restricted:{1},external:{2},value:{3})", (object) this.FieldName, restricted != null ? (object) string.Join(",", restricted) : (object) false.ToString(), (object) external, value);
    if (restricted == null)
    {
      if (external || value == null)
        throw new PXSetPropertyException(row, this.CustomMessageElementDoesntExist ?? "'{0}' cannot be found in the system.", new object[1]
        {
          (object) $"[{this._FieldName}]"
        });
      throw new PXSetPropertyException(row, this.CustomMessageValueDoesntExist ?? "{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) $"[{this._FieldName}]",
        value
      });
    }
    if (external || value == null)
      throw new PXSetPropertyException(row, this.CustomMessageElementDoesntExistOrNoRights ?? "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
      {
        (object) $"[{this._FieldName}]"
      });
    throw new PXSetPropertyException(row, this.CustomMessageValueDoesntExistOrNoRights ?? "{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
    {
      (object) $"[{this._FieldName}]",
      value
    });
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    bool isItemDeleted = false;
    if (this._SubstituteKey == (System.Type) null && e.ReturnValue != null && this.IsReadDeletedSupported && sender.Graph.GetType() != typeof (PXGraph) && (!this._BqlTable.IsAssignableFrom(this._BqlType) || sender.Keys.Count == 0 || string.Compare(sender.Keys[sender.Keys.Count - 1], this._FieldName, StringComparison.OrdinalIgnoreCase) != 0))
    {
      object returnValue = e.ReturnValue;
      PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
      object obj = (object) null;
      if (this._CacheGlobal)
      {
        dict = this.GetGlobalCache();
        lock (dict.SyncRoot)
        {
          PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
          if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, returnValue), out cacheValue))
          {
            obj = cacheValue.Item;
            isItemDeleted = cacheValue.IsDeleted;
          }
        }
      }
      if (obj == null)
      {
        PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, returnValue);
        object foreignItem = sender._InvokeSelectorGetter(e.Row, this._FieldName, viewWithParameters.View, viewWithParameters.Parameters, true) ?? viewWithParameters.SelectSingleBound(e.Row);
        if (foreignItem == null)
        {
          using (new PXReadDeletedScope())
          {
            foreignItem = viewWithParameters.SelectSingleBound(e.Row);
            isItemDeleted = foreignItem != null;
          }
        }
        this.cacheOnReadItem(dict, viewWithParameters.Cache, this.ForeignField.Name, foreignItem, returnValue, isItemDeleted);
      }
    }
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
    {
      if (this._HeaderList == null)
        this.populateFields(sender, PXContext.GetSlot<bool>("selectorBypassInit"));
      PXErrorLevel errorLevel = isItemDeleted ? PXErrorLevel.Warning : PXErrorLevel.Undefined;
      string error = isItemDeleted ? "The record has been deleted." : (string) null;
      if (this.ShowPopupWarning && e.Row != null)
      {
        object lastParameter = sender.GetValue(e.Row, this._FieldOrdinal);
        PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, lastParameter);
        object data = sender._InvokeSelectorGetter(e.Row, this._FieldName, viewWithParameters.View, viewWithParameters.Parameters, true) ?? viewWithParameters.SelectSingleBound(e.Row);
        string popupNote = PXNoteAttribute.GetPopupNote(viewWithParameters.Cache, data);
        if (!string.IsNullOrEmpty(popupNote) && errorLevel < PXErrorLevel.Warning)
        {
          errorLevel = PXErrorLevel.Warning;
          error = popupNote;
        }
      }
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, fieldName: this._FieldName, descriptionName: this._DescriptionField != (System.Type) null ? this._DescriptionField.Name : (string) null, error: error, errorLevel: errorLevel, viewName: this._ViewName, fieldList: this._FieldList, headerList: this._HeaderList);
      ((PXFieldState) e.ReturnState).ValueField = this._SubstituteKey == (System.Type) null ? this.ValueField.Name : this._SubstituteKey.Name;
      ((PXFieldState) e.ReturnState).SelectorMode = sender.IsAutoNumber(this._FieldName) ? this.SelectorMode : this._SelectorMode;
    }
    else
    {
      if (!isItemDeleted)
        return;
      e.ReturnState = sender.GetStateExt(e.Row, this._FieldName);
    }
  }

  protected virtual bool IsReadDeletedSupported
  {
    get
    {
      if (!this._isReadDeletedSupported.HasValue)
        this._isReadDeletedSupported = new bool?(PXDatabase.IsReadDeletedSupported(this._BqlType));
      return this._isReadDeletedSupported.Value;
    }
  }

  protected virtual void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    object key,
    ref bool deleted)
  {
    object obj = (object) null;
    PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
    if (this._CacheGlobal)
    {
      dict = this.GetGlobalCache();
      lock (dict.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, key), out cacheValue))
        {
          obj = cacheValue.Item;
          deleted = cacheValue.IsDeleted;
        }
      }
    }
    if (obj == null)
    {
      PXCache itemCache = sender;
      this.readItem(itemCache, e.Row, key, out itemCache, out obj, ref deleted);
      this.cacheOnReadItem(dict, itemCache, this.ForeignField.Name, obj, key, deleted);
    }
    if (obj == null)
      return;
    PXCache itemCache1 = sender.Graph.Caches[this._Type];
    e.ReturnValue = itemCache1.GetValue(obj, this._DescriptionField.Name);
    if (e.ReturnValue != null)
      return;
    this.readItem(itemCache1, e.Row, key, out itemCache1, out obj, ref deleted);
    if (obj == null)
      return;
    this.cacheOnReadItem(dict, itemCache1, this.ForeignField.Name, obj, key, deleted);
    e.ReturnValue = itemCache1.GetValue(obj, this._DescriptionField.Name);
  }

  /// <exclude />
  public virtual void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string alias)
  {
    bool deleted = false;
    if (e.Row != null)
    {
      object key = sender.GetValue(e.Row, this._FieldOrdinal);
      if (key != null)
        this.DescriptionFieldSelecting(sender, e, key, ref deleted);
    }
    if (((e.Row == null ? 1 : (e.IsAltered ? 1 : 0)) | (deleted ? 1 : 0)) == 0)
      return;
    int? length;
    string descriptionName = this.getDescriptionName(sender, out length);
    if (this._UIFieldRef != null && this._UIFieldRef.UIFieldAttribute == null)
      this._UIFieldRef.UIFieldAttribute = sender.GetAttributes(this.FieldName).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    bool flag = true;
    PXUIVisibility visibility = PXUIVisibility.Visible;
    if (this._UIFieldRef?.UIFieldAttribute != null)
    {
      flag = this._UIFieldRef.UIFieldAttribute.Visible;
      visibility = this._UIFieldRef.UIFieldAttribute.Visibility;
      if (!this._UIFieldRef.UIFieldAttribute.ViewRights)
        visibility = PXUIVisibility.HiddenByAccessRights;
      else if ((visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible && (!sender.Keys.Contains(this._FieldName) || !string.Equals(alias, this._UIFieldRef.UIFieldAttribute.FieldName + "_description", StringComparison.OrdinalIgnoreCase)))
        visibility = PXUIVisibility.Visible;
    }
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), length: length, fieldName: alias, displayName: descriptionName, error: deleted ? "The record has been deleted." : (string) null, errorLevel: deleted ? PXErrorLevel.Warning : PXErrorLevel.Undefined, enabled: new bool?(false), visible: new bool?(flag), visibility: visibility);
  }

  protected void readItem(
    PXCache sender,
    object row,
    object key,
    out PXCache itemCache,
    out object item,
    ref bool deleted)
  {
    itemCache = sender;
    item = (object) null;
    if (this._UnconditionalSelect.GetTables()[0] == this._PrimarySimpleSelect.GetTables()[0])
    {
      PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, key);
      itemCache = viewWithParameters.Cache;
      using (new PXReadBranchRestrictedScope())
      {
        try
        {
          item = sender._InvokeSelectorGetter(row, this._FieldName, viewWithParameters.View, viewWithParameters.Parameters, true) ?? viewWithParameters.SelectSingleBound(row);
          if (item == null)
          {
            if (this.IsReadDeletedSupported)
            {
              using (new PXReadDeletedScope())
              {
                item = viewWithParameters.SelectSingleBound(row);
                deleted = item != null;
              }
            }
          }
        }
        catch
        {
        }
      }
    }
    if (item != null || this.SuppressUnconditionalSelect)
      return;
    PXView unconditionalView = this.GetUnconditionalView(sender);
    itemCache = unconditionalView.Cache;
    using (new PXReadBranchRestrictedScope())
    {
      try
      {
        ref object local = ref item;
        object obj = sender._InvokeSelectorGetter(row, this._FieldName, unconditionalView, new object[1]
        {
          key
        }, true);
        if (obj == null)
          obj = PXSelectorAttribute.SelectSingleBound(unconditionalView, new object[1]
          {
            row
          }, key);
        local = obj;
        if (item != null || !this.IsReadDeletedSupported)
          return;
        using (new PXReadDeletedScope())
        {
          item = PXSelectorAttribute.SelectSingleBound(unconditionalView, new object[1]
          {
            row
          }, key);
          deleted = item != null;
        }
      }
      catch (FormatException ex)
      {
      }
      catch (InvalidCastException ex)
      {
      }
    }
  }

  private bool CanCacheItem(
    PXSelectorAttribute.GlobalDictionary dict,
    PXCache foreignCache,
    object foreignItem)
  {
    return this._CacheGlobal && dict != null && foreignItem != null && this.CanCacheGlobal(foreignCache) && foreignCache.GetItemType().IsInstanceOfType(foreignItem) && !PXDatabase.ReadDeleted && foreignCache.GetStatus(foreignItem) == PXEntryStatus.Notchanged;
  }

  internal void cacheOnReadItem(
    PXSelectorAttribute.GlobalDictionary dict,
    PXCache foreignCache,
    object foreignItem,
    bool isItemDeleted = false)
  {
    if (!this.CanCacheItem(dict, foreignCache, foreignItem))
      return;
    object keyValue1 = foreignCache.GetValue(foreignItem, this.ForeignField.Name);
    if (keyValue1 == null)
      return;
    PXSelectorAttribute.CheckIntegrityAndPutGlobal(dict, foreignCache, this.ForeignField.Name, foreignItem, this.CreateGlobalCacheKey(foreignCache, foreignItem, keyValue1), isItemDeleted);
    object keyValue2;
    if (this._SubstituteKey != (System.Type) null && (keyValue2 = foreignCache.GetValue(foreignItem, this._SubstituteKey.Name)) != null)
      PXSelectorAttribute.CheckIntegrityAndPutGlobal(dict, foreignCache, this._SubstituteKey.Name, foreignItem, this.CreateGlobalCacheKey(foreignCache, foreignItem, keyValue2), isItemDeleted);
    this.OnItemCached(foreignCache, foreignItem, isItemDeleted);
  }

  private void cacheOnReadItem(
    PXSelectorAttribute.GlobalDictionary dict,
    PXCache foreignCache,
    string foreignField,
    object foreignItem,
    object ownKey,
    bool isItemDeleted)
  {
    if (!this.CanCacheItem(dict, foreignCache, foreignItem))
      return;
    PXSelectorAttribute.CheckIntegrityAndPutGlobal(dict, foreignCache, foreignField, foreignItem, this.CreateGlobalCacheKey(foreignCache, foreignItem, ownKey), isItemDeleted);
    this.OnItemCached(foreignCache, foreignItem, isItemDeleted);
  }

  protected virtual void OnItemCached(PXCache foreignCache, object foreignItem, bool isItemDeleted)
  {
  }

  protected PXSelectorAttribute.SubstituteKeyInfo getSubstituteKeyMask(PXCache sender)
  {
    PXSelectorAttribute.SubstituteKeyInfo substituteKeyInfo = (PXSelectorAttribute.SubstituteKeyInfo) null;
    if (this._SubstituteKey != (System.Type) null && !PXSelectorAttribute._substitutekeys.TryGetValue(this._SubstituteKey, out substituteKeyInfo))
    {
      PXCache readonlyCache = sender.Graph._GetReadonlyCache(this._Type);
      int? length = new int?();
      bool? isUnicode = new bool?();
      string mask = (string) null;
      foreach (PXEventSubscriberAttribute attribute in readonlyCache.GetAttributes(this._SubstituteKey.Name))
      {
        if (attribute is PXDBStringAttribute)
        {
          length = new int?(((PXDBStringAttribute) attribute).Length);
          isUnicode = new bool?(((PXDBStringAttribute) attribute).IsUnicode);
          mask = ((PXDBStringAttribute) attribute).InputMask;
        }
        else if (attribute is PXStringAttribute)
        {
          length = new int?(((PXStringAttribute) attribute).Length);
          isUnicode = new bool?(((PXStringAttribute) attribute).IsUnicode);
          mask = ((PXStringAttribute) attribute).InputMask;
        }
        if (mask != null)
          break;
      }
      if (readonlyCache.BqlTable.IsAssignableFrom(this._Type))
        PXSelectorAttribute._substitutekeys[this._SubstituteKey] = substituteKeyInfo = new PXSelectorAttribute.SubstituteKeyInfo(mask, length, isUnicode);
    }
    return substituteKeyInfo ?? new PXSelectorAttribute.SubstituteKeyInfo();
  }

  protected string getDescriptionName(PXCache sender, out int? length)
  {
    length = new int?();
    string key1 = (string) null;
    Dictionary<string, KeyValuePair<string, int?>> dictionary = PXContext.GetSlot<Dictionary<string, KeyValuePair<string, int?>>>("_DescriptionFieldFullName$" + Thread.CurrentThread.CurrentCulture.Name);
    if (dictionary == null)
    {
      string key2 = "_DescriptionFieldFullName$" + Thread.CurrentThread.CurrentCulture.Name;
      string key3 = "_DescriptionFieldFullName$" + Thread.CurrentThread.CurrentCulture.Name;
      System.Type[] typeArray = new System.Type[1]{ this._BqlType };
      Dictionary<string, KeyValuePair<string, int?>> slot;
      dictionary = slot = PXDatabase.GetSlot<Dictionary<string, KeyValuePair<string, int?>>>(key3, typeArray);
      PXContext.SetSlot<Dictionary<string, KeyValuePair<string, int?>>>(key2, slot);
    }
    string key4 = $"{sender.Graph.GetType().FullName}${this._DescriptionField.FullName}";
    bool flag = false;
    KeyValuePair<string, int?> keyValuePair;
    lock (((ICollection) dictionary).SyncRoot)
      flag = dictionary.TryGetValue(key4, out keyValuePair);
    if (!flag)
    {
      PXCache readonlyCache = sender.Graph._GetReadonlyCache(this._Type);
      foreach (PXEventSubscriberAttribute attribute in readonlyCache.GetAttributes(this._DescriptionField.Name))
      {
        switch (attribute)
        {
          case PXUIFieldAttribute _:
            key1 = ((PXUIFieldAttribute) attribute).DisplayName;
            break;
          case PXDBStringAttribute _:
            length = new int?(((PXDBStringAttribute) attribute).Length);
            break;
          case PXStringAttribute _:
            length = new int?(((PXStringAttribute) attribute).Length);
            break;
        }
        if (key1 != null)
        {
          if (length.HasValue)
            break;
        }
      }
      if (key1 == null)
        key1 = this._DescriptionField.Name;
      if (readonlyCache.BqlTable.IsAssignableFrom(this._Type))
      {
        lock (((ICollection) dictionary).SyncRoot)
          dictionary[key4] = new KeyValuePair<string, int?>(key1, length);
      }
    }
    else
    {
      key1 = keyValuePair.Key;
      length = keyValuePair.Value;
    }
    if (!string.IsNullOrEmpty(this._DescriptionDisplayName))
      return this._DescriptionDisplayName;
    if (this._FieldList != null && this._HeaderList != null && this._FieldList.Length == this._HeaderList.Length)
    {
      for (int index = 0; index < this._FieldList.Length; ++index)
      {
        if (this._FieldList[index] == this._DescriptionField.Name)
          return this._HeaderList[index];
      }
    }
    return key1;
  }

  protected static string _GetSlotName(System.Type type, byte keysCount)
  {
    return $"GlobalDictionary${$"{keysCount}keys_"}{type.FullName}";
  }

  /// <exclude />
  public virtual void SelfRowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (sender.RowId == null || !this.CanCacheGlobal(sender))
      return;
    PXDataRecord record = e.Record;
    if ((record != null ? (record._isTableChanging ? 1 : 0) : 0) != 0)
      return;
    PXSelectorAttribute.GlobalDictionary globalCache = this.GetGlobalCache();
    lock (globalCache.SyncRoot)
    {
      object keyValue = sender.GetValue(e.Row, sender.RowId);
      object globalCacheKey = this.CreateGlobalCacheKey(sender, e.Row, keyValue);
      if (keyValue == null || globalCache.TryGetValue(globalCacheKey, out PXSelectorAttribute.GlobalDictionary.CacheValue _))
        return;
      object row;
      if (sender.Graph.GetType() == typeof (PXGenericInqGrph) || PXView.CurrentRestrictedFields.Any() || PXFieldScope.IsScoped || OptimizedExportScope.IsScoped || PXDatabase.DelayedFieldScope)
      {
        Dictionary<string, object> values = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
        {
          {
            this._FieldName,
            sender.GetValue(e.Row, this._FieldName)
          }
        };
        if (!string.Equals(this._FieldName, sender.RowId, StringComparison.OrdinalIgnoreCase))
          values.Add(sender.RowId, keyValue);
        if (this._DescriptionField != (System.Type) null)
          values.Add(this._DescriptionField.Name, sender.GetValue(e.Row, this._DescriptionField.Name));
        this.AppendOtherValues(values, sender, e.Row);
        row = (object) values;
      }
      else
        row = e.Row;
      globalCache.Set(globalCacheKey, row, false);
    }
  }

  /// <exclude />
  public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    bool deleted = false;
    bool flag = false;
    if (e.ReturnValue != null)
    {
      object obj = (object) null;
      PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
      if (this._CacheGlobal)
      {
        dict = this.GetGlobalCache();
        lock (dict.SyncRoot)
        {
          PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
          if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, e.ReturnValue), out cacheValue))
          {
            obj = cacheValue.Item;
            deleted = cacheValue.IsDeleted;
          }
        }
      }
      if (obj == null)
      {
        if (e.ReturnValue.GetType() == sender.GetFieldType(this._FieldName))
        {
          PXCache itemCache = sender;
          this.readItem(itemCache, e.Row, e.ReturnValue, out itemCache, out obj, ref deleted);
          if (obj != null)
          {
            this.cacheOnReadItem(dict, itemCache, obj, deleted);
            e.ReturnValue = itemCache.GetValue(obj, this._SubstituteKey.Name);
          }
          else
            flag = true;
        }
      }
      else
      {
        PXCache itemCache = sender.Graph.Caches[this._Type];
        object returnValue = e.ReturnValue;
        e.ReturnValue = itemCache.GetValue(obj, this._SubstituteKey.Name);
        if (e.ReturnValue == null)
        {
          this.readItem(itemCache, e.Row, returnValue, out itemCache, out obj, ref deleted);
          if (obj != null)
          {
            e.ReturnValue = itemCache.GetValue(obj, this._SubstituteKey.Name);
            if (e.ReturnValue != null)
              this.cacheOnReadItem(dict, itemCache, obj, deleted);
          }
          else
            flag = true;
        }
      }
    }
    if (!e.IsAltered)
      e.IsAltered = deleted || sender.HasAttributes(e.Row);
    if (((this._AttributeLevel == PXAttributeLevel.Item ? 1 : (e.IsAltered ? 1 : 0)) | (flag ? 1 : 0)) == 0)
      return;
    PXSelectorAttribute.SubstituteKeyInfo substituteKeyMask = this.getSubstituteKeyMask(sender);
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, substituteKeyMask.Length, new bool?(), this._FieldName, new bool?(), new int?(), substituteKeyMask.Mask, (string[]) null, (string[]) null, new bool?(), (string) null);
    if (e.ReturnValue != null && e.ReturnValue.GetType() != typeof (string))
      e.ReturnValue = (object) e.ReturnValue.ToString();
    if (deleted)
    {
      ((PXFieldState) e.ReturnState).Error = "The record has been deleted.";
      ((PXFieldState) e.ReturnState).ErrorLevel = PXErrorLevel.Warning;
      ((PXFieldState) e.ReturnState).SelectorMode = sender.IsAutoNumber(this._FieldName) ? this.SelectorMode : this._SelectorMode;
    }
    else
    {
      if (!flag || !this.ShowWarningForNotExistsOnSelect)
        return;
      ((PXFieldState) e.ReturnState).Error = PXLocalizer.LocalizeFormat(this.CustomMessageElementDoesntExist, (object) $"[{this._FieldName}]");
      ((PXFieldState) e.ReturnState).ErrorLevel = PXErrorLevel.Warning;
    }
  }

  /// <exclude />
  public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this.MacroVariablesManager != null)
      e.NewValue = this.MacroVariablesManager.TryResolveExt(e.NewValue, sender, this.FieldName, e.Row);
    if (e.Cancel || e.NewValue == null)
      return;
    object obj1 = (object) null;
    PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
    if (this._CacheGlobal)
    {
      dict = this.GetGlobalCache();
      lock (dict.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, e.NewValue), out cacheValue))
        {
          if (cacheValue.IsDeleted && !PXDatabase.ReadDeleted)
            throw new PXForeignRecordDeletedException();
          obj1 = cacheValue.Item;
        }
      }
    }
    if (obj1 == null)
    {
      PXView select = this.GetView(sender, this._NaturalSelect, !this._DirtyRead);
      object[] pars = this.MakeParameters(e.NewValue, true);
      bool flag = e.NewValue.GetType() != select.Cache.GetFieldType(this._SubstituteKey.Name) && e.NewValue.GetType() == sender.GetFieldType(this._FieldName);
      Func<object> func = (Func<object>) (() =>
      {
        PXView view = select;
        object[] currents = new object[1]{ e.Row };
        object[] objArray;
        if (!this._CacheGlobal)
          objArray = pars;
        else
          objArray = new object[1]{ e.NewValue };
        return PXSelectorAttribute.SelectSingleBound(view, currents, objArray);
      });
      if (!flag)
        obj1 = sender._InvokeSelectorGetter(e.Row, this._FieldName, select, pars, true) ?? func();
      if (obj1 != null)
      {
        this.cacheOnReadItem(dict, select.Cache, obj1);
        e.NewValue = select.Cache.GetValue(obj1, this.ForeignField.Name);
      }
      else
      {
        using (new PXReadBranchRestrictedScope())
        {
          if (!flag)
          {
            obj1 = func();
            if (obj1 == null && this.IsReadDeletedSupported)
            {
              using (new PXReadDeletedScope())
              {
                obj1 = func();
                if (obj1 != null)
                {
                  this.cacheOnReadItem(dict, select.Cache, obj1);
                  throw new PXForeignRecordDeletedException();
                }
              }
            }
          }
          if (e.NewValue.GetType() == sender.GetFieldType(this._FieldName))
          {
            PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, e.NewValue);
            obj1 = (object) null;
            try
            {
              obj1 = viewWithParameters.SelectSingleBound(e.Row);
            }
            catch (FormatException ex)
            {
            }
            if (obj1 != null)
              return;
          }
          this._BypassFieldVerifying.Value = true;
          try
          {
            object newValue = e.NewValue;
            sender.OnFieldVerifying(this._FieldName, e.Row, ref newValue, true);
            if (newValue != null)
            {
              if (newValue.GetType() == sender.GetFieldType(this._FieldName))
              {
                e.NewValue = newValue;
                return;
              }
            }
          }
          catch (Exception ex)
          {
            if (ex is PXSetPropertyException)
              throw PXException.PreserveStack(ex);
          }
          finally
          {
            this._BypassFieldVerifying.Value = false;
          }
          string[] restricted;
          if (obj1 == null)
            restricted = PXSelectorAttribute.hasRestrictedAccess(sender, this._NaturalSelect, e.Row);
          else
            restricted = new string[1]{ true.ToString() };
          this.throwNoItem(restricted, !sender.Graph.IsContractBasedAPI, e.NewValue);
        }
      }
    }
    else
    {
      PXCache cach = sender.Graph.Caches[this._Type];
      object newValue = e.NewValue;
      e.NewValue = cach.GetValue(obj1, this.ForeignField.Name);
      if (e.NewValue != null)
        return;
      PXView view = this.GetView(sender, this._NaturalSelect, !this._DirtyRead);
      object obj2 = sender._InvokeSelectorGetter(e.Row, this._FieldName, view, new object[1]
      {
        newValue
      }, true);
      if (obj2 == null)
        obj2 = PXSelectorAttribute.SelectSingleBound(view, new object[1]
        {
          e.Row
        }, newValue);
      object data = obj2;
      if (data != null)
        e.NewValue = view.Cache.GetValue(data, this.ForeignField.Name);
      else
        this.throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, this._NaturalSelect, e.Row), !sender.Graph.IsContractBasedAPI, newValue);
    }
  }

  /// <exclude />
  public virtual void SubstituteKeyCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    bool flag = e.Operation.Option() == PXDBOperation.SubselectForExport;
    if (!PXSelectorAttribute.ShouldPrepareCommandForSubstituteKey(e))
      return;
    e.Cancel = true;
    using (IEnumerator<PXDBFieldAttribute> enumerator = sender.GetAttributes(this._FieldName).OfType<PXDBFieldAttribute>().GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXDBFieldAttribute current = enumerator.Current;
      e.BqlTable = this._BqlTable;
      string str = this._Type.Name + "Ext";
      SQLExpression r = (SQLExpression) null;
      if (current is PXDBScalarAttribute)
      {
        PXCommandPreparingEventArgs e1 = new PXCommandPreparingEventArgs(e.Row, e.Value, PXDBOperation.Select, e.Table, e.SqlDialect);
        current.CommandPreparing(sender, e1);
        r = e1.Expr;
      }
      if (r == null)
      {
        string name = sender.BqlSelect == null | flag ? current.DatabaseFieldName : this._FieldName;
        System.Type dac = e.Table;
        if ((object) dac == null)
          dac = flag ? current.BqlTable : this._BqlTable;
        r = (SQLExpression) new Column(name, dac);
      }
      SimpleTable t = new SimpleTable(str);
      Column l = !(((IBqlSearch) this._Select).GetFieldExpression(sender.Graph) is Column fieldExpression) ? new Column(this.ForeignField.Name, (Table) t) : new Column(fieldExpression.Name, (Table) t);
      Query q = new Query();
      q.Field((SQLExpression) new Column(this._SubstituteKey.Name, (Table) t, PXDbType.NVarChar)
      {
        PadSpaced = true
      }).From(BqlCommand.GetSQLTable(this._Type, sender.Graph).As(str)).Where(SQLExpressionExt.EQ(l, r));
      if (flag)
      {
        System.Type[] typeArray = BqlCommand.Decompose(this._Select.GetType());
        if (!BqlCommandExtensions.HasMatchParameter((IEnumerable<System.Type>) typeArray))
        {
          if (!BqlCommandExtensions.HasUnknownViews((IEnumerable<System.Type>) typeArray, this._BqlTable, this._Type))
          {
            List<System.Type> list = ((IEnumerable<System.Type>) typeArray).ToList<System.Type>();
            System.Type type1 = e.Table;
            if ((object) type1 == null)
              type1 = current.BqlTable;
            System.Type type2 = type1;
            BqlCommandExtensions.RemoveCurrentAndOptional(list, new Dictionary<string, System.Type>()
            {
              [type2.Name] = type2
            });
            SQLExpression whereExpression = ((IBqlSearch) BqlCommand.CreateInstance(list.ToArray())).GetWhereExpression(sender.Graph);
            if (whereExpression.Oper() != SQLExpression.Operation.NULL)
            {
              whereExpression.substituteTableName(this._Type.Name, str);
              q.AndWhere(whereExpression);
            }
          }
        }
      }
      e.Expr = (SQLExpression) new SubQuery(q);
      if (e.Value == null)
        return;
      e.DataValue = e.Value;
      e.DataType = PXDbType.NVarChar;
      e.DataLength = new int?(((string) e.Value).Length);
    }
  }

  protected static bool ShouldPrepareCommandForSubstituteKey(PXCommandPreparingEventArgs e)
  {
    bool flag = (e.Operation & PXDBOperation.Option) == PXDBOperation.SubselectForExport;
    if (e.Operation.Command() != PXDBOperation.Select || !(e.Operation.Option() == PXDBOperation.External | flag))
      return false;
    return e.Value == null || e.Value is string;
  }

  /// <exclude />
  public virtual void DescriptionFieldCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    bool flag = (e.Operation.Option() & PXDBOperation.External) == PXDBOperation.External;
    if (!(e.Operation.Command() == PXDBOperation.Select & flag) || e.Value != null && !(e.Value is string))
      return;
    e.Cancel = true;
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(this._FieldName))
    {
      if (attribute is PXDBFieldAttribute)
      {
        e.BqlTable = this._BqlTable;
        string str = this._Type.Name + "Ext";
        System.Type itemType = BqlCommand.GetItemType(this._DescriptionField);
        PXCache cach = sender.Graph.Caches[itemType];
        string fieldName = cach.GetFieldName(this._DescriptionField.Name, false);
        Table t1 = BqlCommand.GetSQLTable(this._Type, sender.Graph).As(str);
        Table t2 = t1;
        SQLExpression field = (SQLExpression) new Column(fieldName, t2)
        {
          PadSpaced = true
        };
        if (t1 is SimpleTable)
        {
          PXDBOperation operation = PXDBOperation.Select;
          if (PXDBLocalizableStringAttribute.IsEnabled && cach.GetAttributes(this._DescriptionField.Name).OfType<PXDBLocalizableStringAttribute>().Any<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (l => l.MultiLingual)))
            operation |= PXDBOperation.External;
          PXCommandPreparingEventArgs.FieldDescription description;
          cach.RaiseCommandPreparing(this._DescriptionField.Name, (object) null, (object) null, operation, itemType, out description);
          if (description?.Expr != null)
            field = description?.Expr.substituteTableName(this._Type.Name, str).substituteTableName(itemType.Name, str);
        }
        Query q = new Query();
        Query query = q.Field(field).From(t1);
        Column l1 = new Column(this.ForeignField.Name, str);
        string name = sender.BqlSelect == null ? ((PXDBFieldAttribute) attribute).DatabaseFieldName : this._FieldName;
        System.Type dac = e.Table;
        if ((object) dac == null)
          dac = this._BqlTable;
        Column r = new Column(name, dac);
        SQLExpression w = SQLExpressionExt.EQ(l1, (SQLExpression) r);
        query.Where(w);
        e.Expr = (SQLExpression) new SubQuery(q);
        if (e.Value == null)
          break;
        e.DataValue = e.Value;
        e.DataType = PXDbType.NVarChar;
        e.DataLength = new int?(((string) e.Value).Length);
        break;
      }
    }
  }

  /// <exclude />
  public virtual void ForeignTableRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    PXSelectorAttribute.ClearGlobalCache(this._Type, this.KnownForeignKeysCount);
  }

  /// <exclude />
  public virtual void ReadDeletedFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || !this.ValidateValue || e.Cancel || this._BqlTable.IsAssignableFrom(this._BqlType) && sender.Keys.Count > 0 && string.Compare(sender.Keys[sender.Keys.Count - 1], this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
      return;
    PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
    object newValue = e.NewValue;
    if (this._CacheGlobal)
    {
      dict = this.GetGlobalCache();
      lock (dict.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, newValue), out cacheValue))
        {
          if (!cacheValue.IsDeleted || PXDatabase.ReadDeleted)
            return;
          throw this.GetForeignRecordDeletedException(sender.Graph, cacheValue.Item);
        }
      }
    }
    PXSelectorAttribute.ViewWithParameters viewWithParameters = this.GetViewWithParameters(sender, newValue);
    bool isItemDeleted = false;
    object obj = viewWithParameters.SelectSingleBound(e.Row);
    if (obj == null)
    {
      using (new PXReadDeletedScope())
      {
        obj = viewWithParameters.SelectSingleBound(e.Row);
        isItemDeleted = true;
      }
    }
    if (obj == null)
      return;
    this.cacheOnReadItem(dict, viewWithParameters.Cache, this.ForeignField.Name, obj, newValue, isItemDeleted);
    if (isItemDeleted)
      throw this.GetForeignRecordDeletedException(sender.Graph, obj);
  }

  private PXForeignRecordDeletedException GetForeignRecordDeletedException(
    PXGraph graph,
    object foreignDacRow)
  {
    DacDescriptor? emptyDacDescriptor = graph.GetNonEmptyDacDescriptor(foreignDacRow);
    return !emptyDacDescriptor.HasValue ? new PXForeignRecordDeletedException() : new PXForeignRecordDeletedException(emptyDacDescriptor.Value);
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    lock (((ICollection) PXSelectorAttribute._SelectorFields).SyncRoot)
    {
      List<KeyValuePair<string, System.Type>> source;
      if (!PXSelectorAttribute._SelectorFields.TryGetValue(bqlTable, out source))
        PXSelectorAttribute._SelectorFields[bqlTable] = source = new List<KeyValuePair<string, System.Type>>();
      if (!source.Any<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (pair => pair.Key == this.FieldName)))
      {
        System.Type foreignField = this.ForeignField;
        System.Type itemType = BqlCommand.GetItemType(foreignField);
        if (!(itemType == (System.Type) null) && bqlTable.IsAssignableFrom(itemType))
        {
          if (string.Equals(foreignField.Name, this.FieldName, StringComparison.OrdinalIgnoreCase))
            goto label_11;
        }
        source.Add(new KeyValuePair<string, System.Type>(this.FieldName, this.ForeignField));
      }
    }
label_11:
    this._originalBqlTable = bqlTable;
    if (this._AttributeLevel != PXAttributeLevel.Type)
      return;
    if (this.TableReferenceCollector == null || this.ReferenceConverter == null)
    {
      PXTrace.WriteWarning("Reference collection for {ReferenceOrigins} references is turned off because either ITableReferenceCollector or SelectorToReferenceConverter is not registered or attribute-level DI is not enabled", (object) new ReferenceOrigin[1]
      {
        ReferenceOrigin.SelectorAttribute
      });
    }
    else
    {
      if (this.TableReferenceCollector.AllReferencesAreCollected.IsCompleted)
        return;
      Reference reference = this.ReferenceConverter.CreateReference(this, this._originalBqlTable);
      if (!(reference != (Reference) null))
        return;
      this.TableReferenceCollector.TryCollectReference(reference);
    }
  }

  /// <exclude />
  public static List<KeyValuePair<string, System.Type>> GetSelectorFields(System.Type table)
  {
    DacMetadata.InitializationCompleted.Wait();
    lock (((ICollection) PXSelectorAttribute._SelectorFields).SyncRoot)
    {
      List<KeyValuePair<string, System.Type>> keyValuePairList;
      if (PXSelectorAttribute._SelectorFields.TryGetValue(table, out keyValuePairList))
      {
        HashSet<string> distinct = (HashSet<string>) null;
        List<KeyValuePair<string, System.Type>> selectorFields = keyValuePairList;
        while ((table = table.BaseType) != typeof (object))
        {
          List<KeyValuePair<string, System.Type>> source;
          if (PXSelectorAttribute._SelectorFields.TryGetValue(table, out source))
          {
            if (distinct == null)
            {
              distinct = new HashSet<string>(keyValuePairList.Select<KeyValuePair<string, System.Type>, string>((Func<KeyValuePair<string, System.Type>, string>) (_ => _.Key)));
              selectorFields = new List<KeyValuePair<string, System.Type>>((IEnumerable<KeyValuePair<string, System.Type>>) keyValuePairList);
            }
            int count = selectorFields.Count;
            selectorFields.AddRange(source.Where<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (_ => !distinct.Contains(_.Key))));
            selectorFields.GetRange(count, selectorFields.Count - count).ForEach((System.Action<KeyValuePair<string, System.Type>>) (_ => distinct.Add(_.Key)));
          }
        }
        return selectorFields;
      }
    }
    return new List<KeyValuePair<string, System.Type>>();
  }

  protected void populateFields(PXCache sender, bool bypassInit)
  {
    System.Type cacheType = sender.Graph.Caches.GetCacheType(this._Type);
    string key;
    if (this._FieldList == null)
    {
      System.Type type = this._Type;
      if (cacheType != (System.Type) null && cacheType != this._Type || cacheType == (System.Type) null && (type = PXSubstManager.Substitute(this._Type, sender.Graph.GetType())) != this._Type)
      {
        key = !(cacheType != (System.Type) null) ? $"{type.FullName}${sender.Graph.GetType().FullName}" : $"{cacheType.FullName}${sender.Graph.GetType().FullName}";
      }
      else
      {
        key = this._Type.FullName;
        if (sender.Graph.HasGraphSpecificFields(this._Type))
          key = $"{key}${sender.Graph.GetType().FullName}";
      }
    }
    else
    {
      key = $"{sender.GetItemType().FullName}${this._FieldName}";
      if (sender.IsGraphSpecificField(this._FieldName) || sender.Graph.HasGraphSpecificFields(this._Type) || cacheType != (System.Type) null && cacheType != this._Type || cacheType == (System.Type) null && PXSubstManager.Substitute(this._Type, sender.Graph.GetType()) != this._Type)
        key = $"{key}${sender.Graph.GetType().FullName}";
    }
    string str = $"{key}@{Thread.CurrentThread.CurrentUICulture.Name}";
    PXSelectorAttribute.FieldHeaderDictionaryIndependant withContextCache;
    if (PXDBAttributeAttribute._BqlTablesUsed.ContainsKey(this._BqlType))
      withContextCache = (PXSelectorAttribute.FieldHeaderDictionaryIndependant) PXDatabase.GetSlotWithContextCache<PXSelectorAttribute.FieldHeaderDictionaryDependant>("FieldHeaderDictionaryDependant", typeof (PXSelectorAttribute.CSAttributeGroup), typeof (PXSelectorAttribute.FeaturesSet));
    else
      withContextCache = PXDatabase.GetSlotWithContextCache<PXSelectorAttribute.FieldHeaderDictionaryIndependant>("FieldHeaderDictionaryIndependant");
    string[] headerspecified = this._HeaderList;
    if (this._FieldList == null)
      this._FieldList = ConcurrentDictionaryExtensions.GetOrAddOrUpdate<string, string[]>(withContextCache._fields, key, (Func<string, string[]>) (fieldkey =>
      {
        if (bypassInit)
          return (string[]) null;
        this.findFieldsHeaders(sender);
        string[] fieldList = this._FieldList;
        return (fieldList != null ? (fieldList.Length == 0 ? 1 : 0) : 0) != 0 ? (string[]) null : this._FieldList;
      }), (Func<string, string[], string[]>) ((fieldkey, fieldvalue) =>
      {
        if (fieldvalue != null)
          return fieldvalue;
        if (bypassInit)
          return (string[]) null;
        this.findFieldsHeaders(sender);
        string[] fieldList = this._FieldList;
        return (fieldList != null ? (fieldList.Length == 0 ? 1 : 0) : 0) != 0 ? (string[]) null : this._FieldList;
      }));
    if (this._FieldList != null)
      this._HeaderList = ConcurrentDictionaryExtensions.GetOrAddOrUpdate<string, string[]>(withContextCache._headers, $"{str}${string.Join(",", this._FieldList)}", (Func<string, string[]>) (headerkey =>
      {
        if (bypassInit)
          return (string[]) null;
        if (headerspecified != null)
        {
          this._HeaderList = headerspecified;
          for (int index = 0; index < this._HeaderList.Length; ++index)
            this._HeaderList[index] = PXMessages.Localize(this._HeaderList[index], out string _);
        }
        else
          this.findFieldsHeaders(sender);
        string[] headerList = this._HeaderList;
        return (headerList != null ? (headerList.Length == 0 ? 1 : 0) : 0) != 0 ? (string[]) null : this._HeaderList;
      }), (Func<string, string[], string[]>) ((headerkey, headervalue) =>
      {
        if (headervalue != null)
          return headervalue;
        if (bypassInit)
          return (string[]) null;
        if (headerspecified != null)
        {
          this._HeaderList = headerspecified;
          for (int index = 0; index < this._HeaderList.Length; ++index)
            this._HeaderList[index] = PXMessages.Localize(this._HeaderList[index], out string _);
        }
        else
          this.findFieldsHeaders(sender);
        string[] headerList = this._HeaderList;
        return (headerList != null ? (headerList.Length == 0 ? 1 : 0) : 0) != 0 ? (string[]) null : this._HeaderList;
      }));
    if (this._HeaderList != null || headerspecified == null)
      return;
    this._HeaderList = headerspecified;
    if (this._FieldList == null)
      return;
    for (int index = 0; index < this._HeaderList.Length; ++index)
      this._HeaderList[index] = PXMessages.Localize(this._HeaderList[index], out string _);
    withContextCache._fields.TryAdd(key, this._FieldList);
    withContextCache._headers.TryAdd($"{str}${string.Join(",", this._FieldList)}", this._HeaderList);
  }

  protected void findFieldsHeaders(PXCache sender)
  {
    this._HeaderList = new string[0];
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    PXCache pxCache = sender.GetItemType() == this._Type || sender.GetItemType().IsSubclassOf(this._Type) && !Attribute.IsDefined((MemberInfo) sender.GetItemType(), typeof (PXBreakInheritanceAttribute), false) ? sender : sender.Graph._GetReadonlyCache(this._Type);
    PXContext.SetSlot<bool>("selectorBypassInit", true);
    try
    {
      if (this._FieldList == null)
      {
        foreach (string field in (List<string>) pxCache.Fields)
        {
          if (pxCache.GetStateExt((object) null, field) is PXFieldState stateExt && (stateExt.Visibility.HasFlag((Enum) PXUIVisibility.SelectorVisible) || stateExt.Visibility.HasFlag((Enum) PXUIVisibility.Dynamic) && !stateExt.Name.EndsWith("_Attributes")))
          {
            stringList1.Add(stateExt.Name);
            stringList2.Add(stateExt.DisplayName);
          }
        }
      }
      else
      {
        for (int index = 0; index < this._FieldList.Length; ++index)
        {
          bool flag = false;
          if (pxCache.GetStateExt((object) null, this._FieldList[index]) is PXFieldState stateExt1)
          {
            stringList1.Add(this._FieldList[index]);
            stringList2.Add(stateExt1.DisplayName);
            flag = true;
          }
          if (!flag)
          {
            int length1;
            if ((length1 = this._FieldList[index].IndexOf("__")) > 0)
            {
              if (length1 + 2 < this._FieldList[index].Length)
              {
                string str = this._FieldList[index].Substring(0, length1);
                foreach (System.Type table in this._LookupSelect.GetTables())
                {
                  if (table.Name == str)
                  {
                    string fieldName = this._FieldList[index].Substring(length1 + 2, this._FieldList[index].Length - length1 - 2);
                    if (sender.Graph._GetReadonlyCache(table).GetStateExt((object) null, fieldName) is PXFieldState stateExt2)
                    {
                      stringList1.Add(this._FieldList[index]);
                      stringList2.Add(stateExt2.DisplayName);
                      break;
                    }
                    break;
                  }
                }
              }
            }
            else
            {
              int length2;
              if ((length2 = this._FieldList[index].IndexOf('_')) > 0)
              {
                string name = this._FieldList[index].Substring(0, length2);
                foreach (PXSelectorAttribute selectorAttribute in pxCache.GetAttributes(name).OfType<PXSelectorAttribute>())
                {
                  if (!(selectorAttribute._DescriptionField == (System.Type) null))
                  {
                    stringList1.Add(selectorAttribute.FieldName);
                    stringList2.Add(selectorAttribute.getDescriptionName(sender, out int? _));
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      this._HeaderList = (string[]) null;
      PXFirstChanceExceptionLogger.LogMessage("Failed to retrieve selector columns");
      throw;
    }
    finally
    {
      PXContext.SetSlot<bool>("selectorBypassInit", false);
    }
    this._FieldList = stringList1.ToArray();
    this._HeaderList = stringList2.ToArray();
  }

  protected virtual void CreateFilter(PXGraph graph)
  {
    PXSelectorAttribute.PXSelectorFilterView filterView = new PXSelectorAttribute.PXSelectorFilterView(graph, this);
    PXFilterableAttribute.AddFilterView(graph, (PXFilterView) filterView, this._ViewName);
    PXFilterDetailView filterDetailView = new PXFilterDetailView(graph, this._ViewName, new System.Type[0]);
    PXFilterableAttribute.AddFilterDetailView(graph, filterDetailView, this._ViewName);
  }

  /// <exclude />
  public void CreateView(PXCache sender)
  {
    System.Type itemType = sender.GetItemType();
    this._ViewName = $"_Cache#{itemType.FullName}_{this._FieldName}{this._ViewName}";
    PXView pxView1;
    if (sender.Graph.Views.TryGetValue(this._ViewName, out pxView1) && pxView1.BqlSelect.GetType() != this._LookupSelect.GetType())
      pxView1 = (PXView) null;
    if (pxView1 != null)
      return;
    PXView pxView2 = (object) this._ViewHandler == null ? (this._CacheGlobal ? (PXView) new PXSelectorAttribute.viewGlobal(sender.Graph, true, this._LookupSelect, sender, this._FieldName) : new PXView(sender.Graph, true, this._LookupSelect)) : (!this._CacheGlobal ? (PXView) new PXSelectorAttribute.PXAdjustableView(sender.Graph, true, this._LookupSelect, this._ViewHandler) : (PXView) new PXSelectorAttribute.adjustableViewGlobal(sender.Graph, true, this._LookupSelect, this._ViewHandler, sender, this._FieldName));
    pxView2.SetDependToCacheTypes((IEnumerable<System.Type>) new System.Type[1]
    {
      itemType
    });
    sender.Graph.Views[this._ViewName] = pxView2;
    this._IsOwnView = true;
    if (this._DirtyRead)
      pxView2.IsReadOnly = false;
    if (!this._Filterable)
      return;
    this.CreateFilter(sender.Graph);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._UIFieldRef = new UIFieldRef();
    this._CacheType = sender.GetItemType();
    this._BypassFieldVerifying = new PXEventSubscriberAttribute.ObjectRef<bool>();
    if (this._CacheGlobal && this.IsSelfReferencing)
    {
      sender.RowPersisted += new PXRowPersisted(this.ForeignTableRowPersisted);
      this.GetGlobalCache();
    }
    this.populateFields(sender, true);
    this.CreateView(sender);
    this._ViewCreated = true;
    if (this.IsSelfReferencing & string.Equals(this._FieldName, this.ForeignField.Name, StringComparison.OrdinalIgnoreCase))
    {
      this.SelectorMode |= PXSelectorMode.NoAutocomplete;
      if (sender.Graph.GetType() != typeof (PXGraph) && (!PXSelectorAttribute.SelfSelectingTables.ContainsKey(this._Type) || !PXSelectorAttribute.SelfSelectingTables[this._Type].Contains(sender.Graph)))
      {
        sender.RowSelecting += new PXRowSelecting(this.SelfRowSelecting);
        PXSelectorAttribute.SelfSelectingTables.GetOrAdd(this._Type, (Func<System.Type, WeakSet<PXGraph>>) (key => new WeakSet<PXGraph>())).Add(sender.Graph);
      }
    }
    else
      this.EmitColumnForDescriptionField(sender);
    if (this._SubstituteKey != (System.Type) null)
    {
      string lower = this._FieldName.ToLower();
      sender.FieldSelectingEvents[lower] += new PXFieldSelecting(this.SubstituteKeyFieldSelecting);
      sender.FieldUpdatingEvents[lower] += new PXFieldUpdating(this.SubstituteKeyFieldUpdating);
      if (string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
        return;
      sender.CommandPreparingEvents[lower] += new PXCommandPreparing(this.SubstituteKeyCommandPreparing);
    }
    else
    {
      if (!this.IsReadDeletedSupported)
        return;
      sender.FieldVerifyingEvents[this._FieldName.ToLower()] += new PXFieldVerifying(this.ReadDeletedFieldVerifying);
      this._CacheGlobal = true;
    }
  }

  protected void EmitDescriptionFieldAlias(PXCache sender, string alias)
  {
    if (this._DescriptionField == (System.Type) null || sender.Fields.Contains(alias))
      return;
    string lower = alias.ToLower();
    sender.Fields.Add(alias);
    sender.FieldSelectingEvents[lower] += (PXFieldSelecting) ((cache, e) => this.DescriptionFieldSelecting(cache, e, alias));
    sender.CommandPreparingEvents[lower] += new PXCommandPreparing(this.DescriptionFieldCommandPreparing);
  }

  protected virtual void EmitColumnForDescriptionField(PXCache sender)
  {
    if (this._DescriptionField == (System.Type) null)
      return;
    this.EmitDescriptionFieldAlias(sender, $"{this._FieldName}_{this._Type.Name}_{this._DescriptionField.Name}");
    this.EmitDescriptionFieldAlias(sender, this._FieldName + "_description");
  }

  private class PXSelectorFilterView : PXFilterView
  {
    private readonly string _alias;

    public PXSelectorFilterView(PXGraph graph, PXSelectorAttribute selector)
      : base(graph, "SELECTOR", PXSelectorAttribute.PXSelectorFilterView.GetViewName(PXSelectorAttribute.PXSelectorFilterView.GetAlias(selector)))
    {
      graph.CommandPreparing.AddHandler<FilterRow.dataField>(new PXCommandPreparing(this.FilterRow_DataField_CommandPreparing));
      this._alias = PXSelectorAttribute.PXSelectorFilterView.GetAlias(selector);
    }

    private static string GetAlias(PXSelectorAttribute selector)
    {
      System.Type type = selector._FilterEntity;
      if ((object) type == null)
        type = selector._Type;
      return type.Name;
    }

    private static string GetViewName(string alias) => $"_{alias}_";

    private void FilterRow_DataField_CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
    {
      string str = e.Value as string;
      if (e.Row == null || string.IsNullOrEmpty(str) || str.Contains("__") || !(PXParentAttribute.SelectParent(sender, e.Row, typeof (FilterHeader)) is FilterHeader filterHeader) || !string.Equals(filterHeader.ViewName, PXSelectorAttribute.PXSelectorFilterView.GetViewName(this._alias), StringComparison.Ordinal))
        return;
      e.Value = (object) $"{this._alias}__{str}";
    }
  }

  /// <exclude />
  public interface IPXAdjustableView
  {
  }

  /// <exclude />
  public class PXAdjustableView(
    PXGraph graph,
    bool isReadOnly,
    BqlCommand select,
    Delegate handler) : PXView(graph, isReadOnly, select, handler), PXSelectorAttribute.IPXAdjustableView
  {
  }

  protected struct ViewWithParameters(PXView view, object[] parameters)
  {
    public PXView View { get; } = view;

    public object[] Parameters { get; } = parameters;

    public PXCache Cache => this.View.Cache;

    public object SelectSingleBound(object current)
    {
      return PXSelectorAttribute.SelectSingleBound(this.View, new object[1]
      {
        current
      }, this.Parameters);
    }

    public List<object> SelectMultiBound(object current)
    {
      return this.View.SelectMultiBound(new object[1]
      {
        current
      }, this.Parameters);
    }

    public object[] PrepareParameters(object current)
    {
      return this.View.PrepareParameters(new object[1]
      {
        current
      }, this.Parameters);
    }
  }

  private sealed class CSAttributeGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  private sealed class FeaturesSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  protected class FieldHeaderDictionaryIndependant
  {
    public ConcurrentDictionary<string, string[]> _fields = new ConcurrentDictionary<string, string[]>();
    public ConcurrentDictionary<string, string[]> _headers = new ConcurrentDictionary<string, string[]>();
  }

  protected sealed class FieldHeaderDictionaryDependant : 
    PXSelectorAttribute.FieldHeaderDictionaryIndependant,
    IPXCompanyDependent
  {
  }

  /// <exclude />
  internal sealed class GlobalDictionary : IPXCompanyDependent
  {
    internal const string SlotPrefix = "GlobalDictionary$";
    private readonly Dictionary<object, PXSelectorAttribute.GlobalDictionary.CacheValue> Items = new Dictionary<object, PXSelectorAttribute.GlobalDictionary.CacheValue>();

    public static PXSelectorAttribute.GlobalDictionary GetOrCreate(
      System.Type foreignTable,
      System.Type watchedTable)
    {
      return PXSelectorAttribute.GlobalDictionary.GetOrCreate(foreignTable, watchedTable, (byte) 1);
    }

    public static PXSelectorAttribute.GlobalDictionary GetOrCreate(
      System.Type foreignTable,
      System.Type watchedTable,
      byte keysCount)
    {
      string slotName = PXSelectorAttribute._GetSlotName(foreignTable, keysCount);
      PXSelectorAttribute.GlobalDictionary globalDictionary = PXContext.GetSlot<PXSelectorAttribute.GlobalDictionary>(slotName);
      if (globalDictionary == null)
      {
        string key1 = slotName;
        string key2 = slotName;
        System.Type[] typeArray = new System.Type[1]
        {
          watchedTable
        };
        PXSelectorAttribute.GlobalDictionary localizableSlot;
        globalDictionary = localizableSlot = PXDatabase.GetLocalizableSlot<PXSelectorAttribute.GlobalDictionary>(key2, typeArray);
        PXContext.SetSlot<PXSelectorAttribute.GlobalDictionary>(key1, localizableSlot);
      }
      return globalDictionary;
    }

    public static void ClearFor(System.Type table)
    {
      PXSelectorAttribute.GlobalDictionary.ClearFor(table, (byte) 1);
    }

    public static void ClearFor(System.Type table, byte keysCount)
    {
      string slotName = PXSelectorAttribute._GetSlotName(table, keysCount);
      PXDatabase.ResetLocalizableSlot<PXSelectorAttribute.GlobalDictionary>(slotName, table);
      PXContext.SetSlot(slotName, (object) null);
    }

    public static object NormalizeKeyFieldValue(object key)
    {
      if (key is string str)
        key = (object) str.TrimEnd();
      return key;
    }

    public object SyncRoot => ((ICollection) this.Items).SyncRoot;

    public bool TryGetValue(
      object key,
      out PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue)
    {
      if (!this.Items.TryGetValue(key, out cacheValue))
      {
        cacheValue = new PXSelectorAttribute.GlobalDictionary.CacheValue();
        return false;
      }
      if (cacheValue.Extensions != null)
      {
        IBqlTable key1 = cacheValue.Item as IBqlTable;
        PXCacheExtensionCollection slot = PXCacheExtensionCollection.GetSlot(true);
        lock (((ICollection) slot).SyncRoot)
          slot[key1] = cacheValue.Extensions;
      }
      return true;
    }

    public void Set(object key, object row, bool deleted)
    {
      PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue = new PXSelectorAttribute.GlobalDictionary.CacheValue()
      {
        Item = row,
        IsDeleted = deleted
      };
      if (row is IBqlTable bqlTable)
        cacheValue.Extensions = bqlTable.GetExtensions();
      this.Items[key] = cacheValue;
    }

    /// <exclude />
    internal struct CacheValue
    {
      public object Item;
      public bool IsDeleted;
      public PXCacheExtension[] Extensions;
    }
  }

  protected class SubstituteKeyInfo(string mask, int? length, bool? isUnicode) : 
    Tuple<string, int?, bool?>(mask, length, isUnicode)
  {
    public SubstituteKeyInfo()
      : this((string) null, new int?(), new bool?())
    {
    }

    public string Mask => this.Item1;

    public int? Length => this.Item2;

    public bool? IsUnicode => this.Item3;
  }

  /// <exclude />
  private sealed class adjustableViewGlobal(
    PXGraph graph,
    bool isReadOnly,
    BqlCommand select,
    Delegate handler,
    PXCache sender,
    string fieldName) : PXSelectorAttribute.viewGlobal(graph, isReadOnly, select, handler, sender, fieldName), PXSelectorAttribute.IPXAdjustableView
  {
  }

  /// <exclude />
  private class viewGlobal : PXView
  {
    private BqlCommand _select;
    private readonly PXCache _sender;
    private readonly string _fieldName;
    private PXView.PXSearchColumn[] _sorts;

    public viewGlobal(
      PXGraph graph,
      bool isReadOnly,
      BqlCommand select,
      PXCache sender,
      string fieldName)
      : base(graph, isReadOnly, select)
    {
      this._select = select;
      this._sender = sender;
      this._fieldName = fieldName;
    }

    public viewGlobal(
      PXGraph graph,
      bool isReadOnly,
      BqlCommand select,
      Delegate handler,
      PXCache sender,
      string fieldName)
      : base(graph, isReadOnly, select, handler)
    {
      this._select = select;
      this._sender = sender;
      this._fieldName = fieldName;
    }

    protected override List<object> InvokeDelegate(object[] parameters)
    {
      this._sorts = PXView._Executing.Peek().Sorts;
      return base.InvokeDelegate(parameters);
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
      List<object> objectList = (List<object>) null;
      if (startRow == 0 && maximumRows == 1 && searches != null && searches.Length == 1 && searches[0] != null)
      {
        object key = PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(searches[0]);
        bool flag = this.Cache.Keys.Count <= 1;
        PXSelectorAttribute.GlobalDictionary globalDictionary = (PXSelectorAttribute.GlobalDictionary) null;
        if (flag)
        {
          globalDictionary = PXSelectorAttribute.GlobalDictionary.GetOrCreate(this.Cache.GetItemType(), this.Cache.BqlTable, (byte) 1);
          lock (globalDictionary.SyncRoot)
          {
            PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
            if (globalDictionary.TryGetValue(key, out cacheValue))
              objectList = new List<object>()
              {
                cacheValue.Item
              };
          }
        }
        if (objectList == null)
        {
          objectList = base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
          bool isRowDeleted = false;
          if ((objectList == null || objectList.Count == 0) && PXDatabase.IsReadDeletedSupported(this.Cache.BqlTable))
          {
            using (new PXReadDeletedScope())
              objectList = base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
            isRowDeleted = true;
          }
          PXView.PXSearchColumn pxSearchColumn;
          if (((objectList == null || objectList.Count != 1 ? 0 : (!PXDatabase.ReadDeleted ? 1 : 0)) & (flag ? 1 : 0)) != 0 && sortcolumns.Length == 1 && !string.IsNullOrEmpty(sortcolumns[0]) && (pxSearchColumn = ((IEnumerable<PXView.PXSearchColumn>) this._sorts).FirstOrDefault<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (_ => string.Equals(_.Column, sortcolumns[0], StringComparison.OrdinalIgnoreCase)))) != null && pxSearchColumn.SearchValue != null)
          {
            object foreignRow = (object) PXResult.UnwrapMain(objectList[0]);
            PXSelectorAttribute.CheckIntegrityAndPutGlobal(globalDictionary, this.Cache, sortcolumns[0], foreignRow, PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(pxSearchColumn.SearchValue), isRowDeleted);
          }
        }
      }
      if (objectList == null)
        objectList = base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
      return objectList;
    }
  }

  public class WithCachingByCompositeKeyAttribute : PXSelectorAttribute
  {
    private System.Type _additionalKeyRelations;

    /// <summary>
    /// Indicate that the search query has only conditions with a key fields for item identification.
    /// </summary>
    public bool OnlyKeyConditions { get; set; }

    public WithCachingByCompositeKeyAttribute(System.Type search, System.Type additionalKeysRelation)
      : base(search)
    {
      this.CacheGlobal = true;
      this.AdditionalKeyRelations = additionalKeysRelation;
    }

    public WithCachingByCompositeKeyAttribute(
      System.Type search,
      System.Type additionalKeysRelation,
      System.Type[] fieldList)
      : base(search, fieldList)
    {
      this.CacheGlobal = true;
      this.AdditionalKeyRelations = additionalKeysRelation;
    }

    public WithCachingByCompositeKeyAttribute(
      System.Type search,
      System.Type additionalKeysRelation,
      System.Type lookupJoin,
      System.Type[] fieldList)
      : base(search, lookupJoin, true, fieldList)
    {
      this.AdditionalKeyRelations = additionalKeysRelation;
    }

    public sealed override bool CacheGlobal
    {
      get => base.CacheGlobal;
      set => base.CacheGlobal = value;
    }

    public IReadOnlyCollection<IFieldsRelation> AdditionalKeysRelationsArray { get; private set; }

    private System.Type AdditionalKeyRelations
    {
      get => this._additionalKeyRelations;
      set
      {
        this._additionalKeyRelations = TypeArrayOf<IFieldsRelation>.IsTypeArrayOrElement(value) ? TypeArrayOf<IFieldsRelation>.EmptyOrSingleOrSelf(value) : throw new PXArgumentException(nameof (value), $"Unsupported value {value}.");
        this.AdditionalKeysRelationsArray = (IReadOnlyCollection<IFieldsRelation>) TypeArrayOf<IFieldsRelation>.CheckAndExtractInstances(this._additionalKeyRelations, (string) null);
      }
    }

    protected override BqlCommand BuildNaturalSelect(bool cacheGlobal, System.Type substituteKey)
    {
      System.Type where1 = this.AdditionalKeysRelationsArray.ToWhere();
      System.Type type1 = BqlCommand.Compose(typeof (Where<,>), substituteKey, typeof (Equal<>), typeof (Required<>), substituteKey);
      System.Type type2;
      if (!(where1 == (System.Type) null))
        type2 = BqlCommand.Compose(typeof (Where2<,>), where1, typeof (And<>), type1);
      else
        type2 = type1;
      System.Type where2 = type2;
      if (!cacheGlobal)
        return this._Select.WhereAnd(where2);
      return BqlCommand.CreateInstance(typeof (Search<,>), this.ForeignField, where2);
    }

    protected override void AppendOtherValues(
      Dictionary<string, object> values,
      PXCache cache,
      object row)
    {
      foreach (var data in this.AdditionalKeysRelationsArray.Select(f => new
      {
        FieldName = f.FieldOfParentTable.Name,
        Value = cache.GetValue(row, f.FieldOfParentTable.Name)
      }))
        values.Add(data.FieldName, data.Value);
    }

    protected override void Verify(PXCache sender, PXFieldVerifyingEventArgs e, ref object item)
    {
      bool flag = false;
      PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
      if (this._CacheGlobal && this.OnlyKeyConditions)
      {
        dict = this.GetGlobalCache();
        lock (dict.SyncRoot)
        {
          PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
          if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, e.NewValue), out cacheValue))
          {
            if (!cacheValue.IsDeleted)
            {
              item = cacheValue.Item;
              flag = true;
            }
          }
        }
      }
      base.Verify(sender, e, ref item);
      if (dict == null || item == null || flag)
        return;
      this.cacheOnReadItem(dict, sender.Graph.Caches[this._Type], item);
    }

    protected override byte KnownForeignKeysCount
    {
      get => (byte) (1 + this.AdditionalKeysRelationsArray.Count);
    }

    protected override object CreateGlobalCacheKey(PXCache cache, object row, object keyValue)
    {
      return (object) PXSelectorAttribute.WithCachingByCompositeKeyAttribute.CreateCompositeKey(cache, row, keyValue, (IEnumerable<IFieldsRelation>) this.AdditionalKeysRelationsArray, this._Type.IsAssignableFrom(cache.GetItemType()));
    }

    internal static Composite CreateCompositeKey(
      PXCache cache,
      object row,
      object keyValue,
      IEnumerable<IFieldsRelation> additionalFieldsRelations,
      bool isForeign)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      List<object> list = additionalFieldsRelations.Select<IFieldsRelation, object>((Func<IFieldsRelation, object>) (f => cache.GetValue(row, isForeign ? f.FieldOfParentTable.Name : f.FieldOfChildTable.Name))).Select<object, object>(PXSelectorAttribute.WithCachingByCompositeKeyAttribute.\u003C\u003EO.\u003C0\u003E__NormalizeKeyFieldValue ?? (PXSelectorAttribute.WithCachingByCompositeKeyAttribute.\u003C\u003EO.\u003C0\u003E__NormalizeKeyFieldValue = new Func<object, object>(PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue))).ToList<object>();
      list.Add(PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(keyValue));
      return Composite.Create(list.ToArray());
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.TableChangingScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Description.GI;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

[PXInternalUseOnly]
public class TableChangingScope : IDisposable
{
  public const string IsNewColumn = "IsNew4D74EB2BAF344EFBA2F24DCAB634D145";
  private readonly TableChangingContext _previousChangesContext;
  private readonly TableChangingContext _currentContext;
  private readonly bool previousIsScoped;
  private readonly PXGraph _graph;
  private bool _isDisposed;
  private const string CheckObservedFieldChangedWebConfigKey = "PushNotifications:CheckObservedFieldChanged";

  internal static bool CheckObservedFieldChanged()
  {
    return WebConfig.GetBool("PushNotifications:CheckObservedFieldChanged", false);
  }

  public static TableChangingContext TableChangesContext
  {
    get => PXContext.GetSlot<TableChangingContext>("TableChangingScope.Context");
    private set => PXContext.SetSlot<TableChangingContext>("TableChangingScope.Context", value);
  }

  public static bool IsScoped
  {
    get => PXContext.GetSlot<bool>("TableChangingScope.IsScoped");
    set => PXContext.SetSlot<bool>("TableChangingScope.IsScoped", value);
  }

  public static void InsertIsNewIfNeeded(
    System.Type table,
    PXCache cache,
    BqlCommand.Selection selection,
    string alias = null)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext context = TableChangingScope.TableChangesContext;
    if (context.ChangedDacs.ContainsKey(table) || context.ShouldSkipTransform(alias ?? table.Name))
      return;
    string[] array = TableChangingScope.GetAllTablesNames(table, cache.Graph).Where<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))).ToArray<string>();
    string str;
    if (array.Length == 0)
    {
      List<System.Type> extensionTables = cache.GetExtensionTables();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      str = extensionTables != null ? extensionTables.Select<System.Type, string>(TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName ?? (TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName = new Func<System.Type, string>(BqlCommand.GetTableName))).FirstOrDefault<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))) : (string) null;
    }
    else
      str = (string) null;
    string firstChangedExtension = str;
    if (array.Length == 0 && firstChangedExtension == null)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Tuple<PXCache, PXCommandPreparing, PXRowSelecting> tuple = Tuple.Create<PXCache, PXCommandPreparing, PXRowSelecting>(cache, (PXCommandPreparing) ((sender, args) => TableChangingScope.dummyCommandPreparing(sender, args, firstChangedExtension)), TableChangingScope.\u003C\u003EO.\u003C1\u003E__dummyRowSelecting ?? (TableChangingScope.\u003C\u003EO.\u003C1\u003E__dummyRowSelecting = new PXRowSelecting(TableChangingScope.dummyRowSelecting)));
    context.ChangedDacs.Add(table, tuple);
    cache.Fields.Add("IsNew4D74EB2BAF344EFBA2F24DCAB634D145");
    selection.RestrictedFields?.Add(new RestrictedField(table, "IsNew4D74EB2BAF344EFBA2F24DCAB634D145"));
    if (cache.CommandPreparingEvents.ContainsKey("IsNew4D74EB2BAF344EFBA2F24DCAB634D145"))
      return;
    cache.CommandPreparingEvents.Add("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", tuple.Item2);
    cache.RowSelectingWhileReading += tuple.Item3;
  }

  public static void SetCurrentLevelTable(string table, BqlCommand command = null)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingScope.TableChangesContext.SetCurrentTable(table, command != null ? TableChangingScope.GetFirstChangedDacNameForProjection(table, command) : (string) null);
  }

  public static void RemoveCurrentLevelTable(string table)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext tableChangesContext = TableChangingScope.TableChangesContext;
    if (tableChangesContext.GetCurrentTable().Current != table)
      throw new InvalidOperationException();
    tableChangesContext.RemoveCurrentTable();
  }

  public static Table TransformTablesInSQLTree(Table sqlTable, BqlCommand.Selection selection)
  {
    if (!TableChangingScope.IsScoped)
      return sqlTable;
    TableChangingContext tableChangesContext = TableChangingScope.TableChangesContext;
    if (tableChangesContext._unchangedRealNames == null)
      return sqlTable;
    if (!tableChangesContext.ShouldSkipTransform())
    {
      foreach (TableChange tableChange in tableChangesContext.TableChanges.Values)
      {
        if (tableChangesContext._unchangedRealNames.Contains<string>(tableChange.OldName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        {
          sqlTable.transformTableName(tableChange.OldName, tableChange.NewName);
          for (int index = 0; index < selection.ColExprs.Count; ++index)
            selection.ColExprs[index] = selection.ColExprs[index].substituteTableName(tableChange.OldName, tableChange.NewName);
        }
      }
    }
    tableChangesContext._unchangedRealNames = (List<string>) null;
    return sqlTable;
  }

  public static Table TransformTablesInSQLTree(Table sqlTable)
  {
    if (!TableChangingScope.IsScoped || TableChangingScope.TableChangesContext._unchangedRealNames == null)
      return sqlTable;
    sqlTable = TableChangingScope.TransformTablesInSQLTree(sqlTable, new BqlCommand.Selection());
    return sqlTable;
  }

  private static void dummyCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e,
    string tableName = null)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext tableChangesContext = TableChangingScope.TableChangesContext;
    if (tableChangesContext.GetCurrentTable()._previousStack.Any<string>() && tableChangesContext.DACsToSkip.Contains(tableChangesContext.GetCurrentTable().Current))
      return;
    if (tableName == null)
    {
      BqlCommand bqlSelect = sender.BqlSelect;
      if (bqlSelect != null)
      {
        tableName = e.Table?.Name ?? TableChangingScope.GetFirstChangedTableNameForProjection(bqlSelect);
        e.BqlTable = sender.BqlTable;
      }
      else
        tableName = TableChangingScope.TableChangesContext.GetCurrentTable().FirstChangedForProjection ?? e.Table?.Name ?? TableChangingScope.GetFirstChangedTableName(sender);
    }
    e.Expr = (SQLExpression) new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", tableName, PXDbType.Bit);
  }

  private static string GetFirstChangedDacNameForProjection(
    string projectionName,
    BqlCommand command)
  {
    IEnumerable<System.Type> source = ((IEnumerable<System.Type>) command.GetTables()).Where<System.Type>((Func<System.Type, bool>) (c => c.Name != projectionName));
    TableChangingContext context = TableChangingScope.TableChangesContext;
    Func<System.Type, bool> predicate = (Func<System.Type, bool>) (c => context.TableChanges.ContainsKey(BqlCommand.GetTableName(c)));
    return source.FirstOrDefault<System.Type>(predicate)?.Name ?? projectionName;
  }

  private static string GetFirstChangedTableName(PXCache sender)
  {
    TableChangingContext context = TableChangingScope.TableChangesContext;
    return TableChangingScope.GetAllTablesNames(sender.BqlTable, sender.Graph).FirstOrDefault<string>((Func<string, bool>) (t => context.TableChanges.ContainsKey(t)));
  }

  private static string GetFirstChangedTableNameForProjection(BqlCommand projection)
  {
    TableChangingContext context = TableChangingScope.TableChangesContext;
    return ((IEnumerable<System.Type>) projection.GetTables()).Select<System.Type, string>((Func<System.Type, string>) (t => BqlCommand.GetTableName(t))).FirstOrDefault<string>((Func<string, bool>) (t => context.TableChanges.ContainsKey(t)));
  }

  private static void dummyRowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext tableChangesContext = TableChangingScope.TableChangesContext;
    if (tableChangesContext.DACsToSkip.Contains(tableChangesContext.GetCurrentTable().Current))
      return;
    if (e.Row != null)
    {
      bool? boolean = e.Record.GetBoolean(e.Position);
      BqlTablePair originalObjectContext = sender.GetOriginalObjectContext(e.Row);
      if (originalObjectContext != null && boolean.HasValue)
        originalObjectContext.IsInserted = new bool?(boolean.Value);
    }
    ++e.Position;
  }

  public static void AppendRestrictionsOnIsNew(
    ref SQLExpression exp,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection,
    bool realTables = false)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext context = TableChangingScope.TableChangesContext;
    System.Type type = (!selection.FromProjection || !(selection.ProjectionType != (System.Type) null) ? (IEnumerable<System.Type>) tables.Take<System.Type>(tables.Count - 1).ToList<System.Type>() : (IEnumerable<System.Type>) tables.Take<System.Type>(tables.Count - 1).Where<System.Type>((Func<System.Type, bool>) (c => c != selection.ProjectionType || context.TableChanges.ContainsKey(c.Name))).ToList<System.Type>()).FirstOrDefault<System.Type>((Func<System.Type, bool>) (t =>
    {
      if (realTables)
        return context.TableChanges.ContainsKey(t.Name);
      if (context.ChangedDacs.ContainsKey(t))
        return true;
      return context.TableChanges.ContainsKey(t.Name) && context.ChangedDacs.Any<KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>>((Func<KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>, bool>) (d => t.IsAssignableFrom(d.Key)));
    }));
    if (type == (System.Type) null)
      return;
    System.Type table = tables[tables.Count - 1];
    TableChangingScope.AppendRestrictionsOnIsNew(ref exp, graph, new TableChangingScope.DacTable(type), table != selection.ProjectionType ? new TableChangingScope.DacTable(table) : new TableChangingScope.DacTable(table.Name), realTables);
  }

  internal static void AppendRestrictionsOnIsNew(
    ref SQLExpression exp,
    PXGraph graph,
    TableChangingScope.DacTable firstChangedTable,
    TableChangingScope.DacTable joinedTable,
    bool realTables = false)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingContext context = TableChangingScope.TableChangesContext;
    if (realTables || !firstChangedTable.HasType)
    {
      if (!context.TableChanges.ContainsKey(firstChangedTable.Name))
        return;
    }
    else if (!context.ChangedDacs.ContainsKey(firstChangedTable.Type) && (!context.TableChanges.ContainsKey(firstChangedTable.Type.Name) || !context.ChangedDacs.Any<KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>>((Func<KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>, bool>) (d => firstChangedTable.Type.IsAssignableFrom(d.Key)))))
      return;
    if (context.ShouldSkipTransform(joinedTable.Name) || context.ShouldSkipTransform(firstChangedTable.Name))
      return;
    IEnumerable<string> source;
    if (realTables || !joinedTable.HasType)
      source = (IEnumerable<string>) new string[1]
      {
        joinedTable.Name
      };
    else
      source = TableChangingScope.GetAllTablesNames(joinedTable.Type, graph);
    Func<string, bool> predicate = (Func<string, bool>) (n => context.TableChanges.ContainsKey(n));
    if (!source.Any<string>(predicate))
      return;
    exp = exp?.And(SQLExpressionExt.EQ(new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", (Table) firstChangedTable.ToSqlTable()), (SQLExpression) new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", (Table) joinedTable.ToSqlTable())));
  }

  public static SQLExpression ProcessOnsForGI(
    IReadOnlyList<PXRelation> relations,
    int lastIndex,
    PXGraph graph)
  {
    TableChangingContext tableChangesContext = TableChangingScope.TableChangesContext;
    return !TableChangingScope.IsScoped || TableChangingScope.CheckObservedFieldChanged() ? (SQLExpression) null : TableChangingScope.ProcessOnsForGi(relations, lastIndex, graph, tableChangesContext);
  }

  internal static SQLExpression ProcessOnsForGi(
    IReadOnlyList<PXRelation> relations,
    int lastIndex,
    PXGraph graph,
    TableChangingContext context)
  {
    PXRelation relation = relations[lastIndex];
    string alias = relation.Second.Alias;
    if (context.ShouldSkipTransform(alias) || !TableChangingScope.GetAllTablesNames(relation.Second.CacheType, graph, context.RealTableNamesForDac).Any<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))))
      return (SQLExpression) null;
    Column r1 = new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", alias);
    SQLExpression sqlExpression = (SQLExpression) null;
    if (!context.ShouldSkipTransform(relation.First.Alias) && TableChangingScope.GetAllTablesNames(relation.First.CacheType, graph, context.RealTableNamesForDac).Any<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))))
      return SQLExpressionExt.EQ(new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", relation.First.Alias), (SQLExpression) r1).Embrace();
    foreach (PXRelation pxRelation in relations.Take<PXRelation>(lastIndex - 1))
    {
      if (!context.ShouldSkipTransform(pxRelation.First.Alias) && TableChangingScope.GetAllTablesNames(pxRelation.First.CacheType, graph, context.RealTableNamesForDac).Any<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))))
      {
        Column l = new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", pxRelation.First.Alias);
        SQLExpression r2 = SQLExpressionExt.EQ(l, (SQLExpression) r1).Or(l.IsNull()).Embrace();
        sqlExpression = sqlExpression != null ? sqlExpression.Or(r2) : r2;
      }
    }
    foreach (PXRelation pxRelation in relations.Take<PXRelation>(lastIndex - 1))
    {
      if (!context.ShouldSkipTransform(pxRelation.Second.Alias) && TableChangingScope.GetAllTablesNames(pxRelation.Second.CacheType, graph, context.RealTableNamesForDac).Any<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))))
      {
        Column l = new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", pxRelation.Second.Alias);
        SQLExpression r3 = SQLExpressionExt.EQ(l, (SQLExpression) r1).Or(l.IsNull()).Embrace();
        sqlExpression = sqlExpression != null ? sqlExpression.Or(r3) : r3;
      }
    }
    return sqlExpression?.Embrace();
  }

  public static Query AddIsNewWhereForGI(
    PXQueryDescription description,
    PXGraph graph,
    Query query)
  {
    TableChangingContext context = TableChangingScope.TableChangesContext;
    if (!TableChangingScope.IsScoped || !TableChangingScope.CheckObservedFieldChanged())
      return query;
    PXTable[] array = description.Relations.SelectMany<PXRelation, PXTable>((Func<PXRelation, IEnumerable<PXTable>>) (r => EnumerableExtensions.AsSingleEnumerable<PXTable>(r.First).Concat<PXTable>(EnumerableExtensions.AsSingleEnumerable<PXTable>(r.Second)))).Where<PXTable>((Func<PXTable, bool>) (r => !context.ShouldSkipTransform(r.Alias))).Where<PXTable>((Func<PXTable, bool>) (r => TableChangingScope.GetAllTablesNames(r.CacheType, graph).Concat<string>(TableChangingScope.GetAllTablesNames(r.CacheType, graph)).Any<string>((Func<string, bool>) (c => context.TableChanges.ContainsKey(c))))).ToArray<PXTable>();
    HashSet<PXTable> pxTableSet = new HashSet<PXTable>();
    SQLExpression sqlExpression = (SQLExpression) null;
    Column l = (Column) null;
    foreach (PXTable pxTable in array)
    {
      if (pxTableSet.Add(pxTable))
      {
        Column r1 = new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", pxTable.Alias);
        if (l == null)
        {
          l = r1;
        }
        else
        {
          SQLExpression r2 = SQLExpressionExt.EQ(l, (SQLExpression) r1).Or(l.IsNull()).Or(r1.IsNull()).Embrace();
          sqlExpression = sqlExpression == null ? r2 : sqlExpression.And(r2);
        }
      }
    }
    return query.AndWhere(sqlExpression?.Embrace());
  }

  public static SQLExpression ProcessNoteAttributesWhere(
    SQLExpression where,
    string srcTable,
    System.Type dataTable,
    System.Type dataTableAlias)
  {
    where = where.And(SQLExpressionExt.EQ(new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", srcTable), (SQLExpression) new Column("IsNew4D74EB2BAF344EFBA2F24DCAB634D145", dataTableAlias == (System.Type) null ? dataTable.Name : dataTableAlias.Name)));
    return where;
  }

  private static IEnumerable<string> GetAllTablesNames(System.Type type, PXGraph graph)
  {
    Dictionary<System.Type, IEnumerable<string>> tableNamesForDac = TableChangingScope.TableChangesContext.RealTableNamesForDac;
    return TableChangingScope.GetAllTablesNames(type, graph, tableNamesForDac);
  }

  public static IEnumerable<string> GetAllTablesNames(
    System.Type type,
    PXGraph graph,
    Dictionary<System.Type, IEnumerable<string>> tableNamesForDacCache,
    bool includeExtensions = false)
  {
    PXCache cach = graph.Caches[type];
    IEnumerable<string> first1;
    if (tableNamesForDacCache.TryGetValue(type, out first1))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return !includeExtensions ? first1 : first1.Concat<string>(cach.GetExtensionTables().Select<System.Type, string>(TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName ?? (TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName = new Func<System.Type, string>(BqlCommand.GetTableName))));
    }
    IEnumerable<string> first2;
    if (!typeof (IBqlTable).IsAssignableFrom(type))
      first2 = (IEnumerable<string>) new string[1]
      {
        type.Name
      };
    else if (typeof (PXMappedCacheExtension).IsAssignableFrom(type))
    {
      first2 = Enumerable.Empty<string>();
    }
    else
    {
      if (cach == null)
        throw new PXArgumentException("table", "The argument is out of range.");
      if (cach.BqlSelect == null)
      {
        System.Type itemType = cach.GetItemType();
        first2 = (IEnumerable<string>) new string[1]
        {
          BqlCommand.GetTableName(itemType == type || !itemType.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) itemType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive ? type : itemType)
        };
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        first2 = (IEnumerable<string>) ((IEnumerable<System.Type>) cach.BqlSelect.GetTables()).Select<System.Type, string>(TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName ?? (TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName = new Func<System.Type, string>(BqlCommand.GetTableName))).ToArray<string>();
      }
    }
    tableNamesForDacCache[type] = first2;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return !includeExtensions ? first2 : first2.Concat<string>(cach.GetExtensionTables().Select<System.Type, string>(TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName ?? (TableChangingScope.\u003C\u003EO.\u003C0\u003E__GetTableName = new Func<System.Type, string>(BqlCommand.GetTableName))));
  }

  public TableChangingScope(
    IReadOnlyDictionary<string, TableChange> changes,
    PXGraph graph,
    bool trackAllFields)
  {
    if (changes == null)
      return;
    this._previousChangesContext = TableChangingScope.TableChangesContext;
    this.previousIsScoped = TableChangingScope.IsScoped;
    this._currentContext = new TableChangingContext()
    {
      TableChanges = changes,
      TrackAllFields = trackAllFields
    };
    TableChangingScope.TableChangesContext = this._currentContext;
    this._graph = graph;
    TableChangingScope.IsScoped = true;
  }

  public TableChangingScope(TableChangingContext tableChangesContext)
  {
    if (tableChangesContext == null)
      return;
    this._previousChangesContext = tableChangesContext;
    this.previousIsScoped = TableChangingScope.IsScoped;
    this._currentContext = new TableChangingContext()
    {
      TableChanges = tableChangesContext.TableChanges,
      DACsToSkip = tableChangesContext.DACsToSkip,
      TrackAllFields = tableChangesContext.TrackAllFields
    };
    TableChangingScope.TableChangesContext = this._currentContext;
    this._graph = (PXGraph) null;
    TableChangingScope.IsScoped = true;
  }

  public void Dispose()
  {
    this._isDisposed = true;
    TableChangingContext currentContext = this._currentContext;
    foreach (KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>> keyValuePair in (IEnumerable<KeyValuePair<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>>) ((currentContext != null ? (object) currentContext.ChangedDacs : (object) null) ?? (object) ImmutableDictionary<System.Type, Tuple<PXCache, PXCommandPreparing, PXRowSelecting>>.Empty))
      this.CleanCache(keyValuePair.Value);
    TableChangingScope.TableChangesContext = this._previousChangesContext;
    TableChangingScope.IsScoped = this.previousIsScoped;
    if (this.previousIsScoped)
      return;
    this._graph?.Clear();
  }

  private void CleanCache(
    Tuple<PXCache, PXCommandPreparing, PXRowSelecting> changedDacValue)
  {
    PXCache pxCache = changedDacValue.Item1;
    pxCache.Fields.Remove("IsNew4D74EB2BAF344EFBA2F24DCAB634D145");
    pxCache.CommandPreparingEvents.Remove("IsNew4D74EB2BAF344EFBA2F24DCAB634D145");
    pxCache.RowSelectingWhileReading -= changedDacValue.Item3;
  }

  public static Table GetSQLTable(Func<Table> SQLTableGetter, string alias, bool isNewInserted = false)
  {
    if (!TableChangingScope.IsScoped)
      return SQLTableGetter();
    if (!isNewInserted)
      TableChangingScope.SetCurrentLevelTable(alias);
    Table sqlTable = TableChangingScope.TransformTablesInSQLTree(SQLTableGetter());
    if (isNewInserted)
      return sqlTable;
    TableChangingScope.RemoveCurrentLevelTable(alias);
    return sqlTable;
  }

  public static SQLExpression FindFieldInProjection(
    System.Type table,
    PXCache cache,
    List<SQLExpression> selectionColumns,
    List<System.Type> tables,
    string field,
    SQLExpression foundField)
  {
    return !TableChangingScope.IsScoped || field != "IsNew4D74EB2BAF344EFBA2F24DCAB634D145" ? foundField : selectionColumns.FirstOrDefault<SQLExpression>((Func<SQLExpression, bool>) (e => e is Column column && column.AliasOrName().Contains("IsNew4D74EB2BAF344EFBA2F24DCAB634D145"))) ?? foundField;
  }

  public static void AddUnchangedRealName(string tableName)
  {
    if (!TableChangingScope.IsScoped)
      return;
    TableChangingScope.TableChangesContext.AddUnchangedRealName(tableName);
  }

  public void SetTablesToSkip(List<string> tablesToSkip)
  {
    if (this._isDisposed)
      throw new ObjectDisposedException(nameof (TableChangingScope));
    this._currentContext.DACsToSkip = tablesToSkip;
  }

  internal struct DacTable
  {
    public DacTable(System.Type type)
      : this(type, type.Name)
    {
    }

    public DacTable(string str)
      : this((System.Type) null, str)
    {
    }

    private DacTable(System.Type type, string name)
    {
      this.Type = type;
      this.Name = name;
    }

    public System.Type Type { get; }

    public string Name { get; }

    public bool HasType => this.Type != (System.Type) null;

    public SimpleTable ToSqlTable()
    {
      return !this.HasType ? new SimpleTable(this.Name) : new SimpleTable(this.Type);
    }
  }
}

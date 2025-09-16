// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBAttributeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.CS;
using PX.Data.BQL;
using PX.Data.Maintenance.GI;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Data;

/// <exclude />
public class PXDBAttributeAttribute : PXDBFieldAttribute
{
  protected readonly 
  #nullable disable
  BqlCommand _SingleSelect;
  protected readonly BqlCommand _SubSelect;
  protected readonly IBqlWhere _PureWhere;
  protected System.Type _Field;
  protected PXView _View;
  protected BqlCommand _Selector;
  protected PXFieldState[] _Fields;
  protected Dictionary<string, int> _AttributeIndices;
  protected bool _IsActive;
  protected readonly bool aggregateAttributes = true;
  protected string _DescriptionField = "Description";
  protected string _ControlTypeField = "ControlType";
  protected string _EntryMaskField = "EntryMask";
  protected string _ListField = "List";
  protected string _PrecisionField = "Precision";
  internal static ConcurrentDictionary<System.Type, System.Type> _BqlTablesUsed = new ConcurrentDictionary<System.Type, System.Type>();

  /// <summary>Get, set.</summary>
  public string DescriptionField
  {
    get => this._DescriptionField;
    set => this._DescriptionField = value;
  }

  /// <summary>Get, set.</summary>
  public string ControlTypeField
  {
    get => this._ControlTypeField;
    set => this._ControlTypeField = value;
  }

  /// <summary>Get, set.</summary>
  public string EntryMaskField
  {
    get => this._EntryMaskField;
    set => this._EntryMaskField = value;
  }

  /// <summary>Get, set.</summary>
  public string ListField
  {
    get => this._ListField;
    set => this._ListField = value;
  }

  internal System.Type Field => this._Field;

  internal bool IsActive => this._IsActive;

  public static void Activate(PXCache cache)
  {
    foreach (PXDBAttributeAttribute attributeAttribute in cache.GetAttributes((string) null).OfType<PXDBAttributeAttribute>())
      attributeAttribute._IsActive = true;
  }

  public PXDBAttributeAttribute(System.Type valueSearch, System.Type attributeID)
  {
    if (valueSearch == (System.Type) null)
      throw new PXArgumentException("type", "The argument cannot be null.");
    this._Field = !(attributeID == (System.Type) null) ? attributeID : throw new PXArgumentException("field", "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(valueSearch))
      this._SingleSelect = BqlCommand.CreateInstance(valueSearch);
    else
      this._SingleSelect = valueSearch.IsNested && typeof (IBqlField).IsAssignableFrom(valueSearch) ? BqlCommand.CreateInstance(typeof (Search<>), valueSearch) : throw new PXArgumentException(nameof (valueSearch), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) valueSearch
      });
    this._PureWhere = (this._SingleSelect as IHasBqlWhere).GetWhere();
    this._SubSelect = this._SingleSelect.WhereAnd(typeof (Where<,>).MakeGenericType(this._Field, typeof (Equal<PXDBAttributeAttribute.AttributeIDPlaceholder>)));
    this._SingleSelect = this._SingleSelect.WhereAnd(BqlCommand.Compose(typeof (Where<,>), this._Field, typeof (Equal<>), typeof (Required<>), this._Field));
  }

  public PXDBAttributeAttribute(System.Type valueSearch, System.Type attributeID, System.Type attributeSearch)
    : this(valueSearch, attributeID)
  {
    if (attributeSearch == (System.Type) null)
      throw new PXArgumentException(nameof (attributeSearch), "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(attributeSearch))
      this._Selector = BqlCommand.CreateInstance(attributeSearch);
    else
      this._Selector = attributeSearch.IsNested && typeof (IBqlField).IsAssignableFrom(attributeSearch) ? BqlCommand.CreateInstance(typeof (Search<>), attributeSearch) : throw new PXArgumentException(nameof (attributeSearch), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) attributeSearch
      });
  }

  /// <summary>Get, set.</summary>
  public bool DefaultVisible { get; set; }

  protected virtual void AttributeFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    PXFieldState state,
    string attributeName,
    int idx)
  {
    if (!this._IsActive)
      return;
    if (this._AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
    {
      object returnValue = e.ReturnValue;
      e.ReturnState = ((ICloneable) state).Clone();
      if (returnValue != null)
        e.ReturnValue = returnValue;
    }
    if (e.Row != null)
    {
      if (!(sender.GetValue(e.Row, this._FieldOrdinal) is string[] strArray))
      {
        int startRow = 0;
        int totalRows = 0;
        List<object> objectList = this._View.Select(new object[1]
        {
          e.Row
        }, new object[1]{ (object) attributeName }, (object[]) null, (string[]) null, new bool[1], (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
        if (objectList.Count > 0)
        {
          object data = objectList[0];
          if (data is PXResult)
            data = ((PXResult) data)[0];
          e.ReturnValue = this._View.Cache.GetValue(data, ((IBqlSearch) this._SingleSelect).GetField().Name);
        }
      }
      else
        e.ReturnValue = (object) strArray[idx];
    }
    if (!(e.ReturnValue is string returnValue1))
      return;
    if (state.DataType == typeof (bool))
    {
      int result;
      if (!int.TryParse(returnValue1, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return;
      e.ReturnValue = (object) Convert.ToBoolean(result);
    }
    else if (state.DataType == typeof (Decimal))
    {
      Decimal result;
      if (!Decimal.TryParse(returnValue1, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return;
      e.ReturnValue = (object) System.Math.Round(result, state.Precision);
    }
    else
    {
      System.DateTime result;
      if (!(state.DataType == typeof (System.DateTime)) || !System.DateTime.TryParse(returnValue1, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        return;
      e.ReturnValue = (object) result;
    }
  }

  protected virtual void AttributeFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    PXFieldState state,
    string attributeName,
    int iField)
  {
    if (!this._IsActive || !(e.NewValue is string newValue))
      return;
    if (state.DataType == typeof (bool))
    {
      bool result;
      if (!bool.TryParse(newValue, out result))
        return;
      e.NewValue = (object) result;
    }
    else if (state.DataType == typeof (Decimal))
    {
      Decimal result;
      if (!Decimal.TryParse(newValue, out result))
        return;
      e.NewValue = (object) System.Math.Round(result, state.Precision);
    }
    else
    {
      System.DateTime result;
      if (!(state.DataType == typeof (System.DateTime)) || !System.DateTime.TryParse(newValue, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        return;
      e.NewValue = (object) result;
    }
  }

  protected virtual void AttributeCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e,
    PXFieldState state,
    string attributeName,
    int iField)
  {
    if (!this._IsActive || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (!this._BqlTable.IsAssignableFrom(sender.BqlTable))
    {
      if (sender.Graph.Caches[this._BqlTable].BqlSelect != null && ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null))
      {
        e.Cancel = true;
        e.DataType = PXDbType.NVarChar;
        e.DataValue = e.Value;
        e.BqlTable = this._BqlTable;
        e.Expr = (SQLExpression) new Column(state.Name, (e.Operation & PXDBOperation.Option) == PXDBOperation.External ? sender.GetItemType() : this._BqlTable);
      }
      else
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        e.Cancel = !sender.Graph.Caches[this._BqlTable].RaiseCommandPreparing(state.Name, e.Row, e.Value, e.Operation, e.Table, out description);
        if (description == null)
          return;
        e.DataType = description.DataType;
        e.DataValue = description.DataValue;
        e.BqlTable = this._BqlTable;
        e.Expr = description.Expr;
      }
    }
    else if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.Select && e.Value == null)
    {
      e.Cancel = true;
      e.DataValue = e.Value;
      string s = e.Value as string;
      if (state.DataType == typeof (bool))
      {
        e.DataType = PXDbType.Bit;
        bool result;
        if (s != null && bool.TryParse(s, out result))
          e.DataValue = (object) Convert.ToInt32(result).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      else if (state.DataType == typeof (Decimal))
      {
        e.DataType = PXDbType.Decimal;
        Decimal result;
        if (s != null && Decimal.TryParse(s, out result))
          e.DataValue = (object) System.Math.Round(result, state.Precision);
      }
      else if (state.DataType == typeof (System.DateTime))
      {
        e.DataType = PXDbType.DateTime;
        string format = "yyyy-MM-dd HH:mm:ss.fff";
        System.DateTime result;
        if (s != null && System.DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
          e.DataValue = (object) result.ToString(format, (IFormatProvider) CultureInfo.InvariantCulture);
        if (e.Value is System.DateTime dateTime)
          e.DataValue = (object) dateTime.ToString(format, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
        e.DataType = PXDbType.NVarChar;
      List<System.Type> tables = new List<System.Type>();
      if ((e.Operation & PXDBOperation.Option) == PXDBOperation.External)
        tables.Add(sender.GetItemType());
      else if (e.Table != (System.Type) null)
        tables.Add(e.Table);
      else
        tables.Add(this._BqlTable);
      if (this.aggregateAttributes && (e.Operation & PXDBOperation.Option) != PXDBOperation.External)
      {
        e.Expr = (SQLExpression) null;
      }
      else
      {
        SQLExpression singleExpression = BqlCommand.GetSingleExpression(((IBqlSearch) this._SingleSelect).GetField(), sender.Graph, new List<System.Type>((IEnumerable<System.Type>) this._SingleSelect.GetTables()), (BqlCommand.Selection) null, BqlCommand.FieldPlace.Select);
        Query queryInternal = this._SubSelect.GetQueryInternal(sender.Graph, new BqlCommandInfo(false)
        {
          Tables = tables
        }, (BqlCommand.Selection) null);
        queryInternal.ClearSelection();
        queryInternal.Field(singleExpression);
        queryInternal.GetWhere().substituteConstant(new PXDBAttributeAttribute.AttributeIDPlaceholder().Value, attributeName);
        SQLExpression where = queryInternal.GetWhere();
        SQLExpression sqlExpression = where;
        TableChangingScope.AppendRestrictionsOnIsNew(ref where, sender.Graph, tables, new BqlCommand.Selection());
        int num = where != sqlExpression ? 1 : 0;
        queryInternal.Where(where);
        List<Joiner> from = queryInternal.GetFrom();
        SimpleTable sqlTable = (from != null ? from.FirstOrDefault<Joiner>().Table() : (Table) null) as SimpleTable;
        if (num != 0 && sqlTable != null)
        {
          Func<Table> SQLTableGetter = (Func<Table>) (() => (Table) sqlTable);
          TableChangingScope.AddUnchangedRealName(sqlTable.Name);
          queryInternal.GetFrom()[0].setTable(TableChangingScope.GetSQLTable(SQLTableGetter, sqlTable.Name));
        }
        System.Type dataType = state?.DataType;
        System.Type type = typeof (bool);
        e.Expr = !(dataType == type) ? (SQLExpression) new SubQuery(queryInternal) : new SubQuery(queryInternal).Coalesce((SQLExpression) new SQLConst((object) false));
      }
      e.BqlTable = this._BqlTable;
    }
    else
      e.Expr = (SQLExpression) null;
  }

  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!this._IsActive || this._Fields == null)
      return;
    string firstColumn = e.Record.GetString(e.Position);
    ISqlDialect sqlDialect = sender.Graph.SqlDialect;
    string[] attributes;
    if (this.aggregateAttributes && sqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) this._AttributeIndices, out attributes))
    {
      sender.SetValue(e.Row, this._FieldOrdinal, (object) attributes);
      ++e.Position;
    }
    else
    {
      int? int32 = e.Record.GetInt32(e.Position);
      ++e.Position;
      if (int32.HasValue)
      {
        int? nullable = int32;
        int num = 0;
        if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
          return;
        string[] strArray = new string[this._Fields.Length];
        sender.SetValue(e.Row, this._FieldOrdinal, (object) strArray);
        for (int index = 0; index < strArray.Length; ++index)
        {
          strArray[index] = e.Record.GetString(e.Position);
          ++e.Position;
        }
      }
      else
        e.Position += this._Fields.Length;
    }
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (!this._IsActive || this._Fields == null || !e.IsSelect())
      return;
    PXDBOperation pxdbOperation = e.Operation & PXDBOperation.Option;
    bool flag = pxdbOperation == PXDBOperation.External || pxdbOperation == PXDBOperation.Internal || pxdbOperation == PXDBOperation.Select && e.Value == null || pxdbOperation == PXDBOperation.GroupBy && sender.BqlSelect != null;
    System.Type type = pxdbOperation == PXDBOperation.External ? sender.GetItemType() : e.Table ?? this._BqlTable;
    if (!this._BqlTable.IsAssignableFrom(sender.BqlTable))
    {
      if (sender.Graph.Caches[this._BqlTable].BqlSelect != null & flag)
      {
        e.BqlTable = this._BqlTable;
        e.Expr = (SQLExpression) new Column(this._DatabaseFieldName, (e.Operation & PXDBOperation.Option) == PXDBOperation.External ? sender.GetItemType() : this._BqlTable);
      }
      else
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        sender.Graph.Caches[this._BqlTable].RaiseCommandPreparing(this._DatabaseFieldName, e.Row, e.Value, e.Operation, e.Table, out description);
        if (description != null)
        {
          e.DataType = description.DataType;
          e.DataValue = description.DataValue;
          e.BqlTable = this._BqlTable;
          e.Expr = description.Expr;
        }
      }
    }
    else
    {
      ISqlDialect sqlDialect = e.SqlDialect;
      if (flag)
      {
        if (this.aggregateAttributes && (e.Operation & PXDBOperation.Option) != PXDBOperation.External)
        {
          List<System.Type> types = new List<System.Type>()
          {
            type
          };
          e.BqlTable = type;
          e.DataType = PXDbType.NVarChar;
          e.Expr = (SQLExpression) new SubQuery(this.GetAttributesJoinedQuery(sender.Graph, types, ((IBqlSearch) this._SingleSelect).GetField(), this._PureWhere));
          return;
        }
        e.BqlTable = this._BqlTable;
        Query q = new Query().Field((SQLExpression) new SQLConst((object) this._Fields.Length));
        e.Expr = (SQLExpression) new SubQuery(q);
      }
      else
        e.Expr = SQLExpression.Null();
    }
    e.DataType = PXDbType.Int;
    e.DataLength = new int?(4);
  }

  protected Query GetAttributesJoinedQuery(
    PXGraph graph,
    List<System.Type> types,
    System.Type fieldWithValue,
    IBqlWhere _PureWhere)
  {
    System.Type itemType = BqlCommand.GetItemType(fieldWithValue);
    System.Type table = BqlCommand.FindRealTableForType(types, itemType);
    SQLExpression exp1 = (SQLExpression) null;
    _PureWhere.AppendExpression(ref exp1, graph, new BqlCommandInfo(false)
    {
      Tables = types
    }, (BqlCommand.Selection) null);
    List<System.Type> list = types.ToList<System.Type>();
    bool realTables = types.All<System.Type>((Func<System.Type, bool>) (t => graph.Caches[t].BqlSelect == null));
    list.Add(realTables ? table : itemType);
    SQLExpression exp2 = exp1;
    TableChangingScope.AppendRestrictionsOnIsNew(ref exp2, graph, list, new BqlCommand.Selection(), realTables);
    int num = exp2 != exp1 ? 1 : 0;
    Func<Table> SQLTableGetter = (Func<Table>) (() => (Table) new SimpleTable(table.Name));
    Table srcTable;
    if (num != 0)
    {
      TableChangingScope.AddUnchangedRealName(table.Name);
      srcTable = TableChangingScope.GetSQLTable(SQLTableGetter, table.Name);
    }
    else
      srcTable = SQLTableGetter();
    return (Query) new JoinedAttrQuery(srcTable, fieldWithValue.Name, "AttributeID", "RefNoteID", exp2);
  }

  protected System.Type GetBqlTable(System.Type table)
  {
    while (typeof (IBqlTable).IsAssignableFrom(table.BaseType) && !table.IsDefined(typeof (PXTableAttribute), false) && (!table.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) table.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive))
      table = table.BaseType;
    return table;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (sender.Graph.GetType() == typeof (PXDBAttributeAttribute.AttrGraph))
      return;
    if (sender.Graph.GetType() == typeof (PXGraph) || typeof (GenericInquiryDesigner).IsAssignableFrom(sender.Graph.GetType()))
      this._IsActive = true;
    base.CacheAttached(sender);
    this.InitializeFields(sender);
    System.Type clause = this._SingleSelect.GetType();
    for (System.Type table = sender.GetItemType(); table != typeof (object) && clause == this._SingleSelect.GetType(); table = table.BaseType)
      clause = BqlCommand.Parametrize(table, clause);
    this._View = new PXView(sender.Graph, true, BqlCommand.CreateInstance(clause));
  }

  private List<System.Type> GetTables(PXCache sender)
  {
    List<System.Type> tables = new List<System.Type>();
    System.Type table1 = this._SingleSelect.GetTables()[0];
    if (this._Selector == null)
    {
      if (sender.Graph.Caches[table1].GetStateExt((object) null, this._Field.Name) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.ViewName))
      {
        foreach (System.Type table2 in sender.Graph.Views[stateExt.ViewName].BqlSelect.GetTables())
        {
          System.Type bqlTable = this.GetBqlTable(table2);
          if (!tables.Contains(bqlTable))
            tables.Add(bqlTable);
        }
      }
    }
    else
    {
      foreach (System.Type table3 in this._Selector.GetTables())
      {
        System.Type bqlTable = this.GetBqlTable(table3);
        if (!tables.Contains(bqlTable))
          tables.Add(bqlTable);
      }
    }
    return tables;
  }

  protected virtual void InitializeFields(PXCache sender)
  {
    System.Type table = this._SingleSelect.GetTables()[0];
    List<System.Type> tables = this.GetTables(sender);
    this._Fields = this.GetSlot($"{this._FieldName}_{sender.GetItemType().FullName}_{Thread.CurrentThread.CurrentUICulture.Name}", new PXDBAttributeAttribute.DefinitionParams(sender, table, this._Field, this._Selector, this._DescriptionField, this._ControlTypeField, this._EntryMaskField, this._PrecisionField, this._ListField, this._FieldName), tables.ToArray());
    if (this._Fields == null)
      return;
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    for (int index = 0; index < this._Fields.Length; ++index)
    {
      string name = this._Fields[index].Name;
      int length = name.IndexOf('_');
      dictionary.Add(length > 0 ? name.Substring(0, length) : name, index);
    }
    this._AttributeIndices = dictionary;
    int num = sender.Fields.IndexOf(this._FieldName);
    for (int index = 0; index < this._Fields.Length; ++index)
    {
      int idx = index;
      PXFieldState field = (PXFieldState) ((ICloneable) this._Fields[idx]).Clone();
      field.Visible = this.DefaultVisible;
      sender.Fields.Insert(++num, field.Name);
      string name = field.Name.Substring(0, field.Name.Length - this._FieldName.Length - 1);
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), field.Name, (PXFieldSelecting) ((c, a) => this.AttributeFieldSelecting(c, a, field, name, idx)));
      sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), field.Name, (PXFieldUpdating) ((c, a) => this.AttributeFieldUpdating(c, a, field, name, idx)));
      sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), field.Name, (PXCommandPreparing) ((c, a) => this.AttributeCommandPreparing(c, a, field, name, idx)));
    }
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    PXDBAttributeAttribute._BqlTablesUsed.AddOrUpdate(this._BqlTable, this._BqlTable, (Func<System.Type, System.Type, System.Type>) ((t1, t2) => t1));
  }

  protected virtual PXFieldState[] GetSlot(
    string name,
    PXDBAttributeAttribute.DefinitionParams definitionParams,
    System.Type[] tables)
  {
    return PXDatabase.GetSlot<PXDBAttributeAttribute.Definition, PXDBAttributeAttribute.DefinitionParams>(name, definitionParams, tables)?.Fields;
  }

  /// <exclude />
  /// <exclude />
  protected sealed class DefinitionParams(
    PXCache main,
    System.Type foreignType,
    System.Type field,
    BqlCommand selector,
    string descriptionField,
    string controlTypeField,
    string entryMaskField,
    string precisionField,
    string listField,
    string fieldName)
  {
    public readonly PXCache Main = main;
    private readonly System.Type _ForeignType = foreignType;
    private PXCache _Foreign;
    public readonly System.Type Field = field;
    public readonly BqlCommand Selector = selector;
    public readonly string DescriptionField = descriptionField;
    public readonly string ControlTypeField = controlTypeField;
    public readonly string EntryMaskField = entryMaskField;
    public readonly string PrecisionField = precisionField;
    public readonly string ListField = listField;
    public readonly string FieldName = fieldName;

    public PXCache Foreign
    {
      get => this._Foreign ?? (this._Foreign = this.Main.Graph.Caches[this._ForeignType]);
    }

    [Obsolete("Use the version with more parameters instead.")]
    public DefinitionParams(
      PXCache main,
      System.Type foreignType,
      System.Type field,
      BqlCommand selector,
      string descriptionField,
      string controlTypeField,
      string entryMaskField,
      string listField,
      string fieldName)
      : this(main, foreignType, field, selector, descriptionField, controlTypeField, entryMaskField, "Precision", listField, fieldName)
    {
    }
  }

  /// <exclude />
  public class AttributeIDPlaceholder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PXDBAttributeAttribute.AttributeIDPlaceholder>
  {
    public AttributeIDPlaceholder()
      : base("{04d5718e-a4d3-4e57-b476-782b3105c3d3}")
    {
    }
  }

  /// <exclude />
  protected class Definition : 
    IPrefetchable<PXDBAttributeAttribute.DefinitionParams>,
    IPXCompanyDependent
  {
    public PXFieldState[] Fields;

    void IPrefetchable<PXDBAttributeAttribute.DefinitionParams>.Prefetch(
      PXDBAttributeAttribute.DefinitionParams parameters)
    {
      try
      {
        PXContext.SetSlot<bool>("selectorBypassInit", true);
        List<object> objectList1 = new List<object>();
        List<string> stringList = new List<string>();
        PXView pxView1 = (PXView) null;
        IBqlSearch bqlSearch = (IBqlSearch) null;
        if (parameters.Selector == null)
        {
          if (parameters.Foreign.GetStateExt((object) null, parameters.Field.Name) is PXFieldState stateExt1 && !string.IsNullOrEmpty(stateExt1.ViewName))
          {
            pxView1 = parameters.Main.Graph.Views[stateExt1.ViewName];
            bqlSearch = pxView1.BqlSelect as IBqlSearch;
          }
        }
        else
        {
          pxView1 = new PXView(parameters.Main.Graph, true, parameters.Selector);
          bqlSearch = pxView1.BqlSelect as IBqlSearch;
        }
        if (bqlSearch != null)
        {
          if (!pxView1.AllowSelect)
            throw new PXException("You have insufficient rights to access the object ({0}).", new object[1]
            {
              (object) pxView1.GetItemType().Name
            });
          object instance1 = parameters.Main.CreateInstance();
          PXView pxView2 = pxView1;
          object[] currents = new object[1]{ instance1 };
          object[] objArray = Array.Empty<object>();
          foreach (object obj in pxView2.SelectMultiBound(currents, objArray))
          {
            object data = obj is PXResult ? ((PXResult) obj)[0] : obj;
            if (data != null)
            {
              string str = pxView1.Cache.GetValue(data, bqlSearch.GetField().Name) as string;
              if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
              {
                stringList.Add(str);
                objectList1.Add(data);
              }
            }
          }
          PXGraph instance2 = (PXGraph) PXGraph.CreateInstance<PXDBAttributeAttribute.AttrGraph>();
          PXCache cach = instance2.Caches[parameters.Main.GetItemType()];
          foreach (IBqlParameter parameter in pxView1.BqlSelect.GetParameters())
          {
            if (parameter.HasDefault && parameter.GetReferencedType().IsNested && BqlCommand.GetItemType(parameter.GetReferencedType()).IsAssignableFrom(parameters.Main.GetItemType()))
            {
              if (cach.GetStateExt((object) null, parameter.GetReferencedType().Name) is PXFieldState stateExt2 && !string.IsNullOrEmpty(stateExt2.ViewName))
              {
                PXView view = instance2.Views[stateExt2.ViewName];
                List<object> objectList2 = new List<object>();
                if (view.BqlSelect is IBqlSearch bqlSelect)
                {
                  using (List<object>.Enumerator enumerator = view.SelectMultiBound(new object[1]
                  {
                    instance1
                  }).GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      object obj1 = enumerator.Current;
                      if (obj1 is PXResult)
                        obj1 = (object) PXResult.Unwrap(obj1, view.CacheGetItemType());
                      if (obj1 is PXFieldState)
                        obj1 = PXFieldState.UnwrapValue(obj1);
                      object obj2 = view.Cache.GetValue(obj1, bqlSelect.GetField().Name);
                      if (obj2 != null)
                      {
                        parameters.Main.SetValue(instance1, parameter.GetReferencedType().Name, obj2);
                        foreach (object obj3 in pxView1.SelectMultiBound(new object[1]
                        {
                          instance1
                        }))
                        {
                          object data = obj3 is PXResult ? ((PXResult) obj3)[0] : obj3;
                          if (data != null)
                          {
                            string str = pxView1.Cache.GetValue(data, bqlSearch.GetField().Name) as string;
                            if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
                            {
                              stringList.Add(str);
                              objectList1.Add(data);
                            }
                          }
                        }
                      }
                    }
                    break;
                  }
                }
                break;
              }
              break;
            }
          }
        }
        List<PXFieldState> pxFieldStateList = new List<PXFieldState>();
        foreach (object data in objectList1)
        {
          PXFieldState fieldState = PXDBAttributeAttribute.Definition.CreateFieldState($"{(string) pxView1.Cache.GetValue(data, bqlSearch.GetField().Name)}_{parameters.FieldName}", (string) pxView1.Cache.GetValue(data, parameters.DescriptionField), (int) pxView1.Cache.GetValue(data, parameters.ControlTypeField), (string) pxView1.Cache.GetValue(data, parameters.EntryMaskField), (int) pxView1.Cache.GetValue(data, parameters.PrecisionField), (string) pxView1.Cache.GetValue(data, parameters.ListField));
          if (fieldState != null)
            pxFieldStateList.Add(fieldState);
        }
        this.Fields = pxFieldStateList.ToArray();
      }
      finally
      {
        PXContext.SetSlot<bool>("selectorBypassInit", false);
      }
    }

    [Obsolete("Use the version with more parameters instead.")]
    public static PXFieldState CreateFieldState(
      string fieldName,
      string description,
      int ctype,
      string entryMask,
      string list)
    {
      return PXDBAttributeAttribute.Definition.CreateFieldState(fieldName, description, ctype, entryMask, 0, list);
    }

    public static PXFieldState CreateFieldState(
      string fieldName,
      string description,
      int ctype,
      string entryMask,
      int precision,
      string list)
    {
      PXFieldState fieldState = KeyValueHelper.MakeFieldState(new KeyValueHelper.Attribute(new CSAttribute()
      {
        Description = description,
        ControlType = new int?(ctype),
        EntryMask = entryMask,
        Precision = new int?(precision)
      }, ((IEnumerable<string>) Split(list, '\t')).Select<string, string[]>((Func<string, string[]>) (pair => Split(pair, PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar))).Where<string[]>((Func<string[], bool>) (pair => pair.Length != 0)).Select<string[], CSAttributeDetail>((Func<string[], CSAttributeDetail>) (pair =>
      {
        if (pair != null && pair.Length == 2)
        {
          string str1 = pair[0];
          if (str1 != null)
          {
            string str2 = pair[1];
            if (str2 != null)
              return new CSAttributeDetail()
              {
                ValueID = str1,
                Description = str2
              };
          }
        }
        return new CSAttributeDetail()
        {
          ValueID = pair[0],
          Description = pair[0]
        };
      }))), fieldName);
      if (fieldState != null)
      {
        fieldState.Visibility = PXUIVisibility.Dynamic;
        fieldState.DisplayName = $"${PXMessages.Localize("Attributes", out string _)}$-{description}";
        fieldState.Enabled = false;
        fieldState.Visible = false;
      }
      return fieldState;

      static string[] Split(string source, char by)
      {
        string[] strArray;
        if (source == null)
          strArray = (string[]) null;
        else
          strArray = source.Split(new char[1]{ by }, StringSplitOptions.RemoveEmptyEntries);
        return strArray ?? Array.Empty<string>();
      }
    }
  }

  /// <exclude />
  private class AttrGraph : PXGraph
  {
  }
}

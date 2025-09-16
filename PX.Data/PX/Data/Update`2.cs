// Decompiled with JetBrains decompiler
// Type: PX.Data.Update`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class Update<Set, Select> : IBqlUpdate
  where Set : IBqlSet
  where Select : BqlCommand, IBqlSelect, IBqlCreator
{
  private IBqlSet _set;
  private BqlCommand _select;
  private IEnumerable<(SQLExpression column, SQLExpression value)> _assignments;
  private Query _selectQuery;

  public PXDataFieldParam[] GetFieldAssignmentParameters(PXGraph graph, PXDataValue[] pars)
  {
    IEnumerable<(Column, SQLExpression)> tuples = this.GetAssignments(graph).Where<(SQLExpression, SQLExpression)>((Func<(SQLExpression, SQLExpression), bool>) (a => a.column is Column && !(a.value is Column))).Select<(SQLExpression, SQLExpression), (Column, SQLExpression)>((Func<(SQLExpression, SQLExpression), (Column, SQLExpression)>) (a => ((Column) a.column, a.value)));
    System.Type bqlTable = this.GetBqlTable();
    EvaluateVisitor visitor = new EvaluateVisitor(graph.Caches[bqlTable], (object) null, ((IEnumerable<PXDataValue>) pars).Select<PXDataValue, object>((Func<PXDataValue, object>) (p => p.Value)).ToArray<object>());
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
    foreach ((Column, SQLExpression) tuple in tuples)
    {
      Column column = tuple.Item1;
      object obj = tuple.Item2.Accept<object>((ISQLExpressionVisitor<object>) visitor);
      if (!visitor.UnknownValue)
      {
        PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(column, obj);
        pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign);
      }
    }
    return pxDataFieldParamList.ToArray();
  }

  public IEnumerable<(Column setColumn, string valueColumnName)> GetDependentSetColumns(
    PXGraph graph)
  {
    return this.GetAssignments(graph).Where<(SQLExpression, SQLExpression)>((Func<(SQLExpression, SQLExpression), bool>) (a => a.column is Column && a.value is Column)).Select<(SQLExpression, SQLExpression), (Column, string)>((Func<(SQLExpression, SQLExpression), (Column, string)>) (a => ((Column) a.column, ((Column) a.value).Name)));
  }

  public Query GetSelectQuery(PXGraph graph)
  {
    if (this._selectQuery == null)
      this.GetText(graph);
    return this._selectQuery;
  }

  public IEnumerable<(SQLExpression column, SQLExpression value)> GetAssignments(PXGraph graph)
  {
    if (this._assignments == null)
      this.GetText(graph);
    return this._assignments;
  }

  public string GetText(PXGraph graph)
  {
    this.ensureSet();
    this.ensureSelect();
    List<System.Type> tables = new List<System.Type>();
    List<KeyValuePair<SQLExpression, SQLExpression>> keyValuePairList = new List<KeyValuePair<SQLExpression, SQLExpression>>();
    BqlCommandInfo info = new BqlCommandInfo(false)
    {
      Tables = tables,
      Parameters = new List<IBqlParameter>()
    };
    this._set.AppendExpression(graph, info, keyValuePairList);
    BqlCommand.Selection selection1 = new BqlCommand.Selection()
    {
      ParamCounter = info.Parameters.Count
    };
    Query queryInternal = this._select.GetQueryInternal(graph, info, selection1);
    queryInternal.ClearSelection();
    ISqlDialect sqlDialect = graph.SqlDialect;
    tables[0] = this.GetSetTable(true, false);
    string tableName = sqlDialect.quoteDbIdentifier(tables[0].Name);
    int index1 = keyValuePairList.FindIndex((Predicate<KeyValuePair<SQLExpression, SQLExpression>>) (a => a.Key is Column key1 && ((SimpleTable) key1.Table()).Name.OrdinalEquals(tables[0].Name)));
    Query rawFromWhere;
    KeyValuePair<SQLExpression, SQLExpression> keyValuePair1;
    if (this._select is IBqlAggregate)
    {
      queryInternal.As("s");
      foreach (KeyValuePair<SQLExpression, SQLExpression> keyValuePair2 in keyValuePairList)
      {
        if (keyValuePair2.Value != null)
        {
          SQLExpression sqlExpression = keyValuePair2.Value;
          SQLExpression expr;
          if (!sqlExpression.IsNullExpression() && selection1.ColExprs.Contains(expr = selection1.GetExpr(sqlExpression.ToString())))
            queryInternal.Field(expr.As(sqlExpression.ToString()));
        }
      }
      rawFromWhere = new Query().From((Table) queryInternal);
      rawFromWhere.AppendRestrictions();
      this._selectQuery = (Query) rawFromWhere.Duplicate();
    }
    else
    {
      for (int index2 = 0; index2 < keyValuePairList.Count; ++index2)
      {
        Query query1 = queryInternal;
        keyValuePair1 = keyValuePairList[index2];
        SQLExpression key2 = keyValuePair1.Key;
        query1.Field(key2);
        Query query2 = queryInternal;
        keyValuePair1 = keyValuePairList[index2];
        SQLExpression field = keyValuePair1.Value;
        query2.Field(field);
      }
      Query query3 = (Query) queryInternal.Duplicate();
      query3.AppendRestrictions();
      Query query4 = query3.FlattenSubselects();
      List<SQLExpression> selection2 = query4.GetSelection();
      keyValuePairList.Clear();
      for (int index3 = 0; index3 < selection2.Count / 2; ++index3)
        keyValuePairList.Add(new KeyValuePair<SQLExpression, SQLExpression>(selection2[2 * index3].Duplicate(), selection2[2 * index3 + 1].Duplicate()));
      this._selectQuery = (Query) query4.Duplicate();
      rawFromWhere = query4.ClearSelection();
    }
    if (index1 >= 0 && index1 < keyValuePairList.Count)
    {
      keyValuePair1 = keyValuePairList[index1];
      if (keyValuePair1.Key is Column key3)
        tableName = key3.Table().SQLTableName(sqlDialect.GetConnection()).ToString();
    }
    this._assignments = keyValuePairList.Select<KeyValuePair<SQLExpression, SQLExpression>, (SQLExpression, SQLExpression)>((Func<KeyValuePair<SQLExpression, SQLExpression>, (SQLExpression, SQLExpression)>) (a => (a.Key, a.Value)));
    return sqlDialect.scriptUpdateJoin(tables, tableName, rawFromWhere, keyValuePairList);
  }

  public System.Type GetBqlTable()
  {
    this.ensureSelect();
    List<System.Type> typeList = new List<System.Type>();
    SQLExpression sqlExpression = SQLExpression.None();
    BqlCommand select = this._select;
    ref SQLExpression local = ref sqlExpression;
    BqlCommandInfo info = new BqlCommandInfo(false);
    info.Tables = typeList;
    info.BuildExpression = false;
    BqlCommand.Selection selection = new BqlCommand.Selection();
    select.AppendExpression(ref local, (PXGraph) null, info, selection);
    System.Type bqlTable = typeList[0];
    System.Type baseType;
    while (typeof (IBqlTable).IsAssignableFrom(baseType = bqlTable.BaseType) || baseType.IsDefined(typeof (PXTableAttribute), false) || baseType.IsDefined(typeof (PXTableNameAttribute), false) && ((PXTableNameAttribute) baseType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive)
      bqlTable = baseType;
    return bqlTable;
  }

  public System.Type GetSetTable(bool processCacheExtension, bool findAncestor)
  {
    this.ensureSet();
    System.Type element = this._set.GetFieldType();
    if (processCacheExtension && element.BaseType.IsGenericType && element.BaseType.GetGenericTypeDefinition() == typeof (PXCacheExtension<>))
      element = element.BaseType.GenericTypeArguments[0];
    if (!findAncestor)
      return element;
    System.Type baseType;
    for (; element.GetCustomAttribute<PXTableAttribute>(false) == null; element = baseType)
    {
      baseType = element.BaseType;
      if (!typeof (IBqlTable).IsAssignableFrom(baseType))
        return element;
    }
    return element;
  }

  public IBqlParameter[] GetParameters()
  {
    this.ensureSet();
    this.ensureSelect();
    BqlCommandInfo info = new BqlCommandInfo(false)
    {
      Tables = new List<System.Type>(),
      Parameters = new List<IBqlParameter>(),
      BuildExpression = false
    };
    this._set.AppendExpression((PXGraph) null, info, (List<KeyValuePair<SQLExpression, SQLExpression>>) null);
    SQLExpression exp = SQLExpression.None();
    this._select.AppendExpression(ref exp, (PXGraph) null, info, new BqlCommand.Selection());
    return info.Parameters.ToArray();
  }

  private IBqlSet ensureSet()
  {
    if (this._set == null)
      this._set = (IBqlSet) Activator.CreateInstance<Set>();
    return this._set;
  }

  private BqlCommand ensureSelect()
  {
    if (this._select == null)
    {
      this._select = (BqlCommand) Activator.CreateInstance<Select>();
      if (this._select is BqlCommandDecorator select)
        this._select = select.Unwrap();
    }
    return this._select;
  }
}

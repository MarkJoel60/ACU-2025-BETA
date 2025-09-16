// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Api.Export;
using PX.Data.BQL;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data;

/// <summary>The BQL statement generator. This class is the base class for the <tt>Select</tt> and <tt>Search</tt> classes,
/// which is used by the system during the processing of data queries on Acumatica ERP forms.
/// The main purpose of the <tt>BqlCommand</tt> class is to convert BQL commands to SQL query tree (see <see cref="N:PX.Data.SQLTree" />).</summary>
public abstract class BqlCommand : IBqlCreator, IBqlVerifier
{
  internal PXSelectOperationContext Context;
  private System.Type[] _tables;
  private static readonly ConcurrentDictionary<System.Type, string> _dacToTableNameMap = new ConcurrentDictionary<System.Type, string>();
  public static readonly Func<IList<IBqlUnary>> EmptyBqlUnaryList = (Func<IList<IBqlUnary>>) (() => (IList<IBqlUnary>) BqlCommand._EmptyBqlUnaryList);
  internal static readonly ReadOnlyCollection<IBqlUnary> _EmptyBqlUnaryList = new List<IBqlUnary>().AsReadOnly();
  /// <exclude />
  public const string Null = "NULL";
  /// <exclude />
  public const int DBOperationCommands = 3;
  /// <exclude />
  public const int DBOperationOptions = 12;
  /// <exclude />
  protected internal static readonly string invalid_join_criteria_detected = "Invalid Join criteria have been detected.";
  internal List<PXDataRecordMap.FieldEntry> RecordMapEntries = new List<PXDataRecordMap.FieldEntry>();

  /// <exclude />
  protected internal BqlCommand()
  {
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.GetQueryInternal(graph, info, selection).IsOK();
  }

  public static bool AppendExpression<TOperand>(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    ref IBqlCreator field_operator)
    where TOperand : IBqlOperand
  {
    bool flag = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    if (typeof (IBqlField).IsAssignableFrom(typeof (TOperand)))
    {
      if (info.BuildExpression)
        exp = BqlCommand.GetSingleExpression(typeof (TOperand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (TOperand));
    }
    else
    {
      if (!typeof (IBqlCreator).IsAssignableFrom(typeof (TOperand)))
        throw new ArgumentException($"Generic argument {typeof (TOperand).Name} must implement IBqlField or IBqlCreator interface.");
      if (field_operator == null)
        field_operator = field_operator.createOperand<TOperand>();
      flag &= field_operator.AppendExpression(ref exp, graph, info, selection);
    }
    return flag;
  }

  /// <summary>Verifies if the item corresponds to the command.</summary>
  /// <param name="cache">The <see cref="T:PX.Data.PXCache" /> object the item is contained in.</param>
  /// <param name="item">The data item to verify.</param>
  /// <param name="pars">The query parameters.</param>
  /// <param name="result">The result of verification if the verification is possible.</param>
  /// <param name="value">The value of the BQL operand (if any).</param>
  public abstract void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value);

  /// <summary>
  /// Verifies if the item corresponds to the BQL command and provided parameters.
  /// </summary>
  /// <param name="cache">The <see cref="T:PX.Data.PXCache" /> object the item is contained in.</param>
  /// <param name="item">The data item to verify.</param>
  /// <param name="parameters">The query parameters.</param>
  /// <returns>The method returns <see langword="true" /> if the item corresponds to the command and parameters
  /// or if it is impossible to determine the correspondence.</returns>
  public bool Meet(PXCache cache, object item, params object[] parameters)
  {
    List<object> pars = new List<object>((IEnumerable<object>) parameters);
    bool? result = new bool?();
    object obj = (object) null;
    try
    {
      this.Verify(cache, item, pars, ref result, ref obj);
    }
    catch (SystemException ex)
    {
      throw new PXException($"BQL verification failed: {this.ToString()}", (Exception) ex);
    }
    if (!result.HasValue)
      return true;
    bool? nullable = result;
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public Query GetQuery(PXGraph graph, PXView view = null)
  {
    try
    {
      PXMutableCollection mutableCollection = new PXMutableCollection();
      PXContext.SetSlot<PXMutableCollection>(mutableCollection);
      bool flag = view != null && view.RestrictedFields.Any();
      BqlCommand.Selection selection = new BqlCommand.Selection()
      {
        _Command = this,
        Restrict = flag,
        RestrictedFields = flag ? view.RestrictedFields : (RestrictedFieldsSet) null
      };
      this.RecordMapEntries = new List<PXDataRecordMap.FieldEntry>();
      BqlCommandInfo info = new BqlCommandInfo();
      Query queryInternal = this.GetQueryInternal(graph, info, selection);
      if (this.Context != null)
      {
        this.Context.LastCommandMutable = mutableCollection.Count > 0;
        this.Context.LastTables = info.Tables;
        string primaryTableName = queryInternal.GetFrom().FirstOrDefault<Joiner>()?.Table() is SimpleTable simpleTable ? simpleTable.Name.ToLower() : (string) null;
        List<SQLExpression> sqlExpressionList = new List<SQLExpression>()
        {
          (SQLExpression) new SubQuery(queryInternal)
        };
        queryInternal.GetExpressionsOfType((Predicate<SQLExpression>) (e => e is SubQuery), sqlExpressionList);
        List<SimpleTable> source = new List<SimpleTable>();
        for (List<Table> list = sqlExpressionList.OfType<SubQuery>().SelectMany<SubQuery, Table>((Func<SubQuery, IEnumerable<Table>>) (sq => sq.Query().GetFrom().Select<Joiner, Table>((Func<Joiner, Table>) (j => j.Table())))).ToList<Table>(); list.Count > 0; list = list.OfType<Query>().SelectMany<Query, Table>((Func<Query, IEnumerable<Table>>) (sq => sq.GetFrom().Select<Joiner, Table>((Func<Joiner, Table>) (j => j.Table())))).ToList<Table>())
          source.AddRange((IEnumerable<SimpleTable>) list.OfType<SimpleTable>().ToList<SimpleTable>());
        this.Context.LastSqlTables = source.Select<SimpleTable, string>((Func<SimpleTable, string>) (t => t.Name.ToLower())).Where<string>((Func<string, bool>) (_ =>
        {
          if (!_.Equals("note") && !_.Equals("notedoc"))
            return true;
          return primaryTableName != null && _.Equals(primaryTableName);
        })).Distinct<string>().ToList<string>();
      }
      return queryInternal;
    }
    finally
    {
      PXContext.SetSlot<PXMutableCollection>((PXMutableCollection) null);
    }
  }

  protected static SQLExpression AppendExpressionList(
    ref List<SQLExpression> list,
    SQLExpression exp)
  {
    SQLExpression sqlExpression = (SQLExpression) null;
    if (exp == null)
      return (SQLExpression) null;
    if (exp.Oper() == SQLExpression.Operation.SEQ)
    {
      if (exp.LExpr() != null)
        sqlExpression = BqlCommand.AppendExpressionList(ref list, exp.LExpr());
      if (exp.RExpr() != null)
        sqlExpression = BqlCommand.AppendExpressionList(ref list, exp.RExpr()) ?? sqlExpression;
      return sqlExpression;
    }
    if (exp.Oper() == SQLExpression.Operation.COUNT || exp.Oper() == SQLExpression.Operation.COUNT_DISTINCT)
      return exp;
    list.Add(exp);
    return (SQLExpression) null;
  }

  protected Query CreateQuery(PXGraph graph)
  {
    Query query = new Query();
    int num;
    if (graph != null)
    {
      bool? isArchiveContext = graph.IsArchiveContext;
      bool flag = true;
      if (isArchiveContext.GetValueOrDefault() == flag & isArchiveContext.HasValue)
      {
        num = 1;
        goto label_4;
      }
    }
    PXSelectOperationContext context1 = this.Context;
    num = context1 != null ? (context1.ReadArchived ? 1 : 0) : 0;
label_4:
    query.ShowArchivedRecords = num != 0;
    PXSelectOperationContext context2 = this.Context;
    query.SkipDefaultQueryHints = context2 != null && context2.SkipDefaultHints;
    return query;
  }

  public virtual Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    PXTrace.WithSourceLocation(nameof (GetQueryInternal), "C:\\build\\code_repo\\NetTools\\PX.Data\\BQL\\BqlCommand.cs", 242).Warning("Empty parent implementation of BqlCommand.GetQueryInternal() called. ");
    return (Query) null;
  }

  /// <summary>Obtains the DACs that are referenced in the command.</summary>
  /// <returns>The DAC types.</returns>
  public System.Type[] GetTables()
  {
    if (this._tables == null)
    {
      BqlCommandInfo info = new BqlCommandInfo(false)
      {
        Tables = new List<System.Type>(),
        BuildExpression = false
      };
      this.GetQueryInternal((PXGraph) null, info, new BqlCommand.Selection());
      this._tables = info.Tables.ToArray();
    }
    return this._tables;
  }

  public override string ToString()
  {
    return ((IEnumerable<System.Type>) BqlCommand.Decompose(this.GetSelectType())).Select<System.Type, string>((Func<System.Type, string>) (_ => _.Name)).JoinToString<string>("\n");
  }

  /// <exclude />
  public virtual System.Type GetFirstTable() => this.GetTables()[0];

  /// <summary>Obtains the parameters of the command.</summary>
  /// <returns>The method returns the parameter instances.</returns>
  public IBqlParameter[] GetParameters()
  {
    List<IBqlParameter> bqlParameterList = new List<IBqlParameter>();
    List<System.Type> typeList = new List<System.Type>();
    this.GetQueryInternal((PXGraph) null, new BqlCommandInfo(false)
    {
      Tables = typeList,
      Parameters = bqlParameterList,
      BuildExpression = false
    }, new BqlCommand.Selection());
    return bqlParameterList.ToArray();
  }

  /// <summary>
  /// Retrieves the pairs of a referenced field and parameter field from the current command.
  /// </summary>
  /// <returns>The method returns a list of pairs of a referenced field and a parameter field.
  /// In this pair, the key is a referenced field and the value is a parameter field.</returns>
  public List<KeyValuePair<System.Type, System.Type>> GetParameterPairs()
  {
    return this.GetParameterPairs(this.GetSelectType());
  }

  internal bool HasNotEqualParameter(System.Type p)
  {
    return this.GetFieldPairs(this.GetSelectType(), true, true).Any<KeyValuePair<System.Type, System.Type>>((Func<KeyValuePair<System.Type, System.Type>, bool>) (_ => _.Value == p));
  }

  /// <summary>
  /// Retrieves the pairs of a referenced field and parameter field from the specified command.
  /// </summary>
  /// <param name="command">The command from which the pairs should be retrieved.</param>
  /// <returns>The method returns a list of pairs of a referenced field and a parameter field.
  /// In this pair, the key is a referenced field and the value is a parameter field.</returns>
  public List<KeyValuePair<System.Type, System.Type>> GetParameterPairs(System.Type command)
  {
    return this.GetFieldPairs(command, true);
  }

  internal List<KeyValuePair<System.Type, System.Type>> GetFieldPairs()
  {
    return this.GetFieldPairs(this.GetSelectType(), false);
  }

  private List<KeyValuePair<System.Type, System.Type>> GetFieldPairs(
    System.Type command,
    bool parametersOnly,
    bool noteqaulonly = false)
  {
    List<KeyValuePair<System.Type, System.Type>> fieldPairs = new List<KeyValuePair<System.Type, System.Type>>();
    command = BqlCommand.UnwrapCustomPredicate(command);
    System.Type[] genericArguments1 = command.GetGenericArguments();
    if (genericArguments1.Length >= 2 && typeof (IBqlParameter).IsAssignableFrom(genericArguments1[0]) && typeof (IBqlComparison).IsAssignableFrom(genericArguments1[1]))
    {
      System.Type[] genericArguments2 = genericArguments1[0].GetGenericArguments();
      if (((IEnumerable<System.Type>) genericArguments2).Any<System.Type>() && typeof (IBqlField).IsAssignableFrom(genericArguments2[0]))
      {
        System.Type[] genericArguments3 = genericArguments1[1].GetGenericArguments();
        if (((IEnumerable<System.Type>) genericArguments3).Any<System.Type>())
        {
          if (typeof (IBqlField).IsAssignableFrom(genericArguments3[0]))
          {
            if (!noteqaulonly || genericArguments1[1].IsGenericType && typeof (NotEqual<>).IsAssignableFrom(genericArguments1[1].GetGenericTypeDefinition()))
              fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments3[0], genericArguments2[0]));
          }
          else if (noteqaulonly && typeof (IBqlParameter).IsAssignableFrom(genericArguments3[0]))
          {
            if (genericArguments3[0].DeclaringType == typeof (P))
            {
              fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments2[0], genericArguments2[0]));
            }
            else
            {
              System.Type[] genericArguments4 = genericArguments3[0].GetGenericArguments();
              if (((IEnumerable<System.Type>) genericArguments4).Any<System.Type>() && typeof (IBqlField).IsAssignableFrom(genericArguments4[0]))
              {
                fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments4[0], genericArguments2[0]));
                fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments2[0], genericArguments4[0]));
              }
            }
          }
        }
      }
      for (int index = genericArguments1.Length - 1; index >= 2; --index)
        fieldPairs.AddRange((IEnumerable<KeyValuePair<System.Type, System.Type>>) this.GetFieldPairs(genericArguments1[index], parametersOnly, noteqaulonly));
    }
    else if (genericArguments1.Length >= 2 && typeof (IBqlField).IsAssignableFrom(genericArguments1[0]) && typeof (IBqlComparison).IsAssignableFrom(genericArguments1[1]))
    {
      if (!noteqaulonly || genericArguments1[1].IsGenericType && typeof (NotEqual<>).IsAssignableFrom(genericArguments1[1].GetGenericTypeDefinition()))
      {
        System.Type[] genericArguments5 = genericArguments1[1].GetGenericArguments();
        if (((IEnumerable<System.Type>) genericArguments5).Any<System.Type>())
        {
          if (typeof (IBqlParameter).IsAssignableFrom(genericArguments5[0]))
          {
            System.Type[] genericArguments6 = genericArguments5[0].GetGenericArguments();
            if (((IEnumerable<System.Type>) genericArguments6).Any<System.Type>() && typeof (IBqlField).IsAssignableFrom(genericArguments6[0]))
              fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments1[0], genericArguments6[0]));
          }
          else if (!parametersOnly && typeof (IBqlField).IsAssignableFrom(genericArguments5[0]))
            fieldPairs.Add(new KeyValuePair<System.Type, System.Type>(genericArguments1[0], genericArguments5[0]));
        }
      }
      for (int index = genericArguments1.Length - 1; index >= 2; --index)
        fieldPairs.AddRange((IEnumerable<KeyValuePair<System.Type, System.Type>>) this.GetFieldPairs(genericArguments1[index], parametersOnly, noteqaulonly));
    }
    else
    {
      for (int index = genericArguments1.Length - 1; index >= 0; --index)
        fieldPairs.AddRange((IEnumerable<KeyValuePair<System.Type, System.Type>>) this.GetFieldPairs(genericArguments1[index], parametersOnly, noteqaulonly));
    }
    return fieldPairs;
  }

  /// <exclude />
  public System.Type[] GetReferencedFields(bool strictEquality)
  {
    if (!strictEquality)
    {
      List<System.Type> typeList = new List<System.Type>();
      this.GetQueryInternal((PXGraph) null, new BqlCommandInfo(false)
      {
        Fields = typeList,
        BuildExpression = false
      }, new BqlCommand.Selection());
      return typeList.ToArray();
    }
    BqlCommand.EqualityList equalityList = new BqlCommand.EqualityList();
    this.GetQueryInternal((PXGraph) null, new BqlCommandInfo(false)
    {
      Fields = (List<System.Type>) equalityList,
      BuildExpression = false
    }, new BqlCommand.Selection());
    if (equalityList.NonStrict)
      equalityList.Clear();
    return equalityList.ToArray();
  }

  /// <exclude />
  public virtual System.Type GetSelectType() => this.GetType();

  internal virtual object GetUniqueKey() => (object) this.GetSelectType();

  /// <summary>Obtains the columns the command is ordered by.</summary>
  /// <returns>The method returns sort column instances.</returns>
  public IBqlSortColumn[] GetSortColumns()
  {
    List<IBqlSortColumn> bqlSortColumnList = new List<IBqlSortColumn>();
    this.GetQueryInternal((PXGraph) null, new BqlCommandInfo(false)
    {
      SortColumns = bqlSortColumnList,
      BuildExpression = false
    }, new BqlCommand.Selection());
    return bqlSortColumnList.ToArray();
  }

  /// <summary>Obtains explicit columns the command is ordered by.</summary>
  /// <returns>The method returns sort column instances.</returns>
  internal IBqlSortColumn[] GetExplicitSortColumns()
  {
    List<IBqlSortColumn> bqlSortColumnList = new List<IBqlSortColumn>();
    this.GetQueryInternal((PXGraph) null, new BqlCommandInfo(false)
    {
      SortColumns = bqlSortColumnList,
      BuildExpression = false,
      OnlyExplicitSort = true
    }, new BqlCommand.Selection());
    return bqlSortColumnList.ToArray();
  }

  /// <summary>Constructs the command from the current one by replacing the <tt>WHERE</tt> clause.
  /// The new <tt>WHERE</tt> clause is specified as the type parameter.</summary>
  /// <typeparam name="newWhere">The <tt>WHERE</tt> clause with the new restriction.</typeparam>
  /// <returns>The method returns the new command with the <tt>WHERE</tt> clause replaced.</returns>
  /// <example>
  /// <code>
  /// PXSelect&lt;ARAdjust&gt; cmd = new PXSelect&lt;ARAdjust&gt;(this);
  /// cmd.WhereNew&lt;
  ///   Where&lt;ARInvoice.docType.FromCurrent.IsNotEqual&lt;ARDocType.creditMemo&gt;
  ///   .And&lt;ARInvoice.released.FromCurrent.IsEqual&lt;True&gt;&gt;
  ///   .And&lt;ARInvoice.isMigratedRecord.FromCurrent.IsEqual&lt;True&gt;&gt;
  ///   .And&lt;ARInvoice.curyInitDocBal.FromCurrent.IsNotEqual&lt;ARInvoice.curyOrigDocAmt.FromCurrent&gt;&gt;
  ///   .And&lt;ARAdjust.invoiceID.IsEqual&lt;ARInvoice.noteID.FromCurrent&gt;&gt;
  ///   .And&lt;ARAdjust.adjgDocType.IsEqual&lt;Standalone.ARRegisterAlias.docType&gt;&gt;&gt;&gt;();
  /// </code>
  /// </example>
  public abstract BqlCommand WhereNew<newWhere>() where newWhere : IBqlWhere, new();

  /// <summary>
  /// Constructs the command from the current one by replacing the <tt>WHERE</tt> clause.
  /// The new <tt>WHERE</tt> clause is specified as the parameter.</summary>
  /// <param name="newWhere">The <tt>WHERE</tt> clause with the new restriction.</param>
  /// <returns>New command with the where clause replaced</returns>
  /// <example>
  /// <code>
  /// ChildDocument.View.WhereNew(BqlCommand.Compose(
  ///   typeof(Where&lt;,&gt;), originalField,
  ///   typeof(Equal&lt;&gt;), typeof(Required&lt;&gt;), originalField));
  /// </code>
  /// </example>
  public abstract BqlCommand WhereNew(System.Type newWhere);

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>WHERE</tt> clause with the <tt>AND</tt>&gt; operator.
  /// The appended <tt>WHERE</tt> clause is specified as the type parameter.</summary>
  /// <typeparam name="TWhere">The <tt>WHERE</tt> clause with the additional restriction.</typeparam>
  /// <returns>The method returns the created command.</returns>
  /// <example>
  /// <code>
  /// var docList = new SelectFrom&lt;APInvoiceExt&gt;.View(this);
  /// docList.WhereAnd&lt;Where&lt;APInvoiceExt.isMigratedRecord.IsEqual&lt;True&gt;&gt;&gt;();
  /// </code>
  /// </example>
  public abstract BqlCommand WhereAnd<TWhere>() where TWhere : IBqlWhere, new();

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>WHERE</tt> clause with the <tt>AND</tt>&gt; operator.
  /// The appended <tt>WHERE</tt> clause is specified as the parameter.</summary>
  /// <param name="where">The <tt>WHERE</tt> clause with the additional restriction.</param>
  /// <returns>The method returns the created command.</returns>
  /// <example>
  /// <code>
  /// private readonly Type[] _inventoryRestrictingConditions;
  /// private List&lt;(string CD, string Description)&gt; FindAlternateInventory(PXGraph graph, string alternateID)
  /// {
  ///   BqlCommand alternatesSelect = new
  ///     SelectFrom&lt;INItemXRef&gt;.
  ///     InnerJoin&lt;InventoryItem&gt;.
  ///     On&lt;INItemXRef.FK.InventoryItem&gt;.
  ///     Where&lt;Match&lt;AccessInfo.userName.FromCurrent&gt;.And&lt;
  ///       INItemXRef.alternateType.IsEqual&lt;@P.AsString&gt;.And&lt;
  ///       INItemXRef.alternateID.IsEqual&lt;@P.AsString&gt;&gt;&gt;&gt;();
  /// 
  ///   foreach (var restriction in _inventoryRestrictingConditions)
  ///   {
  ///     alternatesSelect = alternatesSelect.WhereAnd(restriction);
  ///   }
  /// }
  /// </code>
  /// </example>
  public abstract BqlCommand WhereAnd(System.Type where);

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>WHERE</tt> clause with the <tt>OR</tt>&gt; operator.
  /// The appended <tt>WHERE</tt> clause is specified as the type parameter.</summary>
  /// <typeparam name="TWhere">The <tt>WHERE</tt> clause with the additional restriction.</typeparam>
  /// <returns>The method returns the created command.</returns>
  /// <example>
  /// <code>
  /// TimeCardMaint maint = PXGraph.CreateInstance&lt;TimeCardMaint&gt;();
  /// maint.Document.WhereOr&lt;Where&lt;PMTask.approverID.IsEqual&lt;EPSummaryFilter.approverID.FromCurrent&gt;&gt;&gt;();
  /// </code>
  /// </example>
  public abstract BqlCommand WhereOr<TWhere>() where TWhere : IBqlWhere, new();

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>WHERE</tt> clause with the <tt>OR</tt>&gt; operator.
  /// The appended <tt>WHERE</tt> clause is specified as the type parameter.</summary>
  /// <param name="where">The <tt>WHERE</tt> clause with the additional restriction.</param>
  /// <returns>The method returns the created command.</returns>
  public abstract BqlCommand WhereOr(System.Type where);

  /// <summary>
  /// Constructs the command from the current one by inversing the <tt>WHERE</tt> clause with the <tt>NOT</tt>&gt; operator.
  /// </summary>
  /// <returns>The method returns the created command.</returns>
  public abstract BqlCommand WhereNot();

  /// <summary>
  /// Constructs the command from the current one by replacing the list of sort columns.
  /// The new <tt>ORDER BY</tt> clause is specified as the type parameter.</summary>
  /// <typeparam name="newOrderBy">The new <tt>ORDER BY</tt> clause with the sort columns.</typeparam>
  /// <returns>The method returns the created command.</returns>
  /// <example>
  /// <para>In the following example, the <tt>ORDER BY</tt> clause specified in the data view is replaced
  /// with the new <tt>ORDER BY</tt> clause in the graph constructor.</para>
  /// <code>
  /// public SelectFrom&lt;ARInvoiceDiscountDetail&gt;.
  ///   Where&lt;ARInvoiceDiscountDetail.docType.IsEqual&lt;ARInvoice.docType.FromCurrent&gt;.
  ///     And&lt;ARInvoiceDiscountDetail.refNbr.IsEqual&lt;ARInvoice.refNbr.FromCurrent&gt;&gt;&gt;.
  ///   OrderBy&lt;ARInvoiceDiscountDetail.lineNbr.Asc&gt;.View ARDiscountDetails;
  /// 
  /// public SOInvoiceEntry() : base()
  /// {
  ///   ARDiscountDetails.OrderByNew&lt;
  ///     OrderBy&lt;ARInvoiceDiscountDetail.orderType.Asc&gt;&gt;();
  ///   ...
  /// }
  /// </code>
  /// </example>
  public abstract BqlCommand OrderByNew<newOrderBy>() where newOrderBy : IBqlOrderBy, new();

  /// <summary>
  /// Constructs the command from the current one by replacing the list of sort columns.
  /// The new <tt>ORDER BY</tt> clause is specified as the parameter.</summary>
  /// <param name="newOrderBy">The new <tt>ORDER BY</tt> clause with the sort columns.</param>
  /// <returns>The method returns the created command.</returns>
  /// <example>
  /// <code>
  /// Type orderByClause = BqlCommand.Compose(typeof(OrderBy&lt;&gt;), orderByClause);
  /// query.View.OrderByNew(orderByClause);
  /// </code>
  /// </example>
  public abstract BqlCommand OrderByNew(System.Type newOrderBy);

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>GROUP BY</tt> clause.
  /// The <tt>GROUP BY</tt> clause is specified as the type parameter.</summary>
  /// <typeparam name="newAggregate">The new <tt>GROUP BY</tt> clause.</typeparam>
  /// <returns>The method returns the created command with the <tt>GROUP BY</tt> clause appended.</returns>
  /// <example>
  /// <code>
  /// BqlCommand cmd = ComposeBQLCommandForRecords(filter);
  /// 
  /// cmd = cmd.AggregateNew&lt;Aggregate&lt;
  ///   Sum&lt;SchedulesInqResult.signTotalAmt,
  ///   Sum&lt;SchedulesInqResult.signDefAmt&gt;&gt;&gt;&gt;();
  /// </code>
  /// </example>
  public virtual BqlCommand AggregateNew<newAggregate>() where newAggregate : IBqlAggregate, new()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  /// <summary>
  /// Constructs the command from the current one by appending the <tt>GROUP BY</tt> clause.
  /// The <tt>GROUP BY</tt> clause is specified as the parameter.</summary>
  /// <param name="newAggregate">The new <tt>GROUP BY</tt> clause.</param>
  /// <returns>The method returns the created command with the <tt>GROUP BY</tt> clause appended.</returns>
  /// <example>
  /// <code>
  /// BqlCommand childActivitiescmd = View.BqlSelect.OrderByNew&lt;BqlNone&gt;();
  /// Type aggregate = BqlTemplate.FromType(
  ///   typeof(Aggregate&lt;
  ///     Sum&lt;CRChildActivity.timeSpent,
  ///     Sum&lt;CRChildActivity.overtimeSpent,
  ///     Sum&lt;CRChildActivity.timeBillable,
  ///     Sum&lt;CRChildActivity.overtimeBillable,
  ///     GroupBy&lt;CRChildActivity.parentNoteID&gt;&gt;&gt;&gt;&gt;&gt;))
  /// .ToType();
  /// childActivitiescmd = childActivitiescmd.AggregateNew(aggregate);
  /// </code>
  /// </example>
  public virtual BqlCommand AggregateNew(System.Type newAggregate)
  {
    throw new PXException("The method or operation is not implemented.");
  }

  protected internal static Table GetSQLTable(
    System.Type table,
    PXGraph graph,
    bool fromProjection = false,
    BqlCommandInfo info = null,
    Query mainQuery = null)
  {
    if (table == (System.Type) null || table == typeof (object))
      throw new PXArgumentException(nameof (table), "The argument cannot be null.");
    if (!typeof (IBqlTable).IsAssignableFrom(table))
      return (Table) new SimpleTable(table.Name);
    PXCache cach = graph.Caches[table];
    if (cach == null)
      throw new PXArgumentException(nameof (table), "The argument is out of range.");
    if (cach.BqlSelect != null)
      return (Table) BqlCommand.GetSubQuery(cach, table.Name, table, graph, fromProjection, info, mainQuery);
    System.Type itemType = cach.GetItemType();
    System.Type table1 = itemType == table || !itemType.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) itemType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive ? table : itemType;
    string tableName = BqlCommand.GetTableName(table1);
    TableChangingScope.AddUnchangedRealName(tableName);
    return (Table) new SimpleTable(tableName, table1.Name);
  }

  protected static Query GetSubQuery(
    PXCache cache,
    string alias,
    System.Type table,
    PXGraph graph,
    bool fromProjection = false,
    BqlCommandInfo info = null,
    Query mainQuery = null)
  {
    BqlCommand bqlSelect = cache.BqlSelect;
    using (OptimizedExportScope.CreateChildScopeIfScoped())
    {
      BqlCommand.Selection selection = new BqlCommand.Selection()
      {
        FromProjection = true,
        ProjectionType = table,
        UseColumnAliases = info != null && info.UseColumnAliases
      };
      TableChangingScope.InsertIsNewIfNeeded(cache.BqlTable, graph.Caches[cache.BqlTable], selection);
      TableChangingScope.SetCurrentLevelTable(alias, bqlSelect);
      PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(cache.BqlTable, graph.Caches[cache.BqlTable], selection);
      PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(cache.BqlTable, graph.Caches[cache.BqlTable]);
      cache.BqlSelect = (BqlCommand) null;
      selection._Command = bqlSelect;
      BqlCommandInfo info1 = new BqlCommandInfo();
      if (info?.Parameters != null)
        info1.Parameters = info.Parameters;
      info1.Fields = (List<System.Type>) null;
      info1.SortColumns = (List<IBqlSortColumn>) null;
      List<System.Type> tables = info1.Tables;
      Query queryInternal = bqlSelect.GetQueryInternal(graph, info1, selection);
      if (!(queryInternal is XMLPathQuery) && !(queryInternal is JoinedAttrQuery) && queryInternal.GetLimit() == 0 && queryInternal.GetOffset() == 0)
        queryInternal.GetOrder()?.Clear();
      if (!cache.BypassCalced)
        cache.BqlSelect = bqlSelect;
      queryInternal.SetSelection(BqlCommand.getSQLSelection(table, cache, selection, tables, fromProjection, mainQuery));
      if (cache.BypassCalced)
        cache.BqlSelect = bqlSelect;
      queryInternal.EnforceSelectionAliases();
      queryInternal.Alias = alias;
      cache.BqlSelect = bqlSelect;
      TableChangingScope.RemoveCurrentLevelTable(alias);
      return queryInternal;
    }
  }

  private static List<SQLExpression> getSQLSelection(
    System.Type table,
    PXCache cache,
    BqlCommand.Selection selection,
    List<System.Type> tables,
    bool fromProjection = false,
    Query mainQuery = null)
  {
    fromProjection = fromProjection && cache.SingleExtended;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ValidateColumnAliasesVisitor visitor = new ValidateColumnAliasesVisitor(tables.Select<System.Type, string>(BqlCommand.\u003C\u003EO.\u003C0\u003E__GetTableName ?? (BqlCommand.\u003C\u003EO.\u003C0\u003E__GetTableName = new Func<System.Type, string>(BqlCommand.GetTableName))));
    Dictionary<string, SQLExpression> dictionary = new Dictionary<string, SQLExpression>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    for (int index = 0; index < cache.Fields.Count; ++index)
    {
      string field = cache.Fields[index];
      SQLExpression foundField = BqlCommand.findFieldInSQLSelection(table, cache, selection.ColExprs, tables, field);
      if (foundField != null)
      {
        if (foundField.IsNullExpression())
          foundField = TableChangingScope.FindFieldInProjection(table, cache, selection.ColExprs, tables, field, foundField);
        if (!fromProjection || foundField is Column || foundField.Accept<bool>((ISQLExpressionVisitor<bool>) visitor))
        {
          foundField.SetAlias(selection.UseColumnAliases ? BqlCommand.GetColumnAlias(field, table) : field);
          if (dictionary.ContainsKey(foundField.Alias()))
            foundField.SetAlias($"{foundField.Alias()}{index}");
          dictionary.Add(foundField.Alias(), foundField);
          if (foundField.IsNullExpression() && mainQuery != null)
          {
            List<SQLExpression> list = mainQuery.GetSelection().Where<SQLExpression>((Func<SQLExpression, bool>) (s => s.IsAggregate() && s.UnwrapAggregate() is Column column && column.Name.OrdinalEquals(field) && column.Table().AliasOrName().OrdinalEquals(table.Name))).ToList<SQLExpression>();
            if (list.Count == 1)
              mainQuery.ReplaceFirstInSelection(list.First<SQLExpression>(), foundField.Duplicate().SetAlias(field));
          }
        }
      }
    }
    return dictionary.Values.ToList<SQLExpression>();
  }

  private static bool IsFieldMatchesToColumn(
    PXCommandPreparingEventArgs.FieldDescription descr,
    SQLExpression colExpr,
    string field)
  {
    return object.Equals((object) descr.Expr, (object) colExpr) || colExpr.IsAggregate() && object.Equals((object) descr.Expr, (object) colExpr.RExpr()) || descr.Expr is Column expr && expr.Table() is SimpleTable simpleTable && colExpr is Column column && simpleTable.Equals(column.Table() as SimpleTable) && string.Equals(field, column.Name, StringComparison.OrdinalIgnoreCase);
  }

  private static SQLExpression findFieldInSQLSelection(
    System.Type table,
    PXCache cache,
    List<SQLExpression> columnExprs,
    List<System.Type> tables,
    string field)
  {
    System.Type type1 = (System.Type) null;
    Stack<System.Type> source = new Stack<System.Type>();
    PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
    System.Type type2;
    do
    {
      PXDBOperation operation1 = PXDBOperation.Select;
      if (cache.BqlSelect is IBqlAggregate || cache.BqlSelect is BqlCommandDecorator bqlSelect && bqlSelect.Unwrap() is IBqlAggregate)
        operation1 |= PXDBOperation.GroupBy;
      PXCommandPreparingEventArgs.FieldDescription description2;
      cache.RaiseCommandPreparing(field, (object) null, (object) null, operation1, type1, out description2);
      if (description2?.Expr == null)
        return (SQLExpression) null;
      type2 = description2.BqlTable;
      bool flag1 = false;
      bool flag2 = OptimizedExportScope.IsScoped && BqlCommand.IsDbCalced(description2);
      if (flag2)
      {
        flag1 = OptimizedExportScope.HasExpression(cache, field);
        if (flag1)
        {
          System.Type type3 = type1;
          if ((object) type3 == null)
            type3 = cache.GetItemType();
          type2 = type3;
          if (tables.Contains(type2))
            return OptimizedExportScope.GetProjectionExpression(cache, description2, field, type2);
        }
        if (description1 == null)
          description1 = description2;
      }
      SQLExpression.Operation operation2 = SQLExpression.Operation.NONE;
      if (!flag1)
      {
        for (int index = 0; index < columnExprs.Count; ++index)
        {
          SQLExpression columnExpr = columnExprs[index];
          string a = columnExpr.Alias();
          columnExpr.SetAlias((string) null);
          try
          {
            if (BqlCommand.IsFieldMatchesToColumn(description2, columnExpr, field))
            {
              columnExprs.RemoveAt(index);
              return flag2 ? BqlCommand.CheckIfAggregateThenWrap(columnExpr.Oper(), OptimizedExportScope.GetProjectionExpression(cache, description2, field, type2)) : columnExpr.Duplicate();
            }
            if (columnExpr.IsAggregate())
            {
              if (columnExpr.RExpr() is Column column)
              {
                if (column.Name.Equals(field))
                  operation2 = columnExpr.Oper();
              }
            }
          }
          finally
          {
            columnExpr.SetAlias(a);
          }
        }
      }
      if (type2 != (System.Type) null && type2.BaseType.IsGenericType && typeof (PXCacheExtension).IsAssignableFrom(type2))
      {
        System.Type[] genericArguments = type2.BaseType.GetGenericArguments();
        type2 = genericArguments[genericArguments.Length - 1];
      }
      if (type2 == table && BqlCommand.IsDbCalced(description2) && !flag1)
        return flag2 ? BqlCommand.CheckIfAggregateThenWrap(operation2, OptimizedExportScope.GetProjectionExpression(cache, description2, field, type2)) : BqlCommand.CheckIfAggregateThenWrap(operation2, description2.Expr);
      if (BqlCommand.IsDbCalced(description2) && description2.IsForcedSubQuery)
        return description2.Expr;
      if (type1 == (System.Type) null)
      {
        if (type2 == (System.Type) null || type2.IsAssignableFrom(table))
        {
          type1 = table;
        }
        else
        {
          type1 = type2;
          System.Type type4 = (System.Type) null;
          foreach (System.Type table1 in tables)
          {
            if (table1 == type1 || table1.IsAssignableFrom(type1))
            {
              source.Clear();
              break;
            }
            if (type1.IsAssignableFrom(table1) && (type4 == (System.Type) null || !type4.IsAssignableFrom(table1)))
            {
              type4 = table1;
              source.Push(type4);
            }
          }
          if (source.Any<System.Type>())
            type1 = source.Pop();
        }
      }
      else
        type1 = type1.BaseType;
      if (!typeof (IBqlTable).IsAssignableFrom(type1) && source.Any<System.Type>())
        type1 = source.Pop();
    }
    while (type2 != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(type1));
    return description1 != null ? OptimizedExportScope.GetProjectionExpression(cache, description1, field, type2) : SQLExpression.Null();
  }

  private static SQLExpression CheckIfAggregateThenWrap(
    SQLExpression.Operation operation,
    SQLExpression expression)
  {
    return SQLExpression.IsAggregate(operation) ? SQLExpression.Aggregate(operation, expression) : expression;
  }

  /// <exclude />
  internal static string GetColumnAlias(string fieldName, System.Type dac)
  {
    return $"{dac.Name}_{fieldName}";
  }

  private static string GetTableNameWithoutCache(System.Type table)
  {
    while (typeof (IBqlTable).IsAssignableFrom(table.BaseType) && !table.IsDefined(typeof (PXTableAttribute), false) && (!table.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) table.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive))
      table = table.BaseType;
    return table.Name;
  }

  /// <exclude />
  public static string GetTableName(System.Type table)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return BqlCommand._dacToTableNameMap.GetOrAdd(table, BqlCommand.\u003C\u003EO.\u003C1\u003E__GetTableNameWithoutCache ?? (BqlCommand.\u003C\u003EO.\u003C1\u003E__GetTableNameWithoutCache = new Func<System.Type, string>(BqlCommand.GetTableNameWithoutCache)));
  }

  /// <exclude />
  public static SQLExpression GetField(System.Type field, PXCache cache, PXDBOperation operation)
  {
    PXCommandPreparingEventArgs.FieldDescription fieldDescription = BqlCommand.GetFieldDescription(field, (object) null, cache, operation);
    if (fieldDescription == null)
      return (SQLExpression) null;
    return cache.BqlSelect != null ? (SQLExpression) new Column(field.Name, cache.GetItemType()) : fieldDescription.Expr;
  }

  /// <exclude />
  public static string GetFieldName(System.Type field, PXCache cache, PXDBOperation operation)
  {
    PXCommandPreparingEventArgs.FieldDescription fieldDescription = BqlCommand.GetFieldDescription(field, (object) null, cache, operation);
    if (fieldDescription == null)
      return (string) null;
    return cache.BqlSelect != null ? $"{cache.GetItemType().Name}.{field.Name}" : fieldDescription.Expr.SQLQuery(cache.Graph.SqlDialect.GetConnection()).ToString();
  }

  /// <exclude />
  public static string GetFieldName<field>(PXCache cache, PXDBOperation operation) where field : IBqlField
  {
    return BqlCommand.GetFieldName(typeof (field), cache, operation);
  }

  /// <exclude />
  public static object GetValue(System.Type field, object value, PXCache cache, PXDBOperation operation)
  {
    return BqlCommand.GetFieldDescription(field, value, cache, operation)?.DataValue;
  }

  /// <exclude />
  public static object GetValue<field>(object value, PXCache cache, PXDBOperation operation) where field : IBqlField
  {
    return BqlCommand.GetValue(typeof (field), value, cache, operation);
  }

  private static PXCommandPreparingEventArgs.FieldDescription GetFieldDescription(
    System.Type field,
    object value,
    PXCache cache,
    PXDBOperation operation)
  {
    if (field == (System.Type) null)
      throw new ArgumentNullException(nameof (field));
    if (!typeof (IBqlField).IsAssignableFrom(field))
      throw new ArgumentException(field.FullName + " must implement IBqlField interface.");
    PXCommandPreparingEventArgs.FieldDescription description;
    return cache.RaiseCommandPreparing(field.Name, (object) null, value, operation, (System.Type) null, out description) ? description : (PXCommandPreparingEventArgs.FieldDescription) null;
  }

  protected internal static bool IsFieldRestricted(
    PXCache cache,
    BqlCommand.Selection selection,
    string field)
  {
    RestrictedFieldsSet restrictedFields = selection.RestrictedFields;
    return restrictedFields == null || !restrictedFields.Any() || restrictedFields.Contains(new RestrictedField(cache.GetItemType(), field)) || restrictedFields.Contains(new RestrictedField(field));
  }

  protected internal static bool IsFieldSpecial(PXCache cache, string field)
  {
    return field.IndexOf("Attributes", StringComparison.OrdinalIgnoreCase) >= 0 || field.Equals("NoteText", StringComparison.OrdinalIgnoreCase) || field.Equals("NotePopupText", StringComparison.OrdinalIgnoreCase) || field.Equals("NoteFiles", StringComparison.OrdinalIgnoreCase) || BqlCommand.IsFieldRecordStatus(cache, field);
  }

  protected internal static bool IsFieldRecordStatus(PXCache cache, string field)
  {
    string recordStatusFieldName;
    return cache.HasRecordStatusSupport((System.Type) null, out recordStatusFieldName) && field.OrdinalEquals(recordStatusFieldName);
  }

  private static bool IsFieldRestrictedMainKvExt(
    PXCache cache,
    BqlCommand.Selection selection,
    string field)
  {
    return cache.IsKvExtAttribute(field) && !BqlCommand.IsFieldRestricted(cache, selection, field) && selection.RestrictedFields.Any<RestrictedField>((Func<RestrictedField, bool>) (rf => cache.IsKvExtAttribute(rf.Field) && BqlCommand.IsFieldRestricted(cache, selection, rf.Field)));
  }

  internal static Query GetNoteAttributesJoined(
    System.Type attrTableValue,
    System.Type dataTable,
    System.Type dataTableAlias,
    PXDBOperation oper)
  {
    string srcTable = attrTableValue?.DeclaringType.Name ?? PXDatabase.Provider.SqlDialect.GetKvExtTableName(PXCache.GetBqlTable(dataTable).Name);
    string name = attrTableValue?.Name;
    string valCol = attrTableValue != (System.Type) null ? "ExtFieldID" : "FieldName";
    string str = attrTableValue != (System.Type) null ? "NoteID" : "RecordID";
    string b = srcTable;
    using (new TableChangingScope(TableChangingScope.TableChangesContext))
    {
      TableChangingScope.AddUnchangedRealName(srcTable);
      srcTable = (TableChangingScope.GetSQLTable((Func<Table>) (() => (Table) new SimpleTable(srcTable)), srcTable) as SimpleTable).Name;
    }
    System.Type dac = dataTableAlias;
    if ((object) dac == null)
      dac = dataTable;
    SQLExpression r = (SQLExpression) new Column("NoteID", dac);
    if ((oper & PXDBOperation.Option) == PXDBOperation.GroupBy)
      r = r.Max();
    SQLExpression where = SQLExpressionExt.EQ(new Column(str, srcTable), r);
    if (!string.Equals(srcTable, b, StringComparison.OrdinalIgnoreCase))
      where = TableChangingScope.ProcessNoteAttributesWhere(where, srcTable, dataTable, dataTableAlias);
    return (Query) new JoinedAttrQuery(srcTable, name, valCol, str, where);
  }

  protected internal static bool AppendSingleField(
    Query query,
    System.Type field,
    PXGraph graph,
    BqlCommand.Selection selection)
  {
    System.Type itemType = BqlCommand.GetItemType(field);
    PXCache cach = graph.Caches[itemType];
    string name = field.Name;
    PXDataRecordMap.FieldEntry fieldEntry = new PXDataRecordMap.FieldEntry(name, itemType, selection._PositionInResult);
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(name, (object) null, (object) null, PXDBOperation.Select, itemType, out description);
    if (description != null && description.Expr != null)
      BqlCommand.IsDbCalced(description);
    if (description.Expr == null)
      return true;
    if (!selection.Restrict || BqlCommand.IsFieldRestricted(cach, selection, name) || BqlCommand.IsFieldSpecial(cach, name) || BqlCommand.IsFieldRestrictedMainKvExt(cach, selection, name))
    {
      query.GetSelection().Add(description.Expr);
      selection.AddExpr(name, description.Expr);
      fieldEntry.PositionInQuery = selection._PositionInQuery;
      ++selection._PositionInQuery;
    }
    if (selection.Restrict)
      selection._Command.RecordMapEntries.Add(fieldEntry);
    ++selection._PositionInResult;
    return true;
  }

  protected internal static bool AppendFields(
    Query query,
    System.Type table,
    PXGraph graph,
    BqlCommand.Selection selection,
    System.Type outerTable = null,
    bool mainTableOnly = false,
    Func<string> getColumnAlias = null)
  {
    if (selection == null)
      return true;
    PXCacheCollection caches = graph.Caches;
    System.Type key = outerTable;
    if ((object) key == null)
      key = table;
    PXCache cache = caches[key];
    TableChangingScope.InsertIsNewIfNeeded(table, cache, selection);
    PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(table, cache, selection);
    PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(table, cache);
    System.Type bqlTable = outerTable == (System.Type) null ? table : (System.Type) null;
    TableHeader tableStructure = mainTableOnly ? PXDatabase.GetTableStructure(table.Name) : (TableHeader) null;
    foreach (string field in (List<string>) cache.Fields)
      BqlCommand.AppendField(query, field, cache, table, bqlTable, tableStructure, selection, mainTableOnly, getColumnAlias);
    return true;
  }

  internal static void AppendField(
    Query query,
    string field,
    PXCache cache,
    System.Type table,
    System.Type bqlTable,
    TableHeader tableStructure,
    BqlCommand.Selection selection,
    bool mainTableOnly,
    Func<string> getColumnAlias)
  {
    PXDataRecordMap.FieldEntry fieldEntry = new PXDataRecordMap.FieldEntry(field, table, selection._PositionInResult);
    PXCommandPreparingEventArgs.FieldDescription description;
    cache.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, bqlTable, out description);
    if (description?.Expr == null)
      return;
    if (!selection.Restrict || BqlCommand.IsFieldRestricted(cache, selection, field) || BqlCommand.IsFieldSpecial(cache, field) || BqlCommand.IsFieldRestrictedMainKvExt(cache, selection, field))
    {
      bool flag = BqlCommand.IsDbCalced(description);
      SQLExpression val;
      if (cache.BqlSelect == null || cache.BypassCalced & flag)
        val = description.Expr;
      else if (cache.BqlSelect != null & mainTableOnly && description.Expr is Column expr && !expr.Name.OrdinalEquals(field) && description.BqlTable == cache.BqlTable)
      {
        val = description.Expr;
        val.SetAlias(field);
      }
      else
        val = (SQLExpression) new Column(field, (Table) new SimpleTable(table), description.Expr.GetDBTypeOrDefault());
      if (mainTableOnly && description != null && (description.BqlTable != table || description.BqlTable == table && cache.BypassCalced | flag) && tableStructure != null && tableStructure.Columns.All<TableColumn>((Func<TableColumn, bool>) (x => !((TableEntityBase) x).Name.OrdinalEquals(field))))
        val = SQLExpression.Null();
      if (selection.UseColumnAliases)
        val.SetAlias((getColumnAlias != null ? getColumnAlias() : (string) null) ?? BqlCommand.GetColumnAlias(field, table));
      query.GetSelection().Add(val);
      selection.AddExpr(field, val);
      fieldEntry.PositionInQuery = selection._PositionInQuery;
      ++selection._PositionInQuery;
    }
    if (selection.Restrict)
      selection._Command.RecordMapEntries.Add(fieldEntry);
    ++selection._PositionInResult;
  }

  protected internal static bool IsDbCalced(
    PXCommandPreparingEventArgs.FieldDescription description)
  {
    return !(description.Expr is Column);
  }

  protected internal static void AppendAggregatedCalculated(
    Query query,
    PXGraph graph,
    BqlCommand.Selection selection,
    IBqlFunction[] functions,
    BqlCommandInfo info)
  {
    if (selection == null || functions == null)
      return;
    SQLExpression exp = (SQLExpression) null;
    foreach (IBqlOperandFunction bqlOperandFunction in functions.OfType<IBqlOperandFunction>())
    {
      System.Type field = bqlOperandFunction.GetField();
      if (typeof (IBqlAggregateOperand).IsAssignableFrom(field) && typeof (IBqlCreator).IsAssignableFrom(field))
      {
        ((IBqlCreator) null).createOperand(field).AppendExpression(ref exp, graph, info, selection);
        if (!(exp is Column) && exp.GetDBTypeOrDefault() == PXDbType.Decimal)
          exp = exp.AddCastToIfNeeded(typeof (Decimal), 0, 0);
        exp = !(bqlOperandFunction.GetFunction() == "SUM") ? SQLExpression.None() : exp?.Sum();
        query.GetSelection().Add(exp);
        selection.AddExpr(exp?.ToString(), exp);
        ++selection._PositionInQuery;
        ++selection._PositionInResult;
      }
    }
  }

  protected internal static bool AppendAggregatedFields(
    Query query,
    System.Type table,
    PXGraph graph,
    BqlCommand.Selection selection,
    IBqlFunction[] functions,
    bool mainTableOnly = false)
  {
    if (selection == null)
      return true;
    PXCache cach = graph.Caches[table];
    PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(table, cach, selection);
    PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(table, cach);
    TableHeader tableStructure = mainTableOnly ? PXDatabase.GetTableStructure(table.Name) : (TableHeader) null;
    foreach (string field1 in (List<string>) cach.Fields)
    {
      string field = field1;
      PXDataRecordMap.FieldEntry fieldEntry = new PXDataRecordMap.FieldEntry(field, table, selection._PositionInResult);
      SQLExpression exp = (SQLExpression) null;
      string str = "MAX";
      IBqlFunction bqlFunction = (IBqlFunction) null;
      foreach (IBqlFunction function in functions)
      {
        System.Type field2 = function.GetField();
        if (field2 != (System.Type) null && string.Equals(field, field2.Name, StringComparison.OrdinalIgnoreCase) && (BqlCommand.GetItemType(field2) == table || table.IsSubclassOf(BqlCommand.GetItemType(field2)) && !typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(field2))))
        {
          str = function.GetFunction();
          if (str != string.Empty)
          {
            bqlFunction = function;
            break;
          }
          BqlCommandInfo info = new BqlCommandInfo(false)
          {
            Parameters = new List<IBqlParameter>(),
            Tables = new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
            {
              table
            })
          };
          function.AppendExpression(ref exp, graph, info, new BqlCommand.Selection());
          if (exp != null)
          {
            exp = SQLExpression.GetFirstInSequence(exp);
            break;
          }
          break;
        }
      }
      PXDBOperation operation = str == string.Empty ? PXDBOperation.Select : PXDBOperation.GroupBy;
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(field, (object) null, (object) null, operation, table, out description);
      if (description?.Expr != null)
      {
        SQLExpression sqlExpression1;
        if (cach.BqlSelect != null && (!(cach.BqlSelect is IPXExtensibleTableAttribute bqlSelect) || !bqlSelect.IsSingleTableExtension || BqlCommand.IsColumnOrJoinedAttrQuery(description.Expr)))
        {
          SQLExpression expr1 = description.Expr;
          if ((expr1 != null ? (expr1.Oper() == SQLExpression.Operation.NULL ? 1 : 0) : 0) == 0)
          {
            sqlExpression1 = (SQLExpression) new Column(field, table, description.Expr is Column expr2 ? expr2.GetDBType() : PXDbType.Unspecified);
            goto label_17;
          }
        }
        sqlExpression1 = description.Expr.Duplicate();
label_17:
        SQLExpression sqlExpression2 = sqlExpression1;
        SQLExpression sqlExpression3 = sqlExpression2.Duplicate();
        SQLExpression sqlExpression4 = exp?.Duplicate();
        if (selection.UseColumnAliases)
        {
          string columnAlias = BqlCommand.GetColumnAlias(field, table);
          sqlExpression2?.SetAlias(columnAlias);
          exp?.SetAlias(columnAlias);
        }
        if (!selection.Restrict || BqlCommand.IsFieldRestricted(cach, selection, field) || BqlCommand.IsFieldSpecial(cach, field) || BqlCommand.IsFieldRestrictedMainKvExt(cach, selection, field))
        {
          if (mainTableOnly && description != null && (description.BqlTable != table || description.BqlTable == table && (cach.BypassCalced || BqlCommand.IsDbCalced(description))) && tableStructure != null && tableStructure.Columns.All<TableColumn>((Func<TableColumn, bool>) (x => !((TableEntityBase) x).Name.OrdinalEquals(field))))
            sqlExpression2 = (SQLExpression) null;
          if (sqlExpression2 != null && exp != null)
          {
            query.GetSelection().Add(exp);
            selection.AddExpr(sqlExpression4.ToString(), exp);
          }
          else if (sqlExpression2 != null)
          {
            if (sqlExpression2.Oper() == SQLExpression.Operation.NULL)
            {
              query.GetSelection().Add(sqlExpression2);
              selection.AddExpr("(NULL)", sqlExpression2);
            }
            else
            {
              SQLExpression val;
              if (sqlExpression2.IsNotAggregatedJoinedAttrQuery())
                val = sqlExpression2;
              else if (str != string.Empty && description.IsForcedSubQuery && sqlExpression2.Oper() == SQLExpression.Operation.SUB_QUERY)
              {
                val = !BqlCommand.OrderedSubQueryExpressionAllWhereInGroupBy(sqlExpression2 as SubQuery, table, graph, functions) ? SQLExpression.Null() : sqlExpression2;
              }
              else
              {
                switch (str)
                {
                  case "MAX":
                    val = sqlExpression2.Max();
                    break;
                  case "MIN":
                    val = sqlExpression2.Min();
                    break;
                  case "AVG":
                    val = sqlExpression2.Avg();
                    break;
                  case "SUM":
                    val = sqlExpression2.Sum();
                    break;
                  case "COUNT":
                    val = SQLExpression.Count(sqlExpression2);
                    break;
                  case "CONCAT":
                    val = (SQLExpression) new SQLAggConcat(((IAggConcat) bqlFunction).GetSeparator(), sqlExpression2);
                    break;
                  default:
                    val = SQLExpression.None();
                    break;
                }
              }
              if (selection.UseColumnAliases)
                val.SetAlias(sqlExpression2.Alias());
              query.GetSelection().Add(val);
              selection.AddExpr(sqlExpression3.ToString(), val);
            }
          }
          else
          {
            SQLExpression val = SQLExpression.Null();
            if (selection.UseColumnAliases)
              val.SetAlias(BqlCommand.GetColumnAlias(field, table));
            query.GetSelection().Add(val);
            cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, table, out description);
            if (description?.Expr != null && description.Expr.Oper() != SQLExpression.Operation.NULL)
              selection.AddExpr(description.Expr.ToString(), val);
            else
              selection.AddExpr(val);
          }
          fieldEntry.PositionInQuery = selection._PositionInQuery;
          ++selection._PositionInQuery;
        }
        if (selection.Restrict)
          selection._Command.RecordMapEntries.Add(fieldEntry);
        ++selection._PositionInResult;
      }
    }
    return true;
  }

  private static bool OrderedSubQueryExpressionAllWhereInGroupBy(
    SubQuery subQuery,
    System.Type table,
    PXGraph graph,
    IBqlFunction[] functions)
  {
    if (subQuery == null)
      return false;
    HashSet<string> tables = subQuery.Query().GetAllTables().Select<Table, string>((Func<Table, string>) (t => t.AliasOrName())).Where<string>((Func<string, bool>) (t => !string.IsNullOrEmpty(t))).ToHashSet<string>();
    List<Column> source = new List<Column>();
    List<Column> columnList = source;
    SQLExpression where = subQuery.Query().GetWhere();
    object collection = (where != null ? (object) where.GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (c => !tables.Contains(c.Table().AliasOrName()))).Distinct<Column>() : (object) null) ?? (object) Array.Empty<Column>();
    columnList.AddRange((IEnumerable<Column>) collection);
    if (functions.Length < source.Count)
      return false;
    foreach (IBqlFunction function in functions)
    {
      SQLExpression exp = (SQLExpression) null;
      BqlCommandInfo info = new BqlCommandInfo(false)
      {
        Parameters = new List<IBqlParameter>(),
        Tables = new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
        {
          table
        })
      };
      function.AppendExpression(ref exp, graph, info, new BqlCommand.Selection());
      if (exp != null)
        exp = SQLExpression.GetFirstInSequence(exp);
      source.Remove(exp as Column);
    }
    return !source.Any<Column>();
  }

  private static bool IsColumnOrJoinedAttrQuery(SQLExpression expr)
  {
    return expr is Column || expr.IsNotAggregatedJoinedAttrQuery();
  }

  /// <exclude />
  public static IList<IBqlUnary> SplitPredicateIntoConjunctions(IBqlUnary whereClause)
  {
    if (whereClause == null)
      return BqlCommand.EmptyBqlUnaryList();
    List<IBqlUnary> bqlUnaryList = new List<IBqlUnary>();
    Stack<IBqlUnary> bqlUnaryStack = new Stack<IBqlUnary>();
    IBqlUnary whereClause1 = whereClause;
    bool flag = true;
    while (true)
    {
      for (; whereClause1 != null; whereClause1 = bqlPredicateChain.GetContainedPredicate())
      {
        if (!(whereClause1 is IBqlPredicateChain bqlPredicateChain))
        {
          bqlUnaryList.Add(whereClause1);
          break;
        }
        if (bqlPredicateChain.UseParenthesis() && !flag)
        {
          bqlUnaryList.AddRange((IEnumerable<IBqlUnary>) BqlCommand.SplitPredicateIntoConjunctions(whereClause1));
          break;
        }
        IBqlBinary nextPredicate = bqlPredicateChain.GetNextPredicate();
        if (nextPredicate != null)
        {
          if (!nextPredicate.IsConjunction())
            return (IList<IBqlUnary>) new List<IBqlUnary>()
            {
              whereClause
            };
          bqlUnaryStack.Push(nextPredicate.GetUnary());
        }
        if (bqlPredicateChain.ContainsOperandWithComparison())
        {
          bqlUnaryList.Add(bqlPredicateChain.GetContainedPredicate());
          break;
        }
        flag = false;
      }
      flag = false;
      if (bqlUnaryStack.Count != 0)
        whereClause1 = bqlUnaryStack.Pop();
      else
        break;
    }
    return (IList<IBqlUnary>) bqlUnaryList;
  }

  public static List<BqlCommand.BqlBinaryPairFound> GetBinaryPairs(
    IEnumerable<IBqlUnary> predicates,
    bool demandEquality = true)
  {
    List<BqlCommand.BqlBinaryPairFound> binaryPairs = new List<BqlCommand.BqlBinaryPairFound>();
    int num = -1;
    foreach (IBqlUnary predicate in predicates)
    {
      ++num;
      System.Type type1 = predicate.GetType();
      if (type1.IsGenericType && (!(type1.GetGenericTypeDefinition() != typeof (Where<,>)) || !(type1.GetGenericTypeDefinition() != typeof (WhereNp<,>))))
      {
        System.Type[] genericArguments = type1.GetGenericArguments();
        System.Type type2 = genericArguments[0];
        System.Type type3 = genericArguments[1];
        if (type3.IsGenericType)
        {
          System.Type genericTypeDefinition = type3.GetGenericTypeDefinition();
          if ((!demandEquality || !(genericTypeDefinition != typeof (Equal<>))) && (!(genericTypeDefinition != typeof (Greater<>)) || !(genericTypeDefinition != typeof (GreaterEqual<>)) || !(genericTypeDefinition != typeof (NotEqual<>)) || !(genericTypeDefinition != typeof (NotIn<>)) || !(genericTypeDefinition != typeof (Less<>)) || !(genericTypeDefinition != typeof (LessEqual<>)) || !(genericTypeDefinition != typeof (Equal<>)) || !(genericTypeDefinition != typeof (In<>))))
          {
            System.Type genericArgument = type3.GetGenericArguments()[0];
            binaryPairs.Add(new BqlCommand.BqlBinaryPairFound()
            {
              operand1 = type2,
              operand2 = genericArgument,
              index = num,
              @operator = genericTypeDefinition
            });
          }
        }
      }
    }
    return binaryPairs;
  }

  public static System.Type[] DecomposeToCurrentOptionalRequireds(System.Type pred)
  {
    System.Type[] array = BqlCommand.Decompose(pred);
    for (int index = 0; index < array.Length; ++index)
    {
      System.Type type = array[index];
      if (type == typeof (Required<>) || type == typeof (Current<>) || type == typeof (Current2<>) || type == typeof (Optional<>) || type == typeof (Optional2<>))
      {
        array[index] = BqlCommand.Compose(type, array[index + 1]);
        int length = array.Length - (index + 1) - 1;
        if (length > 0)
          Array.Copy((Array) array, index + 2, (Array) array, index + 1, length);
        Array.Resize<System.Type>(ref array, index + length + 1);
      }
    }
    return array;
  }

  protected static IEnumerable<IBqlJoin> enumerateJoins(IBqlJoin firstJoin)
  {
    for (IBqlJoin nextJoin = firstJoin; nextJoin != null; nextJoin = nextJoin.getNextJoin())
      yield return nextJoin;
  }

  /// <exclude />
  public static IEnumerable<IBqlUnary> GetTransitionedPredicates(
    IEnumerable<IBqlUnary> firstSet,
    IEnumerable<IBqlUnary> secondSet)
  {
    List<BqlCommand.BqlBinaryPairFound> equalPairs1 = BqlCommand.GetBinaryPairs(firstSet, false);
    List<BqlCommand.BqlBinaryPairFound> equalPairs2 = BqlCommand.GetBinaryPairs(secondSet, false);
    List<IBqlUnary> bqlUnaryList = new List<IBqlUnary>();
    for (int i = 0; i < equalPairs1.Count; ++i)
    {
      for (int j = 0; j < equalPairs2.Count; ++j)
      {
        System.Type transitionedPredicate = BqlCommand.getTransitionedPredicate(equalPairs1[i], equalPairs2[j]);
        if (transitionedPredicate != (System.Type) null)
          yield return Activator.CreateInstance(transitionedPredicate) as IBqlUnary;
      }
    }
  }

  private static System.Type getTransitionedPredicate(
    BqlCommand.BqlBinaryPairFound q1,
    BqlCommand.BqlBinaryPairFound q2)
  {
    bool flag = q1.@operator == typeof (Equal<>);
    if (!(q2.@operator == typeof (Equal<>)) && !flag)
      return (System.Type) null;
    if (q1.operand1 == q2.operand1)
      return !flag ? q1.replaceOperand(q2.operand2) : q2.replaceOperand(q1.operand2);
    if (q1.operand1 == q2.operand2)
      return !flag ? q1.replaceOperand(q2.operand1) : q2.replaceOperand(o2: q1.operand2);
    if (q1.operand2 == q2.operand1)
      return !flag ? q1.replaceOperand(o2: q2.operand2) : q2.replaceOperand(q1.operand1);
    if (!(q1.operand2 == q2.operand2))
      return (System.Type) null;
    return !flag ? q1.replaceOperand(o2: q2.operand1) : q2.replaceOperand(o2: q1.operand1);
  }

  /// <summary>Obtains the DAC type from a field type.</summary>
  /// <param name="field">A BQL field.</param>
  /// <returns>The method returns a DAC type. </returns>
  public static System.Type GetItemType(System.Type field)
  {
    if (field == (System.Type) null)
      return (System.Type) null;
    System.Type declaringType = field.DeclaringType;
    return declaringType == (System.Type) null ? (System.Type) null : BqlCommand.GetItemTypeFromExtension(declaringType, field);
  }

  private static System.Type GetItemTypeFromExtension(System.Type tableType, System.Type fieldType)
  {
    if (!typeof (PXMappedCacheExtension).IsAssignableFrom(tableType) || !tableType.IsGenericTypeDefinition)
      return BqlCommand.GetItemTypeFromExtension(tableType);
    int count = ((IEnumerable<System.Type>) ((TypeInfo) tableType).GenericTypeParameters).Count<System.Type>();
    return tableType.MakeGenericType(((IEnumerable<System.Type>) fieldType.GetGenericArguments()).Take<System.Type>(count).ToArray<System.Type>());
  }

  public static System.Type GetItemTypeFromExtension(System.Type type)
  {
    if (!typeof (PXCacheExtension).IsAssignableFrom(type) || !type.BaseType.IsGenericType || typeof (PXMappedCacheExtension).IsAssignableFrom(type))
      return type;
    System.Type[] genericArguments = type.BaseType.GetGenericArguments();
    return genericArguments[genericArguments.Length - 1];
  }

  /// <summary>Obtains the DAC type from a generic field type.</summary>
  /// <typeparam name="TField">A generic type of a BQL field.</typeparam>
  /// <returns>The method returns a DAC type. </returns>
  public static System.Type GetItemType<TField>() where TField : IBqlField
  {
    return BqlCommand.GetItemType(typeof (TField));
  }

  public static SQLExpression GetSingleExpression(
    System.Type field,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection,
    BqlCommand.FieldPlace place)
  {
    if (graph == null)
      return (SQLExpression) null;
    System.Type tableCandidate = BqlCommand.GetItemType(field);
    bool flag1 = false;
    bool flag2 = false;
    if (graph.Caches.ContainsKey(tableCandidate))
    {
      PXCache cach = graph.Caches[tableCandidate];
      flag1 = cach.BqlSelect != null;
      flag2 = cach.BypassCalced;
    }
    // ISSUE: explicit non-virtual call
    System.Type table = tables == null || !__nonvirtual (tables.Contains(field.DeclaringType)) ? BqlCommand.FindRealTableForType(tables, tableCandidate, selection != null && selection.IsNestedView, place == BqlCommand.FieldPlace.OrderBy && typeof (PXCacheExtension).IsAssignableFrom(field.DeclaringType)) : field.DeclaringType;
    PXDBOperation op = PXDBOperation.Select;
    if (op == PXDBOperation.Select)
    {
      if (place == BqlCommand.FieldPlace.Condition)
        op |= PXDBOperation.WhereClause;
      if (place == BqlCommand.FieldPlace.OrderBy)
        op |= PXDBOperation.OrderByClause;
    }
    bool useColumnAliases = selection != null && selection.UseColumnAliases;
    BqlCommand.ExprUnSelected exprUnSelected = graph.Prototype.Memoize<BqlCommand.ExprUnSelected>((Func<BqlCommand.ExprUnSelected>) (() => BqlCommand.GetExprUnSelected(field, graph, tableCandidate, op, table, useColumnAliases)), (object) field, (object) tableCandidate, (object) op, (object) table, (object) useColumnAliases, (object) graph.GetType(), (object) flag1, (object) flag2);
    if (exprUnSelected.isReturn)
      return (SQLExpression) null;
    SQLExpression singleExpression1 = exprUnSelected.unSelected?.Duplicate();
    if (place == BqlCommand.FieldPlace.OrderBy || place == BqlCommand.FieldPlace.Select)
    {
      SQLExpression singleExpression2 = selection == null ? singleExpression1 : selection.GetExpr(singleExpression1?.ToString());
      if (singleExpression2 != null)
        return singleExpression2;
    }
    return singleExpression1;
  }

  private static BqlCommand.ExprUnSelected GetExprUnSelected(
    System.Type field,
    PXGraph graph,
    System.Type tableCandidate,
    PXDBOperation op,
    System.Type table,
    bool useColumnAlias)
  {
    PXCache cach = graph.Caches[tableCandidate];
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(field.Name, (object) null, (object) null, op, table, out description);
    if (description == null)
      return new BqlCommand.ExprUnSelected()
      {
        isReturn = true
      };
    string field1 = cach.GetField(field);
    SQLExpression sqlExpression1;
    if (cach.BqlSelect != null)
      sqlExpression1 = (SQLExpression) new Column(field1, table, description.Expr.GetDBTypeOrDefault())
      {
        PadSpaced = (description.Expr is Column expr && expr.PadSpaced)
      };
    else
      sqlExpression1 = description.Expr;
    SQLExpression sqlExpression2 = sqlExpression1;
    if (useColumnAlias)
      sqlExpression2.SetAlias(BqlCommand.GetColumnAlias(field1, table));
    return new BqlCommand.ExprUnSelected()
    {
      unSelected = sqlExpression2
    };
  }

  /// <exclude />
  public static System.Type FindRealTableForType(
    List<System.Type> tables,
    System.Type table0,
    bool isNestedView = false,
    bool isOrderByFromCacheExtension = false)
  {
    System.Type c1 = table0;
    if (tables != null && tables.Count > 0)
    {
      if (tables[0].IsSubclassOf(table0))
        c1 = tables[0];
      else if (isNestedView && tables.Count > 1 && tables[1].IsSubclassOf(table0))
        c1 = tables[1];
      else if (!typeof (IBqlTable).IsAssignableFrom(c1))
      {
        System.Type type1 = c1;
        System.Type c2 = (System.Type) null;
        foreach (System.Type table in tables)
        {
          if (type1.IsAssignableFrom(table) && (c2 == (System.Type) null || table.IsAssignableFrom(c2)))
            c2 = table;
        }
        System.Type type2 = c2;
        if ((object) type2 == null)
          type2 = type1;
        c1 = type2;
      }
      else if (BqlCommand.IsNamedSubclassOf(tables[0], table0))
        c1 = tables[0];
      else if (isOrderByFromCacheExtension && tables.Count == 1 && tables[0].IsAssignableFrom(table0))
        c1 = tables[0];
    }
    return c1;
  }

  private static bool IsNamedSubclassOf(System.Type source, System.Type target)
  {
    foreach (MemberInfo realBqlTableType in BqlCommand.GetAllRealBqlTableTypes(source))
    {
      if (realBqlTableType.Name.OrdinalEquals(target.Name))
        return true;
    }
    return false;
  }

  private static IEnumerable<System.Type> GetAllRealBqlTableTypes(System.Type type)
  {
    foreach (System.Type realBqlTableType in BqlCommand.GetAllBaseTypes(type).Reverse<System.Type>().Where<System.Type>((Func<System.Type, bool>) (t => t.GetInterface(typeof (IBqlTable).Name) != (System.Type) null)))
    {
      if (!realBqlTableType.GetType().CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (attribute => false)))
        yield return realBqlTableType;
      else
        break;
    }
  }

  private static IEnumerable<System.Type> GetAllBaseTypes(System.Type type)
  {
    for (; type != (System.Type) null; type = type.BaseType)
      yield return type;
  }

  internal static System.Type Parametrize(System.Type table, System.Type clause)
  {
    if (typeof (IBqlField).IsAssignableFrom(clause) && clause.IsNested && (BqlCommand.GetItemType(clause) == table || table.IsSubclassOf(BqlCommand.GetItemType(clause))))
      return typeof (Current<>).MakeGenericType(clause);
    clause = BqlCommand.UnwrapCustomPredicate(clause);
    if (!clause.IsGenericType || typeof (IDoNotParametrize).IsAssignableFrom(clause))
      return clause;
    if (typeof (IBqlParameter).IsAssignableFrom(clause))
    {
      System.Type genericArgument = clause.GetGenericArguments()[0];
      if (!typeof (IBqlField).IsAssignableFrom(genericArgument))
        return clause;
      return typeof (Required<>).MakeGenericType(genericArgument);
    }
    System.Type[] genericArguments = clause.GetGenericArguments();
    System.Type genericTypeDefinition = clause.GetGenericTypeDefinition();
    System.Type[][] array = ((IEnumerable<System.Type>) genericTypeDefinition.GetGenericArguments()).Select<System.Type, System.Type[]>((Func<System.Type, System.Type[]>) (p => p.GetGenericParameterConstraints())).ToArray<System.Type[]>();
    bool flag = false;
    for (int index = 0; index < genericArguments.Length; ++index)
    {
      System.Type t = BqlCommand.Parametrize(table, genericArguments[index]);
      if (t != genericArguments[index] && ((IEnumerable<System.Type>) array[index]).All<System.Type>((Func<System.Type, bool>) (c => c.IsAssignableFrom(t))))
      {
        genericArguments[index] = t;
        flag = true;
      }
    }
    return !flag ? clause : genericTypeDefinition.MakeGenericType(genericArguments);
  }

  internal static BqlCommand Deparametrize(BqlCommand select, System.Type table)
  {
    select = (select is BqlCommandDecorator commandDecorator ? commandDecorator.Unwrap() : (BqlCommand) null) ?? select;
    List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(select.GetType()));
    bool flag = !(select is IBqlJoinedSelect) || ((IBqlJoinedSelect) select).IsInner;
    int index1 = 1;
    while (index1 < typeList.Count)
    {
      typeList[index1] = BqlCommand.UnwrapCustomPredicate(typeList[index1]);
      flag = flag || typeof (Where<>).IsAssignableFrom(typeList[index1]) || typeof (Where<,>).IsAssignableFrom(typeList[index1]) || typeof (Where<,,>).IsAssignableFrom(typeList[index1]) || typeof (Where2<,>).IsAssignableFrom(typeList[index1]);
      if (typeof (IBqlField).IsAssignableFrom(typeList[index1]) && typeList[index1].IsNested && (BqlCommand.GetItemType(typeList[index1]) == table || table.IsSubclassOf(BqlCommand.GetItemType(typeList[index1]))))
      {
        if (typeof (IBqlParameter).IsAssignableFrom(typeList[index1 - 1]))
        {
          if (index1 + 1 < typeList.Count && typeof (IBqlComparison).IsAssignableFrom(typeList[index1 + 1]))
          {
            if (typeList[index1 + 1].IsGenericType)
            {
              for (int index2 = 0; index2 < typeList[index1 + 1].GetGenericArguments().Length && index1 + 2 < typeList.Count; ++index2)
                typeList.RemoveAt(index1 + 2);
            }
            typeList[index1 - 1] = typeof (True);
            typeList[index1] = flag ? typeof (Equal<>) : typeof (NotEqual<>);
            typeList[index1 + 1] = typeof (True);
            index1 += 2;
          }
          else if (index1 - 3 >= 0 && typeof (IBqlComparison).IsAssignableFrom(typeList[index1 - 2]) && typeof (IBqlField).IsAssignableFrom(typeList[index1 - 3]) && (index1 - 4 == 0 || !typeof (IBqlParameter).IsAssignableFrom(typeList[index1 - 4])))
          {
            typeList[index1 - 3] = typeof (True);
            typeList[index1 - 2] = flag ? typeof (Equal<>) : typeof (NotEqual<>);
            typeList[index1 - 1] = typeof (True);
            typeList.RemoveAt(index1);
          }
          else if (index1 > 1 && typeof (IBqlUnary).IsAssignableFrom(typeList[index1 - 2]) && typeList[index1 - 2].IsGenericType && typeList[index1 - 2].GetGenericArguments().Length == 1)
          {
            typeList[index1 - 2] = flag ? typeof (Where<True, Equal<True>>) : typeof (Where<True, NotEqual<True>>);
            typeList.RemoveAt(index1 - 1);
            typeList.RemoveAt(index1 - 1);
            --index1;
          }
          else if (index1 > 2 && typeof (IBqlUnary).IsAssignableFrom(typeList[index1 - 3]) && typeList[index1 - 3].IsGenericType && typeList[index1 - 3].GetGenericArguments().Length == 2)
          {
            typeList[index1 - 3] = flag ? typeof (Where<True, Equal<True>>) : typeof (Where<True, NotEqual<True>>);
            typeList.RemoveAt(index1 - 2);
            typeList.RemoveAt(index1 - 2);
            typeList.RemoveAt(index1 - 2);
            index1 -= 2;
          }
          else
            ++index1;
        }
        else
          ++index1;
      }
      else
        ++index1;
    }
    try
    {
      return BqlCommand.CreateInstance(typeList.ToArray());
    }
    catch
    {
      return select;
    }
  }

  /// <exclude />
  public static BqlCommand CreateInstance(params System.Type[] types)
  {
    return types.Length != 0 && typeof (BqlCommand).IsAssignableFrom(types[0]) ? Activator.CreateInstance(BqlCommand.Compose(types)) as BqlCommand : throw new PXArgumentException(nameof (types), "The first type has to be convertible to the Bqlcommand type.");
  }

  /// <summary>
  /// Generates a command type from an array of type definitions.
  /// </summary>
  /// <param name="types">An array of type definitions.</param>
  /// <returns>The method returns a BQL command.</returns>
  /// <example>
  /// <code>
  /// Type whereType = BqlCommand.Compose(
  ///   typeof(Where&lt;,,&gt;),
  ///     releasedField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;,,&gt;), prebookedField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;,,&gt;), holdField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;,,&gt;), voidedField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;,,&gt;), rejectedField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;,,&gt;), origModuleField, typeof(Equal&lt;&gt;), typeof(BatchModule.moduleAP),
  ///     typeof(And&lt;,,&gt;), isMigratedRecordField, typeof(Equal&lt;&gt;), typeof(False),
  ///     typeof(And&lt;&gt;), typeof(Not&lt;&gt;), typeof(IsPOLinked&lt;,&gt;), docTypeField, refNbrField);
  /// </code>
  /// </example>
  public static System.Type Compose(params System.Type[] types)
  {
    List<System.Type> types1 = new List<System.Type>((IEnumerable<System.Type>) types);
    System.Type type = BqlCommand.makeGenericType(types1);
    if (types1.Count > 0)
      throw new PXArgumentException(nameof (types), "The parameter length exceeds the allowed value.");
    return type;
  }

  /// <summary>Generates an array of type definitions that compose the specified command.</summary>
  /// <param name="command">A BQL command that should be decomposed.</param>
  /// <example>
  /// <para>The following example decomposes the BQL command that is defined in a <see cref="T:PX.Data.PXRestrictorAttribute" /> attribute
  /// and checks whether a field is included in this command.</para>
  /// <code>
  /// TransactionEntry docgraph = CreateInstance&lt;TransactionEntry&gt;();
  /// 
  /// docgraph.Trans.Cache.Adjust&lt;PXRestrictorAttribute&gt;().For&lt;FATran.assetID&gt;(attr =&gt;
  /// {
  ///   Type[] bql = BqlCommand.Decompose(attr.RestrictingCondition);
  ///   if (bql.Contains(typeof(FixedAssetStatus.disposed)))
  ///   {
  ///     attr.SuppressVerify = true;
  ///   }
  /// });
  /// </code>
  /// </example>
  public static System.Type[] Decompose(System.Type command)
  {
    return BqlCommand.extractGenericTypes(BqlCommandDecorator.Unwrap(command)).ToArray();
  }

  /// <summary>
  /// Returns all DAC fields that are used in the specified BQL command.
  /// </summary>
  /// <param name="command">A BQL command.</param>
  /// <returns>The method returns a collection of DAC fields of the BQL command.</returns>
  public static IEnumerable<System.Type> GetFields(System.Type command)
  {
    return !(command == (System.Type) null) ? ((IEnumerable<System.Type>) BqlCommand.Decompose(command)).Where<System.Type>((Func<System.Type, bool>) (t => typeof (IBqlField).IsAssignableFrom(t))) : throw new ArgumentNullException(nameof (command));
  }

  /// <summary>Appends an additional condition to the specified joined DAC in the specified command.</summary>
  /// <param name="command">A BQL command.</param>
  /// <param name="joinTable">A DAC in a <tt>JOIN</tt> clause of the BQL command.</param>
  /// <param name="condition">A condition.</param>
  public static System.Type AddJoinConditions(System.Type command, System.Type joinTable, System.Type condition)
  {
    List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(command));
    for (int index1 = 1; index1 < typeList.Count; ++index1)
    {
      if (typeList[index1] == joinTable && typeof (IBqlJoin).IsAssignableFrom(typeList[index1 - 1]))
      {
        System.Type[] typeArray = BqlCommand.Decompose(condition);
        System.Type[] collection = new System.Type[typeArray.Length + 1];
        collection[0] = typeof (Where<>);
        typeArray.CopyTo((Array) collection, 1);
        int index2 = index1 + 1;
        System.Type c = typeList[index2];
        if (typeof (On<>).IsAssignableFrom(c))
        {
          int index3 = index2 + BqlCommand.GetFinalTypeLength(typeList.ToArray(), index2 + 1) + 1;
          typeList[index2] = typeof (On<,>);
          typeList.Insert(index3, typeof (And<>));
          typeList.InsertRange(index3 + 1, (IEnumerable<System.Type>) collection);
        }
        if (typeof (On<,>).IsAssignableFrom(c))
        {
          int index4 = index2 + BqlCommand.GetFinalTypeLength(typeList.ToArray(), index2 + 2) + 2;
          typeList[index2] = typeof (On<,,>);
          typeList.Insert(index4, typeof (And<>));
          typeList.InsertRange(index4 + 1, (IEnumerable<System.Type>) collection);
        }
        if (typeof (On<,,>).IsAssignableFrom(c))
        {
          int index5 = index2 + BqlCommand.GetFinalTypeLength(typeList.ToArray(), index2 + 2) + 2;
          typeList.Insert(index5, typeof (And2<,>));
          typeList.InsertRange(index5 + 1, (IEnumerable<System.Type>) collection);
        }
        if (typeof (On2<,>).IsAssignableFrom(c))
        {
          typeList.InsertRange(index2 + 1, (IEnumerable<System.Type>) collection);
          typeList.Insert(index2 + collection.Length + 1, typeof (And2<,>));
        }
        return BqlCommand.Compose(typeList.ToArray());
      }
    }
    return command;
  }

  public static BqlCommand AppendJoin<TJoin>(BqlCommand command) where TJoin : IBqlJoin
  {
    return Activator.CreateInstance(BqlCommand.AppendJoin<TJoin>(command.GetType())) as BqlCommand;
  }

  /// <summary>Appends a <tt>JOIN</tt> clause to an existing command.
  /// The <tt>JOIN</tt> clause is specified as a parameter.</summary>
  /// <param name="command">A BQL command to which the <tt>JOIN</tt> clause is appended.</param>
  /// <param name="join">A <tt>JOIN</tt> clause.</param>
  /// <returns>The method returns the created BQL command.</returns>
  /// <example>
  /// <code>
  /// select = BqlCommand.AppendJoin(select, BqlCommand.Compose(typeof(LeftJoin&lt;Sub&gt;.On&lt;APHistoryByPeriod.subID.IsEqual&lt;Sub.subID&gt;&gt;)));
  /// </code>
  /// </example>
  public static BqlCommand AppendJoin(BqlCommand command, System.Type join)
  {
    return Activator.CreateInstance(BqlCommand.AppendJoin(command.GetType(), join)) as BqlCommand;
  }

  public static System.Type AppendJoin<TJoin>(System.Type command) where TJoin : IBqlJoin
  {
    return BqlCommand.AppendJoin(command, typeof (TJoin));
  }

  /// <summary>Appends a <tt>JOIN</tt> clause to an existing command.
  /// The <tt>JOIN</tt> clause is specified as a parameter.</summary>
  /// <param name="command">A BQL command to which the <tt>JOIN</tt> clause is appended.</param>
  /// <param name="join">A <tt>JOIN</tt> clause.</param>
  /// <returns>The method returns the created BQL command.</returns>
  public static System.Type AppendJoin(System.Type command, System.Type join)
  {
    if (command == (System.Type) null)
      throw new ArgumentNullException(nameof (command));
    if (join == (System.Type) null)
      throw new ArgumentNullException(nameof (join));
    System.Type[] collection = typeof (IBqlJoin).IsAssignableFrom(join) ? BqlCommand.Decompose(join) : throw new ArgumentException($"{join} doesn't implement interface {typeof (IBqlJoin)}");
    List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(command));
    for (int index1 = 0; index1 < typeList.Count; ++index1)
    {
      System.Type c = typeList[index1];
      if (typeof (BqlCommand).IsAssignableFrom(c))
      {
        System.Type type1 = (System.Type) null;
        if (typeof (Select<>).IsAssignableFrom(c))
          type1 = typeof (Select2<,>);
        if (typeof (Select<,>).IsAssignableFrom(c))
          type1 = typeof (Select2<,,>);
        if (typeof (Select<,,>).IsAssignableFrom(c))
          type1 = typeof (Select2<,,,>);
        if (typeof (Select3<,>).IsAssignableFrom(c))
          type1 = typeof (Select3<,,>);
        if (typeof (Select4<,>).IsAssignableFrom(c))
          type1 = typeof (Select5<,,>);
        if (typeof (Select4<,,>).IsAssignableFrom(c))
          type1 = typeof (Select5<,,,>);
        if (typeof (Select4<,,,>).IsAssignableFrom(c))
          type1 = typeof (Select5<,,,,>);
        if (typeof (Search<>).IsAssignableFrom(c))
          type1 = typeof (Search2<,>);
        if (typeof (Search<,>).IsAssignableFrom(c))
          type1 = typeof (Search2<,,>);
        if (typeof (Search<,,>).IsAssignableFrom(c))
          type1 = typeof (Search2<,,,>);
        if (typeof (Search3<,>).IsAssignableFrom(c))
          type1 = typeof (Search3<,,>);
        if (typeof (Search4<,>).IsAssignableFrom(c))
          type1 = typeof (Search5<,,>);
        if (typeof (Search4<,,>).IsAssignableFrom(c))
          type1 = typeof (Search5<,,,>);
        if (typeof (Search4<,,,>).IsAssignableFrom(c))
          type1 = typeof (Search5<,,,,>);
        if (type1 != (System.Type) null)
        {
          typeList.RemoveAt(index1);
          typeList.Insert(index1, type1);
          typeList.InsertRange(index1 + 2, (IEnumerable<System.Type>) collection);
        }
        else if (typeof (Select2<,>).IsAssignableFrom(c) || typeof (Select2<,,>).IsAssignableFrom(c) || typeof (Select2<,,,>).IsAssignableFrom(c) || typeof (Select3<,,>).IsAssignableFrom(c) || typeof (Select5<,,>).IsAssignableFrom(c) || typeof (Select5<,,,>).IsAssignableFrom(c) || typeof (Select5<,,,,>).IsAssignableFrom(c) || typeof (Search2<,>).IsAssignableFrom(c) || typeof (Search2<,,>).IsAssignableFrom(c) || typeof (Search2<,,,>).IsAssignableFrom(c) || typeof (Search3<,,>).IsAssignableFrom(c) || typeof (Search5<,,>).IsAssignableFrom(c) || typeof (Search5<,,,>).IsAssignableFrom(c) || typeof (Search5<,,,,>).IsAssignableFrom(c))
        {
          int index2 = index1 + 2;
          System.Type type2 = BqlCommand.GetAppendableJoin(typeList[index2]);
          int index3 = index2 + 1;
          for (int index4 = index2; index4 < index3; ++index4)
          {
            System.Type join1 = typeList[index4];
            if (join1.IsGenericTypeDefinition)
            {
              index3 += join1.GetGenericArguments().Length;
              System.Type appendableJoin = BqlCommand.GetAppendableJoin(join1);
              if (appendableJoin != (System.Type) null)
              {
                index2 = index4;
                type2 = appendableJoin;
              }
            }
          }
          typeList.RemoveAt(index2);
          typeList.Insert(index2, type2);
          typeList.InsertRange(index3, (IEnumerable<System.Type>) collection);
        }
        return BqlCommand.Compose(typeList.ToArray());
      }
    }
    return command;
  }

  /// <summary>Adds a <tt>JOIN</tt> clause to an existing command.
  /// The <tt>JOIN</tt> clause is specified as a parameter.</summary>
  /// <param name="command">A BQL command to which the <tt>JOIN</tt> clause is added.</param>
  /// <param name="join">A <tt>JOIN</tt> clause.</param>
  /// <returns>The method returns the created BQL command.</returns>
  public static System.Type NewJoin(System.Type command, System.Type join)
  {
    if (command == (System.Type) null)
      throw new ArgumentNullException(nameof (command));
    if (join == (System.Type) null)
      throw new ArgumentNullException(nameof (join));
    System.Type[] collection = typeof (IBqlJoin).IsAssignableFrom(join) ? BqlCommand.Decompose(join) : throw new ArgumentException($"{join} doesn't implement interface {typeof (IBqlJoin)}");
    List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(command));
    for (int index1 = 0; index1 < typeList.Count; ++index1)
    {
      System.Type c = typeList[index1];
      if (typeof (BqlCommand).IsAssignableFrom(c))
      {
        System.Type type1 = (System.Type) null;
        if (typeof (Select<>).IsAssignableFrom(c))
          type1 = typeof (Select2<,>);
        if (typeof (Select<,>).IsAssignableFrom(c))
          type1 = typeof (Select2<,,>);
        if (typeof (Select<,,>).IsAssignableFrom(c))
          type1 = typeof (Select2<,,>);
        if (typeof (Select3<,>).IsAssignableFrom(c))
          type1 = typeof (Select3<,,>);
        if (typeof (Select4<,>).IsAssignableFrom(c))
          type1 = typeof (Select5<,,>);
        if (typeof (Select4<,,>).IsAssignableFrom(c))
          type1 = typeof (Select5<,,,>);
        if (type1 != (System.Type) null)
        {
          typeList.RemoveAt(index1);
          typeList.Insert(index1, type1);
          typeList.InsertRange(index1 + 2, (IEnumerable<System.Type>) collection);
        }
        else if (typeof (Select2<,>).IsAssignableFrom(c) || typeof (Select2<,,>).IsAssignableFrom(c) || typeof (Select2<,,,>).IsAssignableFrom(c) || typeof (Select3<,,>).IsAssignableFrom(c) || typeof (Select5<,,>).IsAssignableFrom(c) || typeof (Select5<,,,>).IsAssignableFrom(c))
        {
          int num1 = index1 + 2;
          int num2 = num1 + 1;
          for (int index2 = num1; index2 < num2; ++index2)
          {
            System.Type type2 = typeList[index2];
            if (type2.IsGenericTypeDefinition)
              num2 += type2.GetGenericArguments().Length;
          }
          typeList.RemoveRange(index1 + 2, num2 - index1 - 2);
          typeList.InsertRange(index1 + 2, (IEnumerable<System.Type>) collection);
        }
        return BqlCommand.Compose(typeList.ToArray());
      }
    }
    return command;
  }

  private static System.Type GetAppendableJoin(System.Type join)
  {
    if (typeof (LeftJoin<,,>).IsAssignableFrom(join) || typeof (InnerJoin<,,>).IsAssignableFrom(join) || typeof (RightJoin<,,>).IsAssignableFrom(join) || typeof (CrossJoin<,>).IsAssignableFrom(join) || typeof (FullJoin<,,>).IsAssignableFrom(join) || typeof (LeftJoinSingleTable<,,>).IsAssignableFrom(join) || typeof (InnerJoinSingleTable<,,>).IsAssignableFrom(join) || typeof (InnerJoinStraight<,,>).IsAssignableFrom(join) || typeof (RightJoinSingleTable<,,>).IsAssignableFrom(join) || typeof (CrossJoinSingleTable<,>).IsAssignableFrom(join) || typeof (FullJoinSingleTable<,,>).IsAssignableFrom(join))
      return join;
    if (typeof (LeftJoin<,>).IsAssignableFrom(join))
      return typeof (LeftJoin<,,>);
    if (typeof (InnerJoin<,>).IsAssignableFrom(join))
      return typeof (InnerJoin<,,>);
    if (typeof (RightJoin<,>).IsAssignableFrom(join))
      return typeof (RightJoin<,,>);
    if (typeof (CrossJoin<>).IsAssignableFrom(join))
      return typeof (CrossJoin<,>);
    if (typeof (FullJoin<,>).IsAssignableFrom(join))
      return typeof (FullJoin<,,>);
    if (typeof (LeftJoinSingleTable<,>).IsAssignableFrom(join))
      return typeof (LeftJoinSingleTable<,,>);
    if (typeof (InnerJoinSingleTable<,>).IsAssignableFrom(join))
      return typeof (InnerJoinSingleTable<,,>);
    if (typeof (InnerJoinStraight<,>).IsAssignableFrom(join))
      return typeof (InnerJoinStraight<,,>);
    if (typeof (RightJoinSingleTable<,>).IsAssignableFrom(join))
      return typeof (RightJoinSingleTable<,,>);
    if (typeof (CrossJoinSingleTable<>).IsAssignableFrom(join))
      return typeof (CrossJoinSingleTable<,>);
    return typeof (FullJoinSingleTable<,>).IsAssignableFrom(join) ? typeof (FullJoinSingleTable<,,>) : (System.Type) null;
  }

  private static int GetFinalTypeLength(System.Type[] typesList, int startIndex)
  {
    int startIndex1 = 1;
    System.Type types = typesList[startIndex];
    if (types.IsGenericTypeDefinition)
    {
      int length = types.GetGenericArguments().Length;
      while (--length >= 0)
        startIndex1 += BqlCommand.GetFinalTypeLength(typesList, startIndex1);
    }
    return startIndex1;
  }

  internal static System.Type UnwrapCustomPredicate(System.Type customPredicate)
  {
    return typeof (IBqlCustomPredicate).IsAssignableFrom(customPredicate) && !customPredicate.ContainsGenericParameters ? ((IBqlCustomPredicate) Activator.CreateInstance(customPredicate)).Original.GetType() : customPredicate;
  }

  /// <exclude />
  public static System.Type MakeGenericType(params System.Type[] types)
  {
    return types != null ? BqlCommand.makeGenericType(new List<System.Type>((IEnumerable<System.Type>) types)) : throw new ArgumentNullException(nameof (types));
  }

  private static System.Type makeGenericType(List<System.Type> types)
  {
    return BqlCommand.MakeGenericType((IList<System.Type>) types, 0);
  }

  internal static System.Type MakeGenericType(IList<System.Type> types, int startIndex)
  {
    return BqlCommand.MakeGenericType(types, startIndex, 0);
  }

  internal static System.Type MakeGenericType(
    IList<System.Type> types,
    int startIndex,
    int recursionLevel)
  {
    if (types.Count == 0)
      throw new PXArgumentException(nameof (types), "The parameter length exceeds the allowed value.");
    if (recursionLevel > WebConfig.MaxRecursionLevel)
      throw new PXInvalidOperationException($"Maximum recursion level {WebConfig.MaxRecursionLevel} reached");
    RuntimeHelpers.EnsureSufficientExecutionStack();
    System.Type type = types[startIndex];
    types.RemoveAt(startIndex);
    if (!type.IsGenericTypeDefinition)
      return type;
    System.Type[] typeArray = new System.Type[type.GetGenericArguments().Length];
    for (int index = 0; index < typeArray.Length; ++index)
      typeArray[index] = BqlCommand.MakeGenericType(types, startIndex, ++recursionLevel);
    return type.MakeGenericType(typeArray);
  }

  private static List<System.Type> extractGenericTypes(System.Type type)
  {
    if (type == (System.Type) null)
      throw new ArgumentNullException(nameof (type));
    if (!type.IsGenericType)
      return new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
      {
        type
      });
    List<System.Type> genericTypes = new List<System.Type>();
    genericTypes.Add(type.GetGenericTypeDefinition());
    foreach (System.Type genericArgument in type.GetGenericArguments())
      genericTypes.AddRange((IEnumerable<System.Type>) BqlCommand.extractGenericTypes(genericArgument));
    return genericTypes;
  }

  /// <exclude />
  public static IEnumerable<System.Type> KillAllWheres(IEnumerable<System.Type> decomposed)
  {
    int skip = 0;
    foreach (System.Type c in decomposed)
    {
      if (skip > 0)
      {
        --skip;
        if (c.IsGenericType)
          skip += c.GetGenericArguments().Length;
      }
      else if (typeof (Where<>).IsAssignableFrom(c) || typeof (Where<,>).IsAssignableFrom(c) || typeof (Where<,,>).IsAssignableFrom(c) || typeof (Where2<,>).IsAssignableFrom(c))
      {
        skip = c.GetGenericArguments().Length;
        yield return typeof (Where<,>);
        yield return typeof (True);
        yield return typeof (Equal<>);
        yield return typeof (True);
      }
      else
        yield return c;
    }
  }

  protected static bool IsInnerJoin<Join>() where Join : IBqlJoin
  {
    System.Type genericTypeDefinition = typeof (Join).GetGenericTypeDefinition();
    return genericTypeDefinition == typeof (InnerJoin<,>) || genericTypeDefinition == typeof (InnerJoin<,,>) || genericTypeDefinition == typeof (InnerJoinSingleTable<,>) || genericTypeDefinition == typeof (InnerJoinSingleTable<,,>);
  }

  /// <exclude />
  public class Selection
  {
    internal Dictionary<string, SQLExpression> _origExprs;
    public readonly List<SQLExpression> ColExprs = new List<SQLExpression>();
    public readonly List<System.Type> GrouppedBy = new List<System.Type>();
    internal int _PositionInQuery;
    internal int _PositionInResult;
    internal bool UseColumnAliases;
    public bool Restrict;
    public bool IsNestedView;
    public bool IsBqlRowSelection;
    public bool FromProjection;
    public System.Type ProjectionType;
    public int ParamCounter;
    /// <exclude />
    public Func<IList<IBqlUnary>> PredicatesFromOuterClause;
    public BqlCommand.Selection.BqlParsingMode BqlMode;
    public Dictionary<string, string> preparedColumnsByAlias;

    internal BqlCommand _Command { get; set; }

    public RestrictedFieldsSet RestrictedFields { get; set; }

    public void AddExpr(string key, SQLExpression val)
    {
      if (this._origExprs == null)
        this._origExprs = new Dictionary<string, SQLExpression>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._origExprs[key] = val.Duplicate();
      this.ColExprs.Add(val);
    }

    public void AddExpr(SQLExpression val) => this.ColExprs.Add(val);

    public SQLExpression GetExpr(string origField)
    {
      SQLExpression sqlExpression = (SQLExpression) null;
      return this._origExprs == null || origField == null || !this._origExprs.TryGetValue(origField, out sqlExpression) ? (SQLExpression) null : sqlExpression;
    }

    [Flags]
    public enum BqlParsingMode : short
    {
      Regular = 0,
      DontAllocateParameters = 256, // 0x0100
      DontFindRealTypeOfTable = 512, // 0x0200
      DiscardOrdersInPxSearch = 1024, // 0x0400
    }
  }

  /// <exclude />
  public sealed class EqualityList : List<System.Type>
  {
    private bool _NonStrict;

    public bool NonStrict
    {
      get => this._NonStrict;
      set => this._NonStrict |= value;
    }
  }

  public struct BqlBinaryPairFound
  {
    public System.Type operand1;
    public System.Type @operator;
    public System.Type operand2;
    public int index;

    public System.Type replaceOperand(System.Type o1 = null, System.Type o2 = null)
    {
      System.Type[] typeArray = new System.Type[4]
      {
        typeof (Where<,>),
        null,
        null,
        null
      };
      System.Type type1 = o1;
      if ((object) type1 == null)
        type1 = this.operand1;
      typeArray[1] = type1;
      typeArray[2] = this.@operator;
      System.Type type2 = o2;
      if ((object) type2 == null)
        type2 = this.operand2;
      typeArray[3] = type2;
      return BqlCommand.Compose(typeArray);
    }
  }

  /// <exclude />
  public enum FieldPlace
  {
    Condition,
    GroupBy,
    Select,
    OrderBy,
  }

  private class ExprUnSelected
  {
    public SQLExpression unSelected;
    public bool isReturn;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.Count`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Counts distinct values of the specified field in a group.
/// Equivalent to SQL function <tt>COUNT DISTINCT</tt>.
/// </summary>
/// <typeparam name="Field">The field whose distinct values are counted.</typeparam>
/// <remarks>
/// You access the calculated value through the <tt>RowCount</tt> property of the
/// <tt>PXResult&lt;&gt;</tt> type. Note that you should use only one
/// <tt>Count&lt;&gt;</tt> function in a BQL query, because you won't be
/// able to access other such counted values.
/// </remarks>
/// <example><para>The code below shows how to use Count in BQL and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// foreach(PXResult&lt;Table&gt; row in PXSelectGroupBy&lt;Table1,
///     Aggregate&lt;GroupBy&lt;Table1.field1, Count&lt;Table1.field2&gt;&gt;&gt;&gt;.Select(this))
/// {
///     // The calculated number of distinct values of field2 in a group
///     int field2CountInGroup = row.RowCount;
///     ...
/// }</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT COUNT(DISTINCT Table1.Field2),
///       [MAX(Table1.Field) or NULL for all other fields defined in the Table DAC]
/// FROM Table1
/// GROUP BY Table1.Field1</code>
/// </example>
public sealed class Count<Field> : 
  IBqlFunctionExt,
  IBqlFunction,
  IBqlCreator,
  IBqlVerifier,
  IBqlSimpleAggregator
  where Field : IBqlField
{
  /// <exclude />
  public void GetAggregates(List<IBqlFunction> fields)
  {
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Tables?.Add(typeof (int));
    if (!info.BuildExpression)
      return true;
    SQLExpression singleExpression = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    exp = SQLExpression.CountDistinct(singleExpression);
    selection.AddExpr(exp.ToString(), exp);
    if (selection.Restrict)
      selection._Command.RecordMapEntries.Add(new PXDataRecordMap.FieldEntry(exp.ToString(), (System.Type) null, selection._PositionInResult)
      {
        PositionInQuery = selection._PositionInQuery
      });
    ++selection._PositionInResult;
    ++selection._PositionInQuery;
    return true;
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  /// <exclude />
  public string GetFunction() => "COUNT (DISTINCT {0})";

  public SQLExpression.Operation Operation => SQLExpression.Operation.COUNT_DISTINCT;

  /// <exclude />
  public System.Type GetField() => typeof (int);

  /// <exclude />
  public bool IsGroupBy() => false;
}

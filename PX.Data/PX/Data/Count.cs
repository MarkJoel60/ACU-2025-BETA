// Decompiled with JetBrains decompiler
// Type: PX.Data.Count
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
/// Counts the number of items in a group if a <tt>GroupBy</tt> clause is specified, or the total number of records in the result set otherwise.
/// Could also be used in the <see cref="T:PX.Data.Having`1" /> clause.</summary>
/// <remarks>
/// In the corresponding SQL representation, the class is represented by COUNT(*), which is  added to the list of selected columns. You access the
/// calculated value through the <tt>RowCount</tt> property of the <tt>PXResult&lt;&gt;</tt> type.
/// </remarks>
/// <example>The code below shows how to use Count in BQL and the corresponding SQL query.
/// <code title="Example" lang="CS">
/// PXResult&lt;Table&gt; res =
///     new PXSelectGroupBy&lt;Table1, Aggregate&lt;Count&gt;&gt;.Select(this);
/// // The calculated number of records is stored in the
/// // PXResult.RowCount property.
/// int tableRecordsNumber = res.RowCount;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT [MAX(Table1.Field) or NULL for all fields defined in the Table DAC],
///        COUNT(*)
/// FROM Table1</code>
/// </example>
public sealed class Count : 
  BqlAggregatedOperand<Count, IBqlInt>,
  IBqlFunctionExt,
  IBqlFunction,
  IBqlCreator,
  IBqlVerifier,
  IBqlSimpleAggregator
{
  /// <exclude />
  public void GetAggregates(List<IBqlFunction> fields)
  {
  }

  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Tables?.Add(typeof (int));
    if (!info.BuildExpression)
      return true;
    exp = SQLExpression.Count();
    if (selection.Restrict)
      selection._Command.RecordMapEntries.Add(new PXDataRecordMap.FieldEntry(exp.ToString(), (System.Type) null, selection._PositionInResult)
      {
        PositionInQuery = selection._PositionInQuery
      });
    ++selection._PositionInResult;
    ++selection._PositionInQuery;
    selection.AddExpr(exp);
    return true;
  }

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  /// <exclude />
  public string GetFunction() => "COUNT";

  /// <exclude />
  public System.Type GetField() => typeof (int);

  /// <exclude />
  public bool IsGroupBy() => false;

  public SQLExpression.Operation Operation => SQLExpression.Operation.COUNT;
}

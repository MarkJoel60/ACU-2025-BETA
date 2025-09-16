// Decompiled with JetBrains decompiler
// Type: PX.Data.IsNotNull
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding field is not null. Results in true for data records
/// with this field containing a value. Equivalent to SQL operator IS NOT NULL.
/// </summary>
/// <example><para>The code below shows the data view defined in a graph and the corresponding SQL query.</para>
/// 	<code title="Example" lang="CS">
/// // Data view definition
/// public PXSelect&lt;APTran,
///     Where&lt;APTran.tranType, Equal&lt;Required&lt;APTran.tranType&gt;&gt;,
///         And&lt;APTran.refNbr, Equal&lt;Required&lt;APTran.refNbr&gt;&gt;,
///         And&lt;APTran.box1099, IsNotNull&gt;&gt;&gt;&gt; AP1099Tran_Select;
/// ...
/// private void Update1099(APAdjust adj, APRegister apdoc)
/// {
///     // Data view execution
///     foreach(APTran tran in AP1099Tran_Select.Select(apdoc.DocType, apdoc.RefNbr))
///     {
///         ...
///     }
/// }</code>
/// 	<code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM APTran
/// WHERE ( APTran.TranType = [apdoc.DocType value]
///     AND APTran.RefNbr = [apdoc.RefNbr value]
///     AND APTran.Box1099 IS NOT NULL )</code>
/// </example>
public class IsNotNull : IBqlComparison, IBqlCreator, IBqlVerifier
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(value != null);
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.BuildExpression)
      exp = exp.IsNotNull();
    return true;
  }
}

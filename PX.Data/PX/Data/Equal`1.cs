// Decompiled with JetBrains decompiler
// Type: PX.Data.Equal`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Compares the preceding operand with <tt>Operand</tt> for equality.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
/// <example><para>The code below shows a data view defined in a graph, and the corresponding SQL query.</para>
/// 	<code title="Example" lang="CS">
/// // Data view definition in a graph
/// public PXSelect&lt;CurrencyInfo,
///     Where&lt;CurrencyInfo.curyInfoID, Equal&lt;Required&lt;CurrencyInfo.curyInfoID&gt;&gt;&gt;&gt;
///     CurrencyInfo_CuryInfoID;
/// ...
/// APTaxTran tran = ...
/// // Data view execution at run time
/// CurrencyInfo orig_info = CurrencyInfo_CuryInfoID.Select(tran.CuryInfoID);</code>
/// 	<code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM CurrencyInfo
/// WHERE CurrencyInfo.CuryInfoID = [value]</code>
/// </example>
public class Equal<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    return new bool?(this.collationComparer.Equals(val, value));
  }

  protected override bool isBypass(object val) => false;

  /// <exclude />
  public Equal()
    : base("=")
  {
  }

  /// <exclude />
  public Equal(IBqlOperand op)
    : base("=", operand: op as IBqlCreator)
  {
  }
}

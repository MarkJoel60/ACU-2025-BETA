// Decompiled with JetBrains decompiler
// Type: PX.Data.Current`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Inserts the field value from the <see cref="P:PX.Data.PXCache.Current" /> property of the cache.
/// If the <see cref="P:PX.Data.PXCache.Current" /> property is <see langword="null" /> or the field value is <see langword="null" />,
/// the parameter will be replaced by the default value.
/// </summary>
/// <typeparam name="Field">The inserted field value.</typeparam>
/// <example>The code below shows a data view that uses Current and the corresponding SQL query.
/// In this query, [value] is the TableID value from the Current property of the PXCache&lt;Table1&gt; object.
/// <code title="Example" lang="CS">
/// // Declaration of views in a BLC
/// PXSelect&lt;Table1&gt; MasterRecords;
/// PXSelect&lt;Table2,
///     Where&lt;Table2.tableID, Equal&lt;Current&lt;Table1.tableID&gt;&gt;&gt;&gt; DetailRecords;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table2
/// WHERE Table2.TableID = [value]</code>
/// </example>
public sealed class Current<Field> : ParameterBase<Field> where Field : IBqlField
{
  /// <exclude />
  public override bool TryDefault => true;

  /// <exclude />
  public override bool HasDefault => true;

  /// <exclude />
  public override bool IsVisible => false;

  /// <exclude />
  public override bool IsArgument => false;
}

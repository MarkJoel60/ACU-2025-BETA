// Decompiled with JetBrains decompiler
// Type: PX.Data.Required`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Replaced by a value passed to the <tt>Select()</tt> method.
/// The value type should match the type of the field specified as <tt>Field</tt>.
/// </summary>
/// <typeparam name="Field">The inserted field value.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXResutset&lt;Table&gt; res =
///     new PXSelect&lt;Table1,
///             Where&lt;Table1.field1, Equal&lt;Required&lt;Table1.field1&gt;&gt;&gt;&gt;
///         .Select(this, val);</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 = [the val variable value]</code>
/// </example>
public sealed class Required<Field> : ParameterBase<Field> where Field : IBqlField
{
  /// <exclude />
  public override bool TryDefault => false;

  /// <exclude />
  public override bool HasDefault => false;

  /// <exclude />
  public override bool IsVisible => true;

  /// <exclude />
  public override bool IsArgument => false;
}

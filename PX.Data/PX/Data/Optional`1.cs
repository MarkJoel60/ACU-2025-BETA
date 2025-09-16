// Decompiled with JetBrains decompiler
// Type: PX.Data.Optional`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Inserts the  <tt>Current</tt> property value of the cache or the value explicitly passed to the <tt>Select()</tt> method. In the latter case, the
/// parameter causes raising of the <tt>FieldUpdating</tt> event for the specified field (which can modify or substitute the value). If the null value is passed or
/// the <tt>Current</tt> property is null, the default value of the field is inserted.</summary>
/// <typeparam name="Field">The inserted field value.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXResutset&lt;Table1&gt; res =
///     new PXSelect&lt;Table1,
///             Where&lt;Table1.field1, Equal&lt;Optional&lt;Table2.field1&gt;&gt;&gt;&gt;
///         .Select(this, val);</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 = [value]</code>
/// </example>
public sealed class Optional<Field> : ParameterBase<Field> where Field : IBqlField
{
  /// <exclude />
  public override bool TryDefault => true;

  /// <exclude />
  public override bool HasDefault => true;

  /// <exclude />
  public override bool IsVisible => true;

  /// <exclude />
  public override bool IsArgument => false;
}

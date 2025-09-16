// Decompiled with JetBrains decompiler
// Type: PX.Data.Argument`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// You use this parameter to pass a particular data type value from the UI control to the associated data view.
/// </summary>
/// <typeparam name="ArgumentType">A data type.</typeparam>
/// <example><para>The example below shows a data view that contains all the records from the Field1 that are greater than the argument, the value of which is provided in the Select method.</para>
/// <code title="Example" lang="CS">
/// // Declaration of a data view in a graph
/// PXSelect&lt;Table1, Where&lt;Table1.field1, Greater&lt;Argument&lt;int?&gt;&gt;&gt;&gt; Records;
/// ...
/// // Execution of the data view in code
/// foreach(Table1 rec in Records.Select(5))
/// ...</code>
/// <code title="Example2" description="The expression above will be translated into the following SQL query." groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 &gt; 5</code>
/// </example>
public sealed class Argument<ArgumentType> : ParameterBase<ArgumentType>
{
  /// <exclude />
  public override bool TryDefault => false;

  /// <exclude />
  public override bool HasDefault => false;

  /// <exclude />
  public override bool IsVisible => true;

  /// <exclude />
  public override bool IsArgument => true;
}

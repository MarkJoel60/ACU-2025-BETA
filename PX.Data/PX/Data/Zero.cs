// Decompiled with JetBrains decompiler
// Type: PX.Data.Zero
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// The integer zero, not comparable with floating point numeric types (such as decimal).
/// </summary>
/// <example><para>The code below shows a possible definition of a data view that selects BAccount data records in a graph. The corresponding SQL query is given below.</para>
/// <code title="Example" lang="CS">
/// public PXSelect&lt;CR.BAccount,
///     Where&lt;CR.BAccount.bAccountID, Equal&lt;Current&lt;BAccount.bAccountID&gt;&gt;,
///         Or&lt;Current&lt;BAccount.bAccountID&gt;, Less&lt;Zero&gt;&gt;&gt;&gt; records;</code>
/// <code title="" description="" lang="SQL">
/// SELECT * FROM BAccount
/// WHERE BAccount.bAccountID = [value] OR [value] &lt; 0</code>
/// </example>
public sealed class Zero : BqlType<IBqlInt, int>.Constant<
#nullable disable
Zero>
{
  /// <exclude />
  public Zero()
    : base(0)
  {
  }
}

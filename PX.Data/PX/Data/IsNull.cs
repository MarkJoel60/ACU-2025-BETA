// Decompiled with JetBrains decompiler
// Type: PX.Data.IsNull
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding field is null. Equivalent to SQL operator IS NULL.
/// </summary>
/// <example>
/// <code>
/// public PXSelectJoin&lt;APQuickCheck,
///     LeftJoin&lt;Vendor, On&lt;Vendor.bAccountID, Equal&lt;APQuickCheck.vendorID&gt;&gt;&gt;,
///     Where&lt;APQuickCheck.docType, Equal&lt;Optional&lt;APQuickCheck.docType&gt;&gt;,
///         And&lt;Where&lt;Vendor.bAccountID, IsNull,
///             Or&lt;Match&lt;Vendor, Current&lt;AccessInfo.userName&gt;&gt;&gt;&gt;&gt;&gt;&gt; Document;
/// </code>
/// </example>
public class IsNull : IBqlComparison, IBqlCreator, IBqlVerifier
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(value == null);
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.BuildExpression)
      exp = exp.IsNull();
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
    return true;
  }
}

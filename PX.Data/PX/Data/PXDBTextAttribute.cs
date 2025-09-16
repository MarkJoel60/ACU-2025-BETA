// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBTextAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>string</tt> type to the database
/// column of <tt>nvarchar</tt> or <tt>varchar</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBText(IsUnicode = true)]
/// [PXUIField(DisplayName = "Activity Details")]
/// public virtual string Body { ... }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBTextAttribute : PXDBStringAttribute
{
  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    if (this._DatabaseFieldName == null || (e.Operation & PXDBOperation.Option) != PXDBOperation.GroupBy)
      return;
    e.Expr = SQLExpression.Null();
  }
}

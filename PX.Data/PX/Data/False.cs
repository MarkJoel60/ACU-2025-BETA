// Decompiled with JetBrains decompiler
// Type: PX.Data.False
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// The false value for comparing with boolean fields. In translation
/// to SQL corresponds to CONVERT(BIT, 0).
/// </summary>
/// <example><para>The code below shows a definition of the %PXProcessing:PXProcessing{T}% data view in a graph.</para>
/// <code title="Example" lang="CS">
/// public PXProcessing&lt;TranslationHistory,
///     Where&lt;TranslationHistory.released, Equal&lt;False&gt;&gt;&gt; TranslationReleaseList;</code>
/// </example>
public sealed class False : BoolConstant<False>
{
  /// <exclude />
  public False()
    : base(false)
  {
  }

  /// <exclude />
  protected override Lazy<IBqlUnary> UnaryLazyImpl { get; } = new Lazy<IBqlUnary>((Func<IBqlUnary>) (() => (IBqlUnary) new WhereNp<True, Equal<False>>()));
}

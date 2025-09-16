// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Substring`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Returns the <typeparamref name="TLength" /> characters from the <typeparamref name="TSource" /> string starting
/// from the <typeparamref name="TStartIndex" /> index (the first character has index 1).
/// Equivalent to SQL function SUBSTRING.
/// Is a strongly typed version of <see cref="T:PX.Data.Substring`3" />.
/// </summary>
public sealed class Substring<TSource, TStartIndex, TLength> : 
  BqlFunction<PX.Data.Substring<TSource, TStartIndex, TLength>, IBqlString>
  where TSource : IBqlOperand, IImplement<IBqlString>
  where TStartIndex : IBqlOperand, IImplement<IBqlInteger>
  where TLength : IBqlOperand, IImplement<IBqlInteger>
{
}

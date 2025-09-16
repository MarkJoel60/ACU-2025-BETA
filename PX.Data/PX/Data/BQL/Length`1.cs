// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Length`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Returns the number of characters in the <typeparamref name="TSource" /> string.
/// Equivalent to SQL function LEN.
/// Is a strongly typed version of <see cref="T:PX.Data.StrLen`1" />.
/// </summary>
public sealed class Length<TSource> : BqlFunction<StrLen<TSource>, IBqlLong> where TSource : IBqlOperand, IImplement<IBqlString>
{
}

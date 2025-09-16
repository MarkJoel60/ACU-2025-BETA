// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.RTrim`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Removes all trailing white-space characters from a <typeparamref name="TSource" />.
/// Is a strongly typed version of <see cref="T:PX.Data.RTrim`1" />.
/// </summary>
public sealed class RTrim<TSource> : BqlFunction<PX.Data.RTrim<TSource>, IBqlString> where TSource : IBqlOperand, IImplement<IBqlString>
{
}

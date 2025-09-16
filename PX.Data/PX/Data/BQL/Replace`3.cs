// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Replace`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Replaces all occurrences of a string with another string in the source expression.
/// Equivalent to SQL function REPLACE.
/// Is a strongly typed version of <see cref="T:PX.Data.Replace`3" />.
/// </summary>
public sealed class Replace<TSource, TToReplace, TReplaceWith> : 
  BqlFunction<PX.Data.Replace<TSource, TToReplace, TReplaceWith>, IBqlString>
  where TSource : IBqlOperand, IImplement<IBqlString>
  where TToReplace : IBqlOperand, IImplement<IBqlString>
  where TReplaceWith : IBqlOperand, IImplement<IBqlString>
{
}

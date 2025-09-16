// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Round`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Returns a numeric value rounded to the specified precision.
/// Equivalent to SQL function ROUND.
/// Is a strongly typed version of <see cref="T:PX.Data.Round`2" />.
/// </summary>
public sealed class Round<TOperand, TPrecision> : 
  BqlFunction<PX.Data.Round<TOperand, TPrecision>, IBqlDecimal>
  where TOperand : IBqlOperand, IImplement<IBqlNumeric>
  where TPrecision : IBqlOperand, IImplement<IBqlInteger>
{
}

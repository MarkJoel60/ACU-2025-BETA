// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Minus`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Returns -<typeparamref name="TOperand" /> (multiplies by -1).
/// Is a strongly typed version of <see cref="T:PX.Data.Minus`1" />.
/// </summary>
public sealed class Minus<TOperand> : BqlFunction<PX.Data.Minus<TOperand>, IBqlDecimal> where TOperand : IBqlOperand, IImplement<IBqlNumeric>
{
}

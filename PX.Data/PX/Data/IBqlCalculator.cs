// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlCalculator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>A BQL calculator.</summary>
[Obsolete("Use IBqlCreator instead")]
public interface IBqlCalculator : IBqlOperand
{
  /// <summary>Calculates the expression.</summary>
  /// <param name="cache">A cache instance.</param>
  /// <param name="row">The row on which the calculation is performed.</param>
  /// <param name="digit">The number of significant digits (precision) in the return value. Set to -1 if you don't want to perform rounding.</param>
  /// <returns>Result of calculation or null.</returns>
  object Calculate(PXCache cache, object row, int digit);
}

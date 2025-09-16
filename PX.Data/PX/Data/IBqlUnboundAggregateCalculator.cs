// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlUnboundAggregateCalculator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Bql aggregate calculator</summary>
public interface IBqlUnboundAggregateCalculator
{
  /// <summary>Calculates aggregate expression</summary>
  /// <param name="cache">Cache</param>
  /// <param name="fieldordinal">Position of the field for which this aggregate function is supplied</param>
  /// <param name="records">Set of records</param>
  /// <param name="digit">The number of significant digits (precision) in the return value, set to -1 if you don't want to perform Round operation</param>
  /// <returns>Result of calculation or null</returns>
  object Calculate(PXCache cache, object row, IBqlCreator formula, object[] records, int digit);

  object Calculate(PXCache cache, object row, object oldrow, IBqlCreator formula, int digit);
}

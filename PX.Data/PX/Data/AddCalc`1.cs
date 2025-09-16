// Decompiled with JetBrains decompiler
// Type: PX.Data.AddCalc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class AddCalc<Field> : 
  IBqlAggregateCalculator,
  IBqlUnboundAggregateCalculator,
  IBqlZeroValueChecker
  where Field : IBqlField
{
  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    IBqlCreator formula,
    int digit)
  {
    return this.Calculate(cache, row, oldrow, digit, (AddCalc<Field>._GetValueDelegate) ((sender, item) =>
    {
      object val = (object) null;
      bool? result = new bool?();
      if (item != null)
      {
        BqlFormula.Verify(sender, item, formula, ref result, ref val);
        val = Rounder.Round(val, digit);
      }
      return val;
    }));
  }

  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    IBqlCreator formula,
    object[] records,
    int digit)
  {
    return (object) null;
  }

  [Obsolete("Use digit-parameter in Calculate-methods instead.")]
  private int GetParentFieldPrecision(PXCache childCache)
  {
    List<PXEventSubscriberAttribute> attributesReadonly = childCache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))].GetAttributesReadonly<Field>();
    return (int?) attributesReadonly.OfType<PXDBDecimalAttribute>().FirstOrDefault<PXDBDecimalAttribute>()?.Precision ?? ((int?) attributesReadonly.OfType<PXDecimalAttribute>().FirstOrDefault<PXDecimalAttribute>()?.Precision).GetValueOrDefault();
  }

  private object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    int digit,
    AddCalc<Field>._GetValueDelegate _GetValue)
  {
    if (row == oldrow)
      return (object) null;
    object val = _GetValue(cache, row);
    object oldval = _GetValue(cache, oldrow);
    return AddCalc<Field>.CalculateDiff(digit, val, oldval);
  }

  public object Calculate(PXCache cache, object row, object oldrow, int fieldordinal, int digit)
  {
    if (row == oldrow)
      return (object) null;
    object val = cache.GetValue(row, fieldordinal);
    object oldval = cache.GetValue(oldrow, fieldordinal);
    return AddCalc<Field>.CalculateDiff(digit, val, oldval);
  }

  public object Calculate(
    PXCache cache,
    object row,
    int fieldordinal,
    object[] records,
    int digit)
  {
    return (object) null;
  }

  private static object CalculateDiff(int digit, object val, object oldval)
  {
    switch (val != null ? System.Type.GetTypeCode(val.GetType()) : (oldval != null ? System.Type.GetTypeCode(oldval.GetType()) : TypeCode.Empty))
    {
      case TypeCode.Int32:
        if (oldval == null)
          return val;
        return val == null ? (object) -(int) oldval : (object) ((int) val - (int) oldval);
      case TypeCode.Double:
        if (oldval == null)
          return val;
        return val == null ? (object) -(double) oldval : Rounder.Round((object) ((double) val - (double) oldval), digit);
      case TypeCode.Decimal:
        if (oldval == null)
          return val;
        return val == null ? (object) -(Decimal) oldval : Rounder.Round((object) ((Decimal) val - (Decimal) oldval), digit);
      default:
        return (object) null;
    }
  }

  object IBqlZeroValueChecker.GetZeroValue(object val) => ZeroNumberChecker.GetZeroValue(val);

  bool IBqlZeroValueChecker.IsZeroValue(object val) => ZeroNumberChecker.IsZeroValue(val);

  /// <exclude />
  private delegate object _GetValueDelegate(PXCache cache, object item) where Field : IBqlField;
}

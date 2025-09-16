// Decompiled with JetBrains decompiler
// Type: PX.Data.SumCalc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL.AggregateCalculators;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class SumCalc<Field> : 
  SumCalc,
  IBqlAggregateCalculator,
  IBqlUnboundAggregateCalculator,
  IBqlZeroValueChecker,
  IBqlAggregateValidation
  where Field : IBqlField
{
  /// <exclude />
  public virtual object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    IBqlCreator formula,
    int digit)
  {
    return this.Calculate(cache, row, oldrow, digit, (SumCalc._GetValueDelegate) ((sender, item) =>
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
  public virtual object Calculate(
    PXCache cache,
    object row,
    IBqlCreator formula,
    object[] records,
    int digit)
  {
    return this.Calculate(cache, row, records, digit, (SumCalc._GetValueDelegate) ((sender, item) =>
    {
      object val = (object) null;
      bool? result = new bool?();
      BqlFormula.Verify(sender, item, formula, ref result, ref val);
      return Rounder.Round(val, digit);
    }));
  }

  [Obsolete("Use digit-parameter in Calculate-methods instead.")]
  private int GetParentFieldPrecision(PXCache childCache)
  {
    List<PXEventSubscriberAttribute> attributesReadonly = childCache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))].GetAttributesReadonly<Field>();
    return (int?) attributesReadonly.OfType<PXDBDecimalAttribute>().FirstOrDefault<PXDBDecimalAttribute>()?.Precision ?? ((int?) attributesReadonly.OfType<PXDecimalAttribute>().FirstOrDefault<PXDecimalAttribute>()?.Precision).GetValueOrDefault();
  }

  /// <exclude />
  public virtual object Calculate(
    PXCache cache,
    object row,
    int fieldordinal,
    object[] records,
    int digit)
  {
    return this.Calculate(cache, row, records, digit, (SumCalc._GetValueDelegate) ((sender, item) => sender.GetValue(item, fieldordinal)));
  }

  protected object Calculate(
    PXCache cache,
    object row,
    object[] records,
    int digit,
    SumCalc._GetValueDelegate _GetValue)
  {
    object val1 = (object) null;
    foreach (object record in records)
    {
      object val2 = _GetValue(cache, record);
      if (val2 != null)
      {
        object obj = val1 ?? ZeroNumberChecker.GetZeroValue(val2);
        switch (System.Type.GetTypeCode(val2.GetType()))
        {
          case TypeCode.Double:
            val1 = (object) ((double) obj + (double) val2);
            continue;
          case TypeCode.Decimal:
            val1 = (object) ((Decimal) obj + (Decimal) val2);
            continue;
          default:
            val1 = (object) ((int) obj + (int) val2);
            continue;
        }
      }
    }
    return Rounder.Round(val1, digit);
  }

  object IBqlZeroValueChecker.GetZeroValue(object val) => ZeroNumberChecker.GetZeroValue(val);

  bool IBqlZeroValueChecker.IsZeroValue(object val) => ZeroNumberChecker.IsZeroValue(val);

  public void CreateValidation(
    PXCache cache,
    string fieldName,
    System.Type parentFieldType,
    IBqlCreator formula)
  {
    AggregateValidation validation = AggregateValidation.CreateValidation(cache.Graph.Caches[BqlCommand.GetItemType(parentFieldType)].GetFieldType(parentFieldType.Name));
    if (fieldName != null)
    {
      validation?.Initialize(cache, parentFieldType, fieldName);
    }
    else
    {
      if (formula == null || validation == null)
        return;
      validation.Initialize(cache, parentFieldType, formula);
    }
  }
}

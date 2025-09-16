// Decompiled with JetBrains decompiler
// Type: PX.Data.MinCalc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Calculates the minimum expression over all child data records
/// and assigns it to the specified parent data record field.
/// </summary>
/// <typeparam name="Field">The field of the parent data record.</typeparam>
/// <remarks>This class is used as aggregation formula in the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> and <see cref="T:PX.Data.PXUnboundFormulaAttribute">PXUnboundFormula</see> attributes to compute the parent
/// data record field from the child data record fields. The expression that is calculated for each child data record is set in the first constructor parameter in
/// the attributes.</remarks>
/// <seealso cref="T:PX.Data.PXFormulaAttribute"></seealso>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXFormula(null, typeof(MinCalc&lt;ParentTable1.field1&gt;))]
/// public virtual decimal? ChildField { get; set; }</code>
/// </example>
public sealed class MinCalc<Field> : IBqlAggregateCalculator where Field : IBqlField
{
  /// <exclude />
  public object Calculate(PXCache cache, object row, object oldrow, int fieldordinal, int digit)
  {
    return (object) null;
  }

  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    int fieldordinal,
    object[] records,
    int digit)
  {
    object obj = (object) null;
    foreach (object record in records)
    {
      object strA = cache.GetValue(record, fieldordinal);
      if (strA != null)
      {
        if (obj == null)
        {
          obj = strA;
        }
        else
        {
          switch (System.Type.GetTypeCode(strA.GetType()))
          {
            case TypeCode.Double:
              if ((double) strA < (double) obj)
              {
                obj = strA;
                continue;
              }
              continue;
            case TypeCode.Decimal:
              if ((Decimal) strA < (Decimal) obj)
              {
                obj = strA;
                continue;
              }
              continue;
            case TypeCode.DateTime:
              if ((System.DateTime) strA < (System.DateTime) obj)
              {
                obj = strA;
                continue;
              }
              continue;
            case TypeCode.String:
              if (string.Compare((string) strA, (string) obj) < 0)
              {
                obj = strA;
                continue;
              }
              continue;
            default:
              if ((int) strA < (int) obj)
              {
                obj = strA;
                continue;
              }
              continue;
          }
        }
      }
    }
    return Rounder.Round(obj, digit);
  }
}

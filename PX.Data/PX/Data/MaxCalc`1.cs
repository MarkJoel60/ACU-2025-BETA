// Decompiled with JetBrains decompiler
// Type: PX.Data.MaxCalc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// Calculates the maximum expression over all child data records
/// and assigns it to the specified parent data record field.
/// </summary>
/// <typeparam name="Field">The field of the parent data record.</typeparam>
/// <remarks>This class is used as aggregation formula in the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> and <see cref="T:PX.Data.PXUnboundFormulaAttribute">PXUnboundFormula</see> attributes to compute the parent
/// data record field from the child data record fields. The expression that is calculated for each child data record is set in the first constructor parameter in
/// the attributes.</remarks>
/// <seealso cref="T:PX.Data.PXFormulaAttribute"></seealso>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXFormula(null,typeof(MaxCalc&lt;CABankTranHeader.tranMaxDate&gt;))]
/// public virtual DateTime? TranDate { get; set; }</code>
/// </example>
public sealed class MaxCalc<Field> : IBqlAggregateCalculator, IBqlUnboundAggregateCalculator where Field : IBqlField
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
    return MaxCalc<Field>.Calculate(cache, (IEnumerable<object>) records, digit, (MaxCalc<Field>._GetValueDelegate) ((sender, item) => sender.GetValue(item, fieldordinal)));
  }

  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    IBqlCreator formula,
    int digit)
  {
    return (object) null;
  }

  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    IBqlCreator formula,
    object[] records,
    int digit)
  {
    return MaxCalc<Field>.Calculate(cache, (IEnumerable<object>) records, digit, (MaxCalc<Field>._GetValueDelegate) ((sender, item) =>
    {
      object obj = (object) null;
      bool? result = new bool?();
      BqlFormula.Verify(sender, item, formula, ref result, ref obj);
      return obj;
    }));
  }

  private static object Calculate(
    PXCache cache,
    IEnumerable<object> records,
    int digit,
    MaxCalc<Field>._GetValueDelegate _GetValue)
  {
    object obj = (object) null;
    foreach (object strA in records.Select<object, object>((Func<object, object>) (record => _GetValue(cache, record))).Where<object>((Func<object, bool>) (val => val != null)))
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
            if ((double) strA > (double) obj)
            {
              obj = strA;
              continue;
            }
            continue;
          case TypeCode.Decimal:
            if ((Decimal) strA > (Decimal) obj)
            {
              obj = strA;
              continue;
            }
            continue;
          case TypeCode.DateTime:
            if ((System.DateTime) strA > (System.DateTime) obj)
            {
              obj = strA;
              continue;
            }
            continue;
          case TypeCode.String:
            if (string.CompareOrdinal((string) strA, (string) obj) > 0)
            {
              obj = strA;
              continue;
            }
            continue;
          default:
            if ((int) strA > (int) obj)
            {
              obj = strA;
              continue;
            }
            continue;
        }
      }
    }
    return Rounder.Round(obj, digit);
  }

  /// <exclude />
  private delegate object _GetValueDelegate(PXCache cache, object item) where Field : IBqlField;
}

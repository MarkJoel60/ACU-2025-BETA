// Decompiled with JetBrains decompiler
// Type: PX.Data.SumCalc
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Calculates the aggregated sum of expressions over all child data records and assigns it to the specified parent data record field.</summary>
/// <typeparam name="Field">The field of the parent data record.</typeparam>
/// <remarks>This class is used as aggregation formula in the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> and <see cref="T:PX.Data.PXUnboundFormulaAttribute">PXUnboundFormula</see> attributes to compute the parent
/// data record field from the child data record fields. The expression that is calculated for each child data record is set in the first constructor parameter in
/// the attributes.</remarks>
/// <seealso cref="T:PX.Data.PXFormulaAttribute"></seealso>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXFormula(typeof(INTran.qty.Multiply&lt;INTran.unitPrice&gt;),
///            typeof(SumCalc&lt;INRegister.totalAmount&gt;))]
/// public virtual Decimal? TranAmt { get; set; }</code>
/// </example>
public class SumCalc
{
  /// <exclude />
  public virtual object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    int fieldordinal,
    int digit)
  {
    return this.Calculate(cache, row, oldrow, digit, (SumCalc._GetValueDelegate) ((sender, item) => sender.GetValue(item, fieldordinal)));
  }

  public virtual object Calculate<TField>(PXCache cache, object row, object oldrow, int digit = -1) where TField : IBqlField
  {
    return this.Calculate(cache, row, oldrow, digit, (SumCalc._GetValueDelegate) ((sender, item) => sender.GetValue(item, typeof (TField).Name)));
  }

  protected object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    int digit,
    SumCalc._GetValueDelegate _GetValue)
  {
    if (row == oldrow)
      return (object) null;
    object obj1 = _GetValue(cache, row);
    object obj2 = _GetValue(cache, oldrow);
    switch (obj1 != null ? System.Type.GetTypeCode(obj1.GetType()) : (obj2 != null ? System.Type.GetTypeCode(obj2.GetType()) : TypeCode.Empty))
    {
      case TypeCode.Int32:
        if (obj2 == null)
          return obj1;
        return obj1 == null ? (object) -(int) obj2 : (object) ((int) obj1 - (int) obj2);
      case TypeCode.Double:
        if (obj2 == null)
          return obj1;
        return obj1 == null ? (object) -(double) obj2 : Rounder.Round((object) ((double) obj1 - (double) obj2), digit);
      case TypeCode.Decimal:
        if (obj2 == null)
          return obj1;
        return obj1 == null ? (object) -(Decimal) obj2 : Rounder.Round((object) ((Decimal) obj1 - (Decimal) obj2), digit);
      default:
        return (object) null;
    }
  }

  /// <exclude />
  protected delegate object _GetValueDelegate(PXCache cache, object item);
}

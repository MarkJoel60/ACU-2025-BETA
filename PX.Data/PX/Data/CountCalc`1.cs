// Decompiled with JetBrains decompiler
// Type: PX.Data.CountCalc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL.AggregateCalculators;
using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Calculates the number of the child data records and assigns
/// it to the specified parent data record field.
/// </summary>
/// <typeparam name="Field">The field of the parent data record.</typeparam>
/// <remarks>This class is used as aggregation formula in the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see> and <see cref="T:PX.Data.PXUnboundFormulaAttribute">PXUnboundFormula</see> attributes to compute the parent
/// data record field from the child data record fields. The expression that is calculated for each child data record is set in the first constructor parameter in
/// the attributes.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXFormula(null, typeof(CountCalc&lt;ARSalesPerTran.refCntr&gt;))]
/// public virtual Decimal? CuryTranAmt { get; set; }</code>
/// </example>
public sealed class CountCalc<Field> : 
  IBqlAggregateCalculator,
  ICountCalc,
  IBqlZeroValueChecker,
  IBqlAggregateValidation
  where Field : IBqlField
{
  /// <exclude />
  public object Calculate(PXCache cache, object row, object oldrow, int fieldordinal, int digit)
  {
    if (row != null && oldrow == null)
      return (object) 1;
    return oldrow != null && row == null ? (object) -1 : (object) 0;
  }

  /// <exclude />
  public object Calculate(
    PXCache cache,
    object row,
    int fieldordinal,
    object[] records,
    int digit)
  {
    return (object) records.Length;
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
    if (validation == null || !EnumerableExtensions.IsIn<TypeCode>(validation.TypeCode, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64))
      return;
    validation.Initialize(cache, parentFieldType, (IBqlCreator) new Const<One>());
  }
}

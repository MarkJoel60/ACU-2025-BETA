// Decompiled with JetBrains decompiler
// Type: PX.Data.Row`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Returns the value of the specified field.</summary>
/// <typeparam name="Field">The field whose value is returned.</typeparam>
public sealed class Row<Field> : IBqlOperand, IBqlCreator, IBqlVerifier where Field : IBqlField
{
  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Fields?.Add(typeof (Field));
    return false;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (item is BqlFormula.ItemContainer itemContainer)
      itemContainer.InvolvedFields.Add(typeof (Field));
    value = cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), typeof (Field).Name);
  }

  /// <summary>
  /// Creates additional dependencies for the formula on the provided dependency fields.
  /// Each time the dependency fields are updated, the formula is recalculated.
  /// The formula also depends on all other fields referenced in the formula.
  /// </summary>
  /// <typeparam name="TDependentFields">The <see cref="T:PX.Common.TypeArray" /> of dependent <see cref="T:PX.Data.IBqlField" />s.</typeparam>
  public class WithDependencies<TDependentFields> : IBqlOperand, IBqlCreator, IBqlVerifier where TDependentFields : ITypeArrayOf<IBqlField>, TypeArray.IsNotEmpty
  {
    private static readonly System.Type[] DependentFields = TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TDependentFields), (string) null);

    /// <exclude />
    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      info.Fields?.AddRange((IEnumerable<System.Type>) Row<Field>.WithDependencies<TDependentFields>.DependentFields);
      return true;
    }

    /// <exclude />
    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      if (item is BqlFormula.ItemContainer itemContainer)
      {
        foreach (System.Type dependentField in Row<Field>.WithDependencies<TDependentFields>.DependentFields)
          itemContainer.InvolvedFields.Add(dependentField);
      }
      value = cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), typeof (Field).Name);
    }
  }

  /// <summary>
  /// Creates a dependency of the formula on the provided dependency field.
  /// Each time the dependency field is updated, the formula is recalculated.
  /// The formula also depends on all other fields referenced in the formula.
  /// </summary>
  /// <typeparam name="TDependentField">The dependent field.</typeparam>
  public sealed class WithDependency<TDependentField> : 
    Row<Field>.WithDependencies<TypeArrayOf<IBqlField>.FilledWith<TDependentField>>
    where TDependentField : IBqlField
  {
  }

  /// <summary>
  /// Creates a dependency of the formula on the provided two dependency fields.
  /// Each time the dependency fields are updated, the formula is recalculated.
  /// The formula also depends on all other fields referenced in the formula.
  /// </summary>
  /// <typeparam name="TDependentField1">The first dependent field.</typeparam>
  /// <typeparam name="TDependentField2">The second dependent field.</typeparam>
  public sealed class WithDependencies<TDependentField1, TDependentField2> : 
    Row<Field>.WithDependencies<TypeArrayOf<IBqlField>.FilledWith<TDependentField1, TDependentField2>>
    where TDependentField1 : IBqlField
    where TDependentField2 : IBqlField
  {
  }

  /// <summary>
  /// Creates a dependency of the formula on the provided three dependency fields.
  /// Each time the dependency fields are updated, the formula is recalculated.
  /// The formula also depends on all other fields referenced in the formula.
  /// </summary>
  /// <typeparam name="TDependentField1">The first dependent field.</typeparam>
  /// <typeparam name="TDependentField2">The second dependent field.</typeparam>
  /// <typeparam name="TDependentField3">The third dependent field.</typeparam>
  public sealed class WithDependencies<TDependentField1, TDependentField2, TDependentField3> : 
    Row<Field>.WithDependencies<TypeArrayOf<IBqlField>.FilledWith<TDependentField1, TDependentField2, TDependentField3>>
    where TDependentField1 : IBqlField
    where TDependentField2 : IBqlField
    where TDependentField3 : IBqlField
  {
  }

  /// <summary>
  /// Creates a dependency of the formula on the provided four dependency fields.
  /// Each time the dependency fields are updated, the formula is recalculated.
  /// The formula also depends on all other fields referenced in the formula.
  /// </summary>
  /// <typeparam name="TDependentField1">The first dependent field.</typeparam>
  /// <typeparam name="TDependentField2">The second dependent field.</typeparam>
  /// <typeparam name="TDependentField3">The third dependent field.</typeparam>
  /// <typeparam name="TDependentField4">The fourth dependent field.</typeparam>
  public sealed class WithDependencies<TDependentField1, TDependentField2, TDependentField3, TDependentField4> : 
    Row<Field>.WithDependencies<TypeArrayOf<IBqlField>.FilledWith<TDependentField1, TDependentField2, TDependentField3, TDependentField4>>
    where TDependentField1 : IBqlField
    where TDependentField2 : IBqlField
    where TDependentField3 : IBqlField
    where TDependentField4 : IBqlField
  {
  }
}

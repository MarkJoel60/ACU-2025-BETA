// Decompiled with JetBrains decompiler
// Type: PX.Data.Null
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The null value that is used in Switch clauses as a default value.
/// </summary>
/// <remarks>Do not use this
/// constant for checking fields for null value. Use the <see cref="T:PX.Data.IsNull" /> and
/// <see cref="T:PX.Data.IsNotNull" /> classes instead.</remarks>
/// <example>
///   <para>The code below shows the usage of the Null class in a formula added to the definition of a DAC field.</para>
///   <code title="Example">[PXDBInt()]
/// [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
/// [PXUIField(DisplayName = "Labor Item", Required = false)]
/// [PXFormula(
///     typeof(Null.
///         When&lt;perItemBilling.IsEqual&lt;BillingTypeListAttribute.perActivity&gt;&gt;.
///         Else&lt;labourItemID.FromCurrent&gt;
///     ))]
/// public virtual Int32? LabourItemID { get; set; }</code>
/// </example>
public class Null : BqlOperand<Null, IBqlNull>, IBqlOperand, IBqlCreator, IBqlVerifier
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.BuildExpression)
      exp = SQLExpression.None();
    return true;
  }
}

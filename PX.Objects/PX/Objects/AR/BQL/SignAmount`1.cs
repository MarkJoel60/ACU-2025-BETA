// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BQL.SignAmount`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.BQL;

/// <summary>
/// A BQL function that returns <see cref="M:PX.Objects.AR.ARDocType.SignAmount(System.String)" />
/// </summary>
public sealed class SignAmount<TDocType> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier where TDocType : IBqlOperand
{
  private IBqlCreator _source;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (!BqlFunction.getValue<TDocType>(ref this._source, cache, item, pars, ref result, ref value) || value == null)
      return;
    value = (object) ARDocType.SignAmount((string) value);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Minutes`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public sealed class Minutes<Operand> : IBqlOperand, IBqlCreator, IBqlVerifier where Operand : IBqlOperand
{
  private IBqlCreator _operand;

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (typeof (IBqlField).IsAssignableFrom(typeof (Operand)))
    {
      info.Fields?.Add(typeof (Operand));
      return flag;
    }
    if (this._operand == null)
      this._operand = (object) Activator.CreateInstance<Operand>() as IBqlCreator;
    if (this._operand == null)
      throw new PXArgumentException(nameof (Operand), "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
    return flag & this._operand.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, ref value);
    if (value == null)
      return;
    value = (object) TimeSpan.FromMinutes(Convert.ToDouble(value));
  }
}

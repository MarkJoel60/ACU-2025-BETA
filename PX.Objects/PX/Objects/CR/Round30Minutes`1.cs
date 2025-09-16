// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Round30Minutes`1
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

public sealed class Round30Minutes<Operand> : IBqlOperand, IBqlCreator, IBqlVerifier where Operand : IBqlOperand
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
      return true;
    }
    if (this._operand == null)
      this._operand = (object) Activator.CreateInstance<Operand>() as IBqlCreator;
    if (this._operand == null)
      throw new PXArgumentException(nameof (Operand), "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
    this._operand.AppendExpression(ref exp, graph, info, selection);
    return flag;
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
    DateTime dateTime = Convert.ToDateTime(value);
    int num = dateTime.Minute;
    if (num != 0)
      num = num <= 30 ? 30 : 60;
    value = (object) new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddMinutes((double) num);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.DatePart`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class DatePart<UOM, Operand> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IImplement<IBqlCastableTo<IBqlInt>>
  where UOM : IConstant<string>, IBqlOperand, new()
  where Operand : IBqlOperand
{
  private IBqlCreator _operand;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj;
    if (!BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out obj) || obj == null)
      return;
    System.DateTime dateTime = Convert.ToDateTime(obj);
    string str = new UOM().Value;
    if (str == null)
      return;
    switch (str.Length)
    {
      case 2:
        switch (str[1])
        {
          case 'd':
            if (!(str == "dd"))
              return;
            value = (object) Convert.ToInt32(dateTime.Day);
            return;
          case 'h':
            if (!(str == "hh"))
              return;
            value = (object) Convert.ToInt32(dateTime.Hour);
            return;
          case 'i':
            if (!(str == "mi"))
              return;
            value = (object) Convert.ToInt32(dateTime.Minute);
            return;
          case 'j':
          case 'k':
          case 'l':
            return;
          case 'm':
            if (!(str == "mm"))
              return;
            value = (object) Convert.ToInt32(dateTime.Month);
            return;
          case 'n':
          case 'o':
          case 'p':
          case 'r':
            return;
          case 'q':
            if (!(str == "qq"))
              return;
            value = (object) Convert.ToInt32(dateTime.Month / 3 + 1);
            return;
          case 's':
            if (!(str == "ss"))
              return;
            value = (object) Convert.ToInt32(dateTime.Second);
            return;
          case 'w':
            switch (str)
            {
              case "dw":
                value = (object) Convert.ToInt32((object) dateTime.DayOfWeek);
                return;
              case "ww":
                value = (object) Convert.ToInt32(dateTime.Day / 7 + 1);
                return;
              default:
                return;
            }
          case 'y':
            if (!(str == "dy"))
              return;
            value = (object) Convert.ToInt32(dateTime.DayOfYear);
            return;
          default:
            return;
        }
      case 4:
        if (!(str == "yyyy"))
          break;
        value = (object) Convert.ToInt32(dateTime.Year);
        break;
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num = 1 & (this.GetOperandExpression<Operand>(ref exp1, ref this._operand, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = (SQLExpression) new SQLDatePart((IConstant<string>) new UOM(), exp1);
    return num != 0;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FormatPeriodID`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class FormatPeriodID<Direction, PeriodID> : 
  BqlFormulaEvaluator<Direction, PeriodID>,
  IBqlOperand
  where Direction : IBqlOperand
  where PeriodID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    string parameter1 = (string) parameters[typeof (Direction)];
    string parameter2 = (string) parameters[typeof (PeriodID)];
    switch (parameter1)
    {
      case "D":
        return (object) FinPeriodIDFormattingAttribute.FormatForDisplay(parameter2);
      case "S":
        return (object) FinPeriodIDFormattingAttribute.FormatForStoring(parameter2);
      case "E":
        return (object) FinPeriodIDFormattingAttribute.FormatForError(parameter2);
      default:
        return (object) null;
    }
  }
}

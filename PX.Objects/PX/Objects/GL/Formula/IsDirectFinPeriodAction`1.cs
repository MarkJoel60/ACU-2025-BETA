// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Formula.IsDirectFinPeriodAction`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Formula;

public class IsDirectFinPeriodAction<Action> : BqlFormulaEvaluator<Action>, IBqlOperand where Action : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    string parameter = (string) parameters[typeof (Action)];
    return (object) (bool) (parameter == null || parameter == "Undefined" ? 1 : (FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.GetDirection(parameter) == FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Direct ? 1 : 0));
  }
}

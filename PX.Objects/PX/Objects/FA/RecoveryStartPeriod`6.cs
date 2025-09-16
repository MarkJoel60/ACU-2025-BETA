// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.RecoveryStartPeriod`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class RecoveryStartPeriod<StartDate, BookID, DepreciationMethodID, AveragingConvention, MidMonthType, MidMonthDay> : 
  BqlFormulaEvaluator<StartDate, BookID, DepreciationMethodID, AveragingConvention, MidMonthType, MidMonthDay>
  where StartDate : IBqlOperand
  where BookID : IBqlOperand
  where DepreciationMethodID : IBqlOperand
  where AveragingConvention : IBqlOperand
  where MidMonthType : IBqlOperand
  where MidMonthDay : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    try
    {
      return (object) PeriodIDAttribute.FormatForDisplay(DeprCalcParameters.GetRecoveryStartPeriod(cache.Graph, (FABookBalance) item));
    }
    catch (PXException ex)
    {
      throw new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 4);
    }
    catch
    {
      return (object) null;
    }
  }
}

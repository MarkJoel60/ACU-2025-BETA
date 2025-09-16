// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractExpirationDate`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

public class ContractExpirationDate<ContractType, DurationType, StartDate, Duration> : 
  BqlFormulaEvaluator<StartDate, ContractType, DurationType, StartDate, Duration>,
  IBqlOperand
  where ContractType : IBqlField
  where DurationType : IBqlField
  where StartDate : IBqlField
  where Duration : IBqlField
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    DateTime? parameter1 = (DateTime?) parameters[typeof (StartDate)];
    string parameter2 = (string) parameters[typeof (ContractType)];
    string parameter3 = (string) parameters[typeof (DurationType)];
    int? parameter4 = (int?) parameters[typeof (Duration)];
    if (parameter2 != "U" && parameter1.HasValue && !string.IsNullOrEmpty(parameter3) && parameter4.HasValue)
    {
      DateTime dateTime = parameter1.Value;
      switch (parameter3)
      {
        case "A":
          return (object) dateTime.AddYears(parameter4.Value).AddDays(-1.0);
        case "M":
          return (object) dateTime.AddMonths(parameter4.Value).AddDays(-1.0);
        case "Q":
          return (object) dateTime.AddMonths(3 * parameter4.Value).AddDays(-1.0);
        case "C":
          return (object) dateTime.AddDays((double) parameter4.Value).AddDays(-1.0);
      }
    }
    return (object) null;
  }
}

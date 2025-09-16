// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IRateTable
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public interface IRateTable
{
  IList<PMRateDefinition> GetRateDefinitions(string rateTable);

  object Evaluate(PMObjectType objectName, string fieldName, string attribute, PMTran row);

  Decimal? GetPrice(PMTran row);

  Decimal? ConvertAmountToCurrency(
    string fromCuryID,
    string toCuryID,
    string rateType,
    DateTime? effectiveDate,
    Decimal? value);
}

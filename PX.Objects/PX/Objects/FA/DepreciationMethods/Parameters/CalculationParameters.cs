// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.CalculationParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

/// <exclude />
public class CalculationParameters
{
  public int AssetID { get; set; }

  public int BookID { get; set; }

  public FABookBalance BookBalance { get; set; }

  public List<FAAddition> Additions { get; set; }

  public FAAddition OriginalAddition { get; set; }

  public string MaxDepreciateToPeriodID { get; set; }

  public CalculationParameters(IncomingCalculationParameters incomingData, string maxPeriodID = null)
  {
    if (!incomingData.AssetID.HasValue)
      throw new ArgumentNullException(nameof (AssetID));
    if (!incomingData.BookID.HasValue)
      throw new ArgumentNullException(nameof (BookID));
    int? nullable = incomingData.AssetID;
    this.AssetID = nullable.Value;
    nullable = incomingData.BookID;
    this.BookID = nullable.Value;
    this.BookBalance = incomingData.BookBalance;
    this.Additions = incomingData.Additions;
    this.OriginalAddition = this.Additions.First<FAAddition>();
    this.MaxDepreciateToPeriodID = string.IsNullOrEmpty(maxPeriodID) || string.CompareOrdinal(this.BookBalance.DeprToPeriod, maxPeriodID) <= 0 ? this.BookBalance.DeprToPeriod : maxPeriodID;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.ICuryAPHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

public interface ICuryAPHist
{
  Decimal? CuryPtdCrAdjustments { get; set; }

  Decimal? CuryPtdDrAdjustments { get; set; }

  Decimal? CuryPtdPurchases { get; set; }

  Decimal? CuryPtdPayments { get; set; }

  Decimal? CuryPtdDiscTaken { get; set; }

  Decimal? CuryPtdWhTax { get; set; }

  Decimal? CuryYtdBalance { get; set; }

  Decimal? CuryBegBalance { get; set; }

  Decimal? CuryPtdDeposits { get; set; }

  Decimal? CuryYtdDeposits { get; set; }

  Decimal? CuryYtdRetainageReleased { get; set; }

  Decimal? CuryPtdRetainageReleased { get; set; }

  Decimal? CuryYtdRetainageWithheld { get; set; }

  Decimal? CuryPtdRetainageWithheld { get; set; }
}

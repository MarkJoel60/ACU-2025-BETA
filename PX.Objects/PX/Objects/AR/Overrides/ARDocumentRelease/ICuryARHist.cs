// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.ICuryARHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

public interface ICuryARHist
{
  Decimal? CuryPtdCrAdjustments { get; set; }

  Decimal? CuryPtdDrAdjustments { get; set; }

  Decimal? CuryPtdSales { get; set; }

  Decimal? CuryPtdPayments { get; set; }

  Decimal? CuryPtdDiscounts { get; set; }

  Decimal? CuryPtdFinCharges { get; set; }

  Decimal? CuryYtdBalance { get; set; }

  Decimal? CuryBegBalance { get; set; }

  Decimal? CuryPtdDeposits { get; set; }

  Decimal? CuryYtdDeposits { get; set; }

  Decimal? CuryYtdRetainageReleased { get; set; }

  Decimal? CuryPtdRetainageReleased { get; set; }

  Decimal? CuryYtdRetainageWithheld { get; set; }

  Decimal? CuryPtdRetainageWithheld { get; set; }
}

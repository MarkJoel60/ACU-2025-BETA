// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.IBaseAPHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

public interface IBaseAPHist
{
  bool? DetDeleted { get; set; }

  bool? FinFlag { get; set; }

  Decimal? PtdCrAdjustments { get; set; }

  Decimal? PtdDrAdjustments { get; set; }

  Decimal? PtdPurchases { get; set; }

  Decimal? PtdPayments { get; set; }

  Decimal? PtdDiscTaken { get; set; }

  Decimal? PtdWhTax { get; set; }

  Decimal? PtdRGOL { get; set; }

  Decimal? YtdBalance { get; set; }

  Decimal? BegBalance { get; set; }

  Decimal? PtdDeposits { get; set; }

  Decimal? YtdDeposits { get; set; }

  Decimal? YtdRetainageReleased { get; set; }

  Decimal? PtdRetainageReleased { get; set; }

  Decimal? YtdRetainageWithheld { get; set; }

  Decimal? PtdRetainageWithheld { get; set; }
}

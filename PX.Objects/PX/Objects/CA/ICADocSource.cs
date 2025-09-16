// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ICADocSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CA;

public interface ICADocSource
{
  string CuryID { get; }

  int? CashAccountID { get; }

  long? CuryInfoID { get; }

  DateTime? TranDate { get; }

  DateTime? MatchingPaymentDate { get; }

  int? BAccountID { get; }

  int? LocationID { get; }

  string OrigModule { get; }

  Decimal? CuryOrigDocAmt { get; }

  Decimal? CuryChargeAmt { get; }

  string DrCr { get; }

  string ExtRefNbr { get; }

  string TranDesc { get; }

  string FinPeriodID { get; }

  string PaymentMethodID { get; }

  string InvoiceNbr { get; }

  bool? Cleared { get; }

  DateTime? ClearDate { get; }

  int? CARefTranAccountID { get; }

  long? CARefTranID { get; }

  int? CARefSplitLineNbr { get; }

  int? PMInstanceID { get; }

  string EntryTypeID { get; }

  string ChargeDrCr { get; }

  string ChargeTypeID { get; }

  string ChargeTaxZoneID { get; }

  string ChargeTaxCalcMode { get; }

  Guid? NoteID { get; }
}

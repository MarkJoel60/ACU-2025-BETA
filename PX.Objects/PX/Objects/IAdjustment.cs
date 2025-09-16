// Decompiled with JetBrains decompiler
// Type: PX.Objects.IAdjustment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects;

public interface IAdjustment
{
  long? AdjdCuryInfoID { get; set; }

  long? AdjdOrigCuryInfoID { get; set; }

  long? AdjgCuryInfoID { get; set; }

  DateTime? AdjgDocDate { get; set; }

  DateTime? AdjdDocDate { get; set; }

  Decimal? CuryAdjgAmt { get; set; }

  Decimal? CuryAdjgDiscAmt { get; set; }

  Decimal? CuryAdjdAmt { get; set; }

  Decimal? CuryAdjdDiscAmt { get; set; }

  Decimal? AdjAmt { get; set; }

  Decimal? AdjDiscAmt { get; set; }

  Decimal? RGOLAmt { get; set; }

  bool? Released { get; set; }

  bool? Voided { get; set; }

  bool? ReverseGainLoss { get; set; }

  Decimal? CuryDocBal { get; set; }

  Decimal? DocBal { get; set; }

  Decimal? CuryDiscBal { get; set; }

  Decimal? DiscBal { get; set; }

  Decimal? CuryAdjgWhTaxAmt { get; set; }

  Decimal? CuryAdjdWhTaxAmt { get; set; }

  Decimal? AdjWhTaxAmt { get; set; }

  Decimal? CuryWhTaxBal { get; set; }

  Decimal? WhTaxBal { get; set; }
}

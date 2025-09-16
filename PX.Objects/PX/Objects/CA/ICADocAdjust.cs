// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ICADocAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CA;

public interface ICADocAdjust
{
  string AdjdDocType { get; set; }

  string AdjdRefNbr { get; set; }

  Decimal? CuryAdjgAmount { get; set; }

  Decimal? CuryAdjgWhTaxAmt { get; set; }

  Decimal? CuryAdjgDiscAmt { get; set; }

  Decimal? AdjdCuryRate { get; set; }

  bool? PaymentsByLinesAllowed { get; set; }
}

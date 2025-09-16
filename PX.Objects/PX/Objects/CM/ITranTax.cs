// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.ITranTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CM;

public interface ITranTax
{
  string TranType { get; set; }

  string RefNbr { get; set; }

  int? LineNbr { get; set; }

  string TaxID { get; set; }

  Decimal? CuryTaxableAmt { get; set; }

  Decimal? TaxableAmt { get; set; }

  Decimal? CuryTaxAmt { get; set; }

  Decimal? TaxAmt { get; set; }
}

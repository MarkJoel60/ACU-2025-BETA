// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ITaxableDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.TX;

public interface ITaxableDetail
{
  string TaxID { get; }

  Decimal? GroupDiscountRate { get; }

  Decimal? DocumentDiscountRate { get; }

  Decimal? CuryTranAmt { get; }

  Decimal? TranAmt { get; }

  Decimal? CuryRetainageAmt { get; }

  Decimal? RetainageAmt { get; }
}

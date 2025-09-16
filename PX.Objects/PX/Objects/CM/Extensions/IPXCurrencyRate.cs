// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.IPXCurrencyRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public interface IPXCurrencyRate
{
  string FromCuryID { get; set; }

  DateTime? CuryEffDate { get; set; }

  string CuryMultDiv { get; set; }

  Decimal? CuryRate { get; set; }

  Decimal? RateReciprocal { get; set; }

  string ToCuryID { get; set; }
}

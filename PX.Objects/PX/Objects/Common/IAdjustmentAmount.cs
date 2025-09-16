// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.IAdjustmentAmount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// An abstraction that represents the full set of
/// application amounts in base / adjusting document /
/// adjusted document currencies.
/// </summary>
public interface IAdjustmentAmount
{
  Decimal? CuryAdjgAmt { get; set; }

  Decimal? CuryAdjgDiscAmt { get; set; }

  Decimal? CuryAdjdAmt { get; set; }

  Decimal? CuryAdjdDiscAmt { get; set; }

  Decimal? AdjAmt { get; set; }

  Decimal? AdjDiscAmt { get; set; }

  Decimal? RGOLAmt { get; set; }

  bool? ReverseGainLoss { get; }

  /// <summary>
  /// Represents third amount (in adjusting document currency) that is used in balance calculation methods.
  /// In case of APAdjust it is <see cref="!:APAdjust.CuryAdjgWhTaxAmt" />.
  /// In case of ARAdjust it is <see cref="!:ARAdjust.CuryAdjgWOAmt" />
  /// </summary>
  Decimal? CuryAdjgThirdAmount { get; set; }

  /// <summary>
  /// Represents third amount (in adjusted document currency) that is used in balance calculation methods.
  /// In case of APAdjust it is <see cref="!:APAdjust.CuryAdjdWhTaxAmt" />.
  /// In case of ARAdjust it is <see cref="!:ARAdjust.CuryAdjdWOAmt" />
  /// </summary>
  Decimal? CuryAdjdThirdAmount { get; set; }

  /// <summary>
  /// Represents third amount (in base currency) that is used in balance calculation methods.
  /// In case of APAdjust it is <see cref="!:APAdjust.CuryAdjdWhTaxAmt" />.
  /// In case of ARAdjust it is <see cref="!:ARAdjust.CuryAdjdWOAmt" />
  /// </summary>
  Decimal? AdjThirdAmount { get; set; }
}

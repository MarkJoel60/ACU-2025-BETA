// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ICATranDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Common interface for CA transactions like <see cref="T:PX.Objects.CA.CASplit" /> and <see cref="T:PX.Objects.CA.CABankTranDetail" />.
/// </summary>
internal interface ICATranDetail : IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Gets or sets the identifier of the branch.</summary>
  int? BranchID { get; set; }

  /// <summary>Gets or sets the identifier of the account.</summary>
  int? AccountID { get; set; }

  /// <summary>Gets or sets the identifier of the sub account.</summary>
  int? SubID { get; set; }

  /// <summary>Gets or sets the identifier of the cash account.</summary>
  int? CashAccountID { get; set; }

  /// <summary>Gets or sets information describing the transaction.</summary>
  string TranDesc { get; set; }

  /// <summary>
  /// Gets or sets the identifier of the currency information.
  /// </summary>
  long? CuryInfoID { get; set; }

  /// <summary>
  /// Gets or sets the transaction amount specified in currency.
  /// </summary>
  Decimal? CuryTranAmt { get; set; }

  /// <summary>Gets or sets the transaction amount.</summary>
  Decimal? TranAmt { get; set; }

  /// <summary>Gets or sets the quantity.</summary>
  Decimal? Qty { get; set; }

  /// <summary>Gets or sets the unit price.</summary>
  Decimal? UnitPrice { get; set; }

  /// <summary>Gets or sets the unit price specified in currency.</summary>
  Decimal? CuryUnitPrice { get; set; }
}

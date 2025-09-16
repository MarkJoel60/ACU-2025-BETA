// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.CashAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Represents CashAccount Field with Selector that shows all Cash Accounts.
/// </summary>
[PXDBInt]
public class CashAccountAttribute : CashAccountBaseAttribute
{
  /// <summary>
  /// Constructor of the new CashAccountAttribute object with all default parameters.
  /// Doesn't filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification
  /// </summary>
  public CashAccountAttribute()
    : this(false)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountAttribute object with all default parameters except the <paramref name="search" />.
  /// Doesn't filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification
  /// </summary>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountAttribute(Type search)
    : this(false, search: search)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountAttribute object. Doesn't filter by branch.
  /// </summary>
  /// <param name="suppressActiveVerification">True to suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> verification.</param>
  /// <param name="branchID">(Optional) Identifier for the branch.</param>
  /// <param name="search">(Optional) The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountAttribute(bool suppressActiveVerification, Type branchID = null, Type search = null)
    : base(suppressActiveVerification, branchID, search)
  {
  }

  /// <summary>Constructor of the new CashAccountAttribute object.</summary>
  /// <param name="suppressActiveVerification">True to suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> verification.</param>
  /// <param name="branchID">(Optional) Identifier for the branch.</param>
  /// <param name="search">(Optional) The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountAttribute(
    bool suppressActiveVerification,
    bool filterBranch,
    Type branchID = null,
    Type search = null)
    : base(suppressActiveVerification, filterBranch, branchID, search)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountAttribute object. Filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification.
  /// </summary>
  /// <param name="branchID">Identifier for the branch.</param>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  public CashAccountAttribute(Type branchID, Type search)
    : base(branchID, search)
  {
  }

  /// <summary>
  /// Constructor of the new CashAccountAttribute object. Filter by branch, doesn't suppress <see cref="P:PX.Objects.CA.CashAccount.Active" /> status verification.
  /// </summary>
  /// <param name="branchID">Identifier for the branch.</param>
  /// <param name="search">The type of search. Should implement <see cref="T:PX.Data.IBqlSearch" /> or <see cref="T:PX.Data.IBqlSelect" /></param>
  /// <param name="showClearingColumn"> If True, shows the Clearing Account column in a cash account selector</param>
  public CashAccountAttribute(Type branchID, Type search, bool showClearingColumn)
    : base(branchID, search, showClearingColumn)
  {
  }
}

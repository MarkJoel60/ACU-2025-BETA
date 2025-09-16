// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountDisabledBranchRestrictions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

[PXInternalUseOnly]
internal class CashAccountDisabledBranchRestrictions : CashAccountAttribute
{
  public CashAccountDisabledBranchRestrictions()
    : base(false, false)
  {
  }

  public CashAccountDisabledBranchRestrictions(Type branchID, Type search)
    : base(false, false, branchID, search)
  {
  }

  protected override List<Type> GetFilterConditionsList(Type branchID, bool? filterBranch)
  {
    return new List<Type>()
    {
      typeof (Where<,>),
      typeof (CashAccount.branchID),
      typeof (IsNotNull)
    };
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryPrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class GLHistoryPrimaryGraphAttribute : PXPrimaryGraphAttribute
{
  private static readonly Type[] _possibleGraphTypes = new Type[2]
  {
    typeof (AccountByPeriodEnq),
    typeof (GLBudgetEntry)
  };

  public GLHistoryPrimaryGraphAttribute()
    : base(typeof (AccountByPeriodEnq))
  {
    this.Filter = typeof (AccountByPeriodFilter);
  }

  public virtual Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    Type preferedType)
  {
    int? ledgerID = (int?) cache.GetValue<GLHistoryByPeriod.ledgerID>(row);
    if (!ledgerID.HasValue || !(Ledger.PK.Find(cache.Graph, ledgerID)?.BalanceType == "B"))
      return base.GetGraphType(cache, ref row, checkRights, preferedType);
    this.Filter = typeof (BudgetFilter);
    return typeof (GLBudgetEntry);
  }

  public virtual IEnumerable<Type> GetAllGraphTypes()
  {
    return (IEnumerable<Type>) GLHistoryPrimaryGraphAttribute._possibleGraphTypes;
  }
}

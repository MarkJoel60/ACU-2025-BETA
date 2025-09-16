// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.PostGraph.AHAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Overrides.PostGraph;

public class AHAccumAttribute : PXAccumulatorAttribute
{
  protected int? reacct;
  protected int? niacct;

  public AHAccumAttribute()
    : base(new Type[8]
    {
      typeof (GLHistory.finYtdBalance),
      typeof (GLHistory.tranYtdBalance),
      typeof (GLHistory.curyFinYtdBalance),
      typeof (GLHistory.curyTranYtdBalance),
      typeof (GLHistory.finYtdBalance),
      typeof (GLHistory.tranYtdBalance),
      typeof (GLHistory.curyFinYtdBalance),
      typeof (GLHistory.curyTranYtdBalance)
    }, new Type[8]
    {
      typeof (GLHistory.finBegBalance),
      typeof (GLHistory.tranBegBalance),
      typeof (GLHistory.curyFinBegBalance),
      typeof (GLHistory.curyTranBegBalance),
      typeof (GLHistory.finYtdBalance),
      typeof (GLHistory.tranYtdBalance),
      typeof (GLHistory.curyFinYtdBalance),
      typeof (GLHistory.curyTranYtdBalance)
    })
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    GLSetup glSetup = (GLSetup) sender.Graph.Caches[typeof (GLSetup)].Current ?? PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    if (glSetup != null)
    {
      this.reacct = glSetup.RetEarnAccountID;
      this.niacct = glSetup.YtdNetIncAccountID;
    }
    int[] keys = ((IEnumerable<string>) sender.Keys).Select<string, int>((Func<string, int>) (_ => sender.GetFieldOrdinal(_))).ToArray<int>();
    int accountKey = sender.GetFieldOrdinal(typeof (GLHistory.accountID).Name);
    sender.CustomDeadlockComparison = (Comparison<object>) ((a, b) =>
    {
      for (int index = 0; index < keys.Length; ++index)
      {
        object obj1 = sender.GetValue(a, keys[index]);
        object obj2 = sender.GetValue(b, keys[index]);
        if (keys[index] == accountKey)
        {
          if ((!obj1.Equals((object) this.niacct) || !obj2.Equals((object) this.niacct)) && (!obj1.Equals((object) this.reacct) || !obj2.Equals((object) this.reacct)))
          {
            if (obj1.Equals((object) this.niacct))
              return 1;
            if (obj2.Equals((object) this.niacct))
              return -1;
            if (obj1.Equals((object) this.reacct))
              return obj2.Equals((object) this.niacct) ? -1 : 1;
            if (obj2.Equals((object) this.reacct))
              return obj1.Equals((object) this.niacct) ? 1 : -1;
          }
          else
            continue;
        }
        if (obj1 is IComparable && obj2 is IComparable)
        {
          int num = ((IComparable) obj1).CompareTo(obj2);
          if (num != 0)
            return num;
        }
        else if (obj1 == null)
        {
          if (obj2 != null)
            return -1;
        }
        else if (obj2 == null)
          return 1;
      }
      return 0;
    });
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    columns.InitializeFrom<GLHistory.finBegBalance, GLHistory.finYtdBalance>();
    columns.InitializeFrom<GLHistory.tranBegBalance, GLHistory.tranYtdBalance>();
    columns.InitializeFrom<GLHistory.curyFinBegBalance, GLHistory.curyFinYtdBalance>();
    columns.InitializeFrom<GLHistory.curyTranBegBalance, GLHistory.curyTranYtdBalance>();
    columns.InitializeFrom<GLHistory.finYtdBalance, GLHistory.finYtdBalance>();
    columns.InitializeFrom<GLHistory.tranYtdBalance, GLHistory.tranYtdBalance>();
    columns.InitializeFrom<GLHistory.curyFinYtdBalance, GLHistory.curyFinYtdBalance>();
    columns.InitializeFrom<GLHistory.curyTranYtdBalance, GLHistory.curyTranYtdBalance>();
    GLHistory glHistory = (GLHistory) row;
    columns.UpdateFuture<GLHistory.finBegBalance>((object) glHistory.FinYtdBalance);
    columns.UpdateFuture<GLHistory.tranBegBalance>((object) glHistory.TranYtdBalance);
    columns.UpdateFuture<GLHistory.curyFinBegBalance>((object) glHistory.CuryFinYtdBalance);
    columns.UpdateFuture<GLHistory.curyTranBegBalance>((object) glHistory.CuryTranYtdBalance);
    columns.UpdateFuture<GLHistory.finYtdBalance>((object) glHistory.FinYtdBalance);
    columns.UpdateFuture<GLHistory.tranYtdBalance>((object) glHistory.TranYtdBalance);
    columns.UpdateFuture<GLHistory.curyFinYtdBalance>((object) glHistory.CuryFinYtdBalance);
    columns.UpdateFuture<GLHistory.curyTranYtdBalance>((object) glHistory.CuryTranYtdBalance);
    bool? nullable1;
    ref bool? local = ref nullable1;
    int? nullable2 = glHistory.AccountID;
    int? niacct = this.niacct;
    int num = nullable2.GetValueOrDefault() == niacct.GetValueOrDefault() & nullable2.HasValue == niacct.HasValue ? 1 : 0;
    local = new bool?(num != 0);
    bool? nullable3 = nullable1;
    bool flag = false;
    if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
    {
      int? accountId = glHistory.AccountID;
      nullable2 = this.reacct;
      if (!(accountId.GetValueOrDefault() == nullable2.GetValueOrDefault() & accountId.HasValue == nullable2.HasValue))
      {
        PXCache cach = sender.Graph.Caches[typeof (Account)];
        nullable1 = new bool?();
        foreach (Account account in cach.Cached)
        {
          nullable2 = account.AccountID;
          accountId = glHistory.AccountID;
          if (nullable2.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable2.HasValue == accountId.HasValue)
          {
            nullable1 = new bool?(account.Type == "I" || account.Type == "E");
            break;
          }
        }
        if (!nullable1.HasValue)
        {
          Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) glHistory.AccountID
          }));
          nullable1 = account == null ? new bool?(false) : new bool?(account.Type == "I" || account.Type == "E");
        }
      }
    }
    if (nullable1.GetValueOrDefault())
    {
      columns.RestrictPast<GLHistory.finPeriodID>((PXComp) 3, (object) (glHistory.FinPeriodID.Substring(0, 4) + "01"));
      columns.RestrictFuture<GLHistory.finPeriodID>((PXComp) 5, (object) (glHistory.FinPeriodID.Substring(0, 4) + "99"));
    }
    return true;
  }
}

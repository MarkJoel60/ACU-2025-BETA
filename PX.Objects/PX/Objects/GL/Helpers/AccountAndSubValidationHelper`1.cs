// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Helpers.AccountAndSubValidationHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.GL.Helpers;

/// <summary>
/// Validation helper that checks if Account or SubAccount are active
/// </summary>
/// <typeparam name="T"></typeparam>
public class AccountAndSubValidationHelper<T>(PXCache cache, object row) : ValidationHelper<T>(cache, row)
  where T : AccountAndSubValidationHelper<T>
{
  public static bool SetErrorIfInactiveAccount<TField>(PXCache cache, object row, object value) where TField : IBqlField
  {
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      value
    }));
    if (account != null)
    {
      bool? active = account.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        cache.RaiseExceptionHandling<TField>(row, value, (Exception) new PXSetPropertyException("Account is inactive.", (PXErrorLevel) 5, new object[1]
        {
          (object) account.AccountCD
        }));
        return false;
      }
    }
    return true;
  }

  public static bool SetErrorIfInactiveSubAccount<TField>(PXCache cache, object row, object value) where TField : IBqlField
  {
    Sub sub = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      value
    }));
    if (sub != null)
    {
      bool? active = sub.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        cache.RaiseExceptionHandling<TField>(row, value, (Exception) new PXSetPropertyException("Subaccount {0} is inactive.", (PXErrorLevel) 5, new object[1]
        {
          (object) sub.SubCD
        }));
        return false;
      }
    }
    return true;
  }

  public T SetErrorIfInactiveAccount<TField>(object value) where TField : IBqlField
  {
    this.IsValid &= AccountAndSubValidationHelper<T>.SetErrorIfInactiveAccount<TField>(this.Cache, this.Row, value);
    return (T) this;
  }

  public T SetErrorIfInactiveSubAccount<TField>(object value) where TField : IBqlField
  {
    this.IsValid &= AccountAndSubValidationHelper<T>.SetErrorIfInactiveSubAccount<TField>(this.Cache, this.Row, value);
    return (T) this;
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.AddressValidationExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public abstract class AddressValidationExtension<TGraph, TAddress> : 
  PXGraphExtension<TGraph>,
  IAddressValidationHelper
  where TGraph : PXGraph
  where TAddress : class, IBqlTable, IAddress, new()
{
  protected abstract IEnumerable<PXSelectBase<TAddress>> AddressSelects();

  public virtual void _(Events.RowInserted<TAddress> e) => this.StoreCached(e.Row);

  protected virtual void StoreCached(TAddress address)
  {
    foreach (PXSelectBase<TAddress> addressSelect in this.AddressSelects())
    {
      PXCommandKey pxCommandKey = new PXCommandKey(new object[1]
      {
        (object) address.AddressID
      }, true, new bool?());
      addressSelect.StoreCached(pxCommandKey, new List<object>()
      {
        (object) address
      });
    }
  }

  public bool CurrentAddressRequiresValidation
  {
    get
    {
      return this.AddressSelects().Select<PXSelectBase<TAddress>, TAddress>(new Func<PXSelectBase<TAddress>, TAddress>(this.SelectAddress)).Any<TAddress>(new Func<TAddress, bool>(this.RequiresValidation));
    }
  }

  protected virtual TAddress SelectAddress(PXSelectBase<TAddress> selectBase)
  {
    return PXResultset<TAddress>.op_Implicit(selectBase.Select(Array.Empty<object>()));
  }

  protected virtual bool RequiresValidation(TAddress address)
  {
    if ((object) address != null)
    {
      bool? nullable = address.IsDefaultAddress;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        // ISSUE: variable of a boxed type
        __Boxed<TAddress> local = (object) address;
        if (local == null)
          return false;
        nullable = local.IsValidated;
        bool flag2 = false;
        return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
      }
    }
    return false;
  }

  public void ValidateAddress()
  {
    foreach (TAddress aAddress in this.AddressSelects().Select<PXSelectBase<TAddress>, TAddress>(new Func<PXSelectBase<TAddress>, TAddress>(this.SelectAddress)).Where<TAddress>(new Func<TAddress, bool>(this.RequiresValidation)))
      PXAddressValidator.Validate<TAddress>((PXGraph) this.Base, aAddress, true, true);
  }
}

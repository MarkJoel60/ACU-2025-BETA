// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.CashAccountRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

[PXDBString(10, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public sealed class CashAccountRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "CASHACCOUNT";

  public CashAccountRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CASHACCOUNT", typeof (Search2<CashAccount.cashAccountCD, InnerJoin<Account, On<Account.accountID, Equal<CashAccount.accountID>, And2<Match<Account, Current<AccessInfo.userName>>, And<Match<Account, Current<AccessInfo.branchID>>>>>, InnerJoin<Sub, On<Sub.subID, Equal<CashAccount.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>>>>), typeof (CashAccount.cashAccountCD))
    {
      CacheGlobal = true,
      DescriptionField = typeof (CashAccount.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}

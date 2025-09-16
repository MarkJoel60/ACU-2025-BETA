// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
public class PMAccountAttribute : PXEntityAttribute
{
  public PMAccountAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("ACCOUNT", typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<boolTrue>, And<PX.Objects.GL.Account.accountingType, Equal<AccountEntityType.gLAccount>, And2<Where2<Where<Current<PMAccountGroup.type>, Equal<AccountType.asset>, Or<Current<PMAccountGroup.type>, Equal<AccountType.liability>>>, And<Where<PX.Objects.GL.Account.type, Equal<AccountType.asset>, Or<PX.Objects.GL.Account.type, Equal<AccountType.liability>>>>>, Or2<Where<Current<PMAccountGroup.type>, Equal<AccountType.expense>, And<PX.Objects.GL.Account.type, In3<AccountType.expense, AccountType.income, AccountType.asset, AccountType.liability>>>, Or<Where<Current<PMAccountGroup.type>, Equal<AccountType.income>>>>>>>>>), typeof (PX.Objects.GL.Account.accountCD))
    {
      CacheGlobal = true,
      DescriptionField = typeof (PX.Objects.GL.Account.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }
}

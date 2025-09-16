// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.UnboundAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

[PXInt]
[PXUIField]
public class UnboundAccountAttribute : PXEntityAttribute
{
  public const string DimensionName = "ACCOUNT";

  public UnboundAccountAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("ACCOUNT", typeof (Search<Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), typeof (Account.accountCD))
    {
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.DescriptionField = typeof (Account.description);
    this.Filterable = true;
  }
}

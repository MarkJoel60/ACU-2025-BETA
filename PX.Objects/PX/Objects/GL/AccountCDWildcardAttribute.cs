// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountCDWildcardAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

[PXDBString(10, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class AccountCDWildcardAttribute : PXDimensionWildcardAttribute
{
  private int _UIAttrIndex = -1;
  private const string _DimensionName = "ACCOUNT";

  private void Initialize()
  {
    this._UIAttrIndex = -1;
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes)
    {
      if (attribute is PXUIFieldAttribute)
        this._UIAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).IndexOf(attribute);
    }
  }

  public AccountCDWildcardAttribute()
    : base("ACCOUNT", typeof (Search<Account.accountCD, Where<Account.accountingType, Equal<AccountEntityType.gLAccount>>>))
  {
    this.Initialize();
  }

  public AccountCDWildcardAttribute(Type aSearchType)
    : base("ACCOUNT", aSearchType)
  {
    this.Initialize();
  }

  public string DisplayName
  {
    get
    {
      return this._UIAttrIndex != -1 ? ((PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).DisplayName : (string) null;
    }
    set
    {
      if (this._UIAttrIndex == -1)
        return;
      ((PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).DisplayName = value;
    }
  }
}

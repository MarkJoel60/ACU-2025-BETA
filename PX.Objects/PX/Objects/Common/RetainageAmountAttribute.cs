// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RetainageAmountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

[PXUIField]
public class RetainageAmountAttribute : PXAggregateAttribute
{
  protected int _UIAttrIndex = -1;
  protected Type formulaType;
  protected Type verifyType;

  protected PXUIFieldAttribute UIAttribute
  {
    get
    {
      return this._UIAttrIndex != -1 ? (PXUIFieldAttribute) this._Attributes[this._UIAttrIndex] : (PXUIFieldAttribute) null;
    }
  }

  public RetainageAmountAttribute(
    Type retainedAmtFormula,
    Type curyRetainageAmtField,
    Type retainagePctField)
  {
    this.Initialize();
    this.formulaType = BqlCommand.Compose(new Type[26]
    {
      typeof (Switch<,>),
      typeof (Case<,>),
      typeof (Where<,,>),
      typeof (PendingValue<>),
      curyRetainageAmtField,
      typeof (IsPending),
      typeof (And<,>),
      typeof (UnattendedMode),
      typeof (Equal<False>),
      typeof (decimal0),
      typeof (Switch<,>),
      typeof (Case<,>),
      typeof (Where<,,>),
      retainagePctField,
      typeof (LessEqual<decimal100>),
      typeof (And<,>),
      retainagePctField,
      typeof (GreaterEqual<decimal0>),
      typeof (Mult<,>),
      retainedAmtFormula,
      typeof (Div<,>),
      typeof (IsNull<,>),
      retainagePctField,
      typeof (decimal0),
      typeof (decimal100),
      curyRetainageAmtField
    });
    this.verifyType = BqlCommand.Compose(new Type[20]
    {
      typeof (Where<,,>),
      retainedAmtFormula,
      typeof (GreaterEqual<decimal0>),
      typeof (And<,,>),
      curyRetainageAmtField,
      typeof (GreaterEqual<decimal0>),
      typeof (And<,,>),
      curyRetainageAmtField,
      typeof (LessEqual<>),
      retainedAmtFormula,
      typeof (Or<,,>),
      retainedAmtFormula,
      typeof (LessEqual<decimal0>),
      typeof (And<,,>),
      curyRetainageAmtField,
      typeof (LessEqual<decimal0>),
      typeof (And<,>),
      curyRetainageAmtField,
      typeof (GreaterEqual<>),
      retainedAmtFormula
    });
  }

  protected virtual void Initialize()
  {
    this._UIAttrIndex = -1;
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
    {
      if (attribute is PXUIFieldAttribute)
        this._UIAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).IndexOf(attribute);
    }
  }

  public string DisplayName
  {
    get => this.UIAttribute?.DisplayName;
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.DisplayName = value;
    }
  }

  internal static void AssertRetainageAmount(
    Decimal availableRetainageAmount,
    Decimal newRetainageAmountValue)
  {
    if ((Decimal) (Math.Sign(availableRetainageAmount) * Math.Sign(newRetainageAmountValue)) < 0M)
      throw new PXSetPropertyException("The line retainage amount must have the same sign as the line amount.");
    if (Math.Abs(availableRetainageAmount) < Math.Abs(newRetainageAmountValue))
      throw new PXSetPropertyException(Math.Sign(newRetainageAmountValue) > 0 ? "A retainage amount cannot be greater than the line amount ({0})." : "A retainage amount cannot be less than the line amount ({0}).", new object[1]
      {
        (object) availableRetainageAmount
      });
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.RetainagePercentAttribute
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
public class RetainagePercentAttribute : PXAggregateAttribute
{
  protected int _UIAttrIndex = -1;
  protected Type defaultType;
  protected Type verifyType;
  protected Type formulaType;

  protected PXUIFieldAttribute UIAttribute
  {
    get
    {
      return this._UIAttrIndex != -1 ? (PXUIFieldAttribute) this._Attributes[this._UIAttrIndex] : (PXUIFieldAttribute) null;
    }
  }

  public RetainagePercentAttribute(
    Type retainageApplyField,
    Type defRetainagePctField,
    Type retainedAmtFormula,
    Type curyRetainageAmtField,
    Type retainagePctField)
  {
    this.Initialize();
    Type type1;
    if (!typeof (IConstant).IsAssignableFrom(retainageApplyField))
      type1 = BqlCommand.Compose(new Type[2]
      {
        typeof (Current<>),
        retainageApplyField
      });
    else
      type1 = retainageApplyField;
    Type type2 = type1;
    Type type3;
    if (!typeof (IConstant).IsAssignableFrom(defRetainagePctField))
      type3 = BqlCommand.Compose(new Type[2]
      {
        typeof (Current<>),
        defRetainagePctField
      });
    else
      type3 = defRetainagePctField;
    Type type4 = type3;
    this.defaultType = BqlCommand.Compose(new Type[7]
    {
      typeof (Switch<,>),
      typeof (Case<,>),
      typeof (Where<,>),
      type2,
      typeof (Equal<True>),
      type4,
      typeof (decimal0)
    });
    this.formulaType = BqlCommand.Compose(new Type[18]
    {
      typeof (Switch<,>),
      typeof (Case<,>),
      typeof (Where<,>),
      type2,
      typeof (Equal<True>),
      typeof (ExternalValue<>),
      typeof (Switch<,>),
      typeof (Case<,>),
      typeof (Where<,>),
      retainedAmtFormula,
      typeof (NotEqual<decimal0>),
      typeof (Mult<,>),
      typeof (Div<,>),
      curyRetainageAmtField,
      retainedAmtFormula,
      typeof (decimal100),
      typeof (decimal0),
      typeof (decimal0)
    });
    this.verifyType = BqlCommand.Compose(new Type[6]
    {
      typeof (Where<,,>),
      retainagePctField,
      typeof (LessEqual<decimal100>),
      typeof (And<,>),
      retainagePctField,
      typeof (GreaterEqual<decimal0>)
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
}

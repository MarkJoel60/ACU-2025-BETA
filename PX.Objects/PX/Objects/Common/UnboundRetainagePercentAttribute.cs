// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.UnboundRetainagePercentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

public class UnboundRetainagePercentAttribute : RetainagePercentAttribute
{
  public UnboundRetainagePercentAttribute(
    Type retainageApplyField,
    Type defRetainagePctField,
    Type retainedAmtFormula,
    Type curyRetainageAmtField,
    Type retainagePctField)
    : base(retainageApplyField, defRetainagePctField, retainedAmtFormula, curyRetainageAmtField, retainagePctField)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDecimalAttribute(6)
    {
      MinValue = 0.0,
      MaxValue = 100.0
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundDefaultAttribute(TypeCode.Decimal, "0.0", this.defaultType));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUIVerifyAttribute(this.verifyType, (PXErrorLevel) 4, "A retainage percent must be between 0 and 100.", true, Array.Empty<Type>()));
    this._Attributes.Add((PXEventSubscriberAttribute) new UndefaultFormulaAttribute(this.formulaType));
  }
}

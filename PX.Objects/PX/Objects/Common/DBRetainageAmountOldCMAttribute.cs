// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DBRetainageAmountOldCMAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using System;

#nullable disable
namespace PX.Objects.Common;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
public class DBRetainageAmountOldCMAttribute : RetainageAmountAttribute
{
  public DBRetainageAmountOldCMAttribute(
    Type curyInfoIDField,
    Type retainedAmtFormula,
    Type curyRetainageAmtField,
    Type retainageAmtField,
    Type retainagePctField)
    : base(retainedAmtFormula, curyRetainageAmtField, retainagePctField)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBCurrencyAttribute(curyInfoIDField, retainageAmtField));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDefaultAttribute(TypeCode.Decimal, "0.0")
    {
      PersistingCheck = (PXPersistingCheck) 2
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXFormulaAttribute(this.formulaType));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUIVerifyAttribute(this.verifyType, (PXErrorLevel) 4, "The retainage amount must have the same sign as the line amount and must not exceed the available retainage amount.", Array.Empty<Type>()));
  }
}

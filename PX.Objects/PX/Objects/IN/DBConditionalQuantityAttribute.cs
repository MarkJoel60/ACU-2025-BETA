// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DBConditionalQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// The attribute is derived from <see cref="T:PX.Objects.IN.PXDBQuantityAttribute" /> and allows to turn off automatic
/// conversion from the Line Unit of Measure to the Base Unit of Measure on row persisting.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class DBConditionalQuantityAttribute : PXDBQuantityAttribute
{
  protected Type _conditionValueField;
  protected object _expectedValue;

  public DBConditionalQuantityAttribute(
    Type keyField,
    Type resultField,
    Type conditionValueField,
    object expectedValue)
    : base(keyField, resultField)
  {
    this._conditionValueField = conditionValueField ?? throw new PXArgumentException(nameof (conditionValueField));
    this._expectedValue = expectedValue;
  }

  public DBConditionalQuantityAttribute(
    Type keyField,
    Type resultField,
    InventoryUnitType decimalVerifyUnits,
    Type conditionValueField,
    object expectedValue)
    : base(keyField, resultField, decimalVerifyUnits)
  {
    this._conditionValueField = conditionValueField ?? throw new PXArgumentException(nameof (conditionValueField));
    this._expectedValue = expectedValue;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!object.Equals(sender.GetValue(e.Row, this._conditionValueField.Name), this._expectedValue))
      return;
    base.RowPersisting(sender, e);
  }
}

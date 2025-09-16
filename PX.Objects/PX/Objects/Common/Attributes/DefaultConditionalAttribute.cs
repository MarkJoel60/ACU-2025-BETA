// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DefaultConditionalAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
public class DefaultConditionalAttribute : PXDefaultAttribute
{
  protected Type _valueField;
  protected object[] _expectedValues;

  /// <summary>
  /// Initializes a new instance of the DefaultConditionalAttribute attribute.
  /// If the new value is equal to the expected value, then this field will be verified.
  /// </summary>
  /// <param name="valueField">The reference to a field in same DAC. Cannot be null.</param>
  /// <param name="expectedValues">Expected value for "valueField".</param>
  public DefaultConditionalAttribute(Type valueField, params object[] expectedValues)
  {
    this._valueField = valueField ?? throw new PXArgumentException(nameof (valueField));
    this._expectedValues = expectedValues ?? throw new PXArgumentException(nameof (expectedValues));
  }

  /// <summary>
  /// Initializes a new instance of the DefaultConditionalAttribute attribute.
  /// If the new value is equal to the expected value, then this field will be verified.
  /// </summary>
  /// <param name="sourceType">The value will be passed to PXDefaultAttribute constructor as sourceType parameter.</param>
  /// <param name="valueField">The reference to a field in same DAC. Cannot be null.</param>
  /// <param name="expectedValues">Expected value for "valueField".</param>
  public DefaultConditionalAttribute(
    Type sourceType,
    Type valueField,
    params object[] expectedValues)
    : base(sourceType)
  {
    this._valueField = valueField ?? throw new PXArgumentException(nameof (valueField));
    this._expectedValues = expectedValues ?? throw new PXArgumentException(nameof (expectedValues));
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._valueField.Name);
    foreach (object expectedValue in this._expectedValues)
    {
      if (object.Equals(objA, expectedValue))
      {
        base.RowPersisting(sender, e);
        break;
      }
    }
  }
}

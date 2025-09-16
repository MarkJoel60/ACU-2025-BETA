// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectorState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of a <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> input control for a DAC field.</summary>
public class PXSelectorState : PXStringState
{
  /// <summary>The DAC that contains the field whose values the system uses as the selector values.</summary>
  /// <value>The full name of the DAC, such as <tt>PX.Objects.SO.SOOrder</tt>.</value>
  public string SchemaObject { get; protected set; }

  /// <summary>The field whose values the system uses as the selector values.</summary>
  /// <value>The name of the field, such as <tt>OrderType</tt>.</value>
  public string SchemaField { get; protected set; }

  protected PXSelectorState(object value)
    : base(value)
  {
  }

  /// <summary>Configures a <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> field state from the provided parameters.</summary>
  /// <param name="schemeField">The field whose values the system uses as the selector values.</param>
  /// <param name="schemeObj">The DAC that contains the field whose values the system uses as the selector values.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="value">A field state.</param>
  public static PXFieldState CreateInstance(
    PXFieldState value,
    string schemeObj,
    string schemeField,
    string fieldName)
  {
    if (!(value is PXStringState pxStringState))
    {
      PXSelectorState instance = new PXSelectorState((object) value);
      instance.SchemaObject = schemeObj;
      instance.SchemaField = schemeField;
      instance._FieldName = fieldName;
      return (PXFieldState) instance;
    }
    PXSelectorState instance1 = new PXSelectorState((object) pxStringState);
    instance1._AllowedLabels = pxStringState.AllowedLabels;
    instance1._AllowedValues = pxStringState.AllowedValues;
    instance1._InputMask = pxStringState.InputMask;
    instance1._NeutralLabels = pxStringState._NeutralLabels;
    instance1._ExclusiveValues = pxStringState.ExclusiveValues;
    instance1._EmptyPossible = pxStringState.EmptyPossible;
    instance1._MultiSelect = pxStringState.MultiSelect;
    instance1.SchemaObject = schemeObj;
    instance1.SchemaField = schemeField;
    instance1._ValueField = schemeField;
    instance1._FieldName = fieldName;
    return (PXFieldState) instance1;
  }
}

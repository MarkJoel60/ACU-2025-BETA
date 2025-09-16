// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaEditorState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of a <see cref="T:PX.Data.PXFormulaEditor" /> input control for a DAC field.</summary>
public class PXFormulaEditorState : PXStringState
{
  /// <summary>The name for the <see cref="T:PX.Data.PXView" /> object bound to the <see cref="T:PX.Data.PXFormulaEditor" /> field control.</summary>
  public virtual string FormulaEditorViewName { get; set; }

  public PXFormulaEditorState(object value)
    : base(value)
  {
    if (value is PXStringState pxStringState)
    {
      this._AllowedLabels = pxStringState.AllowedLabels;
      this._AllowedValues = pxStringState.AllowedValues;
      this._InputMask = pxStringState.InputMask;
      this._NeutralLabels = pxStringState._NeutralLabels;
      this._ExclusiveValues = pxStringState.ExclusiveValues;
      this._EmptyPossible = pxStringState.EmptyPossible;
      this._MultiSelect = pxStringState.MultiSelect;
    }
    if (!(value is PXFormulaEditorState formulaEditorState))
      return;
    this.FormulaEditorViewName = formulaEditorState.FormulaEditorViewName;
  }

  /// <summary>Configures a <see cref="T:PX.Data.PXFormulaEditor" /> field state from the provided parameters.</summary>
  /// <param name="value">A field state.</param>
  public static PXFieldState CreateInstance(object value, string formulaEditorViewName)
  {
    switch (value)
    {
      case PXFormulaEditorState instance1:
label_4:
        if (formulaEditorViewName != null)
          instance1.FormulaEditorViewName = formulaEditorViewName;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
          return instance2;
        goto default;
      default:
        instance1 = new PXFormulaEditorState(value);
        goto label_4;
    }
  }
}

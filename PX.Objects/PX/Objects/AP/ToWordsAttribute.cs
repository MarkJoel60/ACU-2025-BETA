// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ToWordsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Converts Decimal value to it's word representation (English only) one way only<br />
/// For example, 1921.14 would be converted to "One thousand nine hundred twenty one and fourteen".<br />
/// Should be placed on the string field
/// <example>
/// [ToWords(typeof(APPayment.curyOrigDocAmt))]
/// </example>
/// </summary>
public class ToWordsAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _DecimalField;
  protected short? _Precision;

  public ToWordsAttribute(System.Type DecimalField) => this._DecimalField = DecimalField.Name;

  public ToWordsAttribute(short Precision) => this._Precision = new short?(Precision);

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?((int) byte.MaxValue), new bool?(), this._FieldName, new bool?(), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(false), (string) null);
    object instance;
    if (!string.IsNullOrEmpty(this._DecimalField))
    {
      instance = sender.GetValue(e.Row, this._DecimalField);
      sender.RaiseFieldSelecting(this._DecimalField, e.Row, ref instance, true);
    }
    else
      instance = (object) PXDecimalState.CreateInstance(e.ReturnValue, new int?((int) this._Precision.Value), this._FieldName, new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    if (!(instance is PXDecimalState))
      return;
    if (((PXFieldState) instance).Value == null)
      e.ReturnValue = (object) string.Empty;
    else
      e.ReturnValue = (object) LangEN.ToWords((Decimal) ((PXFieldState) instance).Value, ((PXFieldState) instance).Precision);
  }
}

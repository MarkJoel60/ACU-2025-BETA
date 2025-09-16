// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXUnitPriceCuryConvAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.CM;

public class PXUnitPriceCuryConvAttribute : PXDecimalAttribute, IPXRowSelectedSubscriber
{
  protected int _SourceOrdinal;
  protected int _KeyOrdinal;
  protected Type _KeyField;
  protected Type _SourceField;

  public PXUnitPriceCuryConvAttribute()
  {
  }

  public PXUnitPriceCuryConvAttribute(Type keyField, Type sourceField)
  {
    this._KeyField = keyField;
    this._SourceField = sourceField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Precision = new int?(CommonSetupDecPl.PrcCst);
    if (this._SourceField != (Type) null)
      this._SourceOrdinal = sender.GetFieldOrdinal(this._SourceField.Name);
    if (!(this._KeyField != (Type) null))
      return;
    this._KeyOrdinal = sender.GetFieldOrdinal(this._KeyField.Name);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = BqlCommand.GetItemType(this._KeyField);
    string name = this._KeyField.Name;
    PXUnitPriceCuryConvAttribute curyConvAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) curyConvAttribute, __vmethodptr(curyConvAttribute, KeyFieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  protected virtual void CalcTran(PXCache sender, object data)
  {
    if (!(this._SourceField != (Type) null))
      return;
    object baseval = sender.GetValue(data, this._SourceOrdinal);
    if (baseval == null)
      return;
    Decimal curyval;
    PXCurrencyAttribute.CuryConvCury(sender, data, (Decimal) baseval, out curyval, this._Precision.Value);
    sender.SetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) curyval);
  }

  public virtual void KeyFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CalcTran(sender, e.Row);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
      return;
    this.CalcTran(sender, e.Row);
  }
}

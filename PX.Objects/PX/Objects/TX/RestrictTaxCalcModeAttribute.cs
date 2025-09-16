// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.RestrictTaxCalcModeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public abstract class RestrictTaxCalcModeAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectedSubscriber
{
  protected Type _TaxZoneID;
  protected Type _TaxCalcMode;
  protected Type _OrigModule;

  protected abstract string _restrictedCalcMode { get; }

  protected abstract bool _enableCalcModeField { get; }

  protected abstract Type _ConditionSelect { get; }

  protected RestrictTaxCalcModeAttribute(Type TaxZoneID, Type TaxCalcMode)
  {
    this._TaxZoneID = TaxZoneID;
    this._TaxCalcMode = TaxCalcMode;
  }

  public RestrictTaxCalcModeAttribute(Type TaxZoneID, Type TaxCalcMode, Type OrigModule)
    : this(TaxZoneID, TaxCalcMode)
  {
    this._OrigModule = OrigModule;
  }

  protected virtual bool CheckCondition(PXCache sender, object row)
  {
    if (this._OrigModule != (Type) null && (string) sender.GetValue(row, this._OrigModule.Name) == "EP")
      return false;
    object[] objArray = this.GetParams(sender, row);
    BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
    {
      this._ConditionSelect
    });
    return sender.Graph.TypedViews.GetView(instance, true).SelectSingleBound(new object[1]
    {
      row
    }, objArray) != null;
  }

  protected virtual object[] GetParams(PXCache sender, object row)
  {
    return new object[1]
    {
      sender.GetValue(row, this._TaxZoneID.Name)
    };
  }

  public virtual void CacheAttached(PXCache sender)
  {
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string name = this._TaxZoneID.Name;
    RestrictTaxCalcModeAttribute calcModeAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) calcModeAttribute, __vmethodptr(calcModeAttribute, TaxZoneID_FieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._TaxCalcMode.Name, new PXFieldSelecting((object) this, __methodptr(TaxCalcMode_FieldSelecting)));
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (((bool?) sender.GetValue(e.Row, this._FieldOrdinal)).HasValue)
      return;
    bool flag = sender.GetValue(e.Row, this._TaxZoneID.Name) != null && this.CheckCondition(sender, e.Row);
    sender.SetValue(e.Row, this._FieldOrdinal, (object) flag);
  }

  public virtual void TaxZoneID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool flag = sender.GetValue(e.Row, this._TaxZoneID.Name) != null && this.CheckCondition(sender, e.Row);
    sender.SetValue(e.Row, this._FieldOrdinal, (object) flag);
    if (!flag)
      return;
    sender.SetValueExt(e.Row, this._TaxCalcMode.Name, (object) this._restrictedCalcMode);
  }

  public void TaxCalcMode_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || !((bool?) sender.GetValue(e.Row, this._FieldOrdinal)).GetValueOrDefault())
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(this._enableCalcModeField), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierUnboundUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.CS;

public class CarrierUnboundUnitAttribute : 
  INUnboundUnitAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatedSubscriber
{
  public string UnitType { get; set; }

  public Type SourceUom { get; set; }

  public CarrierUnboundUnitAttribute(Type sourceUom, string unitType)
  {
    this.UnitType = unitType;
    this.SourceUom = sourceUom;
  }

  public CarrierUnboundUnitAttribute(Type sourceUom, string unitType, Type baseUnitType)
    : base(baseUnitType)
  {
    this.UnitType = unitType;
    this.SourceUom = sourceUom;
  }

  private bool ToDisplay(object row) => ((CarrierPlugin) row).UnitType == this.UnitType;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    e.ReturnValue = this.ToDisplay(e.Row) ? sender.GetValue(e.Row, this.SourceUom.Name) : (object) null;
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || !this.ToDisplay(e.Row))
      return;
    sender.SetValue(e.Row, this.SourceUom.Name, sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName));
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.RowSelected(sender, e);
    if (e.Row == null)
      return;
    bool display = this.ToDisplay(e.Row);
    PXUIFieldAttribute.SetVisible(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, display);
    PXUIFieldAttribute.SetVisibility(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, display ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXDefaultAttribute.SetPersistingCheck(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row, display ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }
}

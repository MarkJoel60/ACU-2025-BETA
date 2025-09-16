// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShowValueWhenAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class ShowValueWhenAttribute : 
  PXBaseConditionAttribute,
  IPXFieldSelectingSubscriber,
  IPXRowPersistingSubscriber
{
  private bool _ClearOnPersisting;

  public ShowValueWhenAttribute(Type conditionType, bool clearOnPersisting = false)
    : base(conditionType)
  {
    this._ClearOnPersisting = clearOnPersisting;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || ((PXBaseFormulaBasedAttribute) this)._Formula == null || PXBaseConditionAttribute.GetConditionResult(sender, e.Row, this.Condition))
      return;
    e.ReturnValue = (object) null;
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!this._ClearOnPersisting || PXBaseConditionAttribute.GetConditionResult(sender, e.Row, this.Condition))
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}

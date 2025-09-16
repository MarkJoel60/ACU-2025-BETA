// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DynamicLabelAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public class DynamicLabelAttribute(Type formulaType) : PXBaseFormulaBasedAttribute(formulaType)
{
  protected virtual string LabelField => ((PXEventSubscriberAttribute) this)._FieldName + "_Label";

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXEventSubscriberAttribute) this).CacheAttached(sender);
    sender.Fields.Insert(((List<string>) sender.Fields).IndexOf(((PXEventSubscriberAttribute) this)._FieldName) + 1, this.LabelField);
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    Type itemType = sender.GetItemType();
    string labelField = this.LabelField;
    DynamicLabelAttribute dynamicLabelAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) dynamicLabelAttribute, __vmethodptr(dynamicLabelAttribute, labelFieldSelecting));
    fieldSelecting.AddHandler(itemType, labelField, pxFieldSelecting);
  }

  protected virtual void labelFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(sender.GetStateExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) is PXFieldState) || e.Row == null)
      return;
    Type formula = this.Formula;
    if (formula == (Type) null)
      return;
    string formulaResult = PXBaseFormulaBasedAttribute.GetFormulaResult<string>(sender, e.Row, formula);
    if (string.IsNullOrEmpty(formulaResult))
      return;
    e.ReturnValue = (object) PXMessages.LocalizeNoPrefix(formulaResult);
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttributeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.CS;

public class CSAttributeSelectorAttribute : PXSelectorAttribute
{
  internal const string AttributeDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

  public CSAttributeSelectorAttribute()
    : base(typeof (CSAttribute.attributeID))
  {
    this.DescriptionField = typeof (CSAttribute.description);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    CRAttribute.Attribute attribute1;
    CRAttribute.Attribute attribute2;
    if (!sender.Graph.IsImport && !sender.Graph.IsExport || sender.Graph.IsCopyPasteContext || !(e.ReturnValue is string) || !((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes).TryGetValue((string) e.ReturnValue, ref attribute1) || !((KList<string, CRAttribute.Attribute>) CRAttribute.AttributesByDescr).TryGetValue(attribute1.Description, ref attribute2) || !(attribute1.ID == attribute2.ID))
      return;
    e.ReturnValue = (object) attribute1.Description;
  }

  public virtual void AttributeIDFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    CRAttribute.Attribute attribute;
    if (((KList<string, CRAttribute.Attribute>) CRAttribute.AttributesByDescr).TryGetValue((string) e.NewValue, ref attribute) && !((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes).Contains((string) e.NewValue))
    {
      e.NewValue = (object) attribute.ID;
    }
    else
    {
      if (!((KList<string, CRAttribute.Attribute>) CRAttribute.AttributesByDescr).TryGetValue((string) e.NewValue, ref attribute) || attribute.ID == null || !string.Equals((string) e.NewValue, attribute.ID, StringComparison.InvariantCultureIgnoreCase))
        return;
      e.NewValue = (object) attribute.ID;
    }
  }

  public virtual void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string alias)
  {
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (obj == null)
      return;
    e.ReturnValue = (object) ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[(string) obj].With<CRAttribute.Attribute, string>((Func<CRAttribute.Attribute, string>) (attr => attr.Description));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    System.Type itemType = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    CSAttributeSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) selectorAttribute, __vmethodptr(selectorAttribute, AttributeIDFieldUpdating));
    fieldUpdating.AddHandler(itemType, fieldName, pxFieldUpdating);
  }
}

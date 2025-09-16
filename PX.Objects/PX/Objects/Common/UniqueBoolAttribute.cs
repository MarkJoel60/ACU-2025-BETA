// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.UniqueBoolAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Attribute ensures that there is only one row with "true" value of given field within scope restricted by key value
/// Works only on boolean fields. Key field must have PXParent Attribute on it
/// <param name="key">Key field with PXParentAttribute on it</param>
/// <param name="groupByField">Field for grouping</param>
/// </summary>
public class UniqueBoolAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber
{
  private Type _key;
  private Type _groupByField;
  private PXParentAttribute _parentAttribute;

  public UniqueBoolAttribute(Type key)
  {
    if (key == (Type) null)
      throw new PXArgumentException("scope", "The parameter should not be null.");
    this._key = key.IsNested && typeof (IBqlField).IsAssignableFrom(key) ? key : throw new PXArgumentException("scope", PXMessages.LocalizeFormatNoPrefixNLA("{0} is not a BqlField.", new object[1]
    {
      (object) key.Name
    }));
  }

  public UniqueBoolAttribute(Type key, Type groupByField)
    : this(key)
  {
    this._groupByField = groupByField.IsNested && typeof (IBqlField).IsAssignableFrom(groupByField) ? groupByField : throw new PXArgumentException("scope", PXMessages.LocalizeFormatNoPrefixNLA("{0} is not a BqlField.", new object[1]
    {
      (object) groupByField.Name
    }));
  }

  private void FindParentAttribute(PXCache sender, object row)
  {
    if (this._parentAttribute != null)
      return;
    foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly(row, this._key.Name))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        this._parentAttribute = (PXParentAttribute) subscriberAttribute;
        break;
      }
    }
  }

  private void UpdateOtherSiblings(PXCache sender, object row)
  {
    bool flag1 = false;
    if (this._parentAttribute == null || ((PXFieldState) sender.GetStateExt(row, this._FieldName)).DataType != typeof (bool))
      return;
    bool? nullable = (bool?) sender.GetValue(row, this._FieldName);
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      return;
    foreach (object selectSibling in PXParentAttribute.SelectSiblings(sender, row, this._parentAttribute.ParentType))
    {
      if (!sender.ObjectsEqual(selectSibling, row) && ((bool?) sender.GetValue(selectSibling, this._FieldName)).GetValueOrDefault() && (this._groupByField == (Type) null || object.Equals(sender.GetValue(row, this._groupByField.Name), sender.GetValue(selectSibling, this._groupByField.Name))))
      {
        sender.SetValue(selectSibling, this._FieldName, (object) false);
        sender.Update(selectSibling);
        flag1 = true;
      }
    }
    if (!flag1)
      return;
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) sender.Graph.Views)
    {
      PXView pxView = view.Value;
      if (this._BqlTable.IsAssignableFrom(pxView.GetItemType()))
        pxView.RequestRefresh();
    }
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.FindParentAttribute(sender, e.Row);
    this.UpdateOtherSiblings(sender, e.Row);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.FindParentAttribute(sender, e.Row);
    this.UpdateOtherSiblings(sender, e.Row);
  }
}

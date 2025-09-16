// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DefLocationIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

public class DefLocationIDAttribute : PXAggregateAttribute
{
  private string _DimensionName = "LOCATION";

  public System.Type DescriptionField
  {
    get => this.GetAttribute<PXSelectorAttribute>().DescriptionField;
    set => this.GetAttribute<PXSelectorAttribute>().DescriptionField = value;
  }

  public System.Type SubstituteKey
  {
    get => this.GetAttribute<PXSelectorAttribute>().SubstituteKey;
    set => this.GetAttribute<PXSelectorAttribute>().SubstituteKey = value;
  }

  public DefLocationIDAttribute(System.Type type, params System.Type[] fieldList)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(this._DimensionName)
    {
      ValidComboRequired = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(type, fieldList)
    {
      DirtyRead = true,
      CacheGlobal = false
    });
  }

  public DefLocationIDAttribute(System.Type type)
    : this(type, typeof (Location.locationCD), typeof (Location.descr))
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
    System.Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXSelectorAttribute attribute = this.GetAttribute<PXSelectorAttribute>();
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, SubstituteKeyFieldUpdating));
    fieldUpdating1.RemoveHandler(itemType1, fieldName1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
    System.Type itemType2 = sender.GetItemType();
    string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
    DefLocationIDAttribute locationIdAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) locationIdAttribute, __vmethodptr(locationIdAttribute, FieldUpdating));
    fieldUpdating2.AddHandler(itemType2, fieldName2, pxFieldUpdating2);
  }

  protected virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PXDimensionAttribute attribute1 = this.GetAttribute<PXDimensionAttribute>();
    // ISSUE: virtual method pointer
    new PXFieldUpdating((object) attribute1, __vmethodptr(attribute1, FieldUpdating)).Invoke(sender, e);
    ((CancelEventArgs) e).Cancel = false;
    PXSelectorAttribute attribute2 = this.GetAttribute<PXSelectorAttribute>();
    // ISSUE: virtual method pointer
    new PXFieldUpdating((object) attribute2, __vmethodptr(attribute2, SubstituteKeyFieldUpdating)).Invoke(sender, e);
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) != typeof (IPXFieldUpdatingSubscriber))
      base.GetSubscriber<ISubscriber>(subscribers);
    if (!(this.SubstituteKey == (System.Type) null) && string.Compare(this.SubstituteKey.Name, ((PXEventSubscriberAttribute) this)._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
      return;
    if (typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber))
      subscribers.Remove(this._Attributes[((List<PXEventSubscriberAttribute>) this._Attributes).Count - 2] as ISubscriber);
    if (typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
    {
      subscribers.Remove(this._Attributes[((List<PXEventSubscriberAttribute>) this._Attributes).Count - 2] as ISubscriber);
    }
    else
    {
      if (!(typeof (ISubscriber) == typeof (IPXRowPersistedSubscriber)))
        return;
      subscribers.Remove(this._Attributes[((List<PXEventSubscriberAttribute>) this._Attributes).Count - 2] as ISubscriber);
    }
  }
}

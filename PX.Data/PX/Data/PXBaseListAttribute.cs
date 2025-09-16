// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
[PXAttributeFamily(typeof (PXBaseListAttribute))]
public abstract class PXBaseListAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldDefaultingSubscriber,
  ILocalizableValues
{
  private readonly IPXDBListAttributeHelper _helper;

  protected PXBaseListAttribute(IPXDBListAttributeHelper helper) => this._helper = helper;

  public System.Type DefaultValueField
  {
    get => this._helper.DefaultValueField;
    set => this._helper.DefaultValueField = value;
  }

  public string EmptyLabel
  {
    get => this._helper.EmptyLabel;
    set => this._helper.EmptyLabel = value;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this._helper.FieldSelecting(sender, e, this._AttributeLevel, this._FieldName);
  }

  public Dictionary<object, string> ValueLabelDic(PXGraph graph)
  {
    return this._helper.ValueLabelDic(graph);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(this._helper.DefaultValueField != (System.Type) null))
      return;
    e.NewValue = this._helper.DefaultValue;
  }

  public string Key => this._helper.Key;

  public string[] Values => this._helper.Values;
}

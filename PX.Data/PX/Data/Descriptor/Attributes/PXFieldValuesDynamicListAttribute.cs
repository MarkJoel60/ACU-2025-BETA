// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.Attributes.PXFieldValuesDynamicListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Descriptor.Attributes;

[PXInternalUseOnly]
public class PXFieldValuesDynamicListAttribute : PXAggregateAttribute
{
  protected PXFieldValuesDynamicListAttribute.PXFieldValuesDynamicListInternal DynamicListAttr
  {
    get => (PXFieldValuesDynamicListAttribute.PXFieldValuesDynamicListInternal) this._Attributes[0];
  }

  protected PXDBStringAttribute DBStringAttr => (PXDBStringAttribute) this._Attributes[1];

  public bool ExclusiveValues
  {
    get => this.DynamicListAttr.ExclusiveValues;
    set => this.DynamicListAttr.ExclusiveValues = value;
  }

  public bool IsActive
  {
    get => this.DynamicListAttr.IsActive;
    set => this.DynamicListAttr.IsActive = value;
  }

  public bool IsKey
  {
    get => this.DBStringAttr.IsKey;
    set => this.DBStringAttr.IsKey = value;
  }

  /// <param name="length">The maximum length of a field value.</param>
  /// <param name="graphType">Graph that contains cache with the field.</param>
  /// <param name="fieldType">Field that need to be provided with value.</param>
  public PXFieldValuesDynamicListAttribute(int length)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXFieldValuesDynamicListAttribute.PXFieldValuesDynamicListInternal());
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(length)
    {
      InputMask = "",
      IsUnicode = true
    });
  }

  internal void SetList(PXCache cache, string[] values, string[] labels)
  {
    this.DynamicListAttr.SetList(cache, values, labels);
  }

  protected class PXFieldValuesDynamicListInternal : 
    PXStringListAttribute,
    IPXFieldUpdatingSubscriber
  {
    internal bool IsActive { get; set; } = true;

    internal void SetList(PXCache cache, string[] values, string[] labels)
    {
      cache.SetAltered(this.FieldName, true);
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) EnumerableExtensions.AsSingleEnumerable<PXFieldValuesDynamicListAttribute.PXFieldValuesDynamicListInternal>(this), values, labels, cache);
    }

    public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!this.IsActive || e.NewValue == null)
        return;
      e.NewValue = (object) e.NewValue.ToString();
    }

    public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (this.IsActive)
      {
        if (e.Row == null)
          return;
        e.Cancel = true;
      }
      else
        base.FieldSelecting(sender, e);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldValuesListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Provides correct value type and list for the field in a cache of the specified graph.</summary>
/// <remarks>This attribute inherits from PXAggregateAttribute because it have to specify
/// strict order of calls to event handlers in List and PXDBString attributes.</remarks>
[PXInternalUseOnly]
public class PXFieldValuesListAttribute : PXAggregateAttribute
{
  protected PXFieldValuesListAttribute.PXFieldValuesListInternal ListAttr
  {
    get => (PXFieldValuesListAttribute.PXFieldValuesListInternal) this._Attributes[0];
  }

  protected PXDBStringAttribute DBStringAttr => (PXDBStringAttribute) this._Attributes[1];

  public bool ExclusiveValues
  {
    get => this.ListAttr.ExclusiveValues;
    set => this.ListAttr.ExclusiveValues = value;
  }

  public bool IsActive
  {
    get => this.ListAttr.IsActive;
    set => this.ListAttr.IsActive = value;
  }

  public bool IsKey
  {
    get => this.DBStringAttr.IsKey;
    set => this.DBStringAttr.IsKey = value;
  }

  /// <param name="length">The maximum length of a field value.</param>
  /// <param name="graphType">Graph that contains cache with the field.</param>
  /// <param name="fieldType">Field that need to be provided with value.</param>
  public PXFieldValuesListAttribute(int length, System.Type graphType, System.Type fieldType)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXFieldValuesListAttribute.PXFieldValuesListInternal(graphType, fieldType));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(length)
    {
      InputMask = "",
      IsUnicode = true
    });
  }

  internal void SetList(PXCache cache, string[] values, string[] labels)
  {
    this.ListAttr.SetList(cache, values, labels);
  }

  /// <exclude />
  protected class PXFieldValuesListInternal : PXStringListAttribute, IPXFieldUpdatingSubscriber
  {
    private readonly PXGraph _Graph;
    private readonly System.Type _CacheItemType;
    private readonly string _CacheFieldName;

    internal bool IsActive { get; set; } = true;

    /// <param name="graphType">Graph that contains cache with the field.</param>
    /// <param name="fieldType">Field that need to be provided with value.</param>
    public PXFieldValuesListInternal(System.Type graphType, System.Type fieldType)
    {
      if (fieldType == (System.Type) null)
        throw new PXArgumentException(nameof (fieldType), "The argument cannot be null.");
      if (!fieldType.IsNested || !typeof (IBqlField).IsAssignableFrom(fieldType))
        throw new PXArgumentException(nameof (fieldType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
        {
          (object) fieldType
        });
      this._Graph = PXGraph.CreateInstance(graphType);
      this._CacheItemType = BqlCommand.GetItemType(fieldType);
      this._CacheFieldName = fieldType.Name;
    }

    internal void SetList(PXCache cache, string[] values, string[] labels)
    {
      cache.SetAltered(this.FieldName, true);
      PXStringListAttribute.SetListInternal((IEnumerable<PXStringListAttribute>) EnumerableExtensions.AsSingleEnumerable<PXFieldValuesListAttribute.PXFieldValuesListInternal>(this), values, labels, cache);
    }

    public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!this.IsActive || e.NewValue == null)
        return;
      e.NewValue = (object) e.NewValue.ToString();
    }

    public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (!this.IsActive)
      {
        base.FieldSelecting(sender, e);
      }
      else
      {
        string tableName;
        string fieldName;
        if (e.Row == null || !(sender.Graph.Caches[this._CacheItemType].GetValue(e.Row, this._CacheFieldName) is string fullName) || !PXFieldNamesListAttribute.SplitNames(fullName, out tableName, out fieldName) || !(this._Graph.Caches[tableName]?.GetStateExt((object) null, fieldName) is PXFieldState stateExt))
          return;
        stateExt.Enabled = true;
        stateExt.Visible = true;
        stateExt.DescriptionName = (string) null;
        stateExt.Value = sender.GetValue(e.Row, this.FieldName);
        if (stateExt.Value == null && stateExt.DataType == typeof (bool))
          sender.SetValue(e.Row, this.FieldName, (object) bool.FalseString);
        e.Cancel = true;
        e.ReturnState = (object) stateExt;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingMessagesCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXProcessingMessagesCollection
{
  public const string ParallelProcessingOffset = "PXParallelProcessingOffset";
  private readonly PXProcessingMessagesCollection.ItemBox[] _items;

  protected PXProcessingMessagesCollection()
  {
  }

  public PXProcessingMessagesCollection(int size)
  {
    this._items = new PXProcessingMessagesCollection.ItemBox[size];
    PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
    if (currentSample == null)
      return;
    currentSample.ProcessingCounter = size;
  }

  public PXProcessingMessage this[int index]
  {
    set => this.SetItem(index, value);
  }

  public int Errors { get; private set; }

  public int Warnings { get; private set; }

  public int Processed { get; private set; }

  private void ChangeSummary(PXProcessingMessage item, PXProcessingMessage prev = null)
  {
    lock (this)
    {
      if (prev != null)
      {
        int num = item.ErrorLevel == PXErrorLevel.Error ? 1 : (prev.ErrorLevel == PXErrorLevel.RowError ? 1 : 0);
        bool flag = item.ErrorLevel == PXErrorLevel.Warning || prev.ErrorLevel == PXErrorLevel.RowWarning;
        if (num != 0)
          --this.Errors;
        else if (flag)
          --this.Warnings;
        --this.Processed;
      }
      if (this.Processed >= this.Length)
        return;
      if ((item.ErrorLevel == PXErrorLevel.Error ? 1 : (item.ErrorLevel == PXErrorLevel.RowError ? 1 : 0)) != 0)
        ++this.Errors;
      if ((item.ErrorLevel == PXErrorLevel.Warning ? 1 : (item.ErrorLevel == PXErrorLevel.RowWarning ? 1 : 0)) != 0)
        ++this.Warnings;
      ++this.Processed;
    }
  }

  private void SetItem(int index, PXProcessingMessage value)
  {
    int valueOrDefault = PXContext.GetSlot<int?>("PXParallelProcessingOffset").GetValueOrDefault();
    PXProcessingMessagesCollection.ItemBox nextItem = this._items[index + valueOrDefault];
    if (nextItem == null)
    {
      this._items[index + valueOrDefault] = new PXProcessingMessagesCollection.ItemBox(value);
      this.ChangeSummary(value);
    }
    else if (value.ItemType == (System.Type) null)
    {
      if (nextItem.ItemType == (System.Type) null)
      {
        this.ChangeSummary(value, nextItem.Value);
        nextItem.Value = value;
      }
      else
      {
        this._items[index + valueOrDefault] = new PXProcessingMessagesCollection.ItemBox(value, nextItem);
        this.ChangeSummary(value, nextItem.Value);
      }
    }
    else
    {
      while (!(nextItem.ItemType == value.ItemType))
      {
        PXProcessingMessagesCollection.ItemBox itemBox = nextItem;
        nextItem = nextItem.NextItem;
        if (nextItem == null)
        {
          itemBox.NextItem = new PXProcessingMessagesCollection.ItemBox(value);
          this.ChangeSummary(value);
          return;
        }
      }
      this.ChangeSummary(value, nextItem.Value);
      nextItem.Value = value;
    }
  }

  public PXProcessingMessage Get<TTable>(int index) where TTable : IBqlTable
  {
    int valueOrDefault = PXContext.GetSlot<int?>("PXParallelProcessingOffset").GetValueOrDefault();
    if (index < 0 || index + valueOrDefault >= this._items.Length)
      throw new IndexOutOfRangeException();
    PXProcessingMessagesCollection.ItemBox nextItem = this._items[index + valueOrDefault];
    while (nextItem != null && nextItem.ItemType != (System.Type) null && !nextItem.ItemType.IsAssignableFrom(typeof (TTable)))
      nextItem = nextItem.NextItem;
    return nextItem.With<PXProcessingMessagesCollection.ItemBox, PXProcessingMessage>((Func<PXProcessingMessagesCollection.ItemBox, PXProcessingMessage>) (_ => _.Value));
  }

  public int Length => this._items.Length;

  /// <exclude />
  private class ItemBox
  {
    public PXProcessingMessagesCollection.ItemBox NextItem { get; set; }

    public PXProcessingMessage Value { get; set; }

    public System.Type ItemType
    {
      get
      {
        return this.Value.With<PXProcessingMessage, System.Type>((Func<PXProcessingMessage, System.Type>) (_ => _.ItemType));
      }
    }

    protected ItemBox()
    {
    }

    public ItemBox(PXProcessingMessage value) => this.Value = value;

    public ItemBox(PXProcessingMessage value, PXProcessingMessagesCollection.ItemBox nextItem)
      : this(value)
    {
      this.NextItem = nextItem;
    }
  }
}

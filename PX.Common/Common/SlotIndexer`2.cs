// Decompiled with JetBrains decompiler
// Type: PX.Common.SlotIndexer`2
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common;

public class SlotIndexer<TIndex, TValue>
{
  private readonly Func<TIndex, string> \u0002;

  public SlotIndexer()
  {
  }

  public SlotIndexer(Func<TIndex, string> converter) => this.\u0002 = converter;

  public TValue this[TIndex index]
  {
    get => PXContext.GetSlot<TValue>(this.ToSlotIndex(index));
    set => PXContext.SetSlot<TValue>(this.ToSlotIndex(index), value);
  }

  protected virtual string ToSlotIndex(TIndex input)
  {
    return this.\u0002 == null ? input.ToString() : this.\u0002(input);
  }
}

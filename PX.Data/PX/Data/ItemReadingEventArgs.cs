// Decompiled with JetBrains decompiler
// Type: PX.Data.ItemReadingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class ItemReadingEventArgs
{
  private readonly object _item;
  private readonly bool _deleting;

  public ItemReadingEventArgs(object item, bool deleting)
  {
    this._item = item;
    this._deleting = deleting;
  }

  public object Item => this._item;

  public bool Deleting => this._deleting;
}

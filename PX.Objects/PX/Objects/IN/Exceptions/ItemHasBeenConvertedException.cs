// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Exceptions.ItemHasBeenConvertedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN.Exceptions;

public class ItemHasBeenConvertedException : PXException
{
  public string InventoryCD { get; set; }

  public ItemHasBeenConvertedException(string inventoryCD)
    : base("The document with the {0} item cannot be processed because the stock status of the item has changed.", new object[1]
    {
      (object) inventoryCD?.Trim()
    })
  {
  }

  public ItemHasBeenConvertedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<ItemHasBeenConvertedException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<ItemHasBeenConvertedException>(this, info);
    base.GetObjectData(info, context);
  }
}

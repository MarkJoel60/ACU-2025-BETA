// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXIntercompanyReceivedNotIssuedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN;

public class PXIntercompanyReceivedNotIssuedException : PXException
{
  public PXIntercompanyReceivedNotIssuedException(PXCache cache, IBqlTable row)
    : base("At least one item with a serial number has not yet been issued from a warehouse of the selling company: the serial number {0} for the item {1}. Wait until the selling company issues the item, and then release the inventory receipt.", new object[2]
    {
      PXForeignSelectorAttribute.GetValueExt<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial.lotSerialNbr>(cache, (object) row),
      PXForeignSelectorAttribute.GetValueExt<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial.inventoryID>(cache, (object) row)
    })
  {
  }

  public PXIntercompanyReceivedNotIssuedException(Exception exc, string message, string receiptNbr)
    : base(exc, message, new object[1]
    {
      (object) receiptNbr
    })
  {
  }

  public PXIntercompanyReceivedNotIssuedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}

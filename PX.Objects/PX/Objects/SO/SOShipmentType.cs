// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentType : INDocType
{
  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("I", "Shipment"),
        PXStringListAttribute.Pair("H", "Drop-Shipment"),
        PXStringListAttribute.Pair("T", "Transfer"),
        PXStringListAttribute.Pair("N", "Invoice")
      })
    {
    }
  }

  public class ShortListAttribute : PXStringListAttribute
  {
    public ShortListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("I", "Shipment"),
        PXStringListAttribute.Pair("T", "Transfer")
      })
    {
    }
  }
}

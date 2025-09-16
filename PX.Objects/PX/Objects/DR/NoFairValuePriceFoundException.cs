// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.NoFairValuePriceFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.DR;

public class NoFairValuePriceFoundException : PXException
{
  public NoFairValuePriceFoundException(
    string InventoryCD,
    string UOM,
    string CuryID,
    DateTime DocDate)
    : base("Reallocation Pool data cannot be calculated, because the fair value price cannot be found for the combination of: the {0} item, the {1} UOM, the {2} currency, and the {3} date.", new object[4]
    {
      (object) InventoryCD.Trim(),
      (object) UOM,
      (object) CuryID,
      (object) DocDate.ToShortDateString()
    })
  {
  }

  public NoFairValuePriceFoundException(
    string ComponentCD,
    string InventoryCD,
    string UOM,
    string CuryID,
    DateTime DocDate)
    : base("Reallocation Pool data cannot be calculated, because the fair value price cannot be found for the combination of: the {0} component of the {1} item, the {2} UOM, the {3} currency, and the {4} date.", new object[5]
    {
      (object) ComponentCD,
      (object) InventoryCD.Trim(),
      (object) UOM,
      (object) CuryID,
      (object) DocDate.ToShortDateString()
    })
  {
  }

  public NoFairValuePriceFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}

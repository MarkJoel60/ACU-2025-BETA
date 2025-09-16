// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APCrossItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.AP;

public class APCrossItemAttribute : CrossItemAttribute
{
  public APCrossItemAttribute()
    : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Data.Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), INPrimaryAlternateType.VPN)
  {
  }
}

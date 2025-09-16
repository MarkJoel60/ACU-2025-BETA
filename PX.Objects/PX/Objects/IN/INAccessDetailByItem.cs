// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccessDetailByItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

public class INAccessDetailByItem : INAccessDetailItem
{
  public PXSave<InventoryItem> Save;
  public PXCancel<InventoryItem> Cancel;
  public PXFirst<InventoryItem> First;
  public PXPrevious<InventoryItem> Prev;
  public PXNext<InventoryItem> Next;
  public PXLast<InventoryItem> Last;
}

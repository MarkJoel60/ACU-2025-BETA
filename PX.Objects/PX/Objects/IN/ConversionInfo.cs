// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ConversionInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.IN;

public class ConversionInfo
{
  public INUnit Conversion { get; }

  public short Type => this.Conversion.UnitType.GetValueOrDefault();

  public InventoryItem Inventory { get; }

  public ConversionInfo(INUnit conversion) => this.Conversion = conversion;

  public ConversionInfo(INUnit conversion, InventoryItem inventory)
    : this(conversion)
  {
    this.Inventory = inventory;
  }
}

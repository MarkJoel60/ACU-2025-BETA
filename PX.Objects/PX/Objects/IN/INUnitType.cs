// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INUnitType
{
  public const short Global = 3;
  public const short ItemClass = 2;
  public const short InventoryItem = 1;

  public class global : BqlType<IBqlShort, short>.Constant<
  #nullable disable
  INUnitType.global>
  {
    public global()
      : base((short) 3)
    {
    }
  }

  public class itemClass : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  INUnitType.itemClass>
  {
    public itemClass()
      : base((short) 2)
    {
    }
  }

  public class inventoryItem : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  INUnitType.inventoryItem>
  {
    public inventoryItem()
      : base((short) 1)
    {
    }
  }
}

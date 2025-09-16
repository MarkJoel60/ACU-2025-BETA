// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryMultiplicator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public static class InventoryMultiplicator
{
  public const short NoUpdate = 0;
  public const short Decrease = -1;
  public const short Increase = 1;

  public class noUpdate : BqlType<IBqlShort, short>.Constant<
  #nullable disable
  InventoryMultiplicator.noUpdate>
  {
    public noUpdate()
      : base((short) 0)
    {
    }
  }

  public class decrease : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  InventoryMultiplicator.decrease>
  {
    public decrease()
      : base((short) -1)
    {
    }
  }

  public class increase : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  InventoryMultiplicator.increase>
  {
    public increase()
      : base((short) 1)
    {
    }
  }
}

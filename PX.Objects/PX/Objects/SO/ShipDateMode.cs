// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ShipDateMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public static class ShipDateMode
{
  public const short Today = 0;
  public const short Tomorrow = 1;
  public const short Custom = 2;

  public class today : BqlType<IBqlShort, short>.Constant<
  #nullable disable
  ShipDateMode.today>
  {
    public today()
      : base((short) 0)
    {
    }
  }

  public class tomorrow : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  ShipDateMode.tomorrow>
  {
    public tomorrow()
      : base((short) 1)
    {
    }
  }

  public class custom : BqlType<
  #nullable enable
  IBqlShort, short>.Constant<
  #nullable disable
  ShipDateMode.custom>
  {
    public custom()
      : base((short) 2)
    {
    }
  }
}

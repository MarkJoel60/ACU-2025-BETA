// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ShowAsListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class ShowAsListAttribute : PXIntListAttribute
{
  public const int Free = 1;
  public const int OutOfOffice = 3;
  public const int Busy = 2;
  public const int Tentative = 4;

  public ShowAsListAttribute()
    : base(new int[4]{ 1, 3, 2, 4 }, new string[4]
    {
      nameof (Free),
      "Out of Office",
      nameof (Busy),
      nameof (Tentative)
    })
  {
  }

  public class free : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  ShowAsListAttribute.free>
  {
    public free()
      : base(1)
    {
    }
  }

  public class outOfOffice : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ShowAsListAttribute.outOfOffice>
  {
    public outOfOffice()
      : base(3)
    {
    }
  }

  public class busy : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ShowAsListAttribute.busy>
  {
    public busy()
      : base(2)
    {
    }
  }

  public class tentative : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ShowAsListAttribute.tentative>
  {
    public tentative()
      : base(4)
    {
    }
  }
}

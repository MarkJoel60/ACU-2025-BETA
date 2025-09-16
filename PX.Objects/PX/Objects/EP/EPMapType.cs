// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPMapType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPMapType
{
  public const int Legacy = 0;
  public const int Assignment = 1;
  public const int Approval = 2;

  public class legacy : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  EPMapType.legacy>
  {
    public legacy()
      : base(0)
    {
    }
  }

  public class assignment : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  EPMapType.assignment>
  {
    public assignment()
      : base(1)
    {
    }
  }

  public class approval : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  EPMapType.approval>
  {
    public approval()
      : base(2)
    {
    }
  }
}

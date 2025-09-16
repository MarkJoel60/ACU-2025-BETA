// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAssyType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INAssyType
{
  public const 
  #nullable disable
  string KitTran = "K";
  public const string CompTran = "C";
  public const string OverheadTran = "O";

  public class kitTran : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAssyType.kitTran>
  {
    public kitTran()
      : base("K")
    {
    }
  }

  public class compTran : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAssyType.compTran>
  {
    public compTran()
      : base("C")
    {
    }
  }

  public class overheadTran : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAssyType.overheadTran>
  {
    public overheadTran()
      : base("O")
    {
    }
  }
}

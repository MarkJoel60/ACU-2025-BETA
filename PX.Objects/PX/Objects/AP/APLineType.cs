// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class APLineType
{
  public const 
  #nullable disable
  string LandedCostAP = "LA";
  public const string LandedCostPO = "LP";
  public const string Discount = "DS";

  public class discount : PX.Data.Constant<string>
  {
    public discount()
      : base("DS")
    {
    }
  }

  public class landedcostAP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APLineType.landedcostAP>
  {
    public landedcostAP()
      : base("LA")
    {
    }
  }

  public class landedcostPO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APLineType.landedcostPO>
  {
    public landedcostPO()
      : base("LP")
    {
    }
  }
}

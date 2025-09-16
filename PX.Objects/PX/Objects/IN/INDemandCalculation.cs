// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDemandCalculation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INDemandCalculation
{
  public const 
  #nullable disable
  string ItemClassSettings = "I";
  public const string HardDemand = "H";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("I", "Item Class Settings"),
        PXStringListAttribute.Pair("H", "Hard Demand Only")
      })
    {
    }
  }

  public class itemClassSettings : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INDemandCalculation.itemClassSettings>
  {
    public itemClassSettings()
      : base("I")
    {
    }
  }

  public class hardDemand : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INDemandCalculation.hardDemand>
  {
    public hardDemand()
      : base("H")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAdjustmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class TaxAdjustmentType
{
  public const 
  #nullable disable
  string AdjustOutput = "INT";
  public const string AdjustInput = "RET";
  public const string InputVAT = "VTI";
  public const string OutputVAT = "VTO";
  public const string ReverseInputVAT = "REI";
  public const string ReverseOutputVAT = "REO";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "INT", "RET" }, new string[2]
      {
        "Adjust Output",
        "Adjust Input"
      })
    {
    }
  }

  public class adjustOutput : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentType.adjustOutput>
  {
    public adjustOutput()
      : base("INT")
    {
    }
  }

  public class adjustInput : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentType.adjustInput>
  {
    public adjustInput()
      : base("RET")
    {
    }
  }

  public class inputVAT : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentType.inputVAT>
  {
    public inputVAT()
      : base("VTI")
    {
    }
  }

  public class outputVAT : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TaxAdjustmentType.outputVAT>
  {
    public outputVAT()
      : base("VTO")
    {
    }
  }

  public class reverseInputVAT : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TaxAdjustmentType.reverseInputVAT>
  {
    public reverseInputVAT()
      : base("REI")
    {
    }
  }

  public class reverseOutputVAT : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TaxAdjustmentType.reverseOutputVAT>
  {
    public reverseOutputVAT()
      : base("REO")
    {
    }
  }
}

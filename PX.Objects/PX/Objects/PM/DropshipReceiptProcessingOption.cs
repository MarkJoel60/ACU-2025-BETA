// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DropshipReceiptProcessingOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class DropshipReceiptProcessingOption
{
  public const 
  #nullable disable
  string GenerateReceipt = "R";
  public const string SkipReceipt = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "R", "S" }, new string[2]
      {
        "Generate Receipt",
        "Skip Receipt Generation"
      })
    {
    }
  }

  public class generateReceipt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DropshipReceiptProcessingOption.generateReceipt>
  {
    public generateReceipt()
      : base("R")
    {
    }
  }

  public class skipReceipt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DropshipReceiptProcessingOption.skipReceipt>
  {
    public skipReceipt()
      : base("S")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DropshipExpenseRecordingOption
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
public static class DropshipExpenseRecordingOption
{
  public const 
  #nullable disable
  string OnBillRelease = "B";
  public const string OnReceiptRelease = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "B", "R" }, new string[2]
      {
        "On Bill Release",
        "On Receipt Release"
      })
    {
    }
  }

  public class onBillRelease : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DropshipExpenseRecordingOption.onBillRelease>
  {
    public onBillRelease()
      : base("B")
    {
    }
  }

  public class onReceiptRelease : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DropshipExpenseRecordingOption.onReceiptRelease>
  {
    public onReceiptRelease()
      : base("R")
    {
    }
  }
}

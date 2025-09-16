// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMNoRateOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMNoRateOption
{
  public const string SetOne = "1";
  public const string SetZero = "0";
  public const string RaiseError = "E";
  public const string DontAllocate = "N";

  public class AllocationListAttribute : PXStringListAttribute
  {
    public AllocationListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("1", "Set @Rate to 1"),
        PXStringListAttribute.Pair("0", "Set @Rate to 0"),
        PXStringListAttribute.Pair("E", "Raise Error"),
        PXStringListAttribute.Pair("N", "Do Not Allocate")
      })
    {
    }
  }

  public class BillingListAttribute : PXStringListAttribute
  {
    public BillingListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("1", "Set @Rate to 1"),
        PXStringListAttribute.Pair("0", "Set @Rate to 0"),
        PXStringListAttribute.Pair("E", "Raise Error"),
        PXStringListAttribute.Pair("N", "Do Not Bill")
      })
    {
    }
  }
}

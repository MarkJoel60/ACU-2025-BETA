// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingType
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
public static class PMBillingType
{
  public const 
  #nullable disable
  string Transaction = "T";
  public const string Budget = "B";

  [ExcludeFromCodeCoverage]
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "T", "B" }, new string[2]
      {
        "Time and Material",
        "Progress Billing"
      })
    {
    }
  }

  public class transaction : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMBillingType.transaction>
  {
    public transaction()
      : base("T")
    {
    }
  }

  public class budget : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMBillingType.budget>
  {
    public budget()
      : base("B")
    {
    }
  }
}

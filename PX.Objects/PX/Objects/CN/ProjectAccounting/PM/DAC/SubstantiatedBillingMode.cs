// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.DAC.SubstantiatedBillingMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.PM.DAC;

public static class SubstantiatedBillingMode
{
  public const string DateOnly = "D";
  public const string ProFormaOnly = "P";
  public const string ProFormaAndDate = "B";

  public static string[] StringListValues
  {
    get => new string[3]{ "D", "P", "B" };
  }

  public static string[] StringListLabels
  {
    get
    {
      return new string[3]
      {
        "Date Range",
        "Pro Forma Invoice",
        "Pro Forma Invoice and Date Range"
      };
    }
  }

  public class dateOnly : BqlType<IBqlString, string>.Constant<SubstantiatedBillingMode.dateOnly>
  {
    public dateOnly()
      : base("D")
    {
    }
  }

  public class proFormaOnly : 
    BqlType<IBqlString, string>.Constant<SubstantiatedBillingMode.proFormaOnly>
  {
    public proFormaOnly()
      : base("P")
    {
    }
  }

  public class proFormaAndDate : 
    BqlType<IBqlString, string>.Constant<SubstantiatedBillingMode.proFormaAndDate>
  {
    public proFormaAndDate()
      : base("B")
    {
    }
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(SubstantiatedBillingMode.StringListValues, SubstantiatedBillingMode.StringListLabels)
    {
    }
  }
}

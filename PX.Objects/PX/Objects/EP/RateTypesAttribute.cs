// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RateTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class RateTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Hourly = "H";
  public const string Salary = "S";
  public const string SalaryWithExemption = "E";

  public RateTypesAttribute()
    : base(new string[3]{ "H", "S", "E" }, new string[3]
    {
      nameof (Hourly),
      "Annual Non-Exempt",
      "Annual Exempt"
    })
  {
  }

  public class hourly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RateTypesAttribute.hourly>
  {
    public hourly()
      : base("H")
    {
    }
  }

  public class salary : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RateTypesAttribute.salary>
  {
    public salary()
      : base("S")
    {
    }
  }

  public class salaryWithExemption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RateTypesAttribute.salaryWithExemption>
  {
    public salaryWithExemption()
      : base("E")
    {
    }
  }
}

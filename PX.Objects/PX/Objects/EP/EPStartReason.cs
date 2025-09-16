// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPStartReason
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPStartReason
{
  public const 
  #nullable disable
  string New = "NEW";
  public const string Rehire = "REH";
  public const string Promotion = "PRO";
  public const string Demotion = "DEM";
  public const string NewSkills = "SKI";
  public const string Reorganization = "REO";
  public const string Other = "OTH";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[7]
      {
        "NEW",
        "REH",
        "PRO",
        "DEM",
        "SKI",
        "REO",
        "OTH"
      }, new string[7]
      {
        "New Hire",
        "Rehire",
        "Promotion",
        "Demotion",
        "New Skills",
        "Reorganization",
        "Other"
      })
    {
    }
  }

  public class newStatus : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.newStatus>
  {
    public newStatus()
      : base("NEW")
    {
    }
  }

  public class rehire : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.rehire>
  {
    public rehire()
      : base("REH")
    {
    }
  }

  public class promotion : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.promotion>
  {
    public promotion()
      : base("PRO")
    {
    }
  }

  public class demotion : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.demotion>
  {
    public demotion()
      : base("DEM")
    {
    }
  }

  public class newSkills : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.newSkills>
  {
    public newSkills()
      : base("SKI")
    {
    }
  }

  public class reorganization : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.reorganization>
  {
    public reorganization()
      : base("REO")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPStartReason.other>
  {
    public other()
      : base("OTH")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationContactType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class NotificationContactType
{
  public const 
  #nullable disable
  string Employee = "E";
  public const string Contact = "C";
  public const string Primary = "P";
  public const string Remittance = "R";
  public const string Shipping = "S";
  public const string Billing = "B";

  public class employee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.employee>
  {
    public employee()
      : base("E")
    {
    }
  }

  public class contact : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.contact>
  {
    public contact()
      : base("C")
    {
    }
  }

  public class primary : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.primary>
  {
    public primary()
      : base("P")
    {
    }
  }

  public class remittance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.remittance>
  {
    public remittance()
      : base("R")
    {
    }
  }

  public class shipping : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.shipping>
  {
    public shipping()
      : base("S")
    {
    }
  }

  public class billing : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationContactType.billing>
  {
    public billing()
      : base("B")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "P", "B", "S", "R", "E", "C" }, new string[6]
      {
        "Account Email",
        "Billing",
        "Account Location Email",
        "Remittance",
        "Employee",
        "Contact"
      })
    {
    }
  }

  public class ProjectListAttribute : PXStringListAttribute
  {
    public ProjectListAttribute()
      : base(new string[3]{ "P", "E", "C" }, new string[3]
      {
        "Account Email",
        "Employee",
        "Contact"
      })
    {
    }
  }

  public class ProjectTemplateListAttribute : PXStringListAttribute
  {
    public ProjectTemplateListAttribute()
      : base(new string[2]{ "P", "E" }, new string[2]
      {
        "Account Email",
        "Employee"
      })
    {
    }
  }
}

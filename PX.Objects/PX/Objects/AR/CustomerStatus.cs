// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

public class CustomerStatus
{
  public const 
  #nullable disable
  string Prospect = "R";
  public const string Active = "A";
  public const string Hold = "H";
  public const string CreditHold = "C";
  public const string OneTime = "T";
  public const string Inactive = "I";
  public const string Initial = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("C", "Credit Hold"),
        PXStringListAttribute.Pair("T", "One-Time"),
        PXStringListAttribute.Pair("I", "Inactive")
      })
    {
    }
  }

  public class BusinessAccountNonCustomerListAttribute : PXStringListAttribute
  {
    public BusinessAccountNonCustomerListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("R", "Prospect"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("C", "Credit Hold"),
        PXStringListAttribute.Pair("T", "One-Time"),
        PXStringListAttribute.Pair("I", "Inactive")
      })
    {
    }
  }

  public class BusinessAccountListAttribute : PXStringListAttribute
  {
    public BusinessAccountListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("R", "Prospect"),
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("I", "Inactive")
      })
    {
    }
  }

  public class prospect : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.prospect>
  {
    public prospect()
      : base("R")
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class creditHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.creditHold>
  {
    public creditHold()
      : base("C")
    {
    }
  }

  public class oneTime : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.oneTime>
  {
    public oneTime()
      : base("T")
    {
    }
  }

  public class inactive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CustomerStatus.inactive>
  {
    public inactive()
      : base("I")
    {
    }
  }
}

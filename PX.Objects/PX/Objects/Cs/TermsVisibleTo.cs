// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsVisibleTo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class TermsVisibleTo
{
  public const 
  #nullable disable
  string All = "AL";
  public const string Vendor = "VE";
  public const string Customer = "CU";
  public const string Disabled = "DS";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "AL", "VE", "CU", "DS" }, new string[4]
      {
        "All",
        "Vendors",
        "Customers",
        "Disabled"
      })
    {
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsVisibleTo.all>
  {
    public all()
      : base("AL")
    {
    }
  }

  public class vendor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsVisibleTo.vendor>
  {
    public vendor()
      : base("VE")
    {
    }
  }

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsVisibleTo.customer>
  {
    public customer()
      : base("CU")
    {
    }
  }

  public class disabled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsVisibleTo.disabled>
  {
    public disabled()
      : base("DS")
    {
    }
  }
}

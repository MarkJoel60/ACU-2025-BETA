// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocTypeList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class LocTypeList
{
  public const 
  #nullable disable
  string CompanyLoc = "CP";
  public const string VendorLoc = "VE";
  public const string CustomerLoc = "CU";
  public const string CombinedLoc = "VC";
  public const string EmployeeLoc = "EP";

  public static bool ActsAsVendor(string locationType)
  {
    return locationType == "VE" || locationType == "VC" || locationType == "EP";
  }

  public static bool ActsAsCustomer(string locationType)
  {
    return locationType == "CU" || locationType == "VC";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "CP", "VE", "CU", "VC", "EP" }, new string[5]
      {
        "Company",
        "Vendor",
        "Customer",
        "Customer & Vendor",
        "Employee"
      })
    {
    }
  }

  public class companyLoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocTypeList.companyLoc>
  {
    public companyLoc()
      : base("CP")
    {
    }
  }

  public class vendorLoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocTypeList.vendorLoc>
  {
    public vendorLoc()
      : base("VE")
    {
    }
  }

  public class customerLoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocTypeList.customerLoc>
  {
    public customerLoc()
      : base("CU")
    {
    }
  }

  public class combinedLoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocTypeList.combinedLoc>
  {
    public combinedLoc()
      : base("VC")
    {
    }
  }

  public class employeeLoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocTypeList.employeeLoc>
  {
    public employeeLoc()
      : base("EP")
    {
    }
  }
}

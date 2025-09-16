// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CR;

public class BAccountType
{
  public static 
  #nullable disable
  Dictionary<string, string> BAccountTypes = new Dictionary<string, string>()
  {
    {
      "VE",
      "Vendor"
    },
    {
      "CU",
      "Customer"
    },
    {
      "VC",
      "Customer & Vendor"
    },
    {
      "PR",
      "Prospect"
    },
    {
      "CP",
      "Branch"
    },
    {
      "EP",
      "Employee"
    },
    {
      "EC",
      "Customer & Employee"
    },
    {
      "OR",
      "Company"
    }
  };
  public const string VendorType = "VE";
  public const string CustomerType = "CU";
  public const string CombinedType = "VC";
  public const string EmployeeType = "EP";
  public const string EmpCombinedType = "EC";
  public const string ProspectType = "PR";
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public const string CompanyType = "CP";
  public const string BranchType = "CP";
  public const string OrganizationType = "OR";
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  public const string OrganizationBranchCombinedType = "OB";

  public static bool ActsAsVendor(string bAccountType)
  {
    return bAccountType == "VE" || bAccountType == "VC" || bAccountType == "EP" || bAccountType == "EC";
  }

  public static bool ActsAsCustomer(string bAccountType)
  {
    return bAccountType == "CU" || bAccountType == "VC" || bAccountType == "EC";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "VE", "CU", "VC", "PR", "CP" }, new string[5]
      {
        "Vendor",
        "Customer",
        "Customer & Vendor",
        "Prospect",
        "Branch"
      })
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      if (!sender.Graph.IsContractBasedAPI)
      {
        this._AllowedLabels = new string[5]
        {
          "Vendor",
          "Customer",
          "Customer & Vendor",
          "Business Account",
          "Branch"
        };
        this._NeutralAllowedLabels = this._AllowedLabels;
      }
      base.CacheAttached(sender);
    }
  }

  public class SalesPersonTypeListAttribute : PXStringListAttribute
  {
    public SalesPersonTypeListAttribute()
      : base(new string[2]{ "VE", "EP" }, new string[2]
      {
        "Vendor",
        "Employee"
      })
    {
    }
  }

  public class vendorType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.vendorType>
  {
    public vendorType()
      : base("VE")
    {
    }
  }

  public class customerType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.customerType>
  {
    public customerType()
      : base("CU")
    {
    }
  }

  public class combinedType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.combinedType>
  {
    public combinedType()
      : base("VC")
    {
    }
  }

  public class employeeType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.employeeType>
  {
    public employeeType()
      : base("EP")
    {
    }
  }

  public class empCombinedType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.empCombinedType>
  {
    public empCombinedType()
      : base("EC")
    {
    }
  }

  public class prospectType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.prospectType>
  {
    public prospectType()
      : base("PR")
    {
    }
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public class companyType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.companyType>
  {
    public companyType()
      : base("CP")
    {
    }
  }

  public class branchType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.branchType>
  {
    public branchType()
      : base("CP")
    {
    }
  }

  public class organizationType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BAccountType.organizationType>
  {
    public organizationType()
      : base("OR")
    {
    }
  }
}

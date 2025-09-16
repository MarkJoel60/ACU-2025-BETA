// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTPRType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CT;

public class CTPRType
{
  public const 
  #nullable disable
  string ContractModule = "CT";
  public const string Contract = "C";
  public const string Project = "P";
  public const string ContractTemplate = "T";
  public const string ProjectTemplate = "R";

  public static bool IsTemplate(string baseType) => baseType == "T" || baseType == "R";

  public class contract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CTPRType.contract>
  {
    [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
    public const string Contract = "C";

    public contract()
      : base("C")
    {
    }
  }

  public class project : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CTPRType.project>
  {
    [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
    public const string Project = "P";

    public project()
      : base("P")
    {
    }
  }

  public class contractTemplate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CTPRType.contractTemplate>
  {
    [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
    public const string ContractTemplate = "T";

    public contractTemplate()
      : base("T")
    {
    }
  }

  public class projectTemplate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CTPRType.projectTemplate>
  {
    [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
    public const string ProjectTemplate = "R";

    public projectTemplate()
      : base("R")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "C", "P", "T", "R" }, new string[4]
      {
        "Contract",
        "Project",
        "Contract Template",
        "Project Template"
      })
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRoleTypeList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class CRRoleTypeList
{
  public const 
  #nullable disable
  string BusinessUser = "BU";
  public const string DecisionMaker = "DM";
  public const string Evaluator = "EV";
  public const string SupportEngineer = "SE";
  public const string Supervisor = "SV";
  public const string TechnicalExpert = "TE";
  public const string RelatedEntity = "RE";
  public const string Referrer = "RF";
  public const string Source = "SR";
  public const string Derivative = "DE";
  public const string Parent = "PR";
  public const string Child = "CH";
  public const string Licensee = "AL";

  public class FullListAttribute : PXStringListAttribute
  {
    public FullListAttribute()
      : base(new string[12]
      {
        "BU",
        "CH",
        "DE",
        "DM",
        "EV",
        "PR",
        "RE",
        "RF",
        "SE",
        "SR",
        "SV",
        "TE"
      }, new string[12]
      {
        "Business User",
        "Child",
        "Derivative",
        "Decision-Maker",
        "Evaluator",
        "Parent",
        "Related Entity",
        "Referrer",
        "Support Engineer",
        "Source",
        "Supervisor",
        "Technical Expert"
      })
    {
    }
  }

  public class ShortListAttribute : PXStringListAttribute
  {
    public ShortListAttribute()
      : base(new string[5]{ "CH", "DE", "PR", "RE", "SR" }, new string[5]
      {
        "Child",
        "Derivative",
        "Parent",
        "Related Entity",
        "Source"
      })
    {
    }
  }

  public sealed class referrer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.referrer>
  {
    public referrer()
      : base("RF")
    {
    }
  }

  public sealed class supervisor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.supervisor>
  {
    public supervisor()
      : base("SV")
    {
    }
  }

  public sealed class businessUser : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRRoleTypeList.businessUser>
  {
    public businessUser()
      : base("BU")
    {
    }
  }

  public sealed class decisionMaker : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRRoleTypeList.decisionMaker>
  {
    public decisionMaker()
      : base("DM")
    {
    }
  }

  public sealed class relatedEntity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRRoleTypeList.relatedEntity>
  {
    public relatedEntity()
      : base("RE")
    {
    }
  }

  public sealed class technicalExpert : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRRoleTypeList.technicalExpert>
  {
    public technicalExpert()
      : base("TE")
    {
    }
  }

  public sealed class supportEngineer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRRoleTypeList.supportEngineer>
  {
    public supportEngineer()
      : base("SE")
    {
    }
  }

  public sealed class evaluator : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.evaluator>
  {
    public evaluator()
      : base("EV")
    {
    }
  }

  public sealed class licensee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.licensee>
  {
    public licensee()
      : base("AL")
    {
    }
  }

  public sealed class source : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.source>
  {
    public source()
      : base("SR")
    {
    }
  }

  public sealed class derivative : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.derivative>
  {
    public derivative()
      : base("DE")
    {
    }
  }

  public sealed class parent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.parent>
  {
    public parent()
      : base("PR")
    {
    }
  }

  public sealed class child : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRRoleTypeList.child>
  {
    public child()
      : base("CH")
    {
    }
  }
}

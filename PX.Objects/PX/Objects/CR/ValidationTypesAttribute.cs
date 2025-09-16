// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ValidationTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class ValidationTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string LeadToLead = "LL";
  public const string LeadToContact = "LC";
  public const string ContactToContact = "CC";
  public const string ContactToLead = "CL";
  public const string LeadToAccount = "LA";
  public const string ContactToAccount = "CA";
  public const string AccountToAccount = "AA";

  public ValidationTypesAttribute()
    : base(new (string, string)[7]
    {
      ("LL", "Lead to Lead"),
      ("LC", "Lead to Contact"),
      ("CC", "Contact to Contact"),
      ("CL", "Contact to Lead"),
      ("LA", "Lead to Account"),
      ("CA", "Contact to Account"),
      ("AA", "Account to Account")
    })
  {
  }

  public sealed class leadToLead : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.leadToLead>
  {
    public leadToLead()
      : base("LL")
    {
    }
  }

  public sealed class leadToContact : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.leadToContact>
  {
    public leadToContact()
      : base("LC")
    {
    }
  }

  public sealed class contactToContact : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.contactToContact>
  {
    public contactToContact()
      : base("CC")
    {
    }
  }

  public sealed class contactToLead : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.contactToLead>
  {
    public contactToLead()
      : base("CL")
    {
    }
  }

  public sealed class leadToAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.leadToAccount>
  {
    public leadToAccount()
      : base("LA")
    {
    }
  }

  public sealed class contactToAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.contactToAccount>
  {
    public contactToAccount()
      : base("CA")
    {
    }
  }

  public sealed class accountToAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ValidationTypesAttribute.accountToAccount>
  {
    public accountToAccount()
      : base("AA")
    {
    }
  }
}

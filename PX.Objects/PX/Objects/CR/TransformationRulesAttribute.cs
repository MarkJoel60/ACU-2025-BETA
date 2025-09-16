// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.TransformationRulesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class TransformationRulesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string DomainName = "DN";
  public const string None = "NO";
  public const string SplitWords = "SW";
  public const string SplitEmailAddresses = "SE";

  public TransformationRulesAttribute()
    : base(new string[4]{ "DN", "NO", "SW", "SE" }, new string[4]
    {
      "Domain Name",
      nameof (None),
      "Split Words",
      "Split Email Addresses"
    })
  {
  }

  public sealed class domainName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TransformationRulesAttribute.domainName>
  {
    public domainName()
      : base("DN")
    {
    }
  }

  public sealed class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TransformationRulesAttribute.none>
  {
    public none()
      : base("NO")
    {
    }
  }

  public sealed class splitWords : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TransformationRulesAttribute.splitWords>
  {
    public splitWords()
      : base("SW")
    {
    }
  }

  public sealed class splitEmailAddresses : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TransformationRulesAttribute.splitEmailAddresses>
  {
    public splitEmailAddresses()
      : base("SE")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DuplicateStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class DuplicateStatusAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string NotValidated = "NV";
  public const string PossibleDuplicated = "PD";
  public const string Validated = "VA";
  public const string Duplicated = "DD";

  public DuplicateStatusAttribute()
    : base(new string[4]{ "NV", "VA", "PD", "DD" }, new string[4]
    {
      "Not Validated",
      nameof (Validated),
      "Possible Duplicate",
      nameof (Duplicated)
    })
  {
  }

  public sealed class notValidated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DuplicateStatusAttribute.notValidated>
  {
    public notValidated()
      : base("NV")
    {
    }
  }

  public sealed class possibleDuplicated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DuplicateStatusAttribute.possibleDuplicated>
  {
    public possibleDuplicated()
      : base("PD")
    {
    }
  }

  public sealed class duplicated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DuplicateStatusAttribute.duplicated>
  {
    public duplicated()
      : base("DD")
    {
    }
  }

  public sealed class validated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DuplicateStatusAttribute.validated>
  {
    public validated()
      : base("VA")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.SM.PersonNameFormatsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class PersonNameFormatsAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string LEGACY = "LEGACY";
  public const string WESTERN = "WESTERN";
  public const string EASTERN = "EASTERN";
  public const string EASTERN_WITH_TITLE = "EASTERN_WITH_TITLE";

  public PersonNameFormatsAttribute()
    : base(new string[4]
    {
      nameof (LEGACY),
      nameof (WESTERN),
      nameof (EASTERN),
      nameof (EASTERN_WITH_TITLE)
    }, new string[4]
    {
      "Eastern Modified",
      "Western",
      "Eastern",
      "Eastern with Title and Middle Name"
    })
  {
  }

  public class legacy : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PersonNameFormatsAttribute.legacy>
  {
    public legacy()
      : base("LEGACY")
    {
    }
  }

  public class western : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PersonNameFormatsAttribute.western>
  {
    public western()
      : base("WESTERN")
    {
    }
  }

  public class eastern : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PersonNameFormatsAttribute.eastern>
  {
    public eastern()
      : base("EASTERN")
    {
    }
  }

  public class easternWithTitle : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PersonNameFormatsAttribute.easternWithTitle>
  {
    public easternWithTitle()
      : base("EASTERN_WITH_TITLE")
    {
    }
  }
}

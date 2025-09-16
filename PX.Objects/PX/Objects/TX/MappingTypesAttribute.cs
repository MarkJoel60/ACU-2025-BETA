// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.MappingTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class MappingTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string OneOrMoreCountires = "C";
  public const string OneOrMoreStates = "S";
  public const string OneOrMorePostalCodes = "P";

  public MappingTypesAttribute()
    : base(new string[3]{ "C", "S", "P" }, new string[3]
    {
      "Countries",
      "States",
      "Postal Codes"
    })
  {
  }

  public class oneOrMoreCountires : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MappingTypesAttribute.oneOrMoreCountires>
  {
    public oneOrMoreCountires()
      : base("C")
    {
    }
  }

  public class oneOrMoreStates : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MappingTypesAttribute.oneOrMoreStates>
  {
    public oneOrMoreStates()
      : base("S")
    {
    }
  }

  public class oneOrMorePostalCodes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MappingTypesAttribute.oneOrMorePostalCodes>
  {
    public oneOrMorePostalCodes()
      : base("P")
    {
    }
  }
}

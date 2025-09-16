// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PhoneTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PhoneTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Business1 = "B1";
  public const string Business2 = "B2";
  public const string Business3 = "B3";
  public const string BusinessAssistant1 = "BA1";
  public const string BusinessFax = "BF";
  public const string Home = "H1";
  public const string HomeFax = "HF";
  public const string Cell = "C";

  public PhoneTypesAttribute()
    : base(new string[8]
    {
      "B1",
      "B2",
      "B3",
      "C",
      "BA1",
      "BF",
      "H1",
      "HF"
    }, new string[8]
    {
      "Business 1",
      "Business 2",
      "Business 3",
      nameof (Cell),
      "Assistant",
      "Fax",
      nameof (Home),
      "Home Fax"
    })
  {
  }

  public class business1 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.business1>
  {
    public business1()
      : base("B1")
    {
    }
  }

  public class business2 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.business2>
  {
    public business2()
      : base("B2")
    {
    }
  }

  public class business3 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.business3>
  {
    public business3()
      : base("B3")
    {
    }
  }

  public class businessAssistant1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PhoneTypesAttribute.businessAssistant1>
  {
    public businessAssistant1()
      : base("BA1")
    {
    }
  }

  public class businessFax : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.businessFax>
  {
    public businessFax()
      : base("BF")
    {
    }
  }

  public class home : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.home>
  {
    public home()
      : base("H1")
    {
    }
  }

  public class homeFax : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.homeFax>
  {
    public homeFax()
      : base("HF")
    {
    }
  }

  public class cell : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PhoneTypesAttribute.cell>
  {
    public cell()
      : base("C")
    {
    }
  }
}

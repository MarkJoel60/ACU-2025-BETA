// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactMethodsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class CRContactMethodsAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Any = "A";
  public const string Email = "E";
  public const string Mail = "M";
  public const string Fax = "F";
  public const string Phone = "P";

  public CRContactMethodsAttribute()
    : base(new string[5]{ "A", "E", "M", "F", "P" }, new string[5]
    {
      nameof (Any),
      nameof (Email),
      nameof (Mail),
      nameof (Fax),
      nameof (Phone)
    })
  {
  }

  public class any : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRContactMethodsAttribute.any>
  {
    public any()
      : base("A")
    {
    }
  }

  public class email : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRContactMethodsAttribute.email>
  {
    public email()
      : base("E")
    {
    }
  }

  public class mail : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRContactMethodsAttribute.mail>
  {
    public mail()
      : base("M")
    {
    }
  }

  public class fax : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRContactMethodsAttribute.fax>
  {
    public fax()
      : base("F")
    {
    }
  }

  public class phone : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRContactMethodsAttribute.phone>
  {
    public phone()
      : base("P")
    {
    }
  }
}

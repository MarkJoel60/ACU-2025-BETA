// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLocationValid
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INLocationValid
{
  public const 
  #nullable disable
  string Validate = "V";
  public const string Warn = "W";
  public const string NoValidate = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("V", "Do Not Allow On-the-Fly Entry"),
        PXStringListAttribute.Pair("W", "Warn But Allow On-the-Fly Entry"),
        PXStringListAttribute.Pair("N", "Allow On-the-Fly Entry")
      })
    {
    }
  }

  public class validate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLocationValid.validate>
  {
    public validate()
      : base("V")
    {
    }
  }

  public class noValidate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLocationValid.noValidate>
  {
    public noValidate()
      : base("N")
    {
    }
  }

  public class warn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLocationValid.warn>
  {
    public warn()
      : base("W")
    {
    }
  }
}

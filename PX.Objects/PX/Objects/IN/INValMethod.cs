// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INValMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INValMethod
{
  public const 
  #nullable disable
  string Standard = "T";
  public const string Average = "A";
  public const string FIFO = "F";
  public const string Specific = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("T", "Standard"),
        PXStringListAttribute.Pair("A", "Average"),
        PXStringListAttribute.Pair("F", "FIFO"),
        PXStringListAttribute.Pair("S", "Specific")
      })
    {
    }
  }

  public class standard : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INValMethod.standard>
  {
    public standard()
      : base("T")
    {
    }
  }

  public class average : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INValMethod.average>
  {
    public average()
      : base("A")
    {
    }
  }

  public class fIFO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INValMethod.fIFO>
  {
    public fIFO()
      : base("F")
    {
    }
  }

  public class specific : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INValMethod.specific>
  {
    public specific()
      : base("S")
    {
    }
  }
}

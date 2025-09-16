// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLayerType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INLayerType
{
  public const 
  #nullable disable
  string Normal = "N";
  public const string Oversold = "O";
  public const string Unmanaged = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Normal"),
        PXStringListAttribute.Pair("O", "Oversold"),
        PXStringListAttribute.Pair("U", "Unmanaged")
      })
    {
    }
  }

  public class normal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLayerType.normal>
  {
    public normal()
      : base("N")
    {
    }
  }

  public class oversold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLayerType.oversold>
  {
    public oversold()
      : base("O")
    {
    }
  }

  public class unmanaged : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLayerType.unmanaged>
  {
    public unmanaged()
      : base("U")
    {
    }
  }
}

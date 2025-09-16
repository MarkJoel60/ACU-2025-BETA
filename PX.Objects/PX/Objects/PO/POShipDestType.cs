// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POShipDestType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POShipDestType
{
  public const 
  #nullable disable
  string Location = "L";
  public const string Site = "S";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("L", "Branch"),
        PXStringListAttribute.Pair("S", "Warehouse")
      })
    {
    }
  }

  public class location : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShipDestType.location>
  {
    public location()
      : base("L")
    {
    }
  }

  public class site : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShipDestType.site>
  {
    public site()
      : base("S")
    {
    }
  }
}

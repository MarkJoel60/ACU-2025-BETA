// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIDetStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPIDetStatus
{
  public const 
  #nullable disable
  string NotEntered = "N";
  public const string Entered = "E";
  public const string Skipped = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Not Entered"),
        PXStringListAttribute.Pair("E", "Entered"),
        PXStringListAttribute.Pair("S", "Skipped")
      })
    {
    }
  }

  public class notEntered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetStatus.notEntered>
  {
    public notEntered()
      : base("N")
    {
    }
  }

  public class entered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetStatus.entered>
  {
    public entered()
      : base("E")
    {
    }
  }

  public class skipped : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPIDetStatus.skipped>
  {
    public skipped()
      : base("S")
    {
    }
  }
}

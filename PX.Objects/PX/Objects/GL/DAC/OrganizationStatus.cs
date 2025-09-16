// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.OrganizationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

public class OrganizationStatus
{
  public const 
  #nullable disable
  string Active = "A";
  public const string Inactive = "I";
  public const string Initial = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("I", "Inactive")
      })
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrganizationStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class inactive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrganizationStatus.inactive>
  {
    public inactive()
      : base("I")
    {
    }
  }
}

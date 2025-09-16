// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ReportGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

public static class ReportGroup
{
  public const 
  #nullable disable
  string Labor = "L";
  public const string Material = "M";
  public const string Subcontract = "S";
  public const string Equipment = "E";
  public const string Other = "O";
  public const string Revenue = "R";
  public const string Empty = null;

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("L", "Labor"),
        PXStringListAttribute.Pair("M", "Material"),
        PXStringListAttribute.Pair("S", "Subcontract"),
        PXStringListAttribute.Pair("E", "Equipment"),
        PXStringListAttribute.Pair("O", "Other"),
        PXStringListAttribute.Pair("R", "Revenue"),
        PXStringListAttribute.Pair((string) null, string.Empty)
      })
    {
    }
  }

  public class labor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.labor>
  {
    public labor()
      : base("L")
    {
    }
  }

  public class material : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.material>
  {
    public material()
      : base("M")
    {
    }
  }

  public class subcontract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.subcontract>
  {
    public subcontract()
      : base("S")
    {
    }
  }

  public class equipment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.equipment>
  {
    public equipment()
      : base("E")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.other>
  {
    public other()
      : base("O")
    {
    }
  }

  public class revenue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReportGroup.revenue>
  {
    public revenue()
      : base("R")
    {
    }
  }
}

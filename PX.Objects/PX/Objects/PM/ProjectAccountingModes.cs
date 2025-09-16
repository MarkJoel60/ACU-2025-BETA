// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectAccountingModes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

public class ProjectAccountingModes
{
  public const 
  #nullable disable
  string ProjectSpecific = "P";
  public const string Valuated = "V";
  public const string Linked = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("P", "Track by Project Quantity and Cost"),
        PXStringListAttribute.Pair("V", "Track by Project Quantity"),
        PXStringListAttribute.Pair("L", "Track by Location")
      })
    {
    }
  }

  public class projectSpecific : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectAccountingModes.projectSpecific>
  {
    public projectSpecific()
      : base("P")
    {
    }
  }

  public class valuated : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectAccountingModes.valuated>
  {
    public valuated()
      : base("V")
    {
    }
  }

  public class linked : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectAccountingModes.linked>
  {
    public linked()
      : base("L")
    {
    }
  }
}

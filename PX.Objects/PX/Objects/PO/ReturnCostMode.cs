// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.ReturnCostMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class ReturnCostMode
{
  public const 
  #nullable disable
  string NotApplicable = "N";
  public const string OriginalCost = "O";
  public const string CostByIssue = "I";
  public const string ManualCost = "M";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("O", "Original Cost from Receipt"),
        PXStringListAttribute.Pair("I", "Cost by Issue Strategy"),
        PXStringListAttribute.Pair("M", "Manual Cost Input")
      })
    {
    }
  }

  public class notApplicable : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReturnCostMode.notApplicable>
  {
    public notApplicable()
      : base("N")
    {
    }
  }

  public class originalCost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReturnCostMode.originalCost>
  {
    public originalCost()
      : base("O")
    {
    }
  }

  public class costByIssue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReturnCostMode.costByIssue>
  {
    public costByIssue()
      : base("I")
    {
    }
  }

  public class manualCost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ReturnCostMode.manualCost>
  {
    public manualCost()
      : base("M")
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.OrchestrationStrategies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public static class OrchestrationStrategies
{
  public const 
  #nullable disable
  string DoNotOrchestrate = "NA";
  public const string WarehousePriority = "PO";
  public const string DestinationPriority = "DP";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("NA", "Do Not Orchestrate"),
        PXStringListAttribute.Pair("DP", "Destination Priority"),
        PXStringListAttribute.Pair("PO", "Warehouse Priority")
      })
    {
    }
  }

  public class ShortListAttribute : PXStringListAttribute
  {
    public ShortListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("DP", "Destination Priority"),
        PXStringListAttribute.Pair("PO", "Warehouse Priority")
      })
    {
    }
  }

  public class doNotOrchestrate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrchestrationStrategies.doNotOrchestrate>
  {
    public doNotOrchestrate()
      : base("NA")
    {
    }
  }

  public class warehousePriority : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrchestrationStrategies.warehousePriority>
  {
    public warehousePriority()
      : base("PO")
    {
    }
  }

  public class destinationPriority : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrchestrationStrategies.destinationPriority>
  {
    public destinationPriority()
      : base("DP")
    {
    }
  }
}

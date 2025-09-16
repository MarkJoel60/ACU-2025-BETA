// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProductivityTrackingType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMProductivityTrackingType
{
  public const 
  #nullable disable
  string NotAllowed = "N";
  public const string OnDemand = "D";
  public const string Template = "T";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("T", "Template"),
        PXStringListAttribute.Pair("N", "Not Allowed"),
        PXStringListAttribute.Pair("D", "On Demand")
      })
    {
    }
  }

  public class notAllowed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PMProductivityTrackingType.notAllowed>
  {
    public notAllowed()
      : base("N")
    {
    }
  }

  public class onDemand : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMProductivityTrackingType.onDemand>
  {
    public onDemand()
      : base("D")
    {
    }
  }

  public class template : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMProductivityTrackingType.template>
  {
    public template()
      : base("T")
    {
    }
  }
}

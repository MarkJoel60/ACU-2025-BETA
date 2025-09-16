// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.OrchestrationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public static class OrchestrationStatus
{
  public const 
  #nullable disable
  string None = "NA";
  public const string NewOrchestration = "NW";
  public const string Processed = "PO";
  public const string Unsuccessful = "PN";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("NA", "None"),
        PXStringListAttribute.Pair("NW", "New"),
        PXStringListAttribute.Pair("PO", "Processed"),
        PXStringListAttribute.Pair("PN", "Unsuccessful")
      })
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrchestrationStatus.none>
  {
    public none()
      : base("NA")
    {
    }
  }

  public class newOrchestration : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrchestrationStatus.newOrchestration>
  {
    public newOrchestration()
      : base("NW")
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrchestrationStatus.processed>
  {
    public processed()
      : base("PO")
    {
    }
  }

  public class unsuccessful : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrchestrationStatus.unsuccessful>
  {
    public unsuccessful()
      : base("PN")
    {
    }
  }
}

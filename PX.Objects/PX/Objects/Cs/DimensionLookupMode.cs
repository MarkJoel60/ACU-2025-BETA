// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DimensionLookupMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class DimensionLookupMode
{
  public const 
  #nullable disable
  string BySegmentsAndAllAvailableSegmentValues = "SA";
  public const string BySegmentsAndChildSegmentValues = "SC";
  public const string BySegmentedKeys = "K0";

  public class bySegmentsAndAllAvailableSegmentValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DimensionLookupMode.bySegmentsAndAllAvailableSegmentValues>
  {
    public bySegmentsAndAllAvailableSegmentValues()
      : base("SA")
    {
    }
  }

  public class bySegmentsAndChildSegmentValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DimensionLookupMode.bySegmentsAndChildSegmentValues>
  {
    public bySegmentsAndChildSegmentValues()
      : base("SC")
    {
    }
  }

  public class bySegmentedKeys : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DimensionLookupMode.bySegmentedKeys>
  {
    public bySegmentedKeys()
      : base("K0")
    {
    }
  }

  public class List : PXStringListAttribute
  {
    public List()
      : base(new string[3]{ "SA", "SC", "K0" }, new string[3]
      {
        "By Segment: All Avail. Segment Values",
        "By Segment: Child Segment Values",
        "By Segmented Key"
      })
    {
    }
  }
}

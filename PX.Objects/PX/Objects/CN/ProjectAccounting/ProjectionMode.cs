// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.ProjectionMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting;

[ExcludeFromCodeCoverage]
public static class ProjectionMode
{
  public const string Auto = "A";
  public const string Manual = "M";
  public const string ManualQuantity = "Q";
  public const string ManualCost = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "A", "M", "Q", "C" }, new string[4]
      {
        "Auto",
        "Manual",
        "Manual Quantity",
        "Manual Cost"
      })
    {
    }
  }

  public class ShortListAttribute : PXStringListAttribute
  {
    public ShortListAttribute()
      : base(new string[2]{ "A", "M" }, new string[2]
      {
        "Auto",
        "Manual"
      })
    {
    }
  }
}

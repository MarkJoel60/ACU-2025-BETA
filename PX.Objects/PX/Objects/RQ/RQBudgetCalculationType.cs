// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBudgetCalculationType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.RQ;

public static class RQBudgetCalculationType
{
  public const string YTD = "Y";
  public const string PTD = "P";
  public const string Annual = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("Y", "YTD Values"),
        PXStringListAttribute.Pair("P", "PTD Values"),
        PXStringListAttribute.Pair("A", "Annual")
      })
    {
    }
  }
}

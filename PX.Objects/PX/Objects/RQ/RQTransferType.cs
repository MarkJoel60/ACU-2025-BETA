// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQTransferType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.RQ;

public static class RQTransferType
{
  public const string None = "N";
  public const string Split = "S";
  public const string Transfer = "T";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("S", "Split"),
        PXStringListAttribute.Pair("T", "Transfer")
      })
    {
    }
  }
}

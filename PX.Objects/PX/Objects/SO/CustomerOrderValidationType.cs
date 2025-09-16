// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CustomerOrderValidationType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public static class CustomerOrderValidationType
{
  public const string None = "N";
  public const string Warn = "W";
  public const string Error = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Allow Duplicates"),
        PXStringListAttribute.Pair("W", "Warn About Duplicates"),
        PXStringListAttribute.Pair("E", "Forbid Duplicates")
      })
    {
    }
  }
}

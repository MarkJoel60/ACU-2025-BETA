// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class ChangeOrderLineType
{
  public const string Update = "U";
  public const string NewDocument = "L";
  public const string NewLine = "D";
  public const string Reopen = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("U", "Update"),
        PXStringListAttribute.Pair("L", "New Document"),
        PXStringListAttribute.Pair("D", "New Line"),
        PXStringListAttribute.Pair("R", "Reopen")
      })
    {
    }
  }
}

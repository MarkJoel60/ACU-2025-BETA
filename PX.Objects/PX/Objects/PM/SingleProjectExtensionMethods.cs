// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SingleProjectExtensionMethods
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable enable
namespace PX.Objects.PM;

public static class SingleProjectExtensionMethods
{
  public static void Deconstruct(
    this ISingleProjectExtension extension,
    out int? detailCount,
    out long? sumProjectID,
    out long? squareSumProjectID)
  {
    detailCount = extension.DetailCount;
    sumProjectID = extension.SumProjectID;
    squareSumProjectID = extension.SquareSumProjectID;
  }
}

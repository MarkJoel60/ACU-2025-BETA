// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ISingleProjectExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// An interface that defines auxiliary fields that are used to find out whether the document contains lines with different projects specified.
/// </summary>
[PXInternalUseOnly]
public interface ISingleProjectExtension
{
  /// <summary>The number of detail lines of the document.</summary>
  int? DetailCount { get; set; }

  /// <summary>
  /// The sum of integer IDs of all projects in document's detail lines.
  /// </summary>
  long? SumProjectID { get; set; }

  /// <summary>
  /// The sum of squares of integer IDs of all projects in document's detail lines.
  /// </summary>
  long? SquareSumProjectID { get; set; }
}

// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IProjectLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Common properties for detail line which is linked to project.
/// </summary>
[PXInternalUseOnly]
public interface IProjectLine
{
  /// <summary>ID of project which is linked to the detail line.</summary>
  int? ProjectID { get; set; }

  /// <summary>
  /// ID of the project task linked to line. Task belongs to <see cref="P:PX.Objects.PM.IProjectLine.ProjectID" />.
  /// </summary>
  int? TaskID { get; set; }

  /// <summary>ID of cost code which is linked to the detail line.</summary>
  int? CostCodeID { get; set; }
}

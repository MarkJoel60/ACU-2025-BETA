// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IProjectTaxes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Common properties which denote taxes for the document with project in header.
/// </summary>
[PXInternalUseOnly]
public interface IProjectTaxes
{
  /// <summary>ID of tax zone to which document belongs.</summary>
  string TaxZoneID { get; set; }
}

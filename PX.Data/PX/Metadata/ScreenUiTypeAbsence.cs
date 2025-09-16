// Decompiled with JetBrains decompiler
// Type: PX.Metadata.ScreenUiTypeAbsence
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// An indicator that a screen lacks a specific type of UI.
/// </summary>
[PXInternalUseOnly]
public enum ScreenUiTypeAbsence
{
  Unknown,
  Legacy,
  None,
}

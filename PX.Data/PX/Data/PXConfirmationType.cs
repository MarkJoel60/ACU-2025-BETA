// Decompiled with JetBrains decompiler
// Type: PX.Data.PXConfirmationType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines values that indicate cases when the confirmation
/// message is shown on a button press. The message box typically asks a
/// user to confirm the action.</summary>
public enum PXConfirmationType
{
  /// <summary>Always show the message box.</summary>
  Always,
  /// <summary>Show the message box when there are unsaved changes on the
  /// webpage.</summary>
  IfDirty,
  /// <summary>Whether to show the message box is not specified.</summary>
  Unspecified,
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXErrorLevel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The level of the <see cref="T:PX.Data.PXSetPropertyException" /> exception. Depending on the level,
/// an error, a warning, or an informational notification is displayed for the UI control
/// that is associated with a field or row.
/// </summary>
public enum PXErrorLevel
{
  /// <summary>An error notification is displayed for the UI control or cell that corresponds to the DAC field
  /// whose <see cref="P:PX.Data.PXFieldState.Error" /> property value is not <see langword="null" />.</summary>
  Undefined,
  /// <summary>An informational notification is displayed for a DAC row.</summary>
  RowInfo,
  /// <summary>A warning notification is displayed for the UI control or cell that corresponds to a DAC field.</summary>
  Warning,
  /// <summary>A warning notification is displayed for a DAC row.</summary>
  RowWarning,
  /// <summary>An error notification is displayed for the UI control or cell that corresponds to a DAC field.</summary>
  Error,
  /// <summary>An error notification is displayed for a DAC row.</summary>
  RowError,
}

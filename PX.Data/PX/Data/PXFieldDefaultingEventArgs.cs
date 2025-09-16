// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldDefaultingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>FieldDefaulting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldDefaulting" />
/// <summary>Provides data for the <tt>FieldDefaulting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldDefaulting" />
public sealed class PXFieldDefaultingEventArgs(object row) : CancelEventArgs
{
  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns or sets the default value for a DAC field.</summary>
  public object NewValue { get; set; }
}

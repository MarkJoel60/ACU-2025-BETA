// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowSelectingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowSelecting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowSelecting" />
/// <summary>Provides data for the <tt>RowSelecting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowSelecting" />
public sealed class PXRowSelectingEventArgs(
  object row,
  PXDataRecord record,
  int position,
  bool isReadOnly) : CancelEventArgs
{
  /// <summary>Returns the DAC object that is being processed.</summary>
  public object Row { get; internal set; } = row;

  /// <summary>Returns the processed data record in the result set.</summary>
  public PXDataRecord Record { get; } = record;

  /// <summary>Returns or sets the index of the processed column in the result set.</summary>
  public int Position { get; set; } = position;

  /// <summary>Returns the value indicating whether the DAC object is read-only.</summary>
  public bool IsReadOnly { get; } = isReadOnly;
}

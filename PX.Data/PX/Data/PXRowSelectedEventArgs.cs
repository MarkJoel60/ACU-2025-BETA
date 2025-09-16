// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowSelectedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowSelected</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowSelected" />
/// <summary>Provides data for the <tt>RowSelected</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowSelected" />
public sealed class PXRowSelectedEventArgs(object row) : EventArgs
{
  /// <summary>Returns the DAC object that is being processed.</summary>
  public object Row { get; } = row;
}

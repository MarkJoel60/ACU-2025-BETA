// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowInsertedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowInserted</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowInserted" />
/// <summary>Provides data for the <tt>RowInserted</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowInserted" />
public sealed class PXRowInsertedEventArgs(object row, bool externalCall) : EventArgs
{
  /// <summary>Returns the DAC object that has been inserted.</summary>
  public object Row { get; } = row;

  /// <summary>
  /// Returns <tt>true</tt> if the DAC object has been inserted in the UI or through the Web Service API; otherwise, it returns <tt>false</tt>.</summary>
  public bool ExternalCall { get; } = externalCall;

  internal object PendingRow { get; init; }
}

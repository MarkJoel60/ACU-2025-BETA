// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowUpdatedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowUpdated</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowUpdated" />
/// <summary>Provides data for the <tt>RowUpdated</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowUpdated" />
public sealed class PXRowUpdatedEventArgs(object row, object oldRow, bool externalCall) : EventArgs
{
  /// <summary>Returns the DAC object that has been updated.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the copy of the original DAC object before the update operation.</summary>
  public object OldRow { get; } = oldRow;

  /// <summary>
  /// Returns <tt>true</tt> if the DAC object has been updated from the UI or through the Web Service API;
  /// otherwise, it returns <tt>false</tt>.
  /// </summary>
  public bool ExternalCall { get; } = externalCall;
}

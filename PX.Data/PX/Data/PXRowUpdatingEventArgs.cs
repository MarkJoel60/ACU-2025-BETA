// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowUpdatingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowUpdating</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowUpdating" />
/// <summary>Provides data for the <tt>RowUpdating</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowUpdating" />
public sealed class PXRowUpdatingEventArgs(object row, object newrow, bool externalCall) : 
  CancelEventArgs
{
  /// <summary>Returns the original DAC object that is being updated.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the updated copy of the DAC object that is going to be
  /// merged with the original one.</summary>
  public object NewRow { get; } = newrow;

  /// <summary>
  /// Returns <tt>true</tt> if the update of the DAC object has been initiated from the UI
  /// or through the Web Service API; otherwise, it returns <tt>false</tt>.
  /// </summary>
  public bool ExternalCall { get; } = externalCall;
}

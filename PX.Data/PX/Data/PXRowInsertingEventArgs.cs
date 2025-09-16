// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowInsertingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowInserting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowInserting" />
/// <summary>Provides data for the <tt>RowInserting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowInserting" />
public sealed class PXRowInsertingEventArgs(object row, bool externalCall) : CancelEventArgs
{
  /// <summary>Returns the DAC object that is being inserted.</summary>
  public object Row { get; } = row;

  /// <summary>
  /// Returns <tt>true</tt> if a DAC object was inserted from the UI or by using the Web API Service; otherwise, it returns <tt>false</tt>.
  /// </summary>
  public bool ExternalCall { get; } = externalCall;
}

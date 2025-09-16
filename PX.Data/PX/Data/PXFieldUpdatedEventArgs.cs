// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldUpdatedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>FieldUpdated</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldUpdated" />
/// <summary>Provides data for the <tt>FieldUpdated</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldUpdated" />
public sealed class PXFieldUpdatedEventArgs(object row, object oldValue, bool externalCall) : 
  EventArgs
{
  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the previous value of the current DAC field.</summary>
  public object OldValue { get; } = oldValue;

  /// <summary>Returns <tt>true</tt> if the new value of the
  /// current DAC field has been changed in the UI or through the Web
  /// Service API; otherwise, it returns <tt>false</tt>.</summary>
  public bool ExternalCall { get; } = externalCall;
}

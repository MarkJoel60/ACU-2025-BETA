// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldVerifyingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>FieldVerifying</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldVerifying" />
/// <summary>Provides data for the <tt>FieldVerifying</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldVerifying" />
public sealed class PXFieldVerifyingEventArgs(object row, object newValue, bool externalCall) : 
  CancelEventArgs
{
  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns or sets the new value of the current DAC field.</summary>
  public object NewValue { get; set; } = newValue;

  /// <summary>Returns <tt>true</tt> if the new value of the current DAC
  /// field has been received from the UI or through the Web Service
  /// API; otherwise, it returns <tt>false</tt>.</summary>
  public bool ExternalCall { get; } = externalCall;
}

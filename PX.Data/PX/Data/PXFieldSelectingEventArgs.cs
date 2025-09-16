// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldSelectingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>FieldSelecting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldSelecting" />
/// <summary>Provides data for the <tt>FieldSelecting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXFieldSelecting" />
public sealed class PXFieldSelectingEventArgs(
  object row,
  object returnValue,
  bool isAltered,
  bool externalCall) : CancelEventArgs
{
  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns or sets the data used to set up the DAC field input control
  /// or cell presentation.</summary>
  public object ReturnState { get; set; } = returnValue;

  /// <summary>Returns or sets the external presentation of the value of the
  /// DAC field.</summary>
  public object ReturnValue
  {
    get => this.ReturnState is PXFieldState returnState ? returnState.Value : this.ReturnState;
    set
    {
      if (this.ReturnState is PXFieldState returnState)
        returnState.Value = value;
      else
        this.ReturnState = value;
    }
  }

  /// <summary>Returns or sets the value indicating whether the
  /// <tt>ReturnState</tt> property should be created for each data
  /// record.</summary>
  public bool IsAltered { get; set; } = isAltered;

  /// <summary>Returns <tt>true</tt> if the current DAC field has been
  /// selected in the UI or through the Web Service API; otherwise, it returns <tt>false</tt>.</summary>
  public bool ExternalCall { get; } = externalCall;
}

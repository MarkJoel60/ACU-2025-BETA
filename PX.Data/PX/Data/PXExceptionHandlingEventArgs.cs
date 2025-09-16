// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExceptionHandlingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>ExceptionHandling</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXExceptionHandling" />
/// <param name="row">The data record.</param>
/// <param name="newValue">The new value.</param>
/// <param name="exception">The exception instance.</param>
/// <summary>Provides data for the <tt>ExceptionHandling</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXExceptionHandling" />
/// <param name="row">The data record.</param>
/// <param name="newValue">The new value.</param>
/// <param name="exception">The exception instance.</param>
public sealed class PXExceptionHandlingEventArgs(object row, object newValue, Exception exception) : 
  CancelEventArgs
{
  /// <summary>Returns the current DAC object.</summary>
  public object Row { get; } = row;

  /// <summary>Returns or sets the values of the DAC field. By default, this property
  /// contains one of the following groups of values:
  /// <list type="bullet">
  /// <item><description>The values that are generated in the process of assigning the default value to a DAC field</description></item>
  /// <item><description>The values that are passed as new values when a field is updated</description></item>
  /// <item><description>The values that are entered in the UI or through the Web Service API</description></item>
  /// <item><description>The values that are received with the <tt>PXCommandPreparingException</tt>,
  /// <tt>PXRowPersistingException</tt>, or <tt>PXRowPersistedException</tt> exception</description></item>
  /// </list>
  /// </summary>
  public object NewValue { get; set; } = newValue;

  /// <summary>Returns the initial exception that caused the event to be generated.</summary>
  public Exception Exception { get; } = exception;
}

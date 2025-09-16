// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowPersistedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowPersisted</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowPersisted" />
/// <summary>Provides data for the <tt>RowPersisted</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowPersisted" />
public sealed class PXRowPersistedEventArgs(
  object row,
  PXDBOperation operation,
  PXTranStatus tranStatus,
  Exception exception) : EventArgs
{
  /// <summary>Returns the DAC object that has been committed to the database.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the status of the transaction scope associated with the
  /// current commitment.</summary>
  public PXTranStatus TranStatus { get; } = tranStatus;

  /// <summary>Returns the <tt>PXDBOperation</tt> value, indicating the type of
  /// the current commitment.</summary>
  public PXDBOperation Operation { get; } = operation;

  /// <summary>Returns the <tt>Exception</tt> object, which is thrown while changes are
  /// committed to the database.</summary>
  public Exception Exception { get; } = exception;
}

// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowPersistingEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Provides data for the <tt>RowPersisting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowPersisting" />
/// <summary>Provides data for the <tt>RowPersisting</tt> event.</summary>
/// <seealso cref="T:PX.Data.PXRowPersisting" />
public sealed class PXRowPersistingEventArgs(PXDBOperation operation, object row) : CancelEventArgs
{
  /// <summary>Returns the DAC object that is being committed to the database.</summary>
  public object Row { get; } = row;

  /// <summary>Returns the <tt>PXDBOperation</tt> of the current commit operation.</summary>
  public PXDBOperation Operation { get; } = operation;
}

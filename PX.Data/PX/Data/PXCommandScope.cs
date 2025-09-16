// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCommandScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Data.Common;

#nullable disable
namespace PX.Data;

public sealed class PXCommandScope : IDisposable
{
  private bool _Disposed;
  private int? _Timeout;
  private PXCommandScope _Previous;

  public PXCommandScope()
  {
    this._Previous = PXContext.GetSlot<PXCommandScope>();
    PXContext.SetSlot<PXCommandScope>(this);
  }

  public PXCommandScope(int timeout)
    : this()
  {
    this._Timeout = new int?(timeout);
  }

  public void Dispose()
  {
    if (this._Disposed)
      return;
    PXContext.SetSlot<PXCommandScope>(this._Previous);
    this._Disposed = true;
  }

  internal static DbCommand GetCommand()
  {
    PXCommandScope slot = PXContext.GetSlot<PXCommandScope>();
    if (slot == null)
      return (DbCommand) null;
    DbCommand command = PXDatabase.CreateCommand();
    if (slot._Timeout.HasValue)
      command.CommandTimeout = slot._Timeout.Value;
    return command;
  }
}

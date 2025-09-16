// Decompiled with JetBrains decompiler
// Type: PX.Data.PXConnectionStringScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Redirects database operations to a remote database.</summary>
internal class PXConnectionStringScope : IDisposable
{
  private readonly string _connectionString;

  internal static string GetConnectionString()
  {
    return PXContext.GetSlot<PXConnectionStringScope>()?._connectionString;
  }

  public PXConnectionStringScope(string connectionString)
  {
    this._connectionString = connectionString;
    if (connectionString == null)
      return;
    PXContext.SetSlot<PXConnectionStringScope>(this);
  }

  public void Dispose()
  {
    if (this._connectionString == null)
      return;
    PXContext.SetSlot<PXConnectionStringScope>((PXConnectionStringScope) null);
  }
}
